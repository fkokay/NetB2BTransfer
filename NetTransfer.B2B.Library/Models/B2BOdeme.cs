using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BOdeme
    {
        public int id { get; set; }
        public int musteri_id { get; set; }
        public string odeme_no { get; set; }
        public object adi { get; set; }
        public object soyadi { get; set; }
        public string unvan { get; set; }
        public string odeme_tipi { get; set; }
        public string durum { get; set; }
        public string durum_mesaj { get; set; }
        public string? odeme_notu { get; set; }
        public string odeme_sistem { get; set; }
        public string doviz_kodu { get; set; }
        public string doviz_kuru { get; set; }
        public string islem_tarihi { get; set; }
        public object onaylanma_tarihi { get; set; }
        public string kart_no { get; set; }
        public string kart_sahibi { get; set; }
        public string komisyon_tutari { get; set; }
        public string banka_taksit_text { get; set; }
        public string komisyon_orani { get; set; }
        public string taksit_aylik_odeme { get; set; }
        public string odeme_turu { get; set; }
        public string ip { get; set; }
        public object basarisiz_tarihi { get; set; }
        public string kart_tipi { get; set; }
        public double hesaba_islenecek_tutar { get; set; }
        public string banka_komisyon_orani { get; set; }
        public int odeme_taksit_id { get; set; }
        public string cekilecek_tutar { get; set; }
        public int ui_kullanici_id { get; set; }
        public int taksit_orani { get; set; }
        public string kart_turu { get; set; }
        public string banka { get; set; }
        public string email { get; set; }
        public object kart_tipleri { get; set; }
        public object paytr_merchant_id { get; set; }
        public object paytr_hash_kod { get; set; }
        public object vakifbank_Xid { get; set; }
        public object vakifbank_VerifyEnrollmentRequestId { get; set; }
        public object vakifbank_api_url { get; set; }
        public object vakifbank_odeme_url { get; set; }
        public object vakifbank_MerchantId { get; set; }
        public object vakifbank_MerchantPassword { get; set; }
        public object vakifbank_TerminalNo { get; set; }
        public object kart_bilgileri { get; set; }
        public int erp_aktarma_durumu { get; set; }
        public object erp_aktarma_durumu_son_tarih { get; set; }
        public object yapikredi_order_id { get; set; }
        public object yapikredi_MERCHANT_ID { get; set; }
        public object yapikredi_TERMINAL_ID { get; set; }
        public object yapikredi_ENCKEY { get; set; }
        public object yapikredi_POSTNET_XML_URL { get; set; }
        public object yapikredi_POSNET_ID { get; set; }
        public object albaraka_order_id { get; set; }
        public object albaraka_MD { get; set; }
        public object albaraka_SecureTransactionId { get; set; }
        public object albaraka_kart_bilgileri { get; set; }
        public object albaraka_SALE_URL { get; set; }
        public object albaraka_MerchantNo { get; set; }
        public object albaraka_TerminalNo { get; set; }
        public object albaraka_MAC { get; set; }
        public object albaraka_ENCKEY { get; set; }
        public object albaraka_AuthCode { get; set; }
        public object albaraka_ReferenceCode { get; set; }
        public object siparis_no { get; set; }
        public object parampos_Islem_ID { get; set; }
        public object parampos_GUID { get; set; }
        public object parampos_CLIENT_CODE { get; set; }
        public int plasiyer_id { get; set; }
        public string islem_tipi { get; set; }
        public object telefon { get; set; }
        public string vergi_dairesi { get; set; }
        public object vergi_no { get; set; }
        public object tc_no { get; set; }
        public string musteri_ozellik { get; set; }
        public string fim_ApiUrl { get; set; }
        public string ApiUser { get; set; }
        public string ApiPassword { get; set; }
        public string ApiClient { get; set; }
        public string musteri_erp_kodu { get; set; }
    }
}
