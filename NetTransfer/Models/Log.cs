using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Models
{
    public class Log
    {
        public string Source { get; set; }
        public string EventId { get; set; }
        public string EventLevel { get; set; }
        public string EventMessage { get; set; }
        public DateTime EventTime { get; set; }
    }
}
