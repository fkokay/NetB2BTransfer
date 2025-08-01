﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreUpdateProduct
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ShortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("FullDescription")]
        public string FullDescription { get; set; }
        //[JsonProperty("StockQuantity")]
        //public int StockQuantity { get; set; }
        //[JsonProperty("Price")]
        //public double Price { get; set; }
        [JsonProperty("ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }
        [JsonProperty("Weight")]
        public double Weight { get; set; }
        [JsonProperty("ManageInventoryMethodId")]
        public int ManageInventoryMethodId { get; set; }
        [JsonProperty("Published")]
        public bool Published { get; set; }
        [JsonProperty("UpdatedOnUtc")]
        public DateTimeOffset UpdatedOnUtc { get; set; }

    }
}
