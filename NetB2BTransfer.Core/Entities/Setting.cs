using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.Entities
{
    [Table("Setting")]
    public class Setting : BaseEntity
    {
        public string Erp { get; set; }
        public DateTime? LastTransferDate { get; set; }
        public bool Active { get; set; }
    }
}
