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
    }

    public class SmartstoreResponse<T>
    {
        public T value { get; set; }
    }
}
