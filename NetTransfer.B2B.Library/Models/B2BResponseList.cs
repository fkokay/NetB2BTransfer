using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BResponseList<T> where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public object Validation { get; set; }
        public object Detay { get; set; }
        public List<T> List { get; set; }
        public int Toplam_Kayit { get; set; }
    }

    public class B2BResponse2List<T> where T : class
    {
        public bool status { get; set; }
        public string type { get; set; }
        public int code { get; set; }
        public string baslik { get; set; }
        public string message { get; set; }
        public List<T> list { get; set; }
        public Sayfalama sayfalama { get; set; }
    }
    public class Sayfalama
    {
        public int mevcut_sayfa { get; set; }
        public int kayit_sayisi { get; set; }
        public int toplam_kayit { get; set; }
        public int son_sayfa_no { get; set; }
    }

}
