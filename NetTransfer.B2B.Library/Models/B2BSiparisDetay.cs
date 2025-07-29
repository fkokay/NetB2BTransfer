using System;
using System.Collections.Generic;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BResponseSiparisDetay : B2BResponse
    {
        
        public B2BSiparisUstBilgiler ust_bilgiler { get; set; }
        public List<B2BSSiparisToplam> siparis_toplamlar { get; set; }
        public List<B2BSiparisKalem> siparis_kalemler { get; set; }
        public List<B2BSiparisDurum> siparis_durumlari { get; set; }
    }

    public class B2BSiparisUstBilgiler
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
        public string siparis_aciklama { get; set; }
        public string aciklama_1 { get; set; }
        public string aciklama_2 { get; set; }
        public string aciklama_3 { get; set; }
        public string aciklama_4 { get; set; }
        public string aciklama_5 { get; set; }
        public string aciklama_6 { get; set; }
        public string aciklama_7 { get; set; }
        public string aciklama_8 { get; set; }
        public string aciklama_9 { get; set; }
        public string aciklama_10 { get; set; }
        public string aciklama_11 { get; set; }
        public string aciklama_12 { get; set; }
        public string aciklama_13 { get; set; }
        public string aciklama_14 { get; set; }
        public string aciklama_15 { get; set; }
        public string aciklama_16 { get; set; }
        public string plasiyer { get; set; }
    }

    public class B2BSSiparisToplam
    {
        public int siparis_toplam_id { get; set; }
        public int siparis_id { get; set; }
        public string toplam_tipi { get; set; }
        public int toplam_tipi_id { get; set; }
        public string toplam_tipi_aciklama { get; set; }
        public double tutar { get; set; }
        public int? sira { get; set; }
        public string para_birimi { get; set; }
    }

    public class B2BSiparisKalem
    {
        public int siparis_hareket_id { get; set; }
        public int siparis_id { get; set; }
        public int urun_id { get; set; }
        public string urun_kodu { get; set; }
        public double miktar { get; set; }
        public double liste_fiyat { get; set; }
        public double iskonto_1 { get; set; }
        public double iskonto_2 { get; set; }
        public double iskonto_3 { get; set; }
        public double iskonto_4 { get; set; }
        public double iskonto_5 { get; set; }
        public double iskonto_6 { get; set; }
        public double net_fiyat { get; set; }
        public double kdv_oran { get; set; }
        public double kdv_dahil_fiyat { get; set; }
        public double tutar { get; set; }
        public double net_tutar { get; set; }
        public double kdv_dahil_tutar { get; set; }
        public double kur { get; set; }
        public string urun_adi { get; set; }
        public string resim { get; set; }
        public string siparis_kalem_bg_renk { get; set; }
        public string siparis_kalem_yazi_renk_kodu { get; set; }
        public double kalan_miktar { get; set; }
        public string doviz_kodu { get; set; }
    }

    public class B2BSiparisDurum
    {
        public int id { get; set; }
        public int durum_id { get; set; }
        public string durum { get; set; }
        public string durum_aciklamasi { get; set; }
        public int siparis_id { get; set; }
        public string email_bildirim { get; set; }
        public string sms_bildirim { get; set; }
        public string uygulama_bildirimi { get; set; }
        public DateTime kayit_tarihi { get; set; }
        public string bg_renk_kodu { get; set; }
        public string yazi_renk_kodu { get; set; }
    }
}
