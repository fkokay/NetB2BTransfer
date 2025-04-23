using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Logo.Library.Models
{
    [DataContract]
    public class ExtItemModel
    {

        [DataMember(Name = "LOGICALREF")]
        public int LOGICALREF { get; set; }

        [DataMember(Name = "ITEMREF")]
        public int ITEMREF { get; set; }

        [DataMember(Name = "FIRMNR")]
        public int FIRMNR { get; set; }

        [DataMember(Name = "STOCKPLACES")]
        public string STOCKPLACES { get; set; }

    }
}
