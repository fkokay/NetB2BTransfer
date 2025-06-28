using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Models
{
    public class QueuedEmail
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string ReplyTo { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int SentTries { get; set; }
        public DateTime? SentOnUtc { get; set; }
        public int EmailAccountId { get; set; }
        public bool SendManually { get; set; }


    }
}
