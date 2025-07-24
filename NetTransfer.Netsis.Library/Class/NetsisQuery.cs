using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Class
{
    public static class NetsisQuery
    {
        public static string GetMalzemeQuery()
        {
            return @"SELECT * FROM OZGUR_B2B_STOKKARTLARI";
        }

        public static string GetMalzemeStokQuery()
        {
            return @"SELECT STOK_KODU,(TOP_GIRIS_MIK-TOP_CIKIS_MIK) AS STOK_MIKTARI FROM TBLSTOKPH WHERE DEPO_KODU='0'";
        }

        public static string GetMalzemeFiyatQuery()
        {
            return @"SELECT STOK_KODU,ISNULL(SATIS_FIAT3,0) AS SATIS_FIAT3,ISNULL(SATIS_FIAT4,0) AS SATIS_FIAT4 FROM TBLSTSABIT WHERE 1=1 AND TBLSTSABIT.KOD_3 IS NOT NULL AND TBLSTSABIT.KOD_4 IS NOT NULL AND TBLSTSABIT.KOD_5 IS NOT NULL";
        }

        public static string GetEvrakQuery(string stok_kodu)
        {
            return @$"SELECT * FROM TBLEVRAK WHERE TABLOTIPI=1 AND KOD='{stok_kodu}' ";
        }
    }
}
