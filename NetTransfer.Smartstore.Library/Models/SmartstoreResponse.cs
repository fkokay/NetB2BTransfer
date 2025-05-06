using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class ResponseSmartList<T>
    {
        public List<T> value { get; set; }
        public Error? error { get; set; }
        public bool status { get; set; }
    }

    public class SmartstoreResponse<T>
    {
        public T value { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }

    public class Detail
    {
        public string code { get; set; }
        public string message { get; set; }
        public string target { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<Detail> details { get; set; }
    }
}
