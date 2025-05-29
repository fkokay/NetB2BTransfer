using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakSiparisKalem
    {
        public int ID { get; set; }
        public int SIPID { get; set; }
        public string USTUUID { get; set; }
        public string KALEMUID { get; set; }
        public string STOKKOD { get; set; }
        public string STOKADI { get; set; }
        public double KDVORANI { get; set; }
        public int MIKTAR { get; set; }
        public double BRUTFIYAT { get; set; }
        public double ISK1 { get; set; }
        public double ISK2 { get; set; }
        public double ISK3 { get; set; }
        public double ISK4 { get; set; }
        public double ISK5 { get; set; }
        public double ISK6 { get; set; }
        public double NETFIYAT { get; set; }
        public string BIRIM { get; set; }
        public string DOVIZADI { get; set; }
        public DateTime TARIH { get; set; }
        public int KUR { get; set; }
        public string ACIKLAMA1 { get; set; }
    }
}
