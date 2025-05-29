using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreOrderItem
    {
        public int Id { get; set; }
        public string OrderItemGuid { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public double UnitPriceInclTax { get; set; }
        public double UnitPriceExclTax { get; set; }
        public double PriceInclTax { get; set; }
        public double PriceExclTax { get; set; }
        public double TaxRate { get; set; }
        public double DiscountAmountInclTax { get; set; }
        public double DiscountAmountExclTax { get; set; }
        public string AttributeDescription { get; set; }
        public string RawAttributes { get; set; }
        public int DownloadCount { get; set; }
        public bool IsDownloadActivated { get; set; }
        public int LicenseDownloadId { get; set; }
        public double ItemWeight { get; set; }
        public string BundleData { get; set; }
        public double ProductCost { get; set; }
        public int? DeliveryTimeId { get; set; }
        public bool DisplayDeliveryTime { get; set; }
    }
}
