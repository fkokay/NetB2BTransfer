using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreOrder
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public string OrderGuid { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public string PaymentMethodSystemName { get; set; }
        public string CustomerCurrencyCode { get; set; }
        public double CurrencyRate { get; set; }
        public string VatNumber { get; set; }
        public double OrderSubtotalInclTax { get; set; }
        public double OrderSubtotalExclTax { get; set; }
        public double OrderSubTotalDiscountInclTax { get; set; }
        public double OrderSubTotalDiscountExclTax { get; set; }
        public double OrderShippingInclTax { get; set; }
        public double OrderShippingExclTax { get; set; }
        public double OrderShippingTaxRate { get; set; }
        public double PaymentMethodAdditionalFeeInclTax { get; set; }
        public double PaymentMethodAdditionalFeeExclTax { get; set; }
        public double PaymentMethodAdditionalFeeTaxRate { get; set; }
        public string TaxRates { get; set; }
        public double OrderTax { get; set; }
        public double OrderDiscount { get; set; }
        public double CreditBalance { get; set; }
        public double OrderTotalRounding { get; set; }
        public double OrderTotal { get; set; }
        public double RefundedAmount { get; set; }
        public bool RewardPointsWereAdded { get; set; }
        public string CheckoutAttributeDescription { get; set; }
        public string RawAttributes { get; set; }
        public int CustomerLanguageId { get; set; }
        public int AffiliateId { get; set; }
        public string CustomerIp { get; set; }
        public bool AllowStoringCreditCardNumber { get; set; }
        public string CardType { get; set; }
        public string CustomerOrderComment { get; set; }
        public string? AuthorizationTransactionId { get; set; }
        public string AuthorizationTransactionCode { get; set; }
        public string AuthorizationTransactionResult { get; set; }
        public string CaptureTransactionId { get; set; }
        public string CaptureTransactionResult { get; set; }
        public string SubscriptionTransactionId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime? PaidDateUtc { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingRateComputationMethodSystemName { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int? RewardPointsRemaining { get; set; }
        public bool HasNewPaymentNotification { get; set; }
        public bool AcceptThirdPartyEmailHandOver { get; set; }
        public int OrderStatusId { get; set; }
        public int PaymentStatusId { get; set; }
        public int ShippingStatusId { get; set; }
        public int CustomerTaxDisplayTypeId { get; set; }

        [JsonIgnore]
        public SmartstoreCustomer OrderCustomer { get; set; }
        [JsonIgnore]
        public List<SmartstoreOrderItem> OrderItems { get; set; }
        [JsonIgnore]
        public SmartstoreBillingAddress BillingAddress { get; set; }
        [JsonIgnore]
        public SmartstoreShippingAddress ShippingAddress { get; set; }
   
    }
}
