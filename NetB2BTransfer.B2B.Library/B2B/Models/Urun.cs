using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.B2B.Library.B2B.Models
{
    public class Urun
    {
        public Marka marka { get; set; }
        public Grup grup { get; set; }
        public Birim birim { get; set; }
        public string barkod_no { get; set; }
        public string urun_kodu { get; set; }
        public string doviz_kodu { get; set; }
        public string baslik { get; set; }
        public string aciklama { get; set; }
        public string icerik { get; set; }
        public string kdv_durumu { get; set; }
        public double kdv_orani { get; set; }
        public double liste_fiyati { get; set; }
        public int durum { get; set; }
        public string yeni_urun { get; set; }
        public DateTime? yeni_urun_tarih { get; set; }
        public int minimum_satin_alma_miktari { get; set; }
        public int sepete_eklenme_miktari { get; set; }
        public string varyant_durumu { get; set; }
        public string asorti_durumu { get; set; }
        public string model_no { get; set; }
        public Varyant varyant_1 { get; set; }
        public Varyant varyant_2 { get; set; }
        public int asorti_miktar { get; set; }
        public string asorti_kod { get; set; }
        public List<Kategori> kategoriler { get; set; }
    }
}
