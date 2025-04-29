using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    public class FaturaModel
    {
        public string FATIRS_NO { get; set; }//SİPARİŞ KODU
        public string CARI_KODU { get; set; }//CARİ KODU
        public DateTime TARIH { get; set; }//SİPARİŞ TARİHİ
        public string PLA_KODU { get; set; }//PLASİYER KODU
        public string PROJE_KODU { get; set; } // PROJE KODU
        public bool KDV_DAHILMI { get; set; } = true; //KDV DAHİL Mİ

        public List<FaturaKalemModel> FATURA_KALEMLER { get; set; } = new List<FaturaKalemModel>();
    }
}
