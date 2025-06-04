using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreTown
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public bool Published { get; set; }
    }
}
