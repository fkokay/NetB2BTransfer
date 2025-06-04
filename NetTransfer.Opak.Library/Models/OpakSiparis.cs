using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    public class OpakSiparis
    {
        public int ID { get; set; }
        public int SUBEID { get; set; } = 0;
        public int DEPOID { get; set; } = 0;
        public string CARIKOD { get; set; } = "H01";
        public string CARIADI { get; set; }
        public string ALTHESAP { get; set; }
        public string BELGENO { get; set; }
        public string TARIH { get; set; }
        public string SAAT { get; set; }
        public string ACIKLAMA1 { get; set; }
        public string ACIKLAMA2 { get; set; }
        public string ACIKLAMA3 { get; set; }
        public string ACIKLAMA4 { get; set; }
        public string ACIKLAMA5 { get; set; }
        public int VADEGUNU { get; set; }
        public string? VADETARIHI { get; set; }
        public string KDVDAHIL { get; set; }
        public double KUR { get; set; }
        public double ALTISKORAN { get; set; }
        public double ALTISKTUTAR { get; set; }
        public string AKTARILDIMI { get; set; }
        public string UUID { get; set; }
        public int ISLEMTIPI { get; set; }
        public string PLASIYERKOD { get; set; }
        public string? TESLIMTARIHI { get; set; }
        public string ADSOYAD { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public string CEPTEL { get; set; }
        public string MAIL { get; set; }
        public string ADRES { get; set; }
        public string ILCE { get; set; }
        public string IL { get; set; }
        public string VERGIDAIRESI { get; set; }
        public string VERGINO { get; set; }
        public string TCNO { get; set; }
        public double KARGOBEDELI { get; set; }
        public List<OpakSiparisKalem> STOKLISTESI { get; set; } = new List<OpakSiparisKalem>();
        public List<OpakSiparisParam> SIPARISEKPARAM { get; set; } = new List<OpakSiparisParam>();
        public List<OpakSiparisOdeme> SIPARISODEME { get; set; } = new List<OpakSiparisOdeme>();
        public int KALEMSAYISI { get; set; }
    }
}
