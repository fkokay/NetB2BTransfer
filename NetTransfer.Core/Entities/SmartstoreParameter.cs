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
        public bool ProductSync { get; set; } = true;
        public bool ProductImageSync { get; set; } = true;

        public int ProductStockTransferMinute { get; set; } = 0;
        public string? ProductStockFilter { get; set; }
        public bool ProductStockSync { get; set; } = true;

        public int ProductPriceTransferMinute { get; set; } = 0;
        public string? ProductPriceFilter { get; set; }
        public bool ProductPriceSync{ get; set; } = true;

        public int OrderTransferMinute { get; set; } = 0;
        public string OrderStatusId { get; set; } = "10,20";
        public int OrderShipmentMinute { get; set; } = 0;
        public DateTime? OrderShipmentLastTransfer { get; set; }
    }
}
