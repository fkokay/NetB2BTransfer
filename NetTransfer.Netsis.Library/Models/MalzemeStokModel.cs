using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    public class MalzemeStokModel
    {
        public string urun_kodu { get; set; }
        public int depo_kodu { get; set; }
        public string depo_baslik { get; set; }
        public int miktar { get; set; }
    }
}
