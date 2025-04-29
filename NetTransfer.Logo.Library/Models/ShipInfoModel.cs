using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Models
{
    [DataContract]
    public class ShipInfoModel
    {
        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; }

        [DataMember(Name = "CLIENTREF")]
        public int CLIENTREF { get; set; }

        [DataMember(Name = "CUSTOMERCODE")]
        public string CUSTOMERCODE { get; set; }

        [DataMember(Name = "CODE")]
        public string CODE { get; set; }

        [DataMember(Name = "NAME")]
        public string NAME { get; set; }

        [DataMember(Name = "SPECODE")]
        public string SPECODE { get; set; }

        [DataMember(Name = "CYPHCODE")]
        public string CYPHCODE { get; set; }

        [DataMember(Name = "ADDR1")]
        public string ADDR1 { get; set; }

        [DataMember(Name = "ADDR2")]
        public string ADDR2 { get; set; }

        [DataMember(Name = "CITY")]
        public string CITY { get; set; }

        [DataMember(Name = "COUNTRY")]
        public string COUNTRY { get; set; }

        [DataMember(Name = "POSTCODE")]
        public string POSTCODE { get; set; }

        [DataMember(Name = "TELNRS1")]
        public string TELNRS1 { get; set; }

        [DataMember(Name = "TELNRS2")]
        public string TELNRS2 { get; set; }

        [DataMember(Name = "FAXNR")]
        public string FAXNR { get; set; }

        [DataMember(Name = "TRADINGGRP")]
        public string TRADINGGRP { get; set; }

        [DataMember(Name = "VATNR")]
        public string VATNR { get; set; }

        [DataMember(Name = "TAXNR")]
        public string TAXNR { get; set; }

        [DataMember(Name = "TAXOFFICE")]
        public string TAXOFFICE { get; set; }

        [DataMember(Name = "TOWNCODE")]
        public string TOWNCODE { get; set; }

        [DataMember(Name = "TOWN")]
        public string TOWN { get; set; }

        [DataMember(Name = "DISTRICTCODE")]
        public string DISTRICTCODE { get; set; }

        [DataMember(Name = "DISTRICT")]
        public string DISTRICT { get; set; }

        [DataMember(Name = "CITYCODE")]
        public string CITYCODE { get; set; }

        [DataMember(Name = "COUNTRYCODE")]
        public string COUNTRYCODE { get; set; }

        [DataMember(Name = "ACTIVE")]
        public int ACTIVE { get; set; }

        [DataMember(Name = "TEXTINC")]
        public int TEXTINC { get; set; }

        [DataMember(Name = "EMAILADDR")]
        public string EMAILADDR { get; set; }

        [DataMember(Name = "INCHANGE")]
        public string INCHANGE { get; set; }

        [DataMember(Name = "TELCODES1")]
        public string TELCODES1 { get; set; }

        [DataMember(Name = "TELCODES2")]
        public string TELCODES2 { get; set; }

        [DataMember(Name = "FAXCODE")]
        public string FAXCODE { get; set; }

        [DataMember(Name = "LONGITUDE")]
        public string LONGITUDE { get; set; }

        [DataMember(Name = "LATITUTE")]
        public string LATITUTE { get; set; }

        [DataMember(Name = "CITYID")]
        public string CITYID { get; set; }

        [DataMember(Name = "TOWNID")]
        public string TOWNID { get; set; }

        [DataMember(Name = "POSTLABELCODE")]
        public string POSTLABELCODE { get; set; }

        [DataMember(Name = "SENDERLABELCODE")]
        public string SENDERLABELCODE { get; set; }

        [DataMember(Name = "TITLE")]
        public string TITLE { get; set; }
    }
}
