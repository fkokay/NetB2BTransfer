using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakMalzemeStok
    {
        public string STOKKOD { get; set; }
        public string STOKTYPE { get; set; }
        public string DEPOADI { get; set; }
        public double BAKIYE { get; set; }
        public DateTime? GUNCELLEMETARIH { get; set; }
    }
}
