using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BOdeme
    {
        public string musteri_erp_kodu { get; set; }
        public string odeme_no { get; set; }
        public string odeme_notu { get; set; }
        public string odeme_sistem { get; set; }
        public string doviz_kodu { get; set; }
        public string doviz_kuru { get; set; }
        public string komisyon_orani { get; set; }
        public string odeme_turu { get; set; }
        public string islem_tarihi { get; set; }
        public string hesaba_islenecek_tutar { get; set; }
        public string takist_orani { get; set; }
        public string cekilecek_tutar { get; set; }
        public string banka_komisyon_orani { get; set; }
        public string durum_mesaj { get; set; }
    }
}
