using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BSiparis
    {
        public string erp_kodu { get; set; }
        public int siparis_id { get; set; }
        public int musteri_id { get; set; }
        public DateTime siparis_tarih { get; set; }
        public string fatura_musteri_adi { get; set; }
        public string fatura_unvan { get; set; }
        public string fatura_vkn { get; set; }
        public string fatura_vergi_dairesi { get; set; }
        public string fatura_email { get; set; }
        public string fatura_adres { get; set; }
        public string fatura_il { get; set; }
        public string fatura_ilce { get; set; }
        public string fatura_ulke { get; set; }
        public string fatura_tel { get; set; }
        public string teslimat_unvan { get; set; }
        public string teslimat_musteri_adi { get; set; }
        public string teslimat_adres { get; set; }
        public string teslimat_il { get; set; }
        public string teslimat_ilce { get; set; }
        public string teslimat_tel { get; set; }
        public string teslimat_email { get; set; }
        public int teslimat_sekli_id { get; set; }
        public string teslimat_sekli { get; set; }
        public string siparis_notu { get; set; }
        public int kullanici_id { get; set; }
        public int odeme_sekli_id { get; set; }
        public string odeme_sekli { get; set; }
        public int durum_id { get; set; }
        public string durum { get; set; }
        public DateTime durum_tarihi { get; set; }
        public string durum_icerik { get; set; }
        public string odeme_no { get; set; }
        public string bg_renk_kodu { get; set; }
        public string yazi_renk_kodu { get; set; }
        public decimal toplam_tutar { get; set; }
    }
}
