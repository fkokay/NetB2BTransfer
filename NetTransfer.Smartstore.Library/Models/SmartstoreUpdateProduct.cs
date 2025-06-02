using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreUpdateProduct
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ShortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("StockQuantity")]
        public int StockQuantity { get; set; }
        [JsonProperty("Price")]
        public double Price { get; set; }
        [JsonProperty("SpecialPrice")]
        public double? SpecialPrice { get; set; }
        [JsonProperty("IsShipEnabled")]
        public bool IsShipEnabled { get; set; }
        [JsonProperty("IsFreeShipping")]
        public bool IsFreeShipping { get; set; }
        [JsonProperty("ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [JsonProperty("HomePageDisplayOrder")]
        public int HomePageDisplayOrder { get; set; }

    }
}
