using Microsoft.Extensions.Logging;
using NetB2BTransfer.B2B.Library.B2B;
using NetB2BTransfer.B2B.Library.B2B.Models;
using NetB2BTransfer.B2B.Library.SmartStore;
using NetB2BTransfer.B2B.Library.SmartStore.Models;
using NetB2BTransfer.Core.Data;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Core.Utils;
using NetB2BTransfer.Logo.Library.Class;
using NetB2BTransfer.Logo.Library.Models;
using NetB2BTransfer.Netsis.Library.Class;
using NetB2BTransfer.Netsis.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.B2B.Library
{
    public class Transfer
    {
        private readonly B2BClient _b2BClient;
        private readonly SmartStoreClient _smartStoreClient;
        private readonly ErpSetting _erpSetting;
        private readonly B2BSetting _b2bSetting;
        private readonly LogoTransferSetting _logoTransferSetting;

        public Transfer(ILogger logger,ErpSetting erpSetting, B2BSetting b2BSetting)
        {
            _erpSetting = erpSetting;
            _b2bSetting = b2BSetting;
            _b2BClient = new B2BClient(b2BSetting);
            _smartStoreClient = new SmartStoreClient(b2BSetting);
        }

        public Transfer(ILogger logger,ErpSetting erpSetting, B2BSetting b2BSetting, LogoTransferSetting logoTransferSetting)
        {
            _erpSetting = erpSetting;
            _b2bSetting = b2BSetting;
            _b2BClient = new B2BClient(b2BSetting);
            _smartStoreClient = new SmartStoreClient(b2BSetting);
            _logoTransferSetting = logoTransferSetting;
        }

        public async Task CariTransfer()
        {
            string errorMessage = string.Empty;

            if (_erpSetting.Erp == "Logo")
            {
                LogoQueryParam param = new LogoQueryParam
                {
                    DbName = "LOGODB",
                    firmnr = "200",
                    periodnr = "1",
                    filter = _logoTransferSetting.CustomerFilter!,
                    limit = "-1",
                    offset = "0",
                    orderbyfieldname = "CLCARD.CODE",
                    ascdesc = "ASC",
                };

                if (_erpSetting.LastTransferDate != null)
                {
                    param.filter += " AND ((CLCARD.CAPIBLOCK_CREADEDDATE >= '" + _erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (CLCARD.CAPIBLOCK_MODIFIEDDATE >= '" + _erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
                }

                var arpList = DataReader.ReadData<ArpModel>(LogoQuery.GetArpsQuery(param), ref errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                    throw new Exception(errorMessage);


                foreach (var item in arpList)
                {
                    var musteri = new Musteri
                    {
                        musteri_ozellik = "kurumsal",
                        unvan = item.DEFINITION_,
                        cari_kod = item.CODE,
                        adi = item.CUSTNAME,
                        soyadi = item.CUSTSURNAME,
                        telefon = item.TELNRS1,
                        adres = item.ADDR1 + " " + item.ADDR2,
                        il = item.CITY,
                        ilce = item.TOWN,
                        vergi_dairesi = item.TAXOFFICE,
                        vergi_no = item.TAXNR,
                        tc_no = item.TCKNO,
                        plasiyer = item.CYPHCODE,
                        depo_kodu = "",
                        erp_kodu = item.CODE,
                        odeme_sekilleri = new List<string>(),
                        musteri_kosul_kodu = item.SPECODE,
                        grup_kodu = "TURKUAZ",
                        fiyat_listesi_kodu = "",
                        email = item.CODE + "@turkuaz.com",
                        kullanici_adi = item.CODE,
                        sifre = item.CODE.Substring(6),
                        email_durum_bildirimi = "H",
                        musteri_durumu = "1",
                    };
                    var result = await _b2BClient.MusteriTransferAsync(musteri);
                    if (result.Code != 0)
                    {
                        throw new Exception(result.Message);
                    }
                }

            }
        }

        public async Task CariBakiyeTransfer()
        {
            string errorMessage = string.Empty;

            if (_erpSetting.Erp == "Logo")
            {
                LogoQueryParam param = new LogoQueryParam
                {
                    DbName = "LOGODB",
                    firmnr = "200",
                    periodnr = "1",
                    filter = string.Empty,
                    limit = "-1",
                    offset = "0",
                    orderbyfieldname = "CLCARD.CODE",
                    ascdesc = "ASC",
                };

                if (_logoTransferSetting.CustomerLastTransfer != null)
                {
                    param.filter += " AND ((CLCARD.CAPIBLOCK_CREADEDDATE >= '" + _logoTransferSetting.CustomerLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (CLCARD.CAPIBLOCK_MODIFIEDDATE >= '" + _logoTransferSetting.CustomerLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
                }

                var arpList = DataReader.ReadData<ArpModel>(LogoQuery.GetArpsQuery(param), ref errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                    throw new Exception(errorMessage);

                List<MusteriBakiye> musteriBakiyeList = new List<MusteriBakiye>();
                foreach (var item in arpList)
                {
                    var musteri = new MusteriBakiye
                    {
                        cari_kod = item.CODE,
                        doviz_kodu = "TL",
                        bakiye = item.BALANCE,
                        gecikmis_gun = 0,
                        gecikmis_bakiye = 0,
                        borc_alacak_tipi = "B",
                    };

                    musteriBakiyeList.Add(musteri);
                }

                var result = await _b2BClient.MusteriBakiyeTransferAsync(musteriBakiyeList);
                if (result != null)
                {
                    if (result.Code != 0)
                    {
                        throw new Exception(result.Message);
                    }
                }

            }
        }

        public async Task MalzemeTransfer()
        {
            string errorMessage = string.Empty;

            if (_erpSetting.Erp == "Logo")
            {
                LogoQueryParam param = new LogoQueryParam
                {
                    DbName = "LOGODB",
                    firmnr = "200",
                    periodnr = "1",
                    filter = " AND (ITEMS.CODE NOT LIKE 'ÿ') AND (LEN(ITEMS.NAME) > 0) AND (ITEMS.NAME NOT IN ('AS 396/A', 'SP 453 M'))",
                    limit = "-1",
                    offset = "0",
                    orderbyfieldname = "ITEMS.CODE",
                    ascdesc = "ASC",
                };

                if (_erpSetting.LastTransferDate != null)
                {
                    param.filter += " AND ((ITEMS.CAPIBLOCK_CREADEDDATE >= '" + _erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ITEMS.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (ITEMS.CAPIBLOCK_MODIFIEDDATE >= '" + _erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ITEMS.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
                }

                var itemList = DataReader.ReadData<ItemModel>(LogoQuery.GetItemsQuery(param), ref errorMessage);

                if (_b2bSetting.B2B == "B2B")
                {
                    List<Urun> urunList = new List<Urun>();
                    foreach (var item in itemList)
                    {
                        Urun urun = new Urun();
                        urun.marka = new Marka() { kod = item.MARKNAME, baslik = item.MARKNAME };
                        urun.grup = new Grup() { kod = item.STGRPCODE, baslik = item.STGRPCODE };
                        urun.birim = new Birim() { kod = item.CYPHCODE, baslik = item.CYPHCODE };
                        urun.barkod_no = item.BARCODE;
                        urun.urun_kodu = item.CODE;
                        urun.doviz_kodu = "TRY";
                        urun.baslik = item.NAME;
                        urun.aciklama = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                        urun.icerik = "";
                        urun.kdv_durumu = "kdv_haric";
                        urun.kdv_orani = item.VAT;
                        urun.liste_fiyati = item.PRICE;
                        urun.durum = item.EXTACCESSFLAGS == 6 || item.EXTACCESSFLAGS == 7 ? 1 : 0;
                        urun.yeni_urun = "0";
                        urun.yeni_urun_tarih = Convert.ToDateTime("2024-01-01");
                        urun.minimum_satin_alma_miktari = 1;
                        urun.sepete_eklenme_miktari = 1;
                        urun.varyant_durumu = "yok";
                        urun.asorti_durumu = "yok";
                        urun.model_no = "";
                        urun.varyant_1 = new Varyant() { kod = "", baslik = "" };
                        urun.varyant_2 = new Varyant() { kod = "", baslik = "" };
                        urun.asorti_miktar = 0;
                        urun.asorti_kod = "";
                        urun.kategoriler = new List<Kategori>();

                        urunList.Add(urun);
                    }

                    int pageCount = PaginationBuilder.GetPageCount(urunList, 100);
                    for (int i = 1; i <= pageCount; i++)
                    {
                        var result = await _b2BClient.MusteriBakiyeTransferAsync(PaginationBuilder.GetPage(urunList, i, 100).ToList());
                        if (result != null)
                        {
                            if (result.Code != 0)
                            {
                                throw new Exception(result.Message);
                            }
                        }
                    }
                }
                else if (_b2bSetting.B2B == "Smartstore")
                {
                    List<Product> productList = new List<Product>();
                    foreach (var item in itemList)
                    {
                        Product product = new Product();
                        product.ProductTypeId = 5;
                        product.ParentGroupedProductId = 0;
                        product.Visibility = "Full";
                        product.Condition = "New";
                        product.Name = item.NAME;
                        product.ShortDescription = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                        product.FullDescription = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                        product.AdminComment = "";
                        product.ProductTemplateId = 1;
                        product.ShowOnHomePage = false;
                        product.HomePageDisplayOrder = 0;
                        product.MetaKeywords = "";
                        product.MetaTitle = item.NAME;
                        product.MetaDescription = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                        product.AllowCustomerReviews = true;
                        product.ApprovedRatingSum = 0;
                        product.NotApprovedRatingSum = 0;
                        product.ApprovedTotalReviews = 0;
                        product.NotApprovedTotalReviews = 0;
                        product.SubjectToAcl = false;
                        product.LimitedToStores = false;
                        product.Sku = item.CODE;
                        product.ManufacturerPartNumber = "";
                        product.Gtin = "";
                        product.IsGiftCard = false;
                        product.GiftCardTypeId = 0;
                        product.RequireOtherProducts = false;
                        product.RequiredProductIds = null;
                        product.AutomaticallyAddRequiredProducts = false;
                        product.IsDownload = false;
                        product.UnlimitedDownloads = true;
                        product.MaxNumberOfDownloads = 10;
                        product.DownloadExpirationDays = null;
                        product.DownloadActivationTypeId = 1;
                        product.HasSampleDownload = false;
                        product.SampleDownloadId = null;
                        product.HasUserAgreement = false;
                        product.UserAgreementText = null;
                        product.IsRecurring = false;
                        product.RecurringCycleLength = 100;
                        product.RecurringCyclePeriodId = 0;
                        product.RecurringTotalCycles = 10;
                        product.IsShippingEnabled = true;
                        product.IsFreeShipping = false;
                        product.AdditionalShippingCharge = 0;
                        product.IsTaxExempt = false;
                        product.IsEsd = false;
                        product.TaxCategoryId = 1;
                        product.ManageInventoryMethodId = 0;
                        product.StockQuantity = 10000;
                        product.DisplayStockAvailability = false;
                        product.DisplayStockQuantity = false;
                        product.MinStockQuantity = 0;
                        product.LowStockActivityId = 0;
                        product.NotifyAdminForQuantityBelow = 0;
                        product.BackorderModeId = 0;
                        product.AllowBackInStockSubscriptions = false;
                        product.OrderMinimumQuantity = 1;
                        product.OrderMaximumQuantity = 50;
                        product.QuantityStep = 1;
                        product.QuantityControlType = "Spinner";
                        product.HideQuantityControl = false;
                        product.AllowedQuantities = null;
                        product.DisableBuyButton = false;
                        product.DisableWishlistButton = false;
                        product.AvailableForPreOrder = false;
                        product.CallForPrice = false;
                        product.Price = item.PRICE;
                        product.ComparePrice = 0;
                        product.ComparePriceLabelId = null;
                        product.SpecialPrice = null;
                        product.SpecialPriceStartDateTimeUtc = null;
                        product.SpecialPriceEndDateTimeUtc = null;
                        product.CustomerEntersPrice = false;
                        product.MinimumCustomerEnteredPrice = 0;
                        product.MaximumCustomerEnteredPrice = 1000;
                        product.HasTierPrices = false;
                        product.LowestAttributeCombinationPrice = null;
                        product.AttributeCombinationRequired = false;
                        product.AttributeChoiceBehaviour = "GrayOutUnavailable";
                        product.Weight = 0;
                        product.Length = 0;
                        product.Width = 0;
                        product.Height = 0;
                        product.AvailableStartDateTimeUtc = null;
                        product.AvailableEndDateTimeUtc = null;
                        product.DisplayOrder = 0;
                        product.Published = true;
                        product.IsSystemProduct = false;
                        product.SystemName = "";
                        product.CreatedOnUtc = DateTimeOffset.UtcNow;
                        product.UpdatedOnUtc = DateTimeOffset.UtcNow;
                        product.DeliveryTimeId = null;
                        product.QuantityUnitId = null;
                        product.CustomsTariffNumber = "";
                        product.CountryOfOriginId = 77;
                        product.BasePriceEnabled = false;
                        product.BasePriceMeasureUnit = null;
                        product.BasePriceAmount = null;
                        product.BasePriceBaseAmount = null;
                        product.BundleTitleText = null;
                        product.BundlePerItemShipping = false;
                        product.BundlePerItemPricing = false;
                        product.BundlePerItemShoppingCart = false;
                        product.MainPictureId = null;
                        product.HasPreviewPicture = false;
                        product.HasDiscountsApplied = false;
                        product.Id = 0;
                        product.GroupedProductConfiguration = null;
                        product.ManufacturerCode = item.STGRPCODE;
                        product.ManufacturerName = item.STGRPNAME;
                        product.Category1 = item.STGRPNAME;

                        productList.Add(product);
                    }

                    foreach (var item in productList)
                    {
                        Product? product = await SmartStoreMalzemeTrasnfer(item);
                        Manufacturer? manufacturer = await SmarStoreManufacturerTransfer(item.ManufacturerName);
                        Category? category = await SmarStoreCategoryTransfer(item.Category1, null);

                        if (product != null && manufacturer != null)
                        {
                            ProductManufacturer? productManufacturer = await SmartStoreProductManufacturerTransfer(product, manufacturer);
                        }

                        if (product != null && category != null)
                        {
                            ProductCategory? productCategory = await SmartStoreProductCategoryTransfer(product, category);
                        }
                    }
                }

            }
            else if (_erpSetting.Erp == "Netsis")
            {
                var malzemeList = DataReader.ReadData<MalzemeModel>(NetsisQuery.GetMalzemeQuery(), ref errorMessage);
                if (_b2bSetting.B2B == "B2B")
                {

                }
                else if (_b2bSetting.B2B == "Smartstore")
                {
                    List<Product> productList = new List<Product>();
                    foreach (var item in malzemeList)
                    {
                        Product product = new Product();
                        product.ProductTypeId = 5;
                        product.ParentGroupedProductId = 0;
                        product.Visibility = "Full";
                        product.Condition = "New";
                        product.Name = NetsisUtils.CevirNetsis(item.STOK_ADI);
                        product.ShortDescription = "";
                        product.FullDescription = "";
                        product.AdminComment = "";
                        product.ProductTemplateId = 1;
                        product.ShowOnHomePage = false;
                        product.HomePageDisplayOrder = 0;
                        product.MetaKeywords = "";
                        product.MetaTitle = NetsisUtils.CevirNetsis(item.STOK_ADI);
                        product.MetaDescription = "";
                        product.AllowCustomerReviews = true;
                        product.ApprovedRatingSum = 0;
                        product.NotApprovedRatingSum = 0;
                        product.ApprovedTotalReviews = 0;
                        product.NotApprovedTotalReviews = 0;
                        product.SubjectToAcl = false;
                        product.LimitedToStores = false;
                        product.Sku = NetsisUtils.CevirNetsis(item.STOK_KODU);
                        product.ManufacturerPartNumber = "";
                        product.Gtin = "";
                        product.IsGiftCard = false;
                        product.GiftCardTypeId = 0;
                        product.RequireOtherProducts = false;
                        product.RequiredProductIds = null;
                        product.AutomaticallyAddRequiredProducts = false;
                        product.IsDownload = false;
                        product.UnlimitedDownloads = true;
                        product.MaxNumberOfDownloads = 10;
                        product.DownloadExpirationDays = null;
                        product.DownloadActivationTypeId = 1;
                        product.HasSampleDownload = false;
                        product.SampleDownloadId = null;
                        product.HasUserAgreement = false;
                        product.UserAgreementText = null;
                        product.IsRecurring = false;
                        product.RecurringCycleLength = 100;
                        product.RecurringCyclePeriodId = 0;
                        product.RecurringTotalCycles = 10;
                        product.IsShippingEnabled = true;
                        product.IsFreeShipping = false;
                        product.AdditionalShippingCharge = 0;
                        product.IsTaxExempt = false;
                        product.IsEsd = false;
                        product.TaxCategoryId = 1;
                        product.ManageInventoryMethodId = 0;
                        product.StockQuantity = 10000;
                        product.DisplayStockAvailability = false;
                        product.DisplayStockQuantity = false;
                        product.MinStockQuantity = 0;
                        product.LowStockActivityId = 0;
                        product.NotifyAdminForQuantityBelow = 0;
                        product.BackorderModeId = 0;
                        product.AllowBackInStockSubscriptions = false;
                        product.OrderMinimumQuantity = 1;
                        product.OrderMaximumQuantity = 50;
                        product.QuantityStep = 1;
                        product.QuantityControlType = "Spinner";
                        product.HideQuantityControl = false;
                        product.AllowedQuantities = null;
                        product.DisableBuyButton = false;
                        product.DisableWishlistButton = false;
                        product.AvailableForPreOrder = false;
                        product.CallForPrice = false;
                        product.Price = item.SATIS_FIAT1;
                        product.ComparePrice = 0;
                        product.ComparePriceLabelId = null;
                        product.SpecialPrice = null;
                        product.SpecialPriceStartDateTimeUtc = null;
                        product.SpecialPriceEndDateTimeUtc = null;
                        product.CustomerEntersPrice = false;
                        product.MinimumCustomerEnteredPrice = 0;
                        product.MaximumCustomerEnteredPrice = 1000;
                        product.HasTierPrices = false;
                        product.LowestAttributeCombinationPrice = null;
                        product.AttributeCombinationRequired = false;
                        product.AttributeChoiceBehaviour = "GrayOutUnavailable";
                        product.Weight = 0;
                        product.Length = 0;
                        product.Width = 0;
                        product.Height = 0;
                        product.AvailableStartDateTimeUtc = null;
                        product.AvailableEndDateTimeUtc = null;
                        product.DisplayOrder = 0;
                        product.Published = true;
                        product.IsSystemProduct = false;
                        product.SystemName = "";
                        product.CreatedOnUtc = DateTimeOffset.UtcNow;
                        product.UpdatedOnUtc = DateTimeOffset.UtcNow;
                        product.DeliveryTimeId = null;
                        product.QuantityUnitId = null;
                        product.CustomsTariffNumber = "";
                        product.CountryOfOriginId = 77;
                        product.BasePriceEnabled = false;
                        product.BasePriceMeasureUnit = null;
                        product.BasePriceAmount = null;
                        product.BasePriceBaseAmount = null;
                        product.BundleTitleText = null;
                        product.BundlePerItemShipping = false;
                        product.BundlePerItemPricing = false;
                        product.BundlePerItemShoppingCart = false;
                        product.MainPictureId = null;
                        product.HasPreviewPicture = false;
                        product.HasDiscountsApplied = false;
                        product.Id = 0;
                        product.GroupedProductConfiguration = null;
                        product.ManufacturerCode = item.KOD_5;
                        product.ManufacturerName = item.KOD_5_ISIM;
                        product.Category1 = item.KOD_3_ISIM;
                        product.Category2 = item.KOD_4_ISIM;




                        productList.Add(product);
                    }

                    foreach (var item in productList)
                    {
                        Product? product = await SmartStoreMalzemeTrasnfer(item);
                        Manufacturer? manufacturer = null;
                        if (!string.IsNullOrEmpty(item.ManufacturerName))
                        {
                            manufacturer = await SmarStoreManufacturerTransfer(item.ManufacturerName);
                        }


                        Category? category1 = null;
                        if (!string.IsNullOrEmpty(item.Category1))
                        {
                            category1 = await SmarStoreCategoryTransfer(item.Category1, null);
                        }

                        Category? category2 = null;
                        if (category1 != null)
                        {
                            if (!string.IsNullOrEmpty(item.Category2))
                            {
                                category2 = await SmarStoreCategoryTransfer(item.Category2, category1.Id);
                            }
                        }

                        if (product != null && manufacturer != null)
                        {
                            ProductManufacturer? productManufacturer = await SmartStoreProductManufacturerTransfer(product, manufacturer);
                        }

                        if (product != null && category2 != null)
                        {
                            ProductCategory? productCategory = await SmartStoreProductCategoryTransfer(product, category2);
                        }
                        else
                        {
                            if (product != null && category1 != null)
                            {
                                ProductCategory? productCategory = await SmartStoreProductCategoryTransfer(product, category1);
                            }
                        }


                        if (product != null)
                        {
                            try
                            {
                                var evrakList = DataReader.ReadData<EvrakModel>(NetsisQuery.GetEvrakQuery(item.Sku), ref errorMessage);
                                if (evrakList != null)
                                {
                                    foreach (var evrak in evrakList)
                                    {
                                        byte[] file = evrak.BILGI;
                                        string fileName = $"content/{Path.GetFileName(evrak.DOSYAADI)}";
                                        string mimeType = GetMimeType(Path.GetExtension(evrak.DOSYAADI));

                                        var fileItemInfo = await SmartStoreMediaFileTransfer(file, fileName, mimeType);
                                        if (fileItemInfo != null)
                                        {
                                            ProductMediaFile? productMediaFile = await SmartStoreProductMediaFileTransfer(product, fileItemInfo);
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
            }
        }

        private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.adobe.air-application-installer-package+zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-itunes-ipa"},
        {".ipg", "application/x-itunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-itunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-itunes-ite"},
        {".itlp", "application/x-itunes-itlp"},
        {".itms", "application/x-itunes-itms"},
        {".itpc", "application/x-itunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/java-archive"},
        {".java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-powerpoint"},
        {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-powerpoint"},
        {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-powerpoint"},
        {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-powerpoint"},
        {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-powerpoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-excel"},
        {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-excel"},
        {".xld", "application/vnd.ms-excel"},
        {".xlk", "application/vnd.ms-excel"},
        {".xll", "application/vnd.ms-excel"},
        {".xlm", "application/vnd.ms-excel"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-excel"},
        {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".zip", "application/x-zip-compressed"},
        #endregion
        
        };

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        private async Task<ProductMediaFile?> SmartStoreProductMediaFileTransfer(Product product, FileItemInfo fileItemInfo)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductMediaFile(product.Id, fileItemInfo.Id);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    return productManufacturerResult.value.First();
                }
                else
                {
                    ProductMediaFile productMediaFile = new ProductMediaFile();
                    productMediaFile.ProductId = product.Id;
                    productMediaFile.MediaFileId = fileItemInfo.Id;
                    productMediaFile.DisplayOrder = 0;


                    return await _smartStoreClient.ProductMediaFileTrasnfer(productMediaFile);
                }
            }

            return null;


        }
        private async Task<ProductManufacturer?> SmartStoreProductManufacturerTransfer(Product product, Manufacturer manufacturer)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductManufacturer(product.Id, manufacturer.Id);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    return productManufacturerResult.value.First();
                }
                else
                {
                    ProductManufacturer productManufacturer = new ProductManufacturer();
                    productManufacturer.ProductId = product.Id;
                    productManufacturer.ManufacturerId = manufacturer.Id;
                    productManufacturer.IsFeaturedProduct = false;
                    productManufacturer.DisplayOrder = 0;


                    return await _smartStoreClient.ProductManufacturerTrasnfer(productManufacturer);
                }
            }

            return null;


        }
        private async Task<ProductCategory?> SmartStoreProductCategoryTransfer(Product product, Category category)
        {
            var productCategoryResult = await _smartStoreClient.GetProductCategory(product.Id, category.Id);
            if (productCategoryResult != null)
            {
                if (productCategoryResult.value.Any())
                {
                    return productCategoryResult.value.First();
                }
                else
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.ProductId = product.Id;
                    productCategory.CategoryId = category.Id;
                    productCategory.IsFeaturedProduct = false;
                    productCategory.DisplayOrder = 0;
                    productCategory.IsSystemMapping = false;


                    return await _smartStoreClient.ProductCategoryTrasnfer(productCategory);
                }
            }

            return null;


        }
        private async Task<Product?> SmartStoreMalzemeTrasnfer(Product product)
        {

            var productResult = await _smartStoreClient.GetProduct(product.Sku);
            if (productResult != null)
            {
                if (productResult.value.Any())
                {
                    return productResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductTransfer(product);
                }
            }

            return null;
        }
        private async Task<Manufacturer?> SmarStoreManufacturerTransfer(string manufacturer)
        {

            var manufacuterResult = await _smartStoreClient.GetManufacturer(manufacturer);
            if (manufacuterResult != null)
            {
                if (manufacuterResult.value.Any())
                {
                    return manufacuterResult.value.First();
                }
                else
                {
                    Manufacturer data = new Manufacturer();
                    data.Name = manufacturer;
                    data.Description = "";
                    data.BottomDescription = "";
                    data.ManufacturerTemplateId = 0;
                    data.MetaKeywords = "";
                    data.MetaDescription = "";
                    data.MetaTitle = manufacturer;
                    data.MediaFileId = null;
                    data.PageSize = 10;
                    data.AllowCustomersToSelectPageSize = false;
                    data.PageSizeOptions = "10,20,50";
                    data.LimitedToStores = false;
                    data.SubjectToAcl = false;
                    data.DisplayOrder = 0;
                    data.Published = true;
                    data.CreatedOnUtc = DateTimeOffset.UtcNow;
                    data.UpdatedOnUtc = DateTimeOffset.UtcNow;
                    data.HasDiscountsApplied = false;
                    data.Id = 0;

                    return await _smartStoreClient.ManufacturerTransfer(data);
                }
            }

            return null;

        }
        private async Task<Category?> SmarStoreCategoryTransfer(string category, int? parentId)
        {

            var categoryResult = await _smartStoreClient.GetCategory(category);
            if (categoryResult != null)
            {
                if (categoryResult.value.Any())
                {
                    return categoryResult.value.First();
                }
                else
                {
                    Category data = new Category();
                    data.Alias = "";
                    data.AllowCustomersToSelectPageSize = false;
                    data.BadgeStyle = 0;
                    data.BadgeText = "";
                    data.BottomDescription = "";
                    data.CategoryTemplateId = 0;
                    data.CreatedOnUtc = DateTime.Now;
                    data.TreePath = "/";
                    data.FullName = category;
                    data.DefaultViewMode = "Grid";
                    data.Description = "";
                    data.DisplayOrder = 0;
                    data.UpdatedOnUtc = DateTime.Now;
                    data.SubjectToAcl = false;
                    data.ShowOnHomePage = false;
                    data.Published = true;
                    data.ParentId = parentId;
                    data.PageSizeOptions = "10,20,50";
                    data.PageSize = 20;
                    data.Name = category;
                    data.MetaTitle = category;
                    data.MetaKeywords = "";
                    data.MetaDescription = "";
                    data.MediaFileId = null;
                    data.LimitedToStores = false;
                    data.IgnoreInMenus = false;
                    data.Id = 0;
                    data.HasDiscountsApplied = false;
                    data.ExternalLink = "";


                    return await _smartStoreClient.CategoryTransfer(data);
                }
            }

            return null;

        }
        private async Task<FileItemInfo?> SmartStoreMediaFileTransfer(byte[] file, string fileName, string mimeType)
        {
            var response = await _smartStoreClient.FileExists(fileName);
            if (response != null)
            {
                if (response.value)
                {
                    return await _smartStoreClient.GetFileByPath(fileName);
                }
            }

            return await _smartStoreClient.MediaFileSave(file, fileName, mimeType);
        }

        public async Task MalzemeStokTransfer()
        {
            string errorMessage = string.Empty;
            if (_erpSetting.Erp == "Logo")
            {

            }
            else if (_erpSetting.Erp == "Netsis")
            {
                var malzemeStokList = DataReader.ReadData<MalzemeStokModel>(NetsisQuery.GetMalzemeStokQuery(), ref errorMessage);
                foreach (var item in malzemeStokList)
                {
                    var productResult = await _smartStoreClient.GetProduct(item.STOK_KODU);
                    if (productResult != null)
                    {
                        if (productResult.value.Any())
                        {
                            var product = productResult.value.First();

                            var stokResult = await _smartStoreClient.UpdateProductStock(product.Id, Convert.ToInt32(item.STOK_MIKTARI));
                        }
                    }
                }
            }
        }

        public async Task MalzemeFiyatTransfer()
        {
            string errorMessage = string.Empty;
            if (_erpSetting.Erp == "Logo")
            {

            }
            else if (_erpSetting.Erp == "Netsis")
            {
                var malzemeFiyatList = DataReader.ReadData<MalzemeFiyatModel>(NetsisQuery.GetMalzemeFiyatQuery(), ref errorMessage);
                foreach (var item in malzemeFiyatList)
                {
                    var productResult = await _smartStoreClient.GetProduct(item.STOK_KODU);
                    if (productResult != null)
                    {
                        if (productResult.value.Any())
                        {
                            var product = productResult.value.First();

                            var priceResult = await _smartStoreClient.UpdateProductPrice(product.Id, item.LISTE_FIYATI, item.INDIRIMLI_FIYATI);
                        }
                    }
                }
            }
        }

        public async Task SiparisTransfer()
        {
            throw new NotImplementedException();
        }

        public async Task SanalPosTransfer()
        {
            throw new NotImplementedException();
        }
    }
}
