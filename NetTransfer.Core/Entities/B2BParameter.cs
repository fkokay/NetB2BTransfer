using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    [Table("B2BParameter")]
    public class B2BParameter : BaseEntity
    {
        public int CustomerTransferMinute { get; set; } = 0;
        public string? CustomerFilter { get; set; }
        public DateTime? CustomerLastTransfer { get; set; }
    }
}
