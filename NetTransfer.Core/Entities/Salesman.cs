using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    [Table("Salesman")]
    public class Salesman : BaseEntity
    {
        public string SalesmanCode { get; set; }
        public string CashierCode { get; set; }
    }
}
