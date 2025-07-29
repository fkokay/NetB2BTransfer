using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    public class ErpDovizTip : BaseEntity
    {
        public string? EticaretDovizKodu { get; set; }
        public string? Eticaret { get; set; }
        public string? Erp { get; set; }
        public string? ErpDovizKodu { get; set; }
    }
}
