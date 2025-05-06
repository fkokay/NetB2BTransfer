using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreProductVariantAttributeCombination
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public string Variant { get; set; }
        [JsonIgnore]
        public string Value { get; set; }
        public string Sku { get; set; }
        public string Gtin { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public decimal? Price { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? BasePriceAmount { get; set; }
        public int? BasePriceBaseAmount { get; set; }
        public string AssignedMediaFileIds { get; set; }
        public bool IsActive { get; set; } = true;
        public int? DeliveryTimeId { get; set; }
        public int? QuantityUnitId { get; set; }
        public string RawAttributes { get; set; }
        public int StockQuantity { get; set; }
        public bool AllowOutOfStockOrders { get; set; }
        public int HashCode { get; set; }
    }
}
