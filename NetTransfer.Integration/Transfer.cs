using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTransfer.B2B.Library;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Data;
using NetTransfer.Integration.Models;
using NetTransfer.Integration.Services.Erp;
using NetTransfer.Integration.Services.VirtualStore;
using NetTransfer.Logo.Library.Class;
using NetTransfer.Logo.Library.Models;
using NetTransfer.Netsis.Library.Models;
using NetTransfer.Opak.Library.Models;
using NetTransfer.Smartstore.Library;
using NetTransfer.Smartstore.Library.Models;
using Newtonsoft.Json;
using RestSharp;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Net;

namespace NetTransfer.Integration
{
    public class Transfer
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly B2BClient _b2BClient;
        private readonly SmartStoreClient _smartStoreClient;
        private readonly ErpSetting _erpSetting;
        private readonly VirtualStoreSetting _virtualStoreSetting;
        private readonly SmartstoreService _smartstoreTransfer;
        private readonly B2BService _b2BTransfer;
        private readonly NetsisSetting _netsisSetting;
        private readonly ErpDovizTip _erpDovizTip;

        private NetTransferContext _context;
        private B2BParameter _b2bParameter;
        private SmartstoreParameter _smartstoreParameter;


        public Transfer(ILogger logger, string connectionString, ErpSetting erpSetting, VirtualStoreSetting virtualStoreSetting, B2BParameter b2BParameter)
        {
            _logger = logger;
            _connectionString = connectionString;
            _erpSetting = erpSetting;
            _virtualStoreSetting = virtualStoreSetting;
            _b2BClient = new B2BClient(virtualStoreSetting);
            _b2BTransfer = new B2BService(_logger, _b2BClient);
            _b2bParameter = b2BParameter;

            InitializeContext();

            if (erpSetting.Erp == "Netsis")
            {
                _netsisSetting = _context.NetsisSetting.FirstOrDefault();
                _erpDovizTip = _context.ErpDovizTip.FirstOrDefault();
            }
        }

        public Transfer(ILogger logger, string connectionString, ErpSetting erpSetting, VirtualStoreSetting virtualStoreSetting, SmartstoreParameter smartstoreParameter)
        {
            _logger = logger;
            _connectionString = connectionString;
            _erpSetting = erpSetting;
            _virtualStoreSetting = virtualStoreSetting;
            _smartStoreClient = new SmartStoreClient(virtualStoreSetting);
            _smartstoreTransfer = new SmartstoreService(_logger, _smartStoreClient);
            _smartstoreParameter = smartstoreParameter;

            InitializeContext();

            if (erpSetting.Erp == "Netsis")
            {
                _netsisSetting = _context.NetsisSetting.FirstOrDefault();
                _erpDovizTip = _context.ErpDovizTip.FirstOrDefault();
            }
        }


        private void InitializeContext()
        {
            _context = new NetTransferContext(_connectionString);
        }

        public async Task CariTransfer()
        {
            _logger.LogInformation("Cari transferi başladı.");
            string errorMessage = string.Empty;
            DateTime startDatetime = DateTime.Now;
            try
            {
                object? musteriList = null;
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        musteriList = logoService.GetArps().Data;

                        if (!string.IsNullOrEmpty(errorMessage))
                            throw new Exception(errorMessage);
                        break;
                    case "Netsis":
                        NetsisService netsisService = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);
                        musteriList = netsisService.GetCariList(ref errorMessage);
                        break;
                    case "Opak":
                        break;
                    case "Mikro":
                        MikroService mikroService = new MikroService(_erpSetting);
                        musteriList = mikroService.GetCariList(ref errorMessage);
                        if (!string.IsNullOrEmpty(errorMessage))
                            throw new Exception(errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: CariTransfer.{erp}", _erpSetting.Erp);
                        break;
                }

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        B2BService b2BTransfer = new B2BService(_logger, _b2BClient);
                        List<B2BMusteriKosulKodu>? b2bList = b2BTransfer.MappingMusteri(_erpSetting.Erp, musteriList);

                        if (b2bList == null)
                        {
                            throw new Exception("Müşteri mapping error");
                        }

                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                        }

                        int count = PaginationBuilder.GetPageCount(b2bList, 25);
                        for (int i = 1; i <= count; i++)
                        {
                            var customerList = PaginationBuilder.GetPage(b2bList, i, 25).ToList();

                            var result = await _b2BClient.MusteriTopluTransferAsync(customerList);
                            if (result == null)
                            {
                                _logger.LogError("Müşteri transferi sırasında hata oluştu.");
                                break;
                            }

                            if (result.Code == 200)
                            {
                                int pagetotal = i == 1 ? 0 : ((i - 1) * 25);
                                int total = pagetotal + customerList.Count;

                                _logger.LogError($"Müşteri aktarım detay : {result.Detay}");
                                _logger.LogInformation($"Müşteri aktarım durumu : {total}/{b2bList.Count} aktarıldı");
                            }
                            else
                            {
                                throw new Exception(result.Message);
                            }
                        }


                        await UpdateCustomerLastTransfer(startDatetime);

                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: CariTransfer.{virtualStore}", _virtualStoreSetting.VirtualStore);
                        break;
                }

                _logger.LogInformation("Cari transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cari transferi sırasında hata oluştu.");
            }
        }

        public async Task CariBakiyeTransfer()
        {
            _logger.LogInformation("Cari bakiye transferi başladı.");

            string errorMessage = string.Empty;

            try
            {
                object? musteriList = null;
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        musteriList = logoService.GetArpBalances(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            throw new Exception(errorMessage);
                        break;
                    case "Netsis":
                        NetsisService netsisService = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);
                        musteriList = netsisService.GetCariBakiyeList(ref errorMessage);
                        break;
                    case "Opak":
                        break;
                    case "Mikro":
                        MikroService mikroService = new MikroService(_erpSetting);
                        musteriList = mikroService.GetCariBakiyeList(ref errorMessage);
                        if (!string.IsNullOrEmpty(errorMessage))
                            throw new Exception(errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: CariTransfer.{erp}", _erpSetting.Erp);
                        break;
                }

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        B2BService b2BTransfer = new B2BService(_logger, _b2BClient);
                        List<B2BMusteriBakiye>? b2bList = b2BTransfer.MappingMusteriBakiye(_erpSetting.Erp, musteriList);

                        if (b2bList == null)
                        {
                            throw new Exception("Müşteri bakiye mapping error");
                        }

                        _logger.LogInformation("Kayıt sayısı : " + b2bList.Count);

                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                        }

                        var result = await _b2BClient.MusteriBakiyeTransferAsync(b2bList);
                        if (result != null)
                        {
                            _logger.LogInformation($"Müşteri bakiye aktarım detay : {result.Detay}");

                            if (result.Code != 200)
                            {
                                throw new Exception(result.Message);
                            }
                            else
                            {
                                _logger.LogInformation("Aktarıldı");
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Geldi");
                        }
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: CariTransfer.{virtualStore}", _virtualStoreSetting.VirtualStore);
                        break;
                }

                _logger.LogInformation("Cari bakiye transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cari bakiye transferi sırasında hata oluştu.");
            }
        }

        public async Task MalzemeTransfer()
        {
            string errorMessage = string.Empty;
            try
            {
                object? malzemeList = null;

                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        malzemeList = logoService.GetMalzemeList(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Netsis":
                        NetsisService netsisService = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);
                        malzemeList = netsisService.GetMalzemeList(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Opak":
                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                        if (_smartstoreParameter.ProductSync)
                        {
                            if (opakService.IsSync(0, 0, ref errorMessage))
                            {
                                malzemeList = opakService.GetMalzemeList(ref errorMessage);
                            }
                            else
                            {
                                _logger.LogWarning("Aktarılacak malzeme bulunamadı.");
                            }
                        }
                        else
                        {
                            malzemeList = opakService.GetMalzemeList(ref errorMessage);
                        }

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Mikro":
                        MikroService mikroService = new MikroService(_erpSetting);
                        malzemeList = mikroService.GetMalzemeList(ref errorMessage);
                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        List<B2BUrun>? b2bList = _b2BTransfer.MappingProduct(_erpSetting.Erp, malzemeList);
                        if (b2bList == null)
                        {
                            throw new Exception("B2B Product mapping error");
                        }

                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                            else
                            {
                                _logger.LogError("Token alınamadı.");
                            }
                        }

                        int count = PaginationBuilder.GetPageCount(b2bList, 1000);
                        for (int i = 1; i <= count; i++)
                        {
                            var productList = PaginationBuilder.GetPage(b2bList, i, 1000).ToList();

                            var json = @" {  ""data"":  " + JsonConvert.SerializeObject(productList) + "  }  ";
                            _logger.LogInformation($"Ürünler : {json}");
                            var result = await _b2BClient.UrunTopluTransferAsync(productList);

                            if (result == null)
                            {
                                _logger.LogError("Ürün transferi sırasında hata oluştu.");
                                continue;
                            }

                            int pagetotal = i == 1 ? 0 : ((i - 1) * 1000);
                            int total = pagetotal + productList.Count;

                            _logger.LogInformation($"Ürün aktarım durumu : {total}/{b2bList.Count} aktarıldı");
                            _logger.LogInformation($"Ürün aktarım detay : {result.Detay}");
                        }
                        break;
                    case "Smartstore":
                        #region Pasif Ürünler
                        List<SmartstoreProduct>? smartStoreList = _smartstoreTransfer.MappingProduct(_erpSetting.Erp, malzemeList);
                        if (smartStoreList == null)
                        {
                            throw new Exception("Smartstore Product mapping error");
                        }

                        var passiveProductList = smartStoreList.Where(m => m.Published == false).Select(m => m.Sku).ToList();
                        _logger.LogWarning("Aktarılacak pasif ürün sayısı : " + passiveProductList.Count);

                        int pageCountPassive = PaginationBuilder.GetPageCount(passiveProductList, 50);
                        for (int i = 1; i <= pageCountPassive; i++)
                        {
                            var skus = PaginationBuilder.GetPage(passiveProductList, i, 50).ToList();
                            var products = await _smartStoreClient.GetProducts(skus);
                            if (products != null)
                            {
                                if (products.status)
                                {
                                    _logger.LogInformation($"Pasif ürünler sayısı: {products.value.Count}");
                                    foreach (var item in products.value)
                                    {
                                        var result = await _smartStoreClient.UpdateProductPublished(item.Id);
                                        if (result)
                                        {
                                            _logger.LogInformation($"Ürün güncellendi - UpdateProductPassive : {item.Id} - {item.Sku}");
                                            if (_erpSetting.Erp == "Opak")
                                            {
                                                OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                                opakService.UpdateSync(item.Sku, 0, 1, ref errorMessage);
                                            }
                                        }
                                        else
                                        {
                                            _logger.LogError($"Ürün güncellenmedi - UpdateProductPassive : {item.Id} - {item.Sku}");
                                            if (_erpSetting.Erp == "Opak")
                                            {
                                                OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                                opakService.UpdateSync(item.Sku, 0, 2, ref errorMessage);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _logger.LogError("Smartstore'dan ürünler alınamadı: " + products.error.message);
                                }

                            }
                            else
                            {
                                _logger.LogError("Smartstore'dan ürünler alınamadı.");
                            }
                        }
                        #endregion


                        #region Aktif Ürünler
                        var activeProductList = smartStoreList.Where(m => m.Published).ToList();
                        _logger.LogWarning("Aktarılacak aktif ürün sayısı : " + activeProductList.Count);

                        var activeProductSkuList = activeProductList.Where(m => m.Published).Select(m => m.Sku).ToList();

                        int pageCountActive = PaginationBuilder.GetPageCount(activeProductSkuList, 50);
                        for (int i = 1; i <= pageCountActive; i++)
                        {
                            var skus = PaginationBuilder.GetPage(activeProductSkuList, i, 50).ToList();
                            var products = await _smartStoreClient.GetProducts(skus);
                            if (products != null && products.value != null)
                            {
                                foreach (var item in products.value)
                                {
                                    var data = activeProductList.Where(m => m.Sku == item.Sku).FirstOrDefault();
                                    if (data == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        data.Id = item.Id;
                                    }
                                }
                            }
                        }

                        foreach (var item in activeProductList)
                        {
                            try
                            {
                                var product = await _smartstoreTransfer.CreateProduct(item, _smartstoreParameter.ProductImageSync);
                                if (product == null)
                                {
                                    _logger.LogError($"Ürün güncellenmedi - UpdateProductPublished : {item.Id} - {item.Sku}");
                                    if (_erpSetting.Erp == "Opak")
                                    {
                                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                        opakService.UpdateSync(item.Sku, 0, 2, ref errorMessage);
                                    }
                                }
                                else
                                {
                                    _logger.LogInformation($"Ürün güncellendi - UpdateProductPublished : {item.Id} - {item.Sku}");
                                    if (_erpSetting.Erp == "Opak")
                                    {
                                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                        opakService.UpdateSync(item.Sku, 0, 1, ref errorMessage);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Ürün güncellenemedi - Stok Kodu : {item.Sku} Ürün Adı : {item.Name}");
                            }
                        }
                        #endregion
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {virtualStore}", _virtualStoreSetting.VirtualStore);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Malzeme transferi sırasında hata oluştu.");
            }
        }

        public async Task MalzemeStokTransfer()
        {
            string errorMessage = string.Empty;
            try
            {
                DateTime startDatetime = DateTime.Now;

                List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        malzemeStokList = logoService.GetMalzemeStokList(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Stok Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Netsis":
                        NetsisService service = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);
                        malzemeStokList = service.GetMalzemeStokList(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Stok Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Opak":
                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                        malzemeStokList = opakService.GetMalzemeStokList(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Stok Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Mikro":
                        MikroService mikroService = new MikroService(_erpSetting);
                        malzemeStokList = mikroService.GetMalzemeStokList(ref errorMessage);
                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Stok Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }

                _logger.LogWarning("Malzeme stok listesi miktarı : " + malzemeStokList.Count);

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken == null)
                            {
                                _logger.LogError("Token alınamadı.");
                                return;
                            }

                            _b2BClient.SetAccessToken(resultAccessToken.token);
                        }

                        int count = PaginationBuilder.GetPageCount(malzemeStokList, 5000);
                        for (int i = 1; i <= count; i++)
                        {
                            var stokList = PaginationBuilder.GetPage(malzemeStokList, i, 5000).ToList();
                            var result = await _b2BTransfer.UpdateProductStock(stokList);

                            int pagetotal = i == 1 ? 0 : ((i - 1) * 5000);
                            int total = pagetotal + stokList.Count;
                            _logger.LogInformation($"Malzeme stok aktarım durumu : {total}/{malzemeStokList.Count} aktarıldı");
                            _logger.LogInformation($"Malzeme stok aktarım detay : {result?.Detay["kayitsiz_urunler"]}");

                            Thread.Sleep(500);
                        }

                        _logger.LogInformation("Malzeme stok aktarım tamalandı");
                        break;
                    case "Smartstore":
                        await _smartstoreTransfer.UpdateProductStock(malzemeStokList.Where(m => m.StokType == "S").ToList());
                        await _smartstoreTransfer.UpdateProductVariantCombinationStok(malzemeStokList.Where(m => m.StokType == "V").ToList());
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {store}", _virtualStoreSetting.VirtualStore);
                        break;
                }
                await UpdateProductStockLastTransfer(startDatetime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Malzeme Stok Transferi sırasında hata oluştu.");
            }
        }

        public async Task MalzemeFiyatTransfer()
        {
            string errorMessage = string.Empty;
            try
            {
                object malzemeFiyatList = null;
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        malzemeFiyatList = logoService.GetMalzemeFiyatList(ref errorMessage);
                        break;
                    case "Netsis":
                        NetsisService service = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);
                        malzemeFiyatList = service.GetMalzemeFiyatList(ref errorMessage);
                        break;
                    case "Opak":
                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                        if (_smartstoreParameter.ProductPriceSync)
                        {
                            if (opakService.IsSync(1, 0, ref errorMessage))
                            {
                                malzemeFiyatList = opakService.GetMalzemeFiyatList(ref errorMessage);
                            }
                            else
                            {
                                _logger.LogWarning("Aktarılacak malzeme fiyatı bulunamadı.");
                                return;
                            }
                        }
                        else
                        {
                            malzemeFiyatList = opakService.GetMalzemeFiyatList(ref errorMessage);
                        }

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Listesi alınamadı. Hata: {error}", errorMessage);

                        break;
                    case "Mikro":
                        MikroService mikroService = new MikroService(_erpSetting);
                        malzemeFiyatList = mikroService.GetMalzemeFiyatList(ref errorMessage);
                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Fiyat Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }
                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                        }

                        if (malzemeFiyatList is List<BaseMalzemeFiyatModel>)
                        {
                            _logger.LogWarning("Malzeme fiyat listesi BaseMalzemeFiyatModel tipinde.");


                            var b2bBaseList = malzemeFiyatList as List<BaseMalzemeFiyatModel>;

                            _logger.LogWarning("Malzeme fiyat liste miktarı : " + b2bBaseList.Count);

                            int count = PaginationBuilder.GetPageCount(b2bBaseList, 5000);
                            for (int i = 1; i <= count; i++)
                            {
                                var data = PaginationBuilder.GetPage(b2bBaseList, i, 5000).ToList();
                                var result = await _b2BTransfer.UpdateProductPrice(data);
                                if (result == null)
                                {
                                    _logger.LogError("Malzeme fiyat aktarımı sırasında bilinmeyen bir hata oluştu");
                                }
                                else
                                {
                                    _logger.LogWarning(result.Message);
                                    _logger.LogInformation($"Malzeme fiyat aktarım detay : {result?.Detay["kayitsiz_urunler"]}");
                                }
                            }
                        }
                        else if (malzemeFiyatList is List<MalzemeFiyatModel>)
                        {
                            _logger.LogWarning("Malzeme fiyat listesi MalzemeFiyatModel tipinde.");
                            var b2bNetsisList = malzemeFiyatList as List<MalzemeFiyatModel>;


                            _logger.LogWarning("Malzeme fiyat liste miktarı : " + b2bNetsisList.Count);

                            int count = PaginationBuilder.GetPageCount(b2bNetsisList, 1000);
                            for (int i = 1; i <= count; i++)
                            {
                                var data = PaginationBuilder.GetPage(b2bNetsisList, i, 1000).ToList();
                                await _b2BTransfer.UpdateProductPrice(data);

                                _logger.LogInformation($"Malzeme fiyat aktarım durumu : {Math.Min(i * 1000, b2bNetsisList.Count)}/{b2bNetsisList.Count} aktarıldı");
                            }

                            _logger.LogWarning("Malzeme fiyat aktarımı tamamlandı");
                        }


                        break;
                    case "Smartstore":

                        var list = malzemeFiyatList as List<BaseMalzemeFiyatModel>;

                        _logger.LogWarning("Malzeme fiyat liste miktarı : " + list.Count);

                        foreach (var item in list.Where(m => m.StokType == "S").ToList())
                        {
                            var priceResult = await _smartstoreTransfer.UpdateProductPrice(item);
                            if (priceResult)
                            {
                                _logger.LogInformation($"Ürün fiyatı güncellendi - Stok Kodu: {item.StokKodu}, Fiyat: {item.Fiyat}");
                                if (_erpSetting.Erp == "Opak")
                                {
                                    OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                    opakService.UpdateSync(item.StokKodu, 1, 1, ref errorMessage);
                                }
                            }
                            else
                            {
                                _logger.LogError($"Ürün fiyatı güncellenemedi - Stok Kodu: {item.StokKodu}, Fiyat: {item.Fiyat}");
                                if (_erpSetting.Erp == "Opak")
                                {
                                    OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                    opakService.UpdateSync(item.StokKodu, 1, 2, ref errorMessage);
                                }
                            }
                        }

                        foreach (var item in list.Where(m => m.StokType == "V").ToList())
                        {
                            var priceResult = await _smartstoreTransfer.UpdateProductVariantCombinationPrice(item);
                            if (priceResult)
                            {
                                _logger.LogInformation($"Ürün fiyatı güncellendi - Stok Kodu: {item.StokKodu}, Fiyat: {item.Fiyat}");
                                if (_erpSetting.Erp == "Opak")
                                {
                                    OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                    opakService.UpdateSync(item.StokKodu, 1, 1, ref errorMessage);
                                }
                            }
                            else
                            {
                                if (_erpSetting.Erp == "Opak")
                                {
                                    OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                                    opakService.UpdateSync(item.StokKodu, 1, 2, ref errorMessage);
                                }
                                _logger.LogError($"Ürün fiyatı güncellenemedi - Stok Kodu: {item.StokKodu}, Fiyat: {item.Fiyat}");
                            }
                        }
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {store}", _virtualStoreSetting.VirtualStore);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Malzeme fiyat aktarım hatası");
            }
        }

        public async Task SiparisTransfer()
        {
            string errorMessage = string.Empty;
            try
            {
                object? orderList = null;

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                        }

                        //orderList = await _b2BClient.Siparisler(Convert.ToInt32(_netsisSetting.SiparisAktarimDurumKodu));
                        orderList = await _b2BClient.Siparisler(2);
                        break;
                    case "Smartstore":
                        orderList = await _smartstoreTransfer.GetSmartstoreOrder(orderStatusId: _smartstoreParameter.OrderStatusId);
                        break;
                }

                if (orderList == null)
                {
                    _logger.LogError("Siparişler yüklenirken bir hata oluştu");
                }
                else
                {
                    _logger.LogInformation("Sipariş sayısı : " + ((orderList is B2BResponseList<B2BSiparis> ? (orderList as B2BResponseList<B2BSiparis>).List.Count : (orderList as List<SmartstoreOrder>) .Count) ) );
                }


                    switch (_erpSetting.Erp)
                    {
                        case "Logo":
                            _logger.LogInformation("Aktarılacak sipariş sayısı :" + (orderList as B2BResponseList<B2BSiparis>).List.Count);

                            LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                            foreach (var item in (orderList as B2BResponseList<B2BSiparis>).List)
                            {
                                var siparisDetayResult = await _b2BClient.SiparisDetay(item.siparis_id);
                                if (siparisDetayResult == null)
                                {
                                    _logger.LogError("Sipariş detay bilgisi alınamadı. Sipariş ID: {siparisId}", item.siparis_id);
                                    break;
                                }

                                var id = logoService.SiparisKaydet(siparisDetayResult, ref errorMessage);
                                var sipariNo = logoService.GetSipariNo(item.siparis_id, ref errorMessage);
                                int index = 1;
                                foreach (var kalem in siparisDetayResult.siparis_kalemler)
                                {
                                    int kalemId = logoService.SiparisKalemKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, ref errorMessage);
                                    index++;
                                    double tutar = kalem.liste_fiyat * kalem.miktar;

                                    if (kalem.iskonto_1 > 0)
                                    {
                                        double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_1 * 0.01)));
                                        tutar = tutar - isk_tutar;
                                        logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_1, ref errorMessage);

                                        index++;
                                    }

                                    if (kalem.iskonto_2 > 0)
                                    {
                                        double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_2 * 0.01)));
                                        tutar = tutar - isk_tutar;
                                        logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_2, ref errorMessage);

                                        index++;
                                    }

                                    if (kalem.iskonto_3 > 0)
                                    {
                                        double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_3 * 0.01)));
                                        tutar = tutar - isk_tutar;
                                        logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_3, ref errorMessage);

                                        index++;
                                    }

                                    if (kalem.iskonto_4 > 0)
                                    {
                                        double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_4 * 0.01)));
                                        tutar = tutar - isk_tutar;
                                        logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_4, ref errorMessage);

                                        index++;
                                    }

                                    if (kalem.iskonto_5 > 0)
                                    {
                                        double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_5 * 0.01)));
                                        tutar = tutar - isk_tutar;
                                        logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_5, ref errorMessage);

                                        index++;
                                    }

                                    if (kalem.iskonto_6 > 0)
                                    {
                                        double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_6 * 0.01)));
                                        tutar = tutar - isk_tutar;
                                        logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_6, ref errorMessage);

                                        index++;
                                    }
                                }

                                var siparisDurumGunceleResult = await _b2BClient.SiparisDurumGunncelle(item.siparis_id, sipariNo);
                                if (siparisDurumGunceleResult == null)
                                {
                                    _logger.LogError("Sipariş durumu güncellenemedi. Sipariş ID: {siparisId}", item.siparis_id);
                                }
                                else
                                {
                                    if (siparisDurumGunceleResult.Code == 200)
                                    {
                                        _logger.LogWarning("Sipariş durumu güncellendi. Sipariş ID: {siparisId} Sipariş NO : {siparisNo}", id, sipariNo);
                                    }
                                    else
                                    {
                                        _logger.LogError(siparisDurumGunceleResult.Message);
                                    }
                                }
                            }

                            break;
                        case "Netsis":
                            NetsisService netsisService = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);

                            _logger.LogInformation("Aktarılacak sipariş sayısı :" + (orderList as B2BResponseList<B2BSiparis>).List.Count);
                            foreach (var item in (orderList as B2BResponseList<B2BSiparis>).List)
                            {
                                var siparisDetayResult = await _b2BClient.SiparisDetay(item.siparis_id);

                                var result = netsisService.SiparisKaydet(item, siparisDetayResult, ref errorMessage);
                                if (string.IsNullOrEmpty(result))
                                {
                                    _logger.LogError($"{errorMessage}. Sipariş No: {item.siparis_id}");
                                }
                                else
                                {
                                    var siparisDurumGunceleResult = await _b2BClient.SiparisDurumGunncelle(item.siparis_id, result);
                                    if (siparisDurumGunceleResult == null)
                                    {
                                        _logger.LogError("Sipariş durumu güncellenemedi. Sipariş ID: {siparisId}", item.siparis_id);
                                    }
                                    else
                                    {
                                        if (siparisDurumGunceleResult.Code == 200)
                                        {
                                            _logger.LogWarning("Sipariş durumu güncellendi. Sipariş ID: {siparisId} Sipariş NO : {siparisNo}", item.siparis_id, result);
                                        }
                                        else
                                        {
                                            _logger.LogError(siparisDurumGunceleResult.Message);
                                        }
                                    }
                                }

                            }
                            break;
                        case "Opak":
                            OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);

                            List<OpakSiparis>? opakList = opakService.MappingOrder(_virtualStoreSetting, orderList);
                            if (opakList == null)
                            {
                                _logger.LogError("Opak sipariş mapping hatası");
                                return;
                            }

                            _logger.LogWarning("Aktarılacak sipariş sayısı: " + opakList.Count);

                            foreach (var item in opakList)
                            {
                                if (item.KALEMSAYISI > 0)
                                {
                                    var result = await opakService.SaveOrderAsync(item);
                                    if (result == null)
                                    {
                                        _logger.LogError("Bilinmeyen bir hata oluştu. Sipariş No: " + item.BELGENO);
                                        continue;
                                    }

                                    if (result.Hata)
                                    {
                                        _logger.LogError($"{result.HataMesaj}. Sipariş No: {item.BELGENO}");
                                    }
                                    else
                                    {
                                        _logger.LogWarning($"{item.BELGENO} nolu sipariş aktarıldı");
                                    }
                                }
                            }

                            break;
                        default:
                            _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                    }

                
                _logger.LogInformation("Sipariş transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bilinmeyen bir hata oluştu");
            }
        }

        public async Task SanalPosTransfer()
        {
            string errorMessage = string.Empty;
            try
            {
                object? paymentList = null;

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                        }

                        paymentList = await _b2BClient.Odemeler();
                        break;
                    case "Smartstore":
                        break;
                }

                if (paymentList == null)
                {
                    _logger.LogError("Ödeme yüklenirken bir hata oluştu");
                }


                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        foreach (var item in (paymentList as B2BResponseList<B2BOdeme>).List)
                        {
                            if (item.durum == "basarili" && item.islem_tipi == "uyelikli")
                            {
                                if (item.musteri_erp_kodu.Contains("120.AŞ") || item.musteri_erp_kodu.Contains("120.MK") || item.musteri_erp_kodu.Contains("120.SG") || item.musteri_erp_kodu.Contains("120.EM"))
                                {
                                    ClFicheModel clFiche = new ClFicheModel();
                                    clFiche.FICHENO = "~";
                                    clFiche.APPROVEDATE = DateTime.Now.ToShortDateString();
                                    clFiche.DOCDATE = item.islem_tarihi;
                                    clFiche.DATE_ = item.islem_tarihi;
                                    clFiche.TRCODE = 70;
                                    clFiche.CUSTOMERCODE = item.musteri_erp_kodu;
                                    clFiche.SALESMAN_CODE = "";
                                    clFiche.GENEXP1 = item.odeme_notu;
                                    clFiche.APPROVE = 1;
                                    clFiche.BANKACCCODE = _erpSetting.DefaultBankCode ?? "";
                                    clFiche.CREDIT = item.hesaba_islenecek_tutar;
                                    clFiche.AFFECTRISK = 1;
                                    clFiche.STATUS = 0;

                                    ClfLineModel clfLine = new ClfLineModel();
                                    clfLine.SIGN = 1;
                                    clfLine.CREDIT = item.hesaba_islenecek_tutar;
                                    clfLine.TRNET = item.hesaba_islenecek_tutar;
                                    clfLine.TRRATE = 1;
                                    clfLine.REPORTRATE = 1;
                                    clfLine.REPORTNET = item.hesaba_islenecek_tutar;
                                    clfLine.BANKACCCODE = _erpSetting.DefaultBankCode ?? "";
                                    clfLine.CUSTOMERCODE = item.musteri_erp_kodu;
                                    clfLine.DOCODE = item.odeme_no;
                                    clfLine.AFFECTRISK = 1;
                                    clfLine.SALESMANCODE = "";

                                    clFiche.GetClfLine.Add(clfLine);

                                    QueryParam queryParam = new QueryParam();
                                    queryParam.firmnr = _erpSetting.FirmNo ?? "";
                                    queryParam.periodnr = _erpSetting.PeriodNo ?? "";
                                    queryParam.data = JsonConvert.SerializeObject(clFiche);
                                    queryParam.DbName = _erpSetting.SqlDatabase ?? "";


                                    var id = ClFiche(queryParam, _erpSetting.RestUrl);
                                    if (id > 0)
                                    {
                                        var siparisDurumGunceleResult = await _b2BClient.OdemeDurumGunncelle(item.odeme_no, true);
                                        if (siparisDurumGunceleResult == null)
                                        {
                                            _logger.LogError("Ödeme durumu güncellenemedi. Ödeme ID: {odeme_no}", item.odeme_no);
                                        }
                                        else
                                        {
                                            if (siparisDurumGunceleResult.Code == 200)
                                            {
                                                _logger.LogWarning("Ödeme durumu güncellendi. Ödeme NO : {siparisNo}", item.odeme_no, true);
                                            }
                                            else
                                            {
                                                _logger.LogError(siparisDurumGunceleResult.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _logger.LogError($"{item.durum_mesaj}. Ödeme No: {item.odeme_no}");
                                    }
                                }
                            }
                        }
                        break;
                    case "Netsis":
                        NetsisService netsisService = new NetsisService(_erpSetting, _netsisSetting, _erpDovizTip);

                        _logger.LogInformation("Aktarılacak ödeme sayısı :" + (paymentList as B2BResponseList<B2BOdeme>).List.Count);
                        foreach (var item in (paymentList as B2BResponseList<B2BOdeme>).List)
                        {
                            var result = await netsisService.OdemeKaydet(item);
                            if (!result)
                            {
                                _logger.LogError($"{item.durum_mesaj}. Ödeme No: {item.odeme_no}");
                            }
                            else
                            {
                                var siparisDurumGunceleResult = await _b2BClient.OdemeDurumGunncelle(item.odeme_no, true);
                                if (siparisDurumGunceleResult == null)
                                {
                                    _logger.LogError("Ödeme durumu güncellenemedi. Ödeme ID: {odeme_no}", item.odeme_no);
                                }
                                else
                                {
                                    if (siparisDurumGunceleResult.Code == 200)
                                    {
                                        _logger.LogWarning("Ödeme durumu güncellendi. Ödeme NO : {siparisNo}", item.odeme_no, result);
                                    }
                                    else
                                    {
                                        _logger.LogError(siparisDurumGunceleResult.Message);
                                    }
                                }
                            }

                        }
                        break;
                    case "Opak":

                        break;
                    default:
                        break;
                }


                _logger.LogInformation("Sanalpos transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        public async Task SevkiyatTransfer()
        {
            string errorMessage = string.Empty;
            try
            {
                object sevkiyatList = null;
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        break;
                    case "Netsis":
                        break;
                    case "Opak":
                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                        sevkiyatList = opakService.GetSevkiyatList(ref errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "Smartstore":

                        var list = sevkiyatList as List<OpakSevkiyat>;

                        _logger.LogInformation("Aktarılacak sevkiyat sayısı: " + list.Count);

                        foreach (var item in list)
                        {
                            SmartstoreAddShipment model = new SmartstoreAddShipment();
                            model.TrackingNumber = item.KARGOBARKODU;
                            model.TrackingUrl = "";
                            model.IsShipped = true;
                            model.NotifyCustomer = true;
                            int orderId = Convert.ToInt32(item.SIPARISNO.Replace("B2C", ""));

                            _logger.LogInformation("Sipariş ID: " + orderId + " Kargo Firması: " + item.KARGOFIRMASI + " Kargo Takip No: " + item.KARGOBARKODU);

                            var result = await _smartstoreTransfer.AddShipment(model, orderId, item.KARGOFIRMASI);
                            if (result != null)
                            {
                                _logger.LogInformation("Sevkiyat aktarıldı " + result.Id);
                            }
                            else
                            {
                                _logger.LogInformation("Sevkiyat aktarımı sırasında bilinmeyen bir hata oluştu");
                            }
                        }

                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {store}", _virtualStoreSetting.VirtualStore);
                        break;
                }

                await UpdateOrderShipmentLastTransfer();

                _logger.LogInformation("Sevkiyat Transferi Bitti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevkiyat Transferi sırasında hata oluştu.");
            }
        }

        public async Task TahsilatTrasnfer()
        {
            string errorMessage = string.Empty;
            try
            {
                object? paymentList = null;

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        if (!_b2BClient.IsAccessToken())
                        {
                            var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                            if (resultAccessToken != null)
                            {
                                _b2BClient.SetAccessToken(resultAccessToken.token);
                            }
                        }

                        paymentList = await _b2BClient.Tahsilatlar();
                        break;
                    case "Smartstore":
                        break;
                }

                if (paymentList == null)
                {
                    _logger.LogError("Ödeme yüklenirken bir hata oluştu");
                    return;
                }

                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        var b2bResponseList = paymentList as B2BResponse2List<B2BTahsilat>;
                        if (b2bResponseList != null && b2bResponseList.list != null)
                        {
                            foreach (var item in b2bResponseList.list)
                            {
                                if (item.tahsilat_tipi == "nakit" && item.durum == 1)//2 reddedildi - 1 onaylandı
                                {
                                    LogoService logoService = new LogoService(_erpSetting, _b2bParameter);

                                    var cyphCode = logoService.GetArpCyphCode(item.musteri_erp_kodu, ref errorMessage);

                                    var salesman = await _context.Salesman.Where(m => m.SalesmanCode == cyphCode).FirstOrDefaultAsync();
                                    if (salesman == null)
                                    {
                                        _logger.LogError($"{item.plasiyer_erp_kodu} kodlu satış elemanı - kasa bağlantısı bulunamadı");
                                        break;
                                    }


                                    SdSlipModel sdSlip = new SdSlipModel();
                                    sdSlip.TRCODE = 11;
                                    sdSlip.KSCARDCODE = salesman.CashierCode;
                                    sdSlip.DATE_ = DateTime.Now.ToShortDateString();
                                    sdSlip.AMOUNT = item.tutar;
                                    sdSlip.CUSTTITLE = item.musteri_adi;
                                    sdSlip.LINEEXP = item.not;
                                    sdSlip.STATUS = 0;
                                    sdSlip.SPECODE = "";
                                    sdSlip.DOCNUMBER = item.id.ToString();
                                    sdSlip.AFFECTRISK = 1;
                                    sdSlip.CUSTOMERCODE = item.musteri_erp_kodu;
                                    sdSlip.SALESMANCODE = salesman.SalesmanCode;


                                    QueryParam queryParam = new QueryParam();
                                    queryParam.firmnr = _erpSetting.FirmNo ?? "";
                                    queryParam.periodnr = _erpSetting.PeriodNo ?? "";
                                    queryParam.data = JsonConvert.SerializeObject(sdSlip);
                                    queryParam.DbName = _erpSetting.SqlDatabase ?? "";

                                    var id = SdSlip(queryParam, _erpSetting.RestUrl);
                                    if (id > 0)
                                    {
                                        var tahsilatDurumResult = await _b2BClient.TahsilatDurumGuncelle(item.id, true);
                                        if (tahsilatDurumResult == null)
                                        {
                                            _logger.LogError("Tahsilat durumu güncellenemedi. TahsilatId ID: {id}", item.id);
                                        }
                                        else
                                        {
                                            if (tahsilatDurumResult.Code == 200)
                                            {
                                                _logger.LogWarning("Tahsilat durumu güncellendi. TahsilatId ID : {id}", item.id);
                                            }
                                            else
                                            {
                                                _logger.LogError(tahsilatDurumResult.Message);
                                            }
                                        }
                                    }
                                }
                                if (item.tahsilat_tipi == "pos" && item.durum == 1)//2 reddedildi - 1 onaylandı
                                {
                                    LogoService logoService = new LogoService(_erpSetting, _b2bParameter);

                                    var cyphCode = logoService.GetArpCyphCode(item.musteri_erp_kodu, ref errorMessage);

                                    var salesman = await _context.Salesman.Where(m => m.SalesmanCode == cyphCode).FirstOrDefaultAsync();

                                    ClFicheModel clFiche = new ClFicheModel();
                                    clFiche.FICHENO = "~";
                                    clFiche.APPROVEDATE = DateTime.Now.ToShortDateString();
                                    clFiche.DOCDATE = DateTime.Now.ToShortDateString();
                                    clFiche.DATE_ = DateTime.Now.ToShortDateString();
                                    clFiche.TRCODE = 70;
                                    clFiche.CUSTOMERCODE = item.musteri_erp_kodu;
                                    clFiche.SALESMAN_CODE = salesman.SalesmanCode;
                                    clFiche.GENEXP1 = item.not;
                                    clFiche.APPROVE = 1;
                                    clFiche.BANKACCCODE = _erpSetting.DefaultBankCode ?? "";
                                    clFiche.CREDIT = item.tutar;
                                    clFiche.AFFECTRISK = 1;
                                    clFiche.STATUS = 0;
                                    clFiche.GENEXP1 = $"{item.plasiyer_adi} SANAL POS TAHSİLATI - {item.id}";

                                    ClfLineModel clfLine = new ClfLineModel();
                                    clfLine.SIGN = 1;
                                    clfLine.CREDIT = item.tutar;
                                    clfLine.TRNET = item.tutar;
                                    clfLine.TRRATE = 1;
                                    clfLine.REPORTRATE = 1;
                                    clfLine.REPORTNET = item.tutar;
                                    clfLine.BANKACCCODE = _erpSetting.DefaultBankCode ?? "";
                                    clfLine.CUSTOMERCODE = item.musteri_erp_kodu;
                                    clfLine.DOCODE = item.id.ToString();
                                    clfLine.AFFECTRISK = 1;
                                    clfLine.SALESMANCODE = salesman.SalesmanCode;
                                    clfLine.LINEEXP = $"{item.plasiyer_adi} SANAL POS TAHSİLATI - {item.id}";

                                    clFiche.GetClfLine.Add(clfLine);

                                    QueryParam queryParam = new QueryParam();
                                    queryParam.firmnr = _erpSetting.FirmNo ?? "";
                                    queryParam.periodnr = _erpSetting.PeriodNo ?? "";
                                    queryParam.data = JsonConvert.SerializeObject(clFiche);
                                    queryParam.DbName = _erpSetting.SqlDatabase ?? "";


                                    var id = ClFiche(queryParam, _erpSetting.RestUrl);
                                    if (id > 0)
                                    {
                                        var tahsilatDurumResult = await _b2BClient.TahsilatDurumGuncelle(item.id, true);
                                        if (tahsilatDurumResult == null)
                                        {
                                            _logger.LogError("Tahsilat durumu güncellenemedi. TahsilatId ID: {id}", item.id);
                                        }
                                        else
                                        {
                                            if (tahsilatDurumResult.Code == 200)
                                            {
                                                _logger.LogWarning("Tahsilat durumu güncellendi. TahsilatId ID : {id}", item.id);
                                            }
                                            else
                                            {
                                                _logger.LogError(tahsilatDurumResult.Message);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            _logger.LogError("B2BResponseList<B2BOdeme> veya List null.");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tahsilat Aktarımı Sırasında Bir Hata Oluştu");
            }

            _logger.LogInformation("Tahsilat Aktarımı Tamamlandı.");
        }

        private async Task UpdateCustomerLastTransfer(DateTime transferDatetime)
        {
            try
            {
                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        NetTransferContext _db = new NetTransferContext(_connectionString);
                        var parameter = _db.B2BParameter.First();
                        parameter.CustomerLastTransfer = transferDatetime;
                        _db.B2BParameter.Update(parameter);
                        await _context.SaveChangesAsync();

                        _b2bParameter.CustomerLastTransfer = parameter.CustomerLastTransfer;
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateCustomerLastTransfer Error");
            }
        }

        private async Task UpdateProductStockLastTransfer(DateTime transferDatetime)
        {
            try
            {
                NetTransferContext _db = new NetTransferContext(_connectionString);
                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        var b2BParameter = _db.B2BParameter.First();
                        b2BParameter.ProductStockLastTransfer = transferDatetime;
                        _db.B2BParameter.Update(b2BParameter);
                        await _db.SaveChangesAsync();

                        _b2bParameter.ProductStockLastTransfer = b2BParameter.ProductStockLastTransfer;
                        break;
                    case "Smartstore":
                        break;
                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateProductStockLastTransfer Error");
            }
        }

        private async Task UpdateOrderShipmentLastTransfer()
        {
            try
            {
                NetTransferContext _db = new NetTransferContext(_connectionString);

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        break;
                    case "Smartstore":
                        var smartstoreParameter = _db.SmartstoreParameter.First();
                        smartstoreParameter.OrderShipmentLastTransfer = DateTime.Now;
                        _db.SmartstoreParameter.Update(smartstoreParameter);
                        await _db.SaveChangesAsync();

                        _smartstoreParameter.OrderShipmentLastTransfer = smartstoreParameter.OrderShipmentLastTransfer;

                        break;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateOrderShipmentLastTransfer Error");
            }
        }

        public int SdSlip(QueryParam param, string url)
        {
            var client = new RestClient(url);

            var request = new RestRequest($"{param.Path}logo/sdslips/", Method.Post)
                .AddJsonBody(param);

            try
            {
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Content == null)
                    {
                        throw new Exception("Bilinmeyen hata oluştu.");
                    }
                    return JsonConvert.DeserializeObject<int>(response.Content);
                }
                else
                {
                    ErrorFault? fault = null;

                    try
                    {
                        if (response.Content == null)
                        {
                            throw new Exception("Bilinmeyen hata oluştu.");
                        }

                        fault = JsonConvert.DeserializeObject<ErrorFault>(response.Content);
                    }
                    catch
                    {
                        throw new Exception("Servisten beklenmeyen bir hata formatı döndü: " + response.Content);
                    }

                    throw new Exception(fault?.ErrMessage ?? "Bilinmeyen hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SdSlip çağrısında hata oluştu: " + ex.Message, ex);
            }
        }
        public int ClFiche(QueryParam param, string url)
        {
            var client = new RestClient(url);

            var request = new RestRequest($"{param.Path}logo/setcreditcardslips/", Method.Post)
                .AddJsonBody(param);

            try
            {
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Content == null)
                    {
                        throw new Exception("Bilinmeyen hata oluştu.");
                    }
                    return JsonConvert.DeserializeObject<int>(response.Content);
                }
                else
                {
                    ErrorFault? fault = null;

                    try
                    {
                        if (response.Content == null)
                        {
                            throw new Exception("Bilinmeyen hata oluştu.");
                        }

                        fault = JsonConvert.DeserializeObject<ErrorFault>(response.Content);
                    }
                    catch
                    {
                        throw new Exception("Servisten beklenmeyen bir hata formatı döndü: " + response.Content);
                    }

                    throw new Exception(fault?.ErrMessage ?? "Bilinmeyen hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SdSlip çağrısında hata oluştu: " + ex.Message, ex);
            }
        }

    }
}
