using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    public class NetsisSetting : BaseEntity
    {
        public string? NumaraSerisi { get; set; }
        public string? DepoKoduCaridenSecilsin { get; set; }
        public string? DepoKodu { get; set; }
        public string? SubeKodu { get; set; }
        public string? CariSubeKoduSecilsin { get; set; }
        public string? CariOtoAcilsin { get; set; }
        public string? CariKodBaslangic { get; set; }
        public string? CariPlasiyerKoduSecilsin { get; set; }
        public string? PlasiyerKodu { get; set; }
        public string? OzelKod1 { get; set; }
        public string? OzelKod2 { get; set; }
        public string? SiparisAciklama { get; set; }
        public string? Aciklama1 { get; set; }
        public string? Aciklama2 { get; set; }
        public string? Aciklama3 { get; set; }
        public string? Aciklama4 { get; set; }
        public string? Aciklama5 { get; set; }
        public string? Aciklama6 { get; set; }
        public string? Aciklama7 { get; set; }
        public string? Aciklama8 { get; set; }
        public string? Aciklama9 { get; set; }
        public string? Aciklama10 { get; set; }
        public string? Aciklama11 { get; set; }
        public string? Aciklama12 { get; set; }
        public string? Aciklama13 { get; set; }
        public string? Aciklama14 { get; set; }
        public string? Aciklama15 { get; set; }
        public string? Aciklama16 { get; set; }
        public string? SiparisAktarimSekli { get; set; }
        public string? IsletmeKodu { get; set; }
        public string? SiparisAktarimDurumKodu { get; set; }
        public string? SiparisAktarildiDurumKodu { get; set; }
        public int? NetsisEsnekYap { get; set; }
        public string? Aciklama { get; set; }
        public string? KdvProjeKodu { get; set; }
        public string? ProjeKodu { get; set; }
    }
}
