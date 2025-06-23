using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Models
{
    public class BaseMalzemeStokModel
    {
        public string StokType { get; set; }
        public string StokKodu { get; set; }
        public string DepoAdi { get; set; }
        public int StokMiktari { get; set; }
    }
}
