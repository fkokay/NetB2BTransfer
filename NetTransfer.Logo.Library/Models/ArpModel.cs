using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Models
{
    [DataContract]
    public class ArpModel
    {

        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; } //(int, not null)
        [DataMember(Name = "ACTIVE")]
        public int ACTIVE { get; set; } //(int, not null)
        [DataMember(Name = "CARDTYPE")]
        public int CARDTYPE { get; set; } //(int, not null)
        [DataMember(Name = "CODE")]
        public string CODE { get; set; } //(varchar(17), null)
        [DataMember(Name = "DEFINITION_")]
        public string DEFINITION_ { get; set; } //(varchar(201), null)
        [DataMember(Name = "DEFINITION2")]
        public string DEFINITION2 { get; set; } //(varchar(201), null)
        [DataMember(Name = "SPECODE")]
        public string SPECODE { get; set; } //(varchar(11), null)
        [DataMember(Name = "SPECODE2")]
        public string SPECODE2 { get; set; } //(varchar(11), null)
        [DataMember(Name = "SPECODE3")]
        public string SPECODE3 { get; set; } //(varchar(11), null)
        [DataMember(Name = "SPECODE4")]
        public string SPECODE4 { get; set; } //(varchar(11), null)
        [DataMember(Name = "SPECODE5")]
        public string SPECODE5 { get; set; } //(varchar(11), null)
        [DataMember(Name = "CYPHCODE")]
        public string CYPHCODE { get; set; } //(varchar(11), null)
        [DataMember(Name = "ADDR1")]
        public string ADDR1 { get; set; } //(varchar(201), null)
        [DataMember(Name = "ADDR2")]
        public string ADDR2 { get; set; } //(varchar(201), null)
        [DataMember(Name = "DISTRICT")]
        public string DISTRICT { get; set; } //(varchar(51), null)
        [DataMember(Name = "POSTCODE")]
        public string POSTCODE { get; set; } //(varchar(11), null)
        [DataMember(Name = "TOWN")]
        public string TOWN { get; set; } //(varchar(51), null)
        [DataMember(Name = "TOWNCODE")]
        public string TOWNCODE { get; set; } //(varchar(51), null)
        [DataMember(Name = "CITY")]
        public string CITY { get; set; } //(varchar(21), null)
        [DataMember(Name = "CITYCODE")]
        public string CITYCODE { get; set; } //(varchar(21), null)
        [DataMember(Name = "COUNTRY")]
        public string COUNTRY { get; set; } //(varchar(21), null)
        [DataMember(Name = "COUNTRYCODE")]
        public string COUNTRYCODE { get; set; } //(varchar(21), null)
        [DataMember(Name = "TELCODES1")]
        public string TELCODES1 { get; set; } //(varchar(9), null)
        [DataMember(Name = "TELNRS1")]
        public string TELNRS1 { get; set; } //(varchar(51), null)
        [DataMember(Name = "TELCODES2")]
        public string TELCODES2 { get; set; } //(varchar(9), null)
        [DataMember(Name = "TELNRS2")]
        public string TELNRS2 { get; set; } //(varchar(51), null)
        [DataMember(Name = "FAXCODE")]
        public string FAXCODE { get; set; } //(varchar(9), null)
        [DataMember(Name = "FAXNR")]
        public string FAXNR { get; set; } //(varchar(51), null)
        [DataMember(Name = "INCHARGE")]
        public string INCHARGE { get; set; } //(varchar(21), null)
        [DataMember(Name = "TAXOFFICE")]
        public string TAXOFFICE { get; set; } //(varchar(31), null)
        [DataMember(Name = "TAXNR")]
        public string TAXNR { get; set; } //(varchar(16), null)
        [DataMember(Name = "TCKNO")]
        public string TCKNO { get; set; } //(varchar(11), null)
        [DataMember(Name = "EMAILADDR")]
        public string EMAILADDR { get; set; } //(varchar(251), null)
        [DataMember(Name = "EMAILADDR2")]
        public string EMAILADDR2 { get; set; } //(varchar(251), null)
        [DataMember(Name = "EMAILADDR3")]
        public string EMAILADDR3 { get; set; } //(varchar(251), null)
        [DataMember(Name = "WEBADDR")]
        public string WEBADDR { get; set; } //(varchar(41), null)
        [DataMember(Name = "MAPID")]
        public string MAPID { get; set; } //(varchar(33), null)
        [DataMember(Name = "LONGITUDE")]
        public string LONGITUDE { get; set; } //(varchar(41), null)
        [DataMember(Name = "LATITUTE")]
        public string LATITUTE { get; set; } //(varchar(41), null)
        [DataMember(Name = "CITYID")]
        public string CITYID { get; set; } //(varchar(9), null)
        [DataMember(Name = "TOWNID")]
        public string TOWNID { get; set; } //(varchar(18), null)
        [DataMember(Name = "DISCRATE")]
        public double DISCRATE { get; set; } //(float, null)
        [DataMember(Name = "TRADINGGRP")]
        public string TRADINGGRP { get; set; } //(varchar(17), null)
        [DataMember(Name = "DEBIT")]
        public double DEBIT { get; set; } //(float, not null)
        [DataMember(Name = "CREDIT")]
        public double CREDIT { get; set; } //(float, not null)
        [DataMember(Name = "TOTALCOUNT")]
        public int TOTALCOUNT { get; set; } //(int, not null)
        [DataMember(Name = "POSTLABELCODE")]
        public string POSTLABELCODE { get; set; }
        [DataMember(Name = "SENDERLABELCODE")]
        public string SENDERLABELCODE { get; set; }
        [DataMember(Name = "ACCEPTEINV")]
        public int ACCEPTEINV { get; set; } //(int, not null)
        [DataMember(Name = "PROFILEID")]
        public int PROFILEID { get; set; } //(int, not null)
        [DataMember(Name = "PAYMENTREF")]
        public int PAYMENTREF { get; set; } //(int, not null)
        [DataMember(Name = "PAYMENTCODE")]
        public string PAYMENTCODE { get; set; }
        [DataMember(Name = "ISPERSCOMP")]//Cari Türü 1 bireysel 0 kurumsal
        public int ISPERSCOMP { get; set; }
        [DataMember(Name = "EDINO")]
        public string EDINO { get; set; }
        [DataMember(Name = "TELEXTNUMS1")]
        public string TELEXTNUMS1 { get; set; }
        [DataMember(Name = "TELEXTNUMS2")]
        public string TELEXTNUMS2 { get; set; }
        [DataMember(Name = "BALANCE")]
        public double BALANCE { get; set; }
        [DataMember(Name = "SALESMANREF")]
        public int SALESMANREF { get; set; }
        [DataMember(Name = "GROUP")]
        public string GROUP { get; set; }
        [DataMember(Name = "CUSTNAME")]
        public string CUSTNAME { get; set; }
        [DataMember(Name = "CUSTSURNAME")]
        public string CUSTSURNAME { get; set; }
        [DataMember(Name = "ARPSTYPE")]
        public int ARPSTYPE { get; set; }
        [DataMember(Name = "TOTALRISKLIMIT")]
        public double TOTALRISKLIMIT { get; set; }
        [DataMember(Name = "TOTALCLOSEDRISK")]
        public double TOTALCLOSEDRISK { get; set; }
        [DataMember(Name = "TOTALRISK")]
        public double TOTALRISK { get; set; }
        [DataMember(Name = "RISKSTATUS")]
        public double RISKSTATUS { get; set; }
        [DataMember(Name = "POINTTOTAL")]
        public double POINTTOTAL { get; set; }

        [DataMember(Name = "POSTLABELCODEDESP")]
        public string POSTLABELCODEDESP { get; set; }

        [DataMember(Name = "SENDERLABELCODEDESP")]
        public string SENDERLABELCODEDESP { get; set; }

        [DataMember(Name = "ACCEPTEDESP")]
        public int ACCEPTEDESP { get; set; }

        [DataMember(Name = "BANKACCNAME")]
        public string BANKACCNAME { get; set; }

        [DataMember(Name = "BANKACCIBAN")]
        public string BANKACCIBAN { get; set; }

        [DataMember(Name = "GLCODE")]
        public string GLCODE { get; set; }

        [DataMember(Name = "DEFINITION2_")]
        public string DEFINITION2_ { get; set; }
        [DataMember(Name = "CUSTOMERSALESMANMODE")]
        public bool CUSTOMERSALESMANMODE { get; set; }

        [DataMember(Name = "BANKNAME1")]
        public string BANKNAME1 { get; set; }

        [DataMember(Name = "BANKNAME2")]
        public string BANKNAME2 { get; set; }

        [DataMember(Name = "BANKNAME3")]
        public string BANKNAME3 { get; set; }

        [DataMember(Name = "BANKACCIBAN1")]
        public string BANKACCIBAN1 { get; set; }

        [DataMember(Name = "BANKACCIBAN2")]
        public string BANKACCIBAN2 { get; set; }

        [DataMember(Name = "BANKACCIBAN3")]
        public string BANKACCIBAN3 { get; set; }

        [DataMember(Name = "DBSBANKCURRENCY1")]
        public int DBSBANKCURRENCY1 { get; set; }

        [DataMember(Name = "DBSBANKCURRENCY2")]
        public int DBSBANKCURRENCY2 { get; set; }

        [DataMember(Name = "DBSBANKCURRENCY3")]
        public int DBSBANKCURRENCY3 { get; set; }

        [DataMember(Name = "BANKBIC1")]
        public string BANKBIC1 { get; set; }

        [DataMember(Name = "ECOMMID")]
        public string ECOMMID { get; set; }

        [DataMember(Name = "ISFOREIGN")]
        public int ISFOREIGN { get; set; }

        [DataMember(Name = "CURRENCY")]
        public int CURRENCY { get; set; }

        [DataMember(Name = "PROJECTREF")]
        public int PROJECTREF { get; set; }

        [DataMember(Name = "INSTEADOFDISPATCH")]
        public int INSTEADOFDISPATCH { get; set; }

        [DataMember(Name = "GetShipInfo")]
        public IList<ShipInfoModel> ShipInfoList { get; set; } = new List<ShipInfoModel>();

    }
}
