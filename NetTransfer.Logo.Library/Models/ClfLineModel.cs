using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Models
{
    [DataContract]
    public class ClfLineModel
    {
        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; }
        [DataMember(Name = "CLIENTREF")]//Müşteri Referansı.
        public int CLIENTREF { get; set; }
        [DataMember(Name = "CLACCREF")] //Cari hesap muhasebe hesabı referansı.
        public int CLACCREF { get; set; }
        [DataMember(Name = "CLCENTERREF")]
        public int CLCENTERREF { get; set; }
        [DataMember(Name = "CASHCENTERREF")]
        public int CASHCENTERREF { get; set; }
        [DataMember(Name = "CASHACCOUNTREF")]
        public int CASHACCOUNTREF { get; set; }
        [DataMember(Name = "VIRMANREF")]
        public int VIRMANREF { get; set; }
        [DataMember(Name = "SOURCEFREF")] //Kaynak fiş referansı.
        public int SOURCEFREF { get; set; }
        [DataMember(Name = "DATE_")]//Fiş Tarihi
        public string DATE_ { get; set; }
        [DataMember(Name = "DEPARTMENT")] //Bölüm
        public int DEPARTMENT { get; set; }
        [DataMember(Name = "BRANCH")]//İşyeri
        public int BRANCH { get; set; }
        [DataMember(Name = "MODULENR")] //Modül Nr
        public int MODULENR { get; set; }
        [DataMember(Name = "TRCODE")] //İşlem türü
        public int TRCODE { get; set; }
        [DataMember(Name = "LINENR")] //Satır numarası
        public int LINENR { get; set; }
        [DataMember(Name = "SPECODE")] //Özel kodu
        public string SPECODE { get; set; }
        [DataMember(Name = "CYPHCODE")]//Yetki kodu
        public string CYPHCODE { get; set; }
        [DataMember(Name = "TRANNO")]  //İşlem no
        public string TRANNO { get; set; }
        [DataMember(Name = "DOCODE")] //Belge numarası
        public string DOCODE { get; set; }
        [DataMember(Name = "LINEEXP")] //Satır Açıklaması
        public string LINEEXP { get; set; }
        [DataMember(Name = "ACCOUNTED")]
        public int ACCOUNTED { get; set; }
        [DataMember(Name = "SIGN")] //Borç(0) Alacak(1) 
        public int SIGN { get; set; }
        [DataMember(Name = "AMOUNT")] //Miktar
        public double AMOUNT { get; set; }
        [DataMember(Name = "TRCURR")] //İşlem döviz türü
        public int TRCURR { get; set; }
        [DataMember(Name = "TRRATE")] //İşlem Türü oranı
        public double TRRATE { get; set; }
        [DataMember(Name = "TRNET")] //İşlem tutarı net 
        public double TRNET { get; set; }
        [DataMember(Name = "REPORTRATE")]
        public double REPORTRATE { get; set; }
        [DataMember(Name = "REPORTNET")]
        public double REPORTNET { get; set; }
        [DataMember(Name = "PAYDEFREF")] //Ödeme plannı referansı.
        public int PAYDEFREF { get; set; }
        [DataMember(Name = "ACCFICHEREF")]//Hesap fiş referansı.
        public int ACCFICHEREF { get; set; }
        [DataMember(Name = "CAPIBLOCK_CREATEDBY")]
        public int CAPIBLOCK_CREATEDBY { get; set; }
        [DataMember(Name = "CANCELLED")] //İtal Edilmiş.
        public int CANCELLED { get; set; }
        [DataMember(Name = "TRADINGGRP")] //ticari işlem grubu
        public string TRADINGGRP { get; set; }
        [DataMember(Name = "CASHAMOUNT")]
        public double CASHAMOUNT { get; set; }
        [DataMember(Name = "PAYMENTREF")]
        public int PAYMENTREF { get; set; }
        [DataMember(Name = "RECSTATUS")]
        public int RECSTATUS { get; set; }
        [DataMember(Name = "CREDITCNO")]//Kredi Kart no
        public string CREDITCNO { get; set; }
        [DataMember(Name = "CLPRJREF")]
        public int CLPRJREF { get; set; }
        [DataMember(Name = "STATUS")]
        public int STATUS { get; set; }
        [DataMember(Name = "MONTH_")]
        public int MONTH_ { get; set; }
        [DataMember(Name = "YEAR_")]
        public int YEAR_ { get; set; }
        [DataMember(Name = "AFFECTRISK")]
        public int AFFECTRISK { get; set; }
        [DataMember(Name = "BATCHNR")]
        public int BATCHNR { get; set; }
        [DataMember(Name = "APPROVENR")] //Onay Kodu
        public int APPROVENR { get; set; }
        [DataMember(Name = "APPROVENUM")]
        public string APPROVENUM { get; set; }
        [DataMember(Name = "SALESMANREF")]
        public int SALESMANREF { get; set; }
        [DataMember(Name = "BANKACCREF")]
        public int BANKACCREF { get; set; }
        [DataMember(Name = "BNACCREF")]
        public int BNACCREF { get; set; }
        [DataMember(Name = "BNCENTERREF")]
        public int BNCENTERREF { get; set; }
        [DataMember(Name = "DEVIR")]
        public int DEVIR { get; set; }
        [DataMember(Name = "DEVIRMODULENR")]
        public int DEVIRMODULENR { get; set; }
        [DataMember(Name = "FTIME")] //Saat
        public int FTIME { get; set; }
        [DataMember(Name = "PAIDINCASH")]
        public int PAIDINCASH { get; set; }
        [DataMember(Name = "NETAMOUNT")]//Net tutar
        public double NETAMOUNT { get; set; }
        [DataMember(Name = "CUSTOMERCODE")] //Müşteri Kodu
        public string CUSTOMERCODE { get; set; }
        [DataMember(Name = "CUSTOMERNAME")] //Müşteri Kodu
        public string CUSTOMERNAME { get; set; }
        [DataMember(Name = "BANKACCCODE")] //Banka hesabı kodu
        public string BANKACCCODE { get; set; }
        [DataMember(Name = "DEBIT")] //Borç
        public double DEBIT { get; set; }
        [DataMember(Name = "CREDIT")] //Alacak
        public double CREDIT { get; set; }
        [DataMember(Name = "BANKACCODE")] //Alacak
        public string BANKACCODE { get; set; }
        [DataMember(Name = "BANKACCNAME")] //Alacak
        public string BANKACCNAME { get; set; }


        [DataMember(Name = "SALESMANCODE")] //Alacak
        public string SALESMANCODE { get; set; }
    }
}
