using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Class
{
    public static class NetsisUtils
    {
        public static string Cevir(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            value = value.Replace("İ", "Ý");
            value = value.Replace("Ş", "Þ");
            value = value.Replace("Ğ", "Ð");
            value = value.Replace("ş", "þ");
            value = value.Replace("ğ", "ð");
            return value;
        }

        public static string CevirNetsis(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            value = value.Replace("Ý", "İ");
            value = value.Replace("Þ", "Ş");
            value = value.Replace("Ð", "Ğ");
            value = value.Replace("ý", "i");
            value = value.Replace("þ", "ş");
            value = value.Replace("ð", "ğ");

            return value;
        }
    }
}
