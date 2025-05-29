using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakSiparisOdeme
    {
        public int ID { get; set; }
        public string ODEMETURU { get; set; }
        public string BANKAKOD { get; set; }
        public double TUTAR { get; set; }
        public int TAKSIT { get; set; }
        public DateTime TARIH { get; set; }
        public string AKTARILDIMI { get; set; }
    }
}
