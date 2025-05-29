using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakSiparisParam
    {
        public int ID { get; set; }
        public string ACIKLAMA { get; set; }
        public string DEGER { get; set; }
        public bool ZORUNLU { get; set; }
        public int TIP { get; set; }
        public string UUID { get; set; }
    }
}
