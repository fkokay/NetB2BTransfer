using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    [Table("VirtualStoreSetting")]
    public class VirtualStoreSetting : BaseEntity
    {
        public string VirtualStore { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
