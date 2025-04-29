using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.B2B.Models
{
    public class Kategori
    {
        public string kod { get; set; }
        public string baslik { get; set; }
        public int sira { get; set; }
        public Kategori child { get; set; }
    }
}
