using Microsoft.Extensions.Logging;
using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Logo.Library.Class;
using NetTransfer.Logo.Library.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTransfer.Smartstore.Library.Models;
using NetTransfer.Smartstore.Library;
using NetTransfer.Integration.VirtualStore;
using NetTransfer.Integration.Erp;
using NetTransfer.Integration.Models;
using NetTransfer.B2B.Library.Models;
using NetTransfer.B2B.Library;
using Microsoft.Data.SqlClient;
using System.Data;
using NetTransfer.Data;

namespace NetTransfer.Integration
{
    public class Transfer
    {
        private NetTransferContext _context;
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly B2BClient _b2BClient;
        private readonly SmartStoreClient _smartStoreClient;
        private readonly ErpSetting _erpSetting;
        private readonly VirtualStoreSetting _b2bSetting;
        private readonly B2BParameter _b2bParameter;
        private readonly SmartstoreParameter _smartstoreParameter;
        private readonly SmartstoreService _smartstoreTransfer;
        private readonly B2BService _b2BTransfer;

        public Transfer(ILogger logger, string connectionString, ErpSetting erpSetting, VirtualStoreSetting b2BSetting, B2BParameter b2BParameter)
        {
            _logger = logger;
            _connectionString = connectionString;
            _erpSetting = erpSetting;
            _b2bSetting = b2BSetting;
            _b2BClient = new B2BClient(b2BSetting);
            _b2BTransfer = new B2BService(_b2BClient);
            _b2bParameter = b2BParameter;

            InitializeContext();
        }

        public Transfer(ILogger logger, string connectionString, ErpSetting erpSetting, VirtualStoreSetting b2BSetting, SmartstoreParameter smartstoreParameter)
        {
            _logger = logger;
            _connectionString = connectionString;
            _erpSetting = erpSetting;
            _b2bSetting = b2BSetting;
            _smartStoreClient = new SmartStoreClient(b2BSetting);
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

                switch (_b2bSetting.VirtualStore)
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


                        await UpdateCustomerLastTransfer();

                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: CariTransfer.{virtualStore}", _b2bSetting.VirtualStore);
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

                switch (_b2bSetting.VirtualStore)
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
                            if (result.Code != 0)
                            {
                                throw new Exception(result.Message);
                            }
                        }
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: CariTransfer.{virtualStore}", _b2bSetting.VirtualStore);
                        break;
                }

                _logger.LogInformation("Cari bakiye transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cari transferi sırasında hata oluştu.");
            }
        }

        public async Task MalzemeTransfer()
        {
            _logger.LogInformation("Malzeme transferi başlıyor.");


            string errorMessage = string.Empty;

            try
            {
                object? malzemeList = null;

                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        malzemeList = logoService.GetMalzemeList(ref errorMessage);
                        break;
                    case "Netsis":
                        NetsisService netsisService = new NetsisService(_erpSetting);
                        malzemeList = netsisService.GetMalzemeList(ref errorMessage);
                        break;
                    case "Opak":
                        OpakService opakService = new OpakService(_erpSetting);
                        malzemeList = opakService.GetMalzemeList(ref errorMessage);
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }

                switch (_b2bSetting.VirtualStore)
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
                        }

                        int index = 1;
                        foreach (var item in b2bList)
                        {
                            var result = await _b2BClient.UrunTransferAsync(item);
                            if (result.Code == 200)
                            {
                                _logger.LogError("B2B ürün aktarımı başarılı {Count}/{index} : {detay}", result.Detay, b2bList.Count, index);
                            }
                            else
                            {
                                _logger.LogError("B2B ürün aktarım hatası {Count}/{index}: {message}{valitaion}", result.Message, result.Validation, b2bList.Count, index);
                            }
                        }

                        await UpdateProductLastTransfer();
                        break;
                    case "Smartstore":
                        List<SmartstoreProduct>? smartStoreList = _smartstoreTransfer.MappingProduct(_erpSetting.Erp, malzemeList);
                        if (smartStoreList == null)
                        {
                            throw new Exception("Smartstore Product mapping error");
                        }
                        foreach (var item in smartStoreList)
                        {
                            _ = await _smartstoreTransfer.CreateProduct(item);
                        }
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {virtualStore}", _b2bSetting.VirtualStore);
                        break;
                }

                _logger.LogInformation("Malzeme transferi bitti.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Malzeme transferi sırasında hata oluştu.");
            }
        }

        public async Task MalzemeStokTransfer()
        {
            _logger.LogInformation("Malzeme Stok Transferi Başladı");
            string errorMessage = string.Empty;
            try
            {
                List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();
                switch (_erpSetting.Erp)
                {
                    case "Logo":
                        LogoService logoService = new LogoService(_erpSetting, _b2bParameter);
                        malzemeStokList = logoService.GetMalzemeStokList(ref errorMessage);
                        break;
                    case "Netsis":
                        NetsisService service = new NetsisService(_erpSetting);
                        malzemeStokList = service.GetMalzemeStokList(ref errorMessage);
                        break;
                    case "Opak":
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }

                switch (_b2bSetting.VirtualStore)
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

                        await _b2BTransfer.UpdateProductStock(malzemeStokList);

                        await UpdateProductStockLastTransfer();
                        break;
                    case "Smartstore":
                        await _smartstoreTransfer.UpdateProductStock(malzemeStokList);
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {store}", _b2bSetting.VirtualStore);
                        break;
                }

                _logger.LogInformation("Malzeme Stok Transferi Bitti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        public async Task MalzemeFiyatTransfer()
        {
            _logger.LogInformation("Malzeme Fiyat Transferi Başladı");

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
                        break;
                    default:
                        _logger.LogError("Geçersiz ERP ayarı: {erp}", _erpSetting.Erp);
                        break;
                }

                switch (_b2bSetting.VirtualStore)
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

                        await _b2BTransfer.UpdateProductPrice(malzemeFiyatList);

                        await UpdateProductPriceLastTransfer();
                        break;
                    case "Smartstore":
                        await _smartstoreTransfer.UpdateProductPrice(malzemeFiyatList);
                        break;
                    default:
                        _logger.LogError("Geçersiz sanal mağaza ayarı: {store}", _b2bSetting.VirtualStore);
                        break;
                }

                _logger.LogInformation("Malzeme Fiyat Transferi Bitti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        public async Task SiparisTransfer()
        {
            _logger.LogInformation("Sipariş transferi başlıyor.");

            string errorMessage = string.Empty;
            try
            {
                LogoService logoService = new LogoService(_erpSetting, _b2bParameter);

                if (!_b2BClient.IsAccessToken())
                {
                    var resultAccessToken = await _b2BClient.GetAccessTokenAsync();
                    if (resultAccessToken != null)
                    {
                        _b2BClient.SetAccessToken(resultAccessToken.token);
                    }
                }

                var result = await _b2BClient.Siparisler(2);
                if (result == null)
                {
                    _logger.LogError("Siparişler alınamadı.");
                    return;
                }

                if (result.Code == 200)
                {
                    foreach (var item in result.List)
                    {
                        var siparisDetayResult = await _b2BClient.SiparisDetay(item.siparis_id);
                        if (siparisDetayResult == null)
                        {
                            _logger.LogError("Sipariş detay bilgisi alınamadı. Sipariş ID: {siparisId}", item.siparis_id);
                            break;
                        }

                        var id = logoService.SiparisKaydet(siparisDetayResult, ref errorMessage);
                        var sipariNo = logoService.GetSipariNo(id, ref errorMessage);
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

                        await _b2BClient.SiparisDurumGunncelle(id, sipariNo);
                    }
                }
                else
                {
                    _logger.LogError(result.Message);

                }

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

        private async Task UpdateCustomerLastTransfer()
        {
            try
            {
                var parameter = _context.B2BParameter.First();
                parameter.CustomerLastTransfer = DateTime.Now;
                _context.B2BParameter.Update(parameter);
                await _context.SaveChangesAsync();

                _b2bParameter.CustomerLastTransfer = parameter.CustomerLastTransfer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Customer Last Trasfer Error");
            }
        }

        private async Task UpdateProductLastTransfer()
        {
            try
            {
                var parameter = _context.B2BParameter.First();
                parameter.ProductLastTransfer = DateTime.Now;
                _context.B2BParameter.Update(parameter);
                await _context.SaveChangesAsync();

                _b2bParameter.ProductLastTransfer = parameter.ProductLastTransfer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Product Last Trasfer Error");
            }
        }

        private async Task UpdateProductPriceLastTransfer()
        {
            try
            {
                var parameter = _context.B2BParameter.First();
                parameter.ProductPriceLastTransfer = DateTime.Now;
                _context.B2BParameter.Update(parameter);
                await _context.SaveChangesAsync();

                _b2bParameter.ProductPriceLastTransfer = parameter.ProductPriceLastTransfer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Product Last Trasfer Error");
            }
        }

        private async Task UpdateProductStockLastTransfer()
        {
            try
            {
                var parameter = _context.B2BParameter.First();
                parameter.ProductStockLastTransfer = DateTime.Now;
                _context.B2BParameter.Update(parameter);
                await _context.SaveChangesAsync();

                _b2bParameter.ProductStockLastTransfer = parameter.ProductStockLastTransfer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Product Last Trasfer Error");
            }
        }
    }
}
