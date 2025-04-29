using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Logo.Library.Models
{
    [DataContract]
    public class ItemModel
    {
        public ItemModel()
        {

        }

        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; } //(int, not null)

        [DataMember(Name = "CODE")]
        public string CODE { get; set; } //(varchar(25), null)

        [DataMember(Name = "NAME")]
        public string NAME { get; set; } //(varchar(51), null)

        [DataMember(Name = "NAME2")]
        public string NAME2 { get; set; } //(varchar(51), null)

        [DataMember(Name = "NAME3")]
        public string NAME3 { get; set; } //(varchar(51), null)

        [DataMember(Name = "NAME4")]
        public string NAME4 { get; set; } //(varchar(51), null)

        [DataMember(Name = "STGRPCODE")]
        public string STGRPCODE { get; set; }
        [DataMember(Name = "STGRPNAME")]
        public string STGRPNAME { get; set; }

        [DataMember(Name = "MARKCODE")]
        public string MARKCODE { get; set; } //(varchar(25), null)
        [DataMember(Name = "MARKNAME")]
        public string MARKNAME { get; set; } //(varchar(25), null)

        [DataMember(Name = "CYPHCODE")]
        public string CYPHCODE { get; set; } //(varchar(11), null)

        [DataMember(Name = "SPECODE")]
        public string SPECODE { get; set; } //(varchar(11), null)

        [DataMember(Name = "SPECODENAME")]
        public string SPECODENAME { get; set; } //(varchar(11), null)

        [DataMember(Name = "SPECODE2")]
        public string SPECODE2 { get; set; } //(varchar(11), null)

        [DataMember(Name = "SPECODE3")]
        public string SPECODE3 { get; set; } //(varchar(11), null)

        [DataMember(Name = "SPECODE4")]
        public string SPECODE4 { get; set; } //(varchar(11), null)

        [DataMember(Name = "SPECODE5")]
        public string SPECODE5 { get; set; } //(varchar(11), null)

        [DataMember(Name = "VAT")]
        public double VAT { get; set; } //(float, null)

        [DataMember(Name = "UNITSETREF")]
        public int UNITSETREF { get; set; } //(varchar(11), null)

        [DataMember(Name = "UOMREF")]
        public int UOMREF { get; set; } //(varchar(11), null)

        [DataMember(Name = "UNITCODE")]
        public string UNITCODE { get; set; } //(varchar(11), null)

        [DataMember(Name = "GNTOTSTOCK")]
        public double GNTOTSTOCK { get; set; } //(float, not null)

        [DataMember(Name = "USTOTSTOCK")]
        public double USTOTSTOCK { get; set; } //(float, not null)

        [DataMember(Name = "PRICE")]
        public double PRICE { get; set; } //(float, not null)

        [DataMember(Name = "PURCHASEPRICE")]
        public double PURCHASEPRICE { get; set; } //(float, not null)

        [DataMember(Name = "DISCOUNTPRICE")]
        public double DISCOUNTPRICE { get; set; } //(float, not null)

        [DataMember(Name = "PR_CURR")]
        public string PR_CURR { get; set; } //(varchar(6), not null)

        [DataMember(Name = "TOTALCOUNT")]
        public int TOTALCOUNT { get; set; } //(int, not null)

        [DataMember(Name = "KEYWORD1")]
        public string KEYWORD1 { get; set; } //(varchar(11), null)

        [DataMember(Name = "KEYWORD2")]
        public string KEYWORD2 { get; set; } //(varchar(11), null)

        [DataMember(Name = "KEYWORD3")]
        public string KEYWORD3 { get; set; } //(varchar(11), null)

        [DataMember(Name = "KEYWORD4")]
        public string KEYWORD4 { get; set; } //(varchar(11), null)

        [DataMember(Name = "KEYWORD5")]
        public string KEYWORD5 { get; set; } //(varchar(11), null)

        [DataMember(Name = "BARCODE")]
        public string BARCODE { get; set; } //(varchar(11), null)

        [DataMember(Name = "SELLVAT")]
        public double SELLVAT { get; set; }

        [DataMember(Name = "AMOUNT")]
        public double AMOUNT { get; set; }

        [DataMember(Name = "DISCOUNTRATE")]
        public double DISCOUNTRATE { get; set; }

        [DataMember(Name = "PRODUCERCODE")]
        public string PRODUCERCODE { get; set; }

        [DataMember(Name = "ISONR")]
        public string ISONR { get; set; }

        [DataMember(Name = "CURRTYPE")]
        public string CURRTYPE { get; set; }

        [DataMember(Name = "CANCONFIGURE")]
        public int CANCONFIGURE { get; set; }

        [DataMember(Name = "SERISTOCK")]
        public int SERISTOCK { get; set; }

        [DataMember(Name = "EXTITEM")]
        public ExtItemModel? EXTITEM { get; set; }

        [DataMember(Name = "IMAGE")]
        public byte[] IMAGE { get; set; }

        [DataMember(Name = "STATUS")]
        public string STATUS { get; set; }

        [DataMember(Name = "DIFFSTOCKAMOUNT")]
        public int DIFFSTOCKAMOUNT { get; set; }

        [DataMember(Name = "UINFO1")]
        public double UINFO1 { get; set; }

        [DataMember(Name = "UINFO2")]
        public double UINFO2 { get; set; }

        [DataMember(Name = "DEDUCTCODE")]
        public string DEDUCTCODE { get; set; }

        [DataMember(Name = "CANDEDUCT")]
        public bool CANDEDUCT { get; set; }

        [DataMember(Name = "SALEDEDUCTPART1")]
        public int SALEDEDUCTPART1 { get; set; }

        [DataMember(Name = "SALEDEDUCTPART2")]
        public int SALEDEDUCTPART2 { get; set; }

        [DataMember(Name = "TRACKTYPE")]
        public int TRACKTYPE { get; set; }

        [DataMember(Name = "LOCTRACKING")]
        public int LOCTRACKING { get; set; }

        [DataMember(Name = "EXTACCESSFLAGS")]
        public int EXTACCESSFLAGS { get; set; }

        
    }
}
