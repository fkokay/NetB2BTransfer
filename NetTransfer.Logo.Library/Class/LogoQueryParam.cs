using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Class
{
    public class LogoQueryParam
    {
        public LogoQueryParam()
        {

        }

        public LogoQueryParam(string orderbyfieldname, string ascdesc)
        {
            limit = "-1";
            offset = "0";
            firmnr = "001";
            periodnr = "01";
            usstocknr = "'-1'";
            orderex = "1";
            SerialNrTracking = false;
            LotTracking = false;
            SerialNrPrint = false;
            this.orderbyfieldname = orderbyfieldname;
            this.ascdesc = ascdesc;

        }

        [DataMember(Name = "datareference")]
        public string datareference { get; set; }

        [DataMember(Name = "offset")]
        public string offset { get; set; }

        [DataMember(Name = "limit")]
        public string limit { get; set; }

        [DataMember(Name = "firmnr")]
        public string firmnr { get; set; }

        [DataMember(Name = "periodnr")]
        public string periodnr { get; set; }

        [DataMember(Name = "userid")]
        public string userid { get; set; }

        [DataMember(Name = "usstocknr")]
        public string usstocknr { get; set; }

        [DataMember(Name = "filter")]
        public string filter { get; set; }
        [DataMember(Name = "linefilter")]
        public string linefilter { get; set; }
        [DataMember(Name = "orderbyfieldname")]
        public string orderbyfieldname { get; set; }
        [DataMember(Name = "ascdesc")]
        public string ascdesc { get; set; }
        [DataMember(Name = "data")]
        public string data { get; set; }
        [DataMember(Name = "begdate")]
        public string begdate { get; set; }
        [DataMember(Name = "enddate")]
        public string enddate { get; set; }
        [DataMember(Name = "exportType")]
        public string exportType { get; set; }
        [DataMember(Name = "coksatilan")]
        public string coksatilan { get; set; }
        [DataMember(Name = "orderex")]
        public string orderex { get; set; }
        [DataMember(Name = "itemex")]
        public string itemex { get; set; }
        [DataMember(Name = "Cargo")]
        public string Cargo { get; set; }
        [DataMember(Name = "PriceType")]
        public string PriceType { get; set; }
        [DataMember(Name = "SerialNrTracking")]
        public bool SerialNrTracking { get; set; }
        [DataMember(Name = "LotTracking")]
        public bool LotTracking { get; set; }

        [DataMember(Name = "SerialNrPrint")]
        public bool SerialNrPrint { get; set; }
        [DataMember(Name = "DbName")]
        public string DbName { get; set; }

        [DataMember(Name = "SendDespatch")]
        public bool SendDespatch { get; set; }

        [DataMember(Name = "DespatchXsltName")]
        public string DespatchXsltName { get; set; }

        [DataMember(Name = "SendEInvoice")]
        public bool SendEInvoice { get; set; }

        [DataMember(Name = "EarchiveXsltName")]
        public string EarchiveXsltName { get; set; }

        [DataMember(Name = "EfaturaXsltName")]
        public string EfaturaXsltName { get; set; }

        [DataMember(Name = "CompanyName")]
        public string CompanyName { get; set; }

        [DataMember(Name = "ServiceUser")]
        public bool ServiceUser { get; set; }

        [DataMember(Name = "Condition")]
        public bool Condition { get; set; }

        [DataMember(Name = "DespatchNumber")]
        public string DespatchNumber { get; set; }

        [DataMember(Name = "DispatchBilling")]
        public bool DispatchBilling { get; set; }


        [DataMember(Name = "UseOrkestraFicheNumber")]
        public bool UseOrkestraFicheNumber { get; set; }


        [DataMember(Name = "EInvoicePrefix")]
        public string EInvoicePrefix { get; set; }

        [DataMember(Name = "EArchivePrefix")]
        public string EArchivePrefix { get; set; }

        [DataMember(Name = "EIntPrefix")]
        public string EIntPrefix { get; set; }


        [DataMember(Name = "ReservedControl")]
        public bool ReservedControl { get; set; }

        [DataMember(Name = "OrficheExt")]
        public bool OrficheExt { get; set; }

        [DataMember(Name = "Subquery")]
        public string Subquery { get; set; }

        [DataMember(Name = "completed")]
        public bool completed { get; set; }

        [DataMember(Name = "prodficheno")]
        public string prodficheno { get; set; }

        [DataMember(Name = "prodamount")]
        public int prodamount { get; set; }

        [DataMember(Name = "Campaign")]
        public bool Campaign { get; set; }

        [DataMember(Name = "UseOtomation")]
        public bool UseOtomation { get; set; }

        [DataMember(Name = "salesmanref")]
        public string salesmanref { get; set; }
    }
}
