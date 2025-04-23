using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Netsis.Library.Class
{
    public static class NetsisQuery
    {
        public static string GetMalzemeQuery()
        {
            return @"SELECT 
            ISNULL((SELECT dbo.TRK(GRUP_ISIM) FROM TBLSTOKKOD3 WHERE GRUP_KOD=TBLSTSABIT.KOD_3),'') AS KOD_3_ISIM,
            ISNULL((SELECT dbo.TRK(GRUP_ISIM) FROM TBLSTOKKOD4 WHERE GRUP_KOD=TBLSTSABIT.KOD_4),'') AS KOD_4_ISIM,
            ISNULL((SELECT dbo.TRK(GRUP_ISIM) FROM TBLSTOKKOD5 WHERE GRUP_KOD=TBLSTSABIT.KOD_5),'') AS KOD_5_ISIM,
            * FROM TBLSTSABIT WHERE 1=1 AND TBLSTSABIT.KOD_3 IS NOT NULL AND TBLSTSABIT.KOD_4 IS NOT NULL AND TBLSTSABIT.KOD_5 IS NOT NULL";
        }
    }
}
