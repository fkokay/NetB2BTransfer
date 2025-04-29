using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class Category
    {
        [JsonProperty("ParentId")]
        public int? ParentId { get; set; }

        [JsonProperty("TreePath")]
        public string TreePath { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("BottomDescription")]
        public string BottomDescription { get; set; }

        [JsonProperty("ExternalLink")]
        public string ExternalLink { get; set; }

        [JsonProperty("BadgeText")]
        public string BadgeText { get; set; }

        [JsonProperty("BadgeStyle")]
        public int BadgeStyle { get; set; }

        [JsonProperty("Alias")]
        public string Alias { get; set; }

        [JsonProperty("CategoryTemplateId")]
        public int CategoryTemplateId { get; set; }

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

        [JsonProperty("ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [JsonProperty("LimitedToStores")]
        public bool LimitedToStores { get; set; }

        [JsonProperty("SubjectToAcl")]
        public bool SubjectToAcl { get; set; }

        [JsonProperty("Published")]
        public bool Published { get; set; }

        [JsonProperty("IgnoreInMenus")]
        public bool IgnoreInMenus { get; set; }

        [JsonProperty("DisplayOrder")]
        public int DisplayOrder { get; set; }

        [JsonProperty("CreatedOnUtc")]
        public DateTimeOffset CreatedOnUtc { get; set; }

        [JsonProperty("UpdatedOnUtc")]
        public DateTimeOffset UpdatedOnUtc { get; set; }

        [JsonProperty("DefaultViewMode")]
        public string DefaultViewMode { get; set; }

        [JsonProperty("HasDiscountsApplied")]
        public bool HasDiscountsApplied { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }
    }
}
