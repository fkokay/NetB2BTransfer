using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.Entities
{
    [Table("B2BSetting")]
    public class B2BSetting : BaseEntity
    {
        public string Ip { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
