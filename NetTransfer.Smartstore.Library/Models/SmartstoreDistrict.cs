using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreDistrict
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TownId { get; set; }
        public bool Published { get; set; }
    }
}
