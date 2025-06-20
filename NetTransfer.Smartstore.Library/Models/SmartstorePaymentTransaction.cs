using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstorePaymentTransaction
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int BankId { get; set; }
        public string CardPrefix { get; set; }
        public string CardHolderName { get; set; }
        public int Installment { get; set; }
        public int ExtraInstallment { get; set; }
        public double TotalAmount { get; set; }
        public int StatusId { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    }
}
