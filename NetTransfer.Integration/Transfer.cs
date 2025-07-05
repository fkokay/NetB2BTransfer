using Microsoft.Extensions.Logging;
using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Smartstore.Library.Models;
using NetTransfer.Smartstore.Library;
using NetTransfer.Integration.VirtualStore;
using NetTransfer.Integration.Erp;
using NetTransfer.Integration.Models;
using NetTransfer.B2B.Library.Models;
using NetTransfer.B2B.Library;
using System.Data;
using NetTransfer.Data;
using NetTransfer.Opak.Library.Models;
using Microsoft.EntityFrameworkCore;

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
            _b2BTransfer = new B2BService(_b2BClient);
            _b2bParameter = b2BParameter;

            InitializeContext();
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
                        musteriList = logoService.GetArps(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            throw new Exception(errorMessage);
                        break;
                    case "Netsis":
                        break;
                    case "Opak":
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: CariTransfer.{erp}", _erpSetting.Erp);
                        break;
                }

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        B2BService b2BTransfer = new B2BService(_b2BClient);
                        List<B2BMusteri>? b2bList = b2BTransfer.MappingMusteri(_erpSetting.Erp, musteriList);

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
                        break;
                    case "Opak":
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: CariTransfer.{erp}", _erpSetting.Erp);
                        break;
                }

                switch (_virtualStoreSetting.VirtualStore)
                {
                    case "B2B":
                        B2BService b2BTransfer = new B2BService(_b2BClient);
                        List<B2BMusteriBakiye>? b2bList = b2BTransfer.MappingMusteriBakiye(_erpSetting.Erp, musteriList);

                        if (b2bList == null)
                        {
                            throw new Exception("Müşteri bakiye mapping error");
                        }

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
                            if (result.Code != 200)
                            {
                                throw new Exception(result.Message);
                            }
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
                        NetsisService netsisService = new NetsisService(_erpSetting);
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

                        int count = PaginationBuilder.GetPageCount(b2bList, 25);
                        for (int i = 1; i <= count; i++)
                        {
                            var productList = PaginationBuilder.GetPage(b2bList, i, 25).ToList();

                            var result = await _b2BClient.UrunTopluTransferAsync(productList);

                            if (result == null)
                            {
                                _logger.LogError("Ürün transferi sırasında hata oluştu.");
                                break;
                            }

                            int pagetotal = i == 1 ? 0 : ((i - 1) * 25);
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
                            if (products != null)
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
                                var product = await _smartstoreTransfer.CreateProduct(item);
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
                        NetsisService service = new NetsisService(_erpSetting);
                        malzemeStokList = service.GetMalzemeStokList(ref errorMessage);

                        if (!string.IsNullOrEmpty(errorMessage))
                            _logger.LogError("Malzeme Stok Listesi alınamadı. Hata: {error}", errorMessage);
                        break;
                    case "Opak":
                        if (_smartstoreParameter.ProductStockLastTransfer.HasValue)
                        {
                            _logger.LogWarning($"Malzeme Stok Son Güncelleme Tarihi : {_smartstoreParameter.ProductStockLastTransfer.Value.ToString()}");
                        }

                        OpakService opakService = new OpakService(_erpSetting, _smartstoreParameter);
                        malzemeStokList = opakService.GetMalzemeStokList(ref errorMessage);

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

                        int count = PaginationBuilder.GetPageCount(malzemeStokList, 100);
                        for (int i = 1; i <= count; i++)
                        {
                            var stokList = PaginationBuilder.GetPage(malzemeStokList, i, 100).ToList();
                            await _b2BTransfer.UpdateProductStock(stokList);
                        }
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
                List<BaseMalzemeFiyatModel> malzemeFiyatList = new List<BaseMalzemeFiyatModel>();
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        malzemeFiyatList = logoService.GetMalzemeFiyatList(ref errorMessage);
                        break;
                    case "Netsis":
                        NetsisService service = new NetsisService(_erpSetting);
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

                        _logger.LogWarning("Malzeme fiyat liste miktarı : " + malzemeFiyatList.Count);

                        int count = PaginationBuilder.GetPageCount(malzemeFiyatList, 100);
                        for (int i = 1; i <= count; i++)
                        {
                            var list = PaginationBuilder.GetPage(malzemeFiyatList, i, 100).ToList();
                            var result = await _b2BTransfer.UpdateProductPrice(list);
                            if (result == null)
                            {
                                _logger.LogError("Malzeme fiyat aktarımı sırasında bilinmeyen bir hata oluştu");
                            }
                            else
                            {
                                _logger.LogWarning(result.Message);
                                _logger.LogWarning("Malzeme fiyat aktarım detay : " + result.Detay);
                            }
                        }
                        break;
                    case "Smartstore":
                        _logger.LogWarning("Malzeme fiyat liste miktarı : " + malzemeFiyatList.Count);

                        foreach (var item in malzemeFiyatList.Where(m => m.StokType == "S").ToList())
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

                        foreach (var item in malzemeFiyatList.Where(m => m.StokType == "V").ToList())
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
                        break;
                    case "Smartstore":
                        orderList = await _smartstoreTransfer.GetSmartstoreOrder(orderStatusId: _smartstoreParameter.OrderStatusId);
                        break;
                }

                if (orderList == null)
                {
                    _logger.LogError("Siparişler yüklenirken bir hata oluştu");
                }


                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        break;
                    case "Netsis":
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
                        break;
                }

                //LogoService logoService = new LogoService(_erpSetting, _b2bParameter);

                //if (!_b2BClient.IsAccessToken())
                //{
                //    var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                //    if (resultAccessToken != null)
                //    {
                //        _b2BClient.SetAccessToken(resultAccessToken.token);
                //    }
                //}

                //var result = await _b2BClient.Siparisler(2);
                //if (result == null)
                //{
                //    _logger.LogError("Siparişler alınamadı.");
                //    return;
                //}

                //if (result.Code == 200)
                //{
                //    foreach (var item in result.List)
                //    {
                //        var siparisDetayResult = await _b2BClient.SiparisDetay(item.siparis_id);
                //        if (siparisDetayResult == null)
                //        {
                //            _logger.LogError("Sipariş detay bilgisi alınamadı. Sipariş ID: {siparisId}", item.siparis_id);
                //            break;
                //        }

                //        var id = logoService.SiparisKaydet(siparisDetayResult, ref errorMessage);
                //        var sipariNo = logoService.GetSipariNo(item.siparis_id, ref errorMessage);
                //        int index = 1;
                //        foreach (var kalem in siparisDetayResult.siparis_kalemler)
                //        {
                //            int kalemId = logoService.SiparisKalemKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, ref errorMessage);
                //            index++;
                //            double tutar = kalem.liste_fiyat * kalem.miktar;

                //            if (kalem.iskonto_1 > 0)
                //            {
                //                double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_1 * 0.01)));
                //                tutar = tutar - isk_tutar;
                //                logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_1, ref errorMessage);

                //                index++;
                //            }

                //            if (kalem.iskonto_2 > 0)
                //            {
                //                double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_2 * 0.01)));
                //                tutar = tutar - isk_tutar;
                //                logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_2, ref errorMessage);

                //                index++;
                //            }

                //            if (kalem.iskonto_3 > 0)
                //            {
                //                double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_3 * 0.01)));
                //                tutar = tutar - isk_tutar;
                //                logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_3, ref errorMessage);

                //                index++;
                //            }

                //            if (kalem.iskonto_4 > 0)
                //            {
                //                double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_4 * 0.01)));
                //                tutar = tutar - isk_tutar;
                //                logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_4, ref errorMessage);

                //                index++;
                //            }

                //            if (kalem.iskonto_5 > 0)
                //            {
                //                double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_5 * 0.01)));
                //                tutar = tutar - isk_tutar;
                //                logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_5, ref errorMessage);

                //                index++;
                //            }

                //            if (kalem.iskonto_6 > 0)
                //            {
                //                double isk_tutar = tutar - (tutar * (1 - (kalem.iskonto_6 * 0.01)));
                //                tutar = tutar - isk_tutar;
                //                logoService.SiparisKalemIskontoKaydet(siparisDetayResult.ust_bilgiler, kalem, id, index, kalemId, isk_tutar, kalem.iskonto_6, ref errorMessage);

                //                index++;
                //            }
                //        }

                //        var siparisDurumGunceleResult = await _b2BClient.SiparisDurumGunncelle(item.siparis_id, sipariNo);
                //        if (siparisDurumGunceleResult == null)
                //        {
                //            _logger.LogError("Sipariş durumu güncellenemedi. Sipariş ID: {siparisId}", item.siparis_id);
                //        }
                //        else
                //        {
                //            if (siparisDurumGunceleResult.Code == 200)
                //            {
                //                _logger.LogWarning("Sipariş durumu güncellendi. Sipariş ID: {siparisId} Sipariş NO : {siparisNo}", id, sipariNo);
                //            }
                //            else
                //            {
                //                _logger.LogError(siparisDurumGunceleResult.Message);
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    _logger.LogError(result.Message);

                //}

                _logger.LogInformation("Sipariş transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        public async Task SanalPosTransfer()
        {

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
                        var smartstoreParameter = _db.SmartstoreParameter.First();
                        smartstoreParameter.ProductStockLastTransfer = transferDatetime;
                        _db.SmartstoreParameter.Update(smartstoreParameter);
                        await _db.SaveChangesAsync();

                        _smartstoreParameter.ProductStockLastTransfer = smartstoreParameter.ProductStockLastTransfer;

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
    }
}
