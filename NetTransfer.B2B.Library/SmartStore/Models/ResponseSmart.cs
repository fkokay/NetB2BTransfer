using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.SmartStore.Models
{
    public class ResponseSmartList<T>
    {
        public List<T> value { get; set; }
    }

    public class ResponseSmart<T>
    {
        public T value { get; set; }
    }
}
