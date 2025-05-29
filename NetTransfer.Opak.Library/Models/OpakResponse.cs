using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakResponse
    {
        public int HataType { get; set; }
        public int HataNo { get; set; }
        public string HataBaslik { get; set; }
        public string HataMesaj { get; set; }
        public bool Hata { get; set; }
        public object ValueTable { get; set; }
        public decimal ValueDecimal { get; set; }
        public int ValueInt { get; set; }
        public DateTime ValueDateTime { get; set; }
        public string ValueString { get; set; }
        public string ValueJSonString { get; set; }
        public object ValueObject { get; set; }
    }
}
