using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Integration.Models;
using NetTransfer.Logo.Library.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using NetTransfer.Smartstore.Library;
using NetTransfer.Smartstore.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.VirtualStore
{
    public class SmartstoreService
    {
        private readonly SmartStoreClient _smartStoreClient;
        public SmartstoreService(SmartStoreClient smartStoreClient)
        {
            _smartStoreClient = smartStoreClient;
        }

        public async Task<SmartstoreProduct?> CreateProduct(SmartstoreProduct product)
        {
            #region Product
            SmartstoreProduct? result = null;

            var productResult = await _smartStoreClient.GetProduct(product.Sku);
            if (productResult != null)
            {
                if (productResult.value.Any())
                {
                    product = productResult.value.First();
                }
                else
                {
                    result = await _smartStoreClient.ProductTransfer(product);
                }
            }

            if (result == null)
            {
                return null;
            }
            #endregion

            #region Manufacturer
            SmartstoreManufacturer? manufacturer = null;
            if (!string.IsNullOrEmpty(product.ManufacturerName))
            {
                manufacturer = await CreateManufacturer(MappingManufacturer(product.ManufacturerName));
            }

            if (manufacturer != null)
            {
                SmartstoreProductManufacturer smartstoreProductManufacturer = new SmartstoreProductManufacturer();
                smartstoreProductManufacturer.ProductId = product.Id;
                smartstoreProductManufacturer.ManufacturerId = manufacturer.Id;
                smartstoreProductManufacturer.IsFeaturedProduct = false;
                smartstoreProductManufacturer.DisplayOrder = 0;

                _ = await CreateProductManufacturer(smartstoreProductManufacturer);
            }
            #endregion

            #region Category
            SmartstoreCategory? category1 = null;
            if (!string.IsNullOrEmpty(product.Category1))
            {
                category1 = await CreateCategory(MappingCategory(product.Category1,null));
            }

            SmartstoreCategory? category2 = null;
            if (category1 != null)
            {
                if (!string.IsNullOrEmpty(product.Category2))
                {
                    category2 = await CreateCategory(MappingCategory(product.Category2, category1.Id));
                }
            }

            SmartstoreProductCategory? productCategory2 = null;
            if (category2 != null)
            {
                SmartstoreProductCategory data = new SmartstoreProductCategory();
                data.ProductId = result.Id;
                data.CategoryId = category2.Id;
                data.IsFeaturedProduct = false;
                data.DisplayOrder = 0;
                data.IsSystemMapping = false;
                data.Id = 0;

                productCategory2 = await CreateProductCategory(data);
            }

            SmartstoreProductCategory? productCategory1 = null;
            if (category1 != null)
            {
                SmartstoreProductCategory data = new SmartstoreProductCategory();
                data.ProductId = result.Id;
                data.CategoryId = category1.Id;
                data.IsFeaturedProduct = false;
                data.DisplayOrder = 0;
                data.IsSystemMapping = false;
                data.Id = productCategory2 == null ? 0 : 1;

                productCategory1 = await CreateProductCategory(data);
            }
            #endregion

            #region Image
            if (product.Files != null)
            {
                foreach (var file in product.Files)
                {
                    SmartstoreFileItemInfo? fileItemInfo = await CreateMediaFile(file);
                    if (fileItemInfo != null)
                    {
                        SmartstoreProductMediaFile smartstoreProductMediaFile = new SmartstoreProductMediaFile();
                        smartstoreProductMediaFile.ProductId = product.Id;
                        smartstoreProductMediaFile.MediaFileId = fileItemInfo.Id;
                        smartstoreProductMediaFile.DisplayOrder = 0;

                        _ = await CreateProductMediaFile(smartstoreProductMediaFile);
                    }
                }
            } 
            #endregion

            return result;
        }
        public async Task UpdateProductPrice(List<BaseMalzemeFiyatModel> malzemeFiyatList)
        {
            foreach (var item in malzemeFiyatList)
            {
                var productResult = await _smartStoreClient.GetProduct(item.StokKodu);
                if (productResult != null)
                {
                    if (productResult.value.Any())
                    {
                        var product = productResult.value.First();

                        var priceResult = await _smartStoreClient.UpdateProductPrice(product.Id, item.Fiyat, item.IndirimliFiyat);
                    }
                }
            }
        }
        public async Task UpdateProductStock(List<BaseMalzemeStokModel> malzemeFiyatList)
        {
            foreach (var item in malzemeFiyatList)
            {
                var productResult = await _smartStoreClient.GetProduct(item.StokKodu);
                if (productResult != null)
                {
                    if (productResult.value.Any())
                    {
                        var product = productResult.value.First();

                        var priceResult = await _smartStoreClient.UpdateProductStock(product.Id, Convert.ToInt32(item.StokMiktari));
                    }
                }
            }
        }
        public async Task<SmartstoreManufacturer?> CreateManufacturer(SmartstoreManufacturer smartstoreManufacturer)
        {
            var manufacuterResult = await _smartStoreClient.GetManufacturer(smartstoreManufacturer.Name);
            if (manufacuterResult != null)
            {
                if (manufacuterResult.value.Any())
                {
                    return manufacuterResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ManufacturerTransfer(smartstoreManufacturer);
                }
            }

            return null;

        }
        public async Task<SmartstoreProductManufacturer?> CreateProductManufacturer(SmartstoreProductManufacturer smartstoreProductManufacturer)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductManufacturer(smartstoreProductManufacturer.ProductId, smartstoreProductManufacturer.ManufacturerId);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    return productManufacturerResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductManufacturerTrasnfer(smartstoreProductManufacturer);
                }
            }

            return null;


        }
        public async Task<SmartstoreCategory?> CreateCategory(SmartstoreCategory smartstoreCategory)
        {
            var categoryResult = await _smartStoreClient.GetCategory(smartstoreCategory.Name);
            if (categoryResult != null)
            {
                if (categoryResult.value.Any())
                {
                    return categoryResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.CategoryTransfer(smartstoreCategory);
                }
            }

            return null;
        }
        public async Task<SmartstoreProductCategory?> CreateProductCategory(SmartstoreProductCategory productCategory)
        {
            var productCategoryResult = await _smartStoreClient.GetProductCategory(productCategory.ProductId, productCategory.CategoryId);
            if (productCategoryResult != null)
            {
                if (productCategoryResult.value.Any())
                {
                    return productCategoryResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductCategoryTrasnfer(productCategory);
                }
            }

            return null;


        }
        public async Task<SmartstoreFileItemInfo?> CreateMediaFile(SmartstoreFile smartstoreFile)
        {
            var response = await _smartStoreClient.FileExists(smartstoreFile.FileName);
            if (response != null)
            {
                if (response.value)
                {
                    return await _smartStoreClient.GetFileByPath(smartstoreFile.FileName);
                }
            }

            return await _smartStoreClient.MediaFileSave(smartstoreFile);
        }
        public async Task<SmartstoreProductMediaFile?> CreateProductMediaFile(SmartstoreProductMediaFile smartstoreProductMediaFile)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductMediaFile(smartstoreProductMediaFile.ProductId, smartstoreProductMediaFile.MediaFileId);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    return productManufacturerResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductMediaFileTrasnfer(smartstoreProductMediaFile);
                }
            }

            return null;


        }

        #region MappingProduct
        public List<SmartstoreProduct>? MappingProduct(string erp, object? malzemeList)
        {
            switch (erp)
            {
                case "Logo":
                    return MappingProductLogo(malzemeList as List<ItemModel>);
                case "Netsis":
                    return MappingProductNetsis(malzemeList as List<MalzemeModel>);
                case "Opak":
                    return MappingProductOpak(malzemeList as List<ItemModel>);
            }

            return null;
        }
        private List<SmartstoreProduct> MappingProductLogo(List<ItemModel>? malzemeList)
        {
            List<SmartstoreProduct> productList = new List<SmartstoreProduct>();
            foreach (var item in malzemeList)
            {
                SmartstoreProduct product = new SmartstoreProduct();
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
            return productList;
        }
        private List<SmartstoreProduct> MappingProductNetsis(List<MalzemeModel>? malzemeList)
        {
            List<SmartstoreProduct> productList = new List<SmartstoreProduct>();
            foreach (var item in malzemeList)
            {
                SmartstoreProduct product = new SmartstoreProduct();
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

                if (item.EVRAK_LIST != null)
                {
                    product.Files = new List<SmartstoreFile>();
                    foreach (var evrak in item.EVRAK_LIST)
                    {
                        if (evrak.BILGI == null || evrak.BILGI.Length == 0)
                            continue;

                        SmartstoreFile smartstoreFile = new SmartstoreFile();
                        smartstoreFile.FileName = $"content/{Path.GetFileName(evrak.DOSYAADI)}";
                        smartstoreFile.File = evrak.BILGI;
                        smartstoreFile.MimeType = MainUtils.GetMimeType(Path.GetExtension(evrak.DOSYAADI)!);

                        product.Files.Add(smartstoreFile);
                    }
                }

                productList.Add(product);
            }
            return productList;
        }
        private List<SmartstoreProduct> MappingProductOpak(List<ItemModel>? malzemeList)
        {
            List<SmartstoreProduct> productList = new List<SmartstoreProduct>();
            return productList;
        }
        #endregion

        #region MappingManufacturer
        public SmartstoreManufacturer MappingManufacturer(string manufacturer)
        {
            SmartstoreManufacturer smartstoreManufacturer = new SmartstoreManufacturer();
            smartstoreManufacturer.Name = manufacturer;
            smartstoreManufacturer.Description = "";
            smartstoreManufacturer.BottomDescription = "";
            smartstoreManufacturer.ManufacturerTemplateId = 0;
            smartstoreManufacturer.MetaKeywords = "";
            smartstoreManufacturer.MetaDescription = "";
            smartstoreManufacturer.MetaTitle = manufacturer;
            smartstoreManufacturer.MediaFileId = null;
            smartstoreManufacturer.PageSize = 10;
            smartstoreManufacturer.AllowCustomersToSelectPageSize = false;
            smartstoreManufacturer.PageSizeOptions = "10,20,50";
            smartstoreManufacturer.LimitedToStores = false;
            smartstoreManufacturer.SubjectToAcl = false;
            smartstoreManufacturer.DisplayOrder = 0;
            smartstoreManufacturer.Published = true;
            smartstoreManufacturer.CreatedOnUtc = DateTimeOffset.UtcNow;
            smartstoreManufacturer.UpdatedOnUtc = DateTimeOffset.UtcNow;
            smartstoreManufacturer.HasDiscountsApplied = false;
            smartstoreManufacturer.Id = 0;

            return smartstoreManufacturer;
        }
        #endregion

        #region MappingCategory
        public SmartstoreCategory MappingCategory(string category,int? parentId)
        {
            SmartstoreCategory smartstoreCategory = new SmartstoreCategory();
            smartstoreCategory.Alias = "";
            smartstoreCategory.AllowCustomersToSelectPageSize = false;
            smartstoreCategory.BadgeStyle = 0;
            smartstoreCategory.BadgeText = "";
            smartstoreCategory.BottomDescription = "";
            smartstoreCategory.CategoryTemplateId = 0;
            smartstoreCategory.CreatedOnUtc = DateTime.Now;
            smartstoreCategory.TreePath = "/";
            smartstoreCategory.FullName = category;
            smartstoreCategory.DefaultViewMode = "Grid";
            smartstoreCategory.Description = "";
            smartstoreCategory.DisplayOrder = 0;
            smartstoreCategory.UpdatedOnUtc = DateTime.Now;
            smartstoreCategory.SubjectToAcl = false;
            smartstoreCategory.ShowOnHomePage = false;
            smartstoreCategory.Published = true;
            smartstoreCategory.ParentId = parentId;
            smartstoreCategory.PageSizeOptions = "10,20,50";
            smartstoreCategory.PageSize = 20;
            smartstoreCategory.Name = category;
            smartstoreCategory.MetaTitle = category;
            smartstoreCategory.MetaKeywords = "";
            smartstoreCategory.MetaDescription = "";
            smartstoreCategory.MediaFileId = null;
            smartstoreCategory.LimitedToStores = false;
            smartstoreCategory.IgnoreInMenus = false;
            smartstoreCategory.Id = 0;
            smartstoreCategory.HasDiscountsApplied = false;
            smartstoreCategory.ExternalLink = "";

            return smartstoreCategory;
        }
        #endregion
    }
}
