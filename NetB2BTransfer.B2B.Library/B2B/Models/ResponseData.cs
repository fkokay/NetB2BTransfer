using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.B2B.Library.B2B.Models
{
    public class ResponseData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public object Validation { get; set; }
        public object Detay { get; set; }
    }
}
