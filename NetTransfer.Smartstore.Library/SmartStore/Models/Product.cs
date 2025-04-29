using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class Product
    {
        [JsonProperty("ProductTypeId")]
        public int ProductTypeId { get; set; }

        [JsonProperty("ParentGroupedProductId")]
        public int ParentGroupedProductId { get; set; }

        [JsonProperty("Visibility")]
        public string Visibility { get; set; }

        [JsonProperty("Condition")]
        public string Condition { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ShortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("FullDescription")]
        public string FullDescription { get; set; }

        [JsonProperty("AdminComment")]
        public string AdminComment { get; set; }

        [JsonProperty("ProductTemplateId")]
        public int ProductTemplateId { get; set; }

        [JsonProperty("ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [JsonProperty("HomePageDisplayOrder")]
        public int HomePageDisplayOrder { get; set; }

        [JsonProperty("MetaKeywords")]
        public string? MetaKeywords { get; set; }

        [JsonProperty("MetaDescription")]
        public string? MetaDescription { get; set; }

        [JsonProperty("MetaTitle")]
        public string? MetaTitle { get; set; }

        [JsonProperty("AllowCustomerReviews")]
        public bool AllowCustomerReviews { get; set; }

        [JsonProperty("ApprovedRatingSum")]
        public int ApprovedRatingSum { get; set; }

        [JsonProperty("NotApprovedRatingSum")]
        public int NotApprovedRatingSum { get; set; }

        [JsonProperty("ApprovedTotalReviews")]
        public int ApprovedTotalReviews { get; set; }

        [JsonProperty("NotApprovedTotalReviews")]
        public int NotApprovedTotalReviews { get; set; }

        [JsonProperty("SubjectToAcl")]
        public bool SubjectToAcl { get; set; }

        [JsonProperty("LimitedToStores")]
        public bool LimitedToStores { get; set; }

        [JsonProperty("Sku")]
        public string Sku { get; set; }

        [JsonProperty("ManufacturerPartNumber")]
        public string ManufacturerPartNumber { get; set; }

        [JsonProperty("Gtin")]
        public string Gtin { get; set; }

        [JsonProperty("IsGiftCard")]
        public bool IsGiftCard { get; set; }

        [JsonProperty("GiftCardTypeId")]
        public int GiftCardTypeId { get; set; }

        [JsonProperty("RequireOtherProducts")]
        public bool RequireOtherProducts { get; set; }

        [JsonProperty("RequiredProductIds")]
        public string? RequiredProductIds { get; set; }

        [JsonProperty("AutomaticallyAddRequiredProducts")]
        public bool AutomaticallyAddRequiredProducts { get; set; }

        [JsonProperty("IsDownload")]
        public bool IsDownload { get; set; }

        [JsonProperty("UnlimitedDownloads")]
        public bool UnlimitedDownloads { get; set; }

        [JsonProperty("MaxNumberOfDownloads")]
        public int MaxNumberOfDownloads { get; set; }

        [JsonProperty("DownloadExpirationDays")]
        public int? DownloadExpirationDays { get; set; }

        [JsonProperty("DownloadActivationTypeId")]
        public int DownloadActivationTypeId { get; set; }

        [JsonProperty("HasSampleDownload")]
        public bool HasSampleDownload { get; set; }

        [JsonProperty("SampleDownloadId")]
        public int? SampleDownloadId { get; set; }

        [JsonProperty("HasUserAgreement")]
        public bool HasUserAgreement { get; set; }

        [JsonProperty("UserAgreementText")]
        public string? UserAgreementText { get; set; }

        [JsonProperty("IsRecurring")]
        public bool IsRecurring { get; set; }

        [JsonProperty("RecurringCycleLength")]
        public int RecurringCycleLength { get; set; }

        [JsonProperty("RecurringCyclePeriodId")]
        public int RecurringCyclePeriodId { get; set; }

        [JsonProperty("RecurringCyclePeriod")]
        public string RecurringCyclePeriod { get; set; }

        [JsonProperty("RecurringTotalCycles")]
        public int RecurringTotalCycles { get; set; }

        [JsonProperty("IsShippingEnabled")]
        public bool IsShippingEnabled { get; set; }

        [JsonProperty("IsFreeShipping")]
        public bool IsFreeShipping { get; set; }

        [JsonProperty("AdditionalShippingCharge")]
        public double AdditionalShippingCharge { get; set; }

        [JsonProperty("IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [JsonProperty("IsEsd")]
        public bool IsEsd { get; set; }

        [JsonProperty("TaxCategoryId")]
        public int TaxCategoryId { get; set; }

        [JsonProperty("ManageInventoryMethodId")]
        public int ManageInventoryMethodId { get; set; }

        [JsonProperty("StockQuantity")]
        public int StockQuantity { get; set; }

        [JsonProperty("DisplayStockAvailability")]
        public bool DisplayStockAvailability { get; set; }

        [JsonProperty("DisplayStockQuantity")]
        public bool DisplayStockQuantity { get; set; }

        [JsonProperty("MinStockQuantity")]
        public int MinStockQuantity { get; set; }

        [JsonProperty("LowStockActivityId")]
        public int LowStockActivityId { get; set; }

        [JsonProperty("NotifyAdminForQuantityBelow")]
        public int NotifyAdminForQuantityBelow { get; set; }

        [JsonProperty("BackorderModeId")]
        public int BackorderModeId { get; set; }

        [JsonProperty("AllowBackInStockSubscriptions")]
        public bool AllowBackInStockSubscriptions { get; set; }

        [JsonProperty("OrderMinimumQuantity")]
        public int OrderMinimumQuantity { get; set; }

        [JsonProperty("OrderMaximumQuantity")]
        public int OrderMaximumQuantity { get; set; }

        [JsonProperty("QuantityStep")]
        public int QuantityStep { get; set; }

        [JsonProperty("QuantityControlType")]
        public string QuantityControlType { get; set; }

        [JsonProperty("HideQuantityControl")]
        public bool HideQuantityControl { get; set; }

        [JsonProperty("AllowedQuantities")]
        public string? AllowedQuantities { get; set; }

        [JsonProperty("DisableBuyButton")]
        public bool DisableBuyButton { get; set; }

        [JsonProperty("DisableWishlistButton")]
        public bool DisableWishlistButton { get; set; }

        [JsonProperty("AvailableForPreOrder")]
        public bool AvailableForPreOrder { get; set; }

        [JsonProperty("CallForPrice")]
        public bool CallForPrice { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("ComparePrice")]
        public double ComparePrice { get; set; }

        [JsonProperty("ComparePriceLabelId")]
        public int? ComparePriceLabelId { get; set; }

        [JsonProperty("ProductCost")]
        public double ProductCost { get; set; }

        [JsonProperty("SpecialPrice")]
        public double? SpecialPrice { get; set; }

        [JsonProperty("SpecialPriceStartDateTimeUtc")]
        public DateTimeOffset? SpecialPriceStartDateTimeUtc { get; set; }

        [JsonProperty("SpecialPriceEndDateTimeUtc")]
        public DateTimeOffset? SpecialPriceEndDateTimeUtc { get; set; }

        [JsonProperty("CustomerEntersPrice")]
        public bool CustomerEntersPrice { get; set; }

        [JsonProperty("MinimumCustomerEnteredPrice")]
        public double MinimumCustomerEnteredPrice { get; set; }

        [JsonProperty("MaximumCustomerEnteredPrice")]
        public double MaximumCustomerEnteredPrice { get; set; }

        [JsonProperty("HasTierPrices")]
        public bool HasTierPrices { get; set; }

        [JsonProperty("LowestAttributeCombinationPrice")]
        public double? LowestAttributeCombinationPrice { get; set; }

        [JsonProperty("AttributeCombinationRequired")]
        public bool AttributeCombinationRequired { get; set; }

        [JsonProperty("AttributeChoiceBehaviour")]
        public string AttributeChoiceBehaviour { get; set; }

        [JsonProperty("Weight")]
        public double Weight { get; set; }

        [JsonProperty("Length")]
        public double Length { get; set; }

        [JsonProperty("Width")]
        public double Width { get; set; }

        [JsonProperty("Height")]
        public double Height { get; set; }

        [JsonProperty("AvailableStartDateTimeUtc")]
        public DateTimeOffset? AvailableStartDateTimeUtc { get; set; }

        [JsonProperty("AvailableEndDateTimeUtc")]
        public DateTimeOffset? AvailableEndDateTimeUtc { get; set; }

        [JsonProperty("DisplayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("Published")]
        public bool Published { get; set; }

        [JsonProperty("IsSystemProduct")]
        public bool IsSystemProduct { get; set; }

        [JsonProperty("SystemName")]
        public string SystemName { get; set; }

        [JsonProperty("CreatedOnUtc")]
        public DateTimeOffset CreatedOnUtc { get; set; }

        [JsonProperty("UpdatedOnUtc")]
        public DateTimeOffset UpdatedOnUtc { get; set; }

        [JsonProperty("DeliveryTimeId")]
        public int? DeliveryTimeId { get; set; }

        [JsonProperty("QuantityUnitId")]
        public int? QuantityUnitId { get; set; }

        [JsonProperty("CustomsTariffNumber")]
        public string CustomsTariffNumber { get; set; }

        [JsonProperty("CountryOfOriginId")]
        public int CountryOfOriginId { get; set; }

        [JsonProperty("BasePriceEnabled")]
        public bool BasePriceEnabled { get; set; }

        [JsonProperty("BasePriceMeasureUnit")]
        public string? BasePriceMeasureUnit { get; set; }

        [JsonProperty("BasePriceAmount")]
        public double? BasePriceAmount { get; set; }

        [JsonProperty("BasePriceBaseAmount")]
        public double? BasePriceBaseAmount { get; set; }

        [JsonProperty("BundleTitleText")]
        public string? BundleTitleText { get; set; }

        [JsonProperty("BundlePerItemShipping")]
        public bool BundlePerItemShipping { get; set; }

        [JsonProperty("BundlePerItemPricing")]
        public bool BundlePerItemPricing { get; set; }

        [JsonProperty("BundlePerItemShoppingCart")]
        public bool BundlePerItemShoppingCart { get; set; }

        [JsonProperty("MainPictureId")]
        public int? MainPictureId { get; set; }

        [JsonProperty("HasPreviewPicture")]
        public bool HasPreviewPicture { get; set; }

        [JsonProperty("HasDiscountsApplied")]
        public bool HasDiscountsApplied { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("GroupedProductConfiguration")]
        public string? GroupedProductConfiguration { get; set; }


        [JsonIgnore]
        public string ManufacturerCode { get; set; }
        [JsonIgnore]
        public string ManufacturerName { get; set; }
        [JsonIgnore]
        public string Category1 { get; set; }
        [JsonIgnore]
        public string Category2 { get; set; }

    }
}
