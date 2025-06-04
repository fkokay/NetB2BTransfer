using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    [Table("SmartstoreParameter")]
    public class SmartstoreParameter : BaseEntity
    {
        public int ProductTransferMinute { get; set; } = 0;
        public string? ProductFilter { get; set; }
        public DateTime? ProductLastTransfer { get; set; }

        public int ProductStockTransferMinute { get; set; } = 0;
        public string? ProductStockFilter { get; set; }
        public DateTime? ProductStockLastTransfer { get; set; }

        public int ProductPriceTransferMinute { get; set; } = 0;
        public string? ProductPriceFilter { get; set; }
        public DateTime? ProductPriceLastTransfer { get; set; }

        public int OrderTransferMinute { get; set; } = 0;
        public int OrderStatusId { get; set; } = 10;
        public int OrderShipmentMinute { get; set; } = 0;
        public DateTime? OrderShipmentLastTransfer { get; set; }
    }
}
