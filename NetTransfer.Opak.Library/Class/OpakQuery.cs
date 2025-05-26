using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Class
{
    public static class OpakQuery
    {
        public static string GetMalzemeQuery()
        {
            return "SELECT * FROM VOW_STOKLAR_ozgurtek WHERE AKTIF='E'";
        }

        public static string GetMalzemeResimQuery(string stok_kodu)
        {
            return @$"SELECT * FROM VOW_B2CSTOKRESIMSB_ozgurtek WHERE KOD='{stok_kodu}' AND SIRA > 0 ";
        }

        public static string GetMalzemeVaryantQuery(string stok_kodu)
        {
            return @$"SELECT * FROM VOW_STOKDETAYNATIVE_ozgurtek WHERE STOKKOD='{stok_kodu}' ORDER BY SIRA";
        }
    }
}
