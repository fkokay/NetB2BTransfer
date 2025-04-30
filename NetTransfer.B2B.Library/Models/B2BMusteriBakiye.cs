using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BMusteriBakiye
    {
        public string cari_kod { get; set; }
        public string doviz_kodu { get; set; }
        public double bakiye { get; set; }
        public double gecikmis_gun { get; set; }
        public double gecikmis_bakiye { get; set; }
        public string borc_alacak_tipi { get; set; }
    }
}
