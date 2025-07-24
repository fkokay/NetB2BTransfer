using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    [DataContract]
    public class MalzemeModel
    {
        public string marka_kod { get; set; }
        public string marka_adi { get; set; }
        public string grup_kod { get; set; }
        public string grup_baslik { get; set; }
        public string birim_kodu { get; set; }
        public string birim_baslik { get; set; }
        public string barkod_no { get; set; }
        public string urun_kodu { get; set; }
        public string doviz_kodu { get; set; }
        public string baslik { get; set; }
        public string aciklama { get; set; }
        public string icerik { get; set; }
        public string kdv_durumu { get; set; }
        public double kdv_orani { get; set; }
        public double liste_fiyati { get; set; }
        public bool durum { get; set; }
        public bool yeni_urun { get; set; }
        public DateTime yeni_urun_tarih { get; set; }
        public int minimum_satin_alma_miktari { get; set; }
        public int sepete_eklenme_miktari { get; set; }
        public bool varyant_durumu { get; set; }
        public string asorti_durumu { get; set; }
        public string model_no { get; set; }
        public string varyant_1_kod { get; set; }
        public string varyant_1_baslik { get; set; }
        public string varyant_1_deger { get; set; }
        public string varyant_1_deger_baslik { get; set; }
        public string varyant_2_kod { get; set; }
        public string varyant_2_baslik { get; set; }
        public string varyant_2_deger { get; set; }
        public string varyant_2_deger_baslik { get; set; }
        public int asorti_miktar { get; set; }
        public string asorti_kod { get; set; }
        public string kategori_kod_1 { get; set; }
        public string kategori_baslik_1 { get; set; }
        public string kategori_kod_2 { get; set; }
        public string kategori_baslik_2 { get; set; }
        public string kategori_kod_3 { get; set; }
        public string kategori_baslik_3 { get; set; }
        public string kategori_kod_4 { get; set; }
        public string kategori_baslik_4 { get; set; }
        public string resim_yolu { get; set; }

        public List<EvrakModel> EvrakList { get; set; } = new List<EvrakModel>();
    }
}
