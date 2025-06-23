using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakMalzemeFiyat
    {
        public string STOKKOD { get; set; }
        public string ANASTOKKOD { get; set; }
        public string STOKTYPE { get; set; }
        public double KDV { get; set; }
        public double FIYAT { get; set; }
        public double TAKSITLIFIYAT { get; set; }
        public string VARYANTLIURUN { get; set; }
        public DateTime GUNCELLEMETARIH { get; set; }
        public DateTime DOVIZGUNCELLEMETARIHI { get; set; }
    }
}
