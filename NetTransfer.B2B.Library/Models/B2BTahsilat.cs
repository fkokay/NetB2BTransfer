using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BTahsilat
    {
        public int id { get; set; }
        public int plasiyer_id { get; set; }
        public string plasiyer_adi { get; set; }
        public string plasiyer_kod { get; set; }
        public string plasiyer_erp_kodu { get; set; }
        public int musteri_id { get; set; }
        public string musteri_adi { get; set; }
        public string musteri_cari_kod { get; set; }
        public string musteri_erp_kodu { get; set; }
        public object virman_musteri { get; set; }
        public object virman_musteri_cari_kod { get; set; }
        public object virman_musteri_erp_kodu { get; set; }
        public string tahsilat_tipi { get; set; }
        public double tutar { get; set; }
        public string doviz_kodu { get; set; }
        public string not { get; set; }
        public string kayit_tarihi { get; set; }
        public int durum { get; set; }
        public string durum_text { get; set; }
        public string durum_tarihi { get; set; }
        public int admin_kullanici_id { get; set; }
        public string islem_yapan_kullanici { get; set; }
        public object cek_no { get; set; }
        public object vade_tarihi { get; set; }
        public object vade_tarihi_formatted { get; set; }
        public object banka_adi { get; set; }
        public object banka_subesi { get; set; }
        public object hesap_no { get; set; }
        public object ciro_eden { get; set; }
        public object ciro_yeri { get; set; }
        public object ciro_tarihi { get; set; }
        public object senet_no { get; set; }
        public object sehir { get; set; }
        public object adres { get; set; }
        public object kefil { get; set; }
        public bool entegrasyon_aktarim_durum { get; set; }
        public object entegrasyon_aktarim_tarihi { get; set; }
    }
}
