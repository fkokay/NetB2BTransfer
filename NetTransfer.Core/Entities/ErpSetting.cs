using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    [Table("ErpSetting")]
    public class ErpSetting : BaseEntity
    {
        public string Erp { get; set; }
        public string? SqlServer { get; set; }
        public string? SqlUser { get; set; }
        public string? SqlPassword { get; set; }
        public string? SqlDatabase { get; set; }
        public string? RestUrl { get; set; }
        public string? ErpUser { get; set; }
        public string? ErpPassword { get; set; }
        public DateTime? LastTransferDate { get; set; }
        public bool Active { get; set; }
    }
}
