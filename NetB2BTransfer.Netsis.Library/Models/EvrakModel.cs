using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace NetB2BTransfer.Netsis.Library.Models
{
    [DataContract]
    public class EvrakModel
    {
        public int KAYITNO { get; set; } // KAYITNO
        public byte TABLOTIPI { get; set; } // TABLOTIPI
        public string KOD { get; set; } // KOD
        public byte? EVRAKTIPI { get; set; } // EVRAKTIPI
        public string? ACIKLAMA { get; set; } // ACIKLAMA
        public DateTime? KAYITTAR { get; set; } // KAYITTAR
        public DateTime? DUZELTTAR { get; set; } // DUZELTTAR
        public int? KULID { get; set; } // KULID
        public string? DOSYAADI { get; set; } // DOSYAADI
        public int BILGIBOYUT { get; set; } // BILGIBOYUT
        public byte[]? BILGI { get; set; } // BILGI
        public int? OBJECTID { get; set; } // OBJECTID
        public int? EVRAKGRUPID { get; set; } // EVRAKGRUPID
        public short STORAGETYPE { get; set; } // STORAGETYPE
        public string? FILETYPE { get; set; } // FILETYPE
        public string? EVRAKPATH { get; set; } // EVRAKPATH
        public string? EVRAKEDITPATH { get; set; } // EVRAKEDITPATH
        public int? DUZELTMEYAPANKUL { get; set; } // DUZELTMEYAPANKUL
        public DateTime? EVRAKTARIHI { get; set; } // EVRAKTARIHI
        public string? EVRAKGUID { get; set; } // EVRAKGUID
    }
}
