using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    public class CariModel
    {
        public string musteri_ozellik { get; set; }
        public string unvan { get; set; }
        public string cari_kod { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string telefon { get; set; }
        public string adres { get; set; }
        public string il{ get; set; }
        public string ilce { get; set; }
        public string vergi_dairesi { get; set; }
        public string vergi_no { get; set; }    
        public string tc_no { get; set; }
        public string plasiyer { get; set; }
        public string depo_kodu { get; set; }
        public string erp_kodu { get; set; }
        public string odeme_sekilleri { get; set; }
        public string musteri_kosul_kodu { get; set; }
        public string grup_kodu { get; set; }
        public string fiyat_listesi_kodu { get; set; }
        public string email { get; set; }
        public string kullanici_adi { get; set; }
        public string sifre { get; set; }
        public string email_durum_bildirimi { get; set; }
        public int musteri_durumu { get; set; }
    }
}
