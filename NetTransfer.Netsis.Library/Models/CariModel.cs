using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    public class CariModel
    {
        public string CARI_KOD { get; set; } // CARİ KODU
        public string CARI_ISIM { get; set; } // CARİ UNVANI
        public string CARI_TIP { get; set; } = "S"; // CARİ TİP 
        public string VERGI_DAIRESI { get; set; } // CARİ VERGİ DAİRESİ
        public string VERGI_NUMARASI { get; set; } // CARİ VERGİ NO
        public string TCKIMLIK_NO { get; set; } // CARİ TC KİMLİK NO
        public string EMAIL { get; set; } // CARİ EMAIL
        public string CARI_ADRES { get; set; } //CARİ ADRES
        public string CARI_TEL { get; set; } // CARİ TELEFON NO  
    }
}
