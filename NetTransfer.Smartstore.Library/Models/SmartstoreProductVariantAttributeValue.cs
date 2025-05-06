using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreProductVariantAttributeValue
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Attribute { get; set; }
        public int ProductVariantAttributeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int MediaFileId { get; set; }
        public string? Color { get; set; }
        public decimal PriceAdjustment { get; set; }
        public decimal WeightAdjustment { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }
        public int ValueTypeId { get; set; }
        public int LinkedProductId { get; set; }
        public int Quantity { get; set; }
    }
}
