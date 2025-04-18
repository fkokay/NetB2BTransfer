using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.Entities
{
    [Table("ErpSetting")]
    public class ErpSetting : BaseEntity
    {
        public string Erp { get; set; }
        public DateTime? LastTransferDate { get; set; }
        public bool Active { get; set; }
    }
}
