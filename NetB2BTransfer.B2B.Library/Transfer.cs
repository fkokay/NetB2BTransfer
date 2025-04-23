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

        public Transfer(ErpSetting erpSetting, B2BSetting b2BSetting)
        {
            _erpSetting = erpSetting;
            _b2bSetting = b2BSetting;
            _b2BClient = new B2BClient(b2BSetting);
            _smartStoreClient = new SmartStoreClient(b2BSetting);
        }

        public Transfer(ErpSetting erpSetting, B2BSetting b2BSetting, LogoTransferSetting logoTransferSetting)
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
                    }
                }
            }
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

        public async Task MalzemeStokTransfer()
        {
            throw new NotImplementedException();
        }

        public async Task MalzemeFiyatTransfer()
        {
            throw new NotImplementedException();
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
