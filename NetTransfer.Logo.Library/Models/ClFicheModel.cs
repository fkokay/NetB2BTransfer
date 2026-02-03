using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Models
{
    [DataContract]
    public class ClFicheModel
    {
        public ClFicheModel()
        {
            GetClfLine = new List<ClfLineModel>();
        }

        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; }
        [DataMember(Name = "FICHENO")] //Fiş No
        public string FICHENO { get; set; }
        [DataMember(Name = "DATE_")]//Fiş Tarihi
        public string DATE_ { get; set; }
        [DataMember(Name = "DOCODE")] //Belge No
        public string DOCODE { get; set; }
        [DataMember(Name = "TRCODE")] //Fiş Türü
        public int TRCODE { get; set; }
        [DataMember(Name = "SPECCODE")] //Özel Kod
        public string SPECCODE { get; set; }
        [DataMember(Name = "CYPHCODE")] //Yetki Kodu
        public string CYPHCODE { get; set; }
        [DataMember(Name = "BRANCH")] //İşyeri
        public int BRANCH { get; set; }
        [DataMember(Name = "DEPARTMENT")] //Bölüm
        public int DEPARTMENT { get; set; }
        [DataMember(Name = "GENEXP1")] //Genel Açıklama1
        public string GENEXP1 { get; set; }
        [DataMember(Name = "GENEXP2")]//Genel Açıklama2
        public string GENEXP2 { get; set; }
        [DataMember(Name = "GENEXP3")]//Genel Açıklama3
        public string GENEXP3 { get; set; }
        [DataMember(Name = "GENEXP4")]//Genel Açıklama4
        public string GENEXP4 { get; set; }
        [DataMember(Name = "DEBIT")] //Borç
        public double DEBIT { get; set; }
        [DataMember(Name = "CREDIT")] //Alacak
        public double CREDIT { get; set; }
        [DataMember(Name = "REPDEBIT")]  //Borç (Raporlama Dövizli)
        public double REPDEBIT { get; set; }
        [DataMember(Name = "REPCREDIT")] //Alacak (Raporlama Dövizli)
        public double REPCREDIT { get; set; }
        [DataMember(Name = "CAPIBLOCK_CREATEDBY")]
        public int CAPIBLOCK_CREATEDBY { get; set; }
        [DataMember(Name = "ACCOUNTED")] //Muhasebeleştirilmiş mi?
        public int ACCOUNTED { get; set; }
        [DataMember(Name = "CASHACCREF")] //Kasa Hesabı Referansı
        public int CASHACCREF { get; set; }
        [DataMember(Name = "CASHCENREF")]
        public int CASHCENREF { get; set; }
        [DataMember(Name = "CANCELLED")]
        public int CANCELLED { get; set; }
        [DataMember(Name = "TRADINGGRP")] //Ticari işlem grubu
        public string TRADINGGRP { get; set; }
        [DataMember(Name = "PROJECTREF")]
        public int PROJECTREF { get; set; }
        [DataMember(Name = "STATUS")]//Durumu
        public int STATUS { get; set; }
        [DataMember(Name = "AFFECTRISK")]
        public int AFFECTRISK { get; set; }
        [DataMember(Name = "APPROVE")] //Onay Kodu
        public int APPROVE { get; set; }
        [DataMember(Name = "APPROVEDATE")] //Onay Tarihi
        public string APPROVEDATE { get; set; }
        [DataMember(Name = "SALESMANREF")] //Satışelemanı referansı.
        public int SALESMANREF { get; set; }
        [DataMember(Name = "SALESMAN_CODE")] //Müşteri Kodu
        public string SALESMAN_CODE { get; set; }
        [DataMember(Name = "DOCDATE")] //Belge tarihi
        public string DOCDATE { get; set; }
        [DataMember(Name = "DEVIR")]
        public int DEVIR { get; set; }
        [DataMember(Name = "CUSTOMERCODE")] //Müşteri Kodu
        public string CUSTOMERCODE { get; set; }
        [DataMember(Name = "BANKACCCODE")] //Banka hesabı kodu
        public string BANKACCCODE { get; set; }
        [DataMember(Name = "FICHETYPETEXT")] //Fiş Türü
        public string FICHETYPETEXT { get; set; }

        [DataMember(Name = "GetClfLine")] //Müşteri hareketleri satırları
        public IList<ClfLineModel> GetClfLine { get; set; }
        //[DataMember(Name = "GetPaymentList")] //Ödeme planı referansı.
        //public IList<cPayment> GetPaymentList { get; set; }
        [DataMember(Name = "TOTALCOUNT")]
        public int TOTALCOUNT { get; set; } //(int, not null)
    }
}
