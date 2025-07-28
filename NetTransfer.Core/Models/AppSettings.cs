using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Models
{
    public class AppSettings
    {
        public string ErpType { get; set; }
        public string ConnectionString { get; set; }
        public string CommerceType { get; set; }
        public string ApiUrl { get; set; }
    }
}
