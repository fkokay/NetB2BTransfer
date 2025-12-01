using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Class
{
    public static class NetsisQuery
    {
        public static string GetCariQuery()
        {
            return @"SELECT * FROM OZGUR_B2B_CARIKARTLARI";
        }
        public static string GetCariBakiyeQuery()
        {
            return @"SELECT * FROM OZGUR_B2B_CARIBAKIYELERI";
        }
        public static string GetMalzemeQuery()
        {
            return @"SELECT * FROM OZGUR_B2B_STOKKARTLARI";
        }

        public static string GetMalzemeStokQuery()
        {
            return @"SELECT * FROM OZGUR_B2B_DEPOBAKIYELER";
        }

        public static string GetMalzemeFiyatQuery()
        {
            return @"SELECT * FROM OZGUR_B2B_FIYATLISTELERI";
        }

        public static string GetEvrakQuery(string stok_kodu)
        {
            return @$"SELECT * FROM TBLEVRAK WHERE TABLOTIPI=1 AND KOD='{stok_kodu}' ";
        }
    }
}
