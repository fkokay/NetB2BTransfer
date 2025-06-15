using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreAttribute
    {
        public List<Attribute> Attributes { get; set; } = new List<Attribute>();
    }

    public class Attribute
    {
        public string Key { get; set; }
        public List<string> Value { get; set; } = new List<string>();
    }
}
