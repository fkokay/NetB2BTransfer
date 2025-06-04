using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakSevkiyat
    {
        public int SIPARISID { get; set; }
        public string SIPARISGUID { get; set; }
        public string SIPARISNO { get; set; }
        public int FATURAID { get; set; }
        public string FATURANO { get; set; }
        public DateTime FATURATARIHI { get; set; }
        public DateTime FATURASONDEGISIKLIKTARIHI { get; set; }
        public string KARGOFIRMASI { get; set; }
        public string KARGOBARKODU { get; set; }
    }
}
