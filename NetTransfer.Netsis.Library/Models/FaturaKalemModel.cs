using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    public class FaturaKalemModel
    {
        public string STOK_KODU { get; set; } // STOK KODU
        public int DEPO_KODU { get; set; } //DEPO KODU
        public double STRA_GCMIK { get; set; } //MİKTAR
        public double STRA_NF { get; set; } // BİRİM NET FİYATI
        public double STRA_BF { get; set; } // BİRİM BRÜT FİYATI
        public string SATIR_BAZI_ACIKLAMA { get; set; } //SATIR AÇIKLAMA
    }
}
