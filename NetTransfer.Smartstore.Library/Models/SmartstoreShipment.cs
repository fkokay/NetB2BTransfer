using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreShipment
    {
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string TrackingUrl { get; set; }
        public double TotalWeight { get; set; }
        public DateTime ShippedDateUtc { get; set; }
        public DateTime? DeliveryDateUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int Id { get; set; }
    }
}
