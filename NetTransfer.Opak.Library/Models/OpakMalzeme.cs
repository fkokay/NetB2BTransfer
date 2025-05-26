using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakMalzeme
    {
        public string STOK_KODU { get; set; }
        public string STOK_ADI { get; set; }
        public string KOD1 { get; set; }
        public string KOD2 { get; set; }
        public string KOD3 { get; set; }
        public string MARKA { get; set; }
        public double KDV { get; set; }
        public string AKTIF { get; set; }
        public string ANASAYFA { get; set; }
        public string ACIKLAMA { get; set; }
        public double SATIS_FIAT1 { get; set; }
        public double SATIS_FIAT2 { get; set; }
        public int DESI { get; set; }
        public string KISAACIKLAMA { get; set; }
        public double MIKTAR { get; set; }
        public int VARYANTLIURUN { get; set; }
        public string STOKMIKTAR { get; set; }

        public List<OpakVaryant> MalzemeVaryantList = new List<OpakVaryant>();
        public List<OpakMalzemeResim> MalzemeResimList = new List<OpakMalzemeResim>();

    }
}
