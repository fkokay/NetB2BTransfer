using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Mikro.Library.Models
{
    public class MikroMalzemeStokModel
    {
        public string urun_kodu { get; set; }
        public int depo_kodu { get; set; }
        public string depo_baslik { get; set; }
        public int miktar { get; set; }
    }
}
