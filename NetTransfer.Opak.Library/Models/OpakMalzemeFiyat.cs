using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakMalzemeFiyat
    {
        public string KOD { get; set; }
        public double KDV { get; set; }
        public double SFIYAT1 { get; set; }
        public double SFIYAT2 { get; set; }
        public double SFIYAT3 { get; set; }
        public double SFIYAT4 { get; set; }
        public int VARYANTLIURUN { get; set; }
        public double VARYANTFIYAT { get; set; }
        public DateTime GUNCELLEMETARIH { get; set; }
    }
}
