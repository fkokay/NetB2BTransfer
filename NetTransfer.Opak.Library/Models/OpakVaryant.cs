using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakVaryant
    {
        public string STOKKOD { get; set; }
        public string KOD { get; set; }
        public string OZELLIK { get; set; }
        public string DEGER { get; set; }
        public double FIYAT1 { get; set; }
        public double FIYAT2 { get; set; }
        public double FIYAT3 { get; set; }
        public double FIYAT4 { get; set; }
        public double FIYAT5 { get; set; }
        public int MIKTAR { get; set; }
        public int SIRA { get; set; }
        public int BIRIMAGIRLIK { get; set; }

        public List<OpakMalzemeResim> MalzemeResimList = new List<OpakMalzemeResim>();
    }
}
