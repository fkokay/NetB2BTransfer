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
        public short SUBE_KODU { get; set; }
        public short ISLETME_KODU { get; set; }
        public string STOK_KODU { get; set; }
        public string URETICI_KODU { get; set; }
        public string STOK_ADI { get; set; }
        public string GRUP_KODU { get; set; }
        public string KOD_1 { get; set; }
        public string KOD_2 { get; set; }
        public string KOD_3 { get; set; }
        [DataMember(Name = "KOD_3_ISIM")]
        public string KOD_3_ISIM { get; set; }
        public string KOD_4 { get; set; }
        [DataMember(Name = "KOD_4_ISIM")]
        public string KOD_4_ISIM { get; set; }
        public string KOD_5 { get; set; }
        [DataMember(Name = "KOD_5_ISIM")]
        public string KOD_5_ISIM { get; set; }
        public string SATICI_KODU { get; set; }
        public string OLCU_BR1 { get; set; }
        public string OLCU_BR2 { get; set; }
        public double PAY_1 { get; set; }
        public double PAYDA_1 { get; set; }
        public string OLCU_BR3 { get; set; }
        public double PAY2 { get; set; }
        public double PAYDA2 { get; set; }
        public string FIAT_BIRIMI { get; set; }
        public double AZAMI_STOK { get; set; }
        public double ASGARI_STOK { get; set; }
        public double TEMIN_SURESI { get; set; }
        public double KUL_MIK { get; set; }
        public short RISK_SURESI { get; set; }
        public string ZAMAN_BIRIMI { get; set; }
        public double SATIS_FIAT1 { get; set; }
        public double SATIS_FIAT2 { get; set; }
        public double SATIS_FIAT3 { get; set; }
        public double SATIS_FIAT4 { get; set; }
        public double DOV_ALIS_FIAT { get; set; }
        public double DOV_MAL_FIAT { get; set; }
        public double DOV_SATIS_FIAT { get; set; }
        public double BIRIM_AGIRLIK { get; set; }
        public double NAKLIYET_TUT { get; set; }
        public short DEPO_KODU { get; set; }
        public string BILESENMI { get; set; }
        public string MAMULMU { get; set; }
        public double FORMUL_TOPLAMI { get; set; }
        public string UPDATE_KODU { get; set; }
        public double MAX_ISKONTO { get; set; }
        public double ECZACI_KARI { get; set; }
        public double MIKTAR { get; set; }
        public double MAL_FAZLASI { get; set; }
        public double KDV_TENZIL_ORAN { get; set; }
        public string KILIT { get; set; }
        public string ONCEKI_KOD { get; set; }
        public string SONRAKI_KOD { get; set; }
        public string BARKOD1 { get; set; }
        public string BARKOD2 { get; set; }
        public string BARKOD3 { get; set; }
        public string ALIS_KDV_KODU { get; set; }
        public double ALIS_FIAT1 { get; set; }
        public double ALIS_FIAT2 { get; set; }
        public double ALIS_FIAT3 { get; set; }
        public double ALIS_FIAT4 { get; set; }
        public double LOT_SIZE { get; set; }
        public double MIN_SIP_MIKTAR { get; set; }
        public short SABIT_SIP_ARALIK { get; set; }
        public string SIP_POLITIKASI { get; set; }
        public double SIP_VER_MAL { get; set; }
        public double ELDE_BUL_MAL { get; set; }
        public double YIL_TAH_KUL_MIK { get; set; }
        public double EKON_SIP_MIKTAR { get; set; }
        public string ESKI_RECETE { get; set; }
        public string OTOMATIK_URETIM { get; set; }
        public string ALFKOD { get; set; }
        public string SAFKOD { get; set; }
        public string KODTURU { get; set; }
        public string S_YEDEK1 { get; set; }
        public string S_YEDEK2 { get; set; }
        public double F_YEDEK3 { get; set; }
        public double F_YEDEK4 { get; set; }
        public string C_YEDEK5 { get; set; }
        public string C_YEDEK6 { get; set; }
        public short I_YEDEK8 { get; set; }
        public DateTime D_YEDEK10 { get; set; }
        public string GIRIS_SERI { get; set; }
        public string CIKIS_SERI { get; set; }
        public string SERI_BAK { get; set; }
        public string SERI_MIK { get; set; }
        public string SERI_GIR_OT { get; set; }
        public string SERI_CIK_OT { get; set; }
        public string SERI_BASLANGIC { get; set; }
        public string FIYATKODU { get; set; }
        public string PLANLANACAK { get; set; }
        public double LOT_SIZECUSTOMER { get; set; }
        public double MIN_SIP_MIKTARCUSTOMER { get; set; }
        public string GUMRUKTARIFEKODU { get; set; }
        public string ABCKODU { get; set; }
        public string PERFORMANSKODU { get; set; }
        public string SATICISIPKILIT { get; set; }
        public string MUSTERISIPKILIT { get; set; }
        public string SATINALMAKILIT { get; set; }
        public string SATISKILIT { get; set; }
        public double EN { get; set; }
        public double BOY { get; set; }
        public double GENISLIK { get; set; }
        public string SIPLIMITVAR { get; set; }
        public string SONSTOKKODU { get; set; }
        public string ONAYTIPI { get; set; }
        public string FIKTIF_MAM { get; set; }
        public string YAPILANDIR { get; set; }
        public string SBOMVARMI { get; set; }
        public string BAGLISTOKKOD { get; set; }
        public string YAPKOD { get; set; }
        public string ALISTALTEKKILIT { get; set; }
        public string SATISTALTEKKILIT { get; set; }
        public string S_YEDEK3 { get; set; }
        public short STOKMEVZUAT { get; set; }
        public string OTVTEVKIFAT { get; set; }
        public string SERIBARKOD { get; set; }
        public string ATIK_URUN { get; set; }
    }
}
