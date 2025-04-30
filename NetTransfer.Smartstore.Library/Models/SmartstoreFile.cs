using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreFile
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}
