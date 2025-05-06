using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Models
{
    
    public class OpakMalzemeResim
    {
        public int SIRA { get; set; }
        public string KOD { get; set; }
        public byte[] RESIM { get; set; }
    }
}
