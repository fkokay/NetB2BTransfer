using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.SmartStore.Models
{
    public class ProductManufacturer
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int ProductId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }   
    }
}
