using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BUrunFiyat
    {
        public string kod { get; set; }
        public string baslik { get; set; }
        public string aciklama { get; set; }
        public int tarih_aralik_durum { get; set; }
        public DateTime baslangic_tarihi { get; set; }
        public DateTime bitis_tarihi { get; set; }
        public bool durum { get; set; }
        public List<B2BUrun> urunler { get; set; }
    }
}
