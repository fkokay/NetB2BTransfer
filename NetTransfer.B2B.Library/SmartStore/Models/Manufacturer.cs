using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.SmartStore.Models
{
    public class Manufacturer
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("BottomDescription")]
        public string BottomDescription { get; set; }

        [JsonProperty("ManufacturerTemplateId")]
        public int ManufacturerTemplateId { get; set; }

        [JsonProperty("MetaKeywords")]
        public string MetaKeywords { get; set; }

        [JsonProperty("MetaDescription")]
        public string MetaDescription { get; set; }

        [JsonProperty("MetaTitle")]
        public string MetaTitle { get; set; }

        [JsonProperty("MediaFileId")]
        public int? MediaFileId { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("AllowCustomersToSelectPageSize")]
        public bool AllowCustomersToSelectPageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }

        [JsonProperty("LimitedToStores")]
        public bool LimitedToStores { get; set; }

        [JsonProperty("SubjectToAcl")]
        public bool SubjectToAcl { get; set; }

        [JsonProperty("Published")]
        public bool Published { get; set; }

        [JsonProperty("DisplayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("CreatedOnUtc")]
        public DateTimeOffset CreatedOnUtc { get; set; }

        [JsonProperty("UpdatedOnUtc")]
        public DateTimeOffset UpdatedOnUtc { get; set; }

        [JsonProperty("HasDiscountsApplied")]
        public bool HasDiscountsApplied { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }
    }
}
