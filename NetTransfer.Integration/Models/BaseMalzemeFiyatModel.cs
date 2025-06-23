using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Models
{
    public class BaseMalzemeFiyatModel
    {
        public string StokType { get; set; }
        public string StokKodu { get; set; }
        public double Fiyat { get; set; }
        public double IndirimliFiyat { get; set; }
    }
}
