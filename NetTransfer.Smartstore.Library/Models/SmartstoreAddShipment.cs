using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreAddShipment
    {
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string TrackingUrl { get; set; }
        public bool IsShipped { get; set; }
        public bool NotifyCustomer { get; set; }
    }
}
