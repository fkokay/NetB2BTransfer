using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreAddShipment
    {
        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }
        [JsonProperty("trackingUrl")]
        public string TrackingUrl { get; set; }
        [JsonProperty("isShipped")]
        public bool IsShipped { get; set; }
        [JsonProperty("notifyCustomer")]
        public bool NotifyCustomer { get; set; }
    }
}
