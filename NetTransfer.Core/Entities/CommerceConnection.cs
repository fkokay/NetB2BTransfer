using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    public class CommerceConnection : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ApiBaseUrl { get; set; }
        public string ApiUsername { get; set; }
        public string ApiPassword { get; set; }
    }
}
