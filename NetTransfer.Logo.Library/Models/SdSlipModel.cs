using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Models
{
    [DataContract]
    public class SdSlipModel
    {
        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; }
        [DataMember(Name = "DATE_")]//Tarih
        public string DATE_ { get; set; }
        [DataMember(Name = "CUSTTITLE")]//Kasa açıklaması
        public string CUSTTITLE { get; set; }
        [DataMember(Name = "LINEEXP")]
        public string LINEEXP { get; set; }
        [DataMember(Name = "AMOUNT")]//Miktar
        public double AMOUNT { get; set; }
        [DataMember(Name = "ACCOUNTED")]
        public int ACCOUNTED { get; set; }
        [DataMember(Name = "CANCELLED")]
        public int CANCELLED { get; set; }
        [DataMember(Name = "AFFECTRISK")]
        public int AFFECTRISK { get; set; }
        [DataMember(Name = "STATUS")]
        public int STATUS { get; set; }
        [DataMember(Name = "TRANNO")]//İşlem No
        public string TRANNO { get; set; }
        [DataMember(Name = "CUSTOMERCODE")]//Müşteri Kodu
        public string CUSTOMERCODE { get; set; }
        [DataMember(Name = "KSCARDCODE")] //Kasa Kodu
        public string KSCARDCODE { get; set; }
        [DataMember(Name = "DOCNUMBER")]//Ödeme Tipi
        public string DOCNUMBER { get; set; }
        [DataMember(Name = "SALESMANCODE")]//Ödeme Tipi
        public string SALESMANCODE { get; set; }
        [DataMember(Name = "DOCDATE")]//Tarih
        public string DOCDATE { get; set; }
        [DataMember(Name = "SPECODE")]//Tarih
        public string SPECODE { get; set; }
        [DataMember(Name = "FICHENO ")]
        public string FICHENO { get; set; }
        [DataMember(Name = "TRANGRPNO ")]
        public string TRANGRPNO { get; set; }
        [DataMember(Name = "DOCODE ")]
        public string DOCODE { get; set; }
        [DataMember(Name = "FICHETYPETEXT ")]
        public string FICHETYPETEXT { get; set; }

        [DataMember(Name = "TRCODE ")]
        public int TRCODE { get; set; }

        [DataMember(Name = "CLCARDCODE ")]
        public string CLCARDCODE { get; set; }

        [DataMember(Name = "CLCARDDEFINITION ")]
        public string CLCARDDEFINITION { get; set; }

        [DataMember(Name = "SIGN ")]
        public int SIGN { get; set; }

        [DataMember(Name = "TYPE ")]
        public int TYPE { get; set; }
    }
}
