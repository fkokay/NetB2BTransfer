using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Class
{
    public static class OpakQuery
    {
        public static string GetMalzemeQuery(bool sync = true, string filter = "")
        {
            string query = $"SELECT * FROM VOW_STOKLAR_ozgurtek WHERE 1=1";
            if (sync)
            {
                query += " AND STOK_KODU IN (SELECT STOK_KODU FROM [TBL_B2C_STOKSYNC] WHERE DURUM=0 AND TIP=0)";

            }
            return query + filter;
        }

        public static string GetPasifMalzemeQuery(bool sync = true)
        {
            string query = $"SELECT * FROM VOW_PASIF_STOKLAR_ozgurtek WHERE 1=1";
            if (sync)
            {
                query += " AND STOK_KODU IN (SELECT STOK_KODU FROM [TBL_B2C_STOKSYNC] WHERE DURUM=0 AND TIP=0)";
            }

            return query;
        }

        public static string GetMalzemeStokQuery(DateTime? guncellemeTarihi)
        {
            if (guncellemeTarihi == null)
            {
                return $"SELECT * FROM [VOW_STOKBAKIYEENTEGRASYON_ozgurtek]";
            }
            else
            {
                return $"SELECT * FROM [VOW_STOKBAKIYEENTEGRASYON_ozgurtek] WHERE GUNCELLEMETARIH > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
            }

        }

        public static string GetMalzemeResimQuery(string stok_kodu)
        {
            return @$"SELECT * FROM VOW_B2CSTOKRESIMSB_ozgurtek WHERE KOD='{stok_kodu}' AND SIRA > 0";
        }

        public static string GetVaryantResimQuery(string stok_kodu)
        {
            return @$"SELECT * FROM VOW_B2CSTOKRESIMSB_ozgurtek WHERE KOD='{stok_kodu}'";
        }

        public static string GetMalzemeVaryantQuery(string stok_kodu)
        {
            return @$"SELECT * FROM VOW_STOKDETAYNATIVE_ozgurtek WHERE STOKKOD='{stok_kodu}' ORDER BY SIRA";
        }

        public static string GetMalzemeFiyatQuery(bool sync = true)
        {
            string query = $"SELECT * FROM VOW_STOKFIYATENTEGRASYON_ozgurtek WHERE 1=1";
            if (sync)
            {
                query += " AND STOKKOD IN (SELECT STOK_KODU FROM [TBL_B2C_STOKSYNC] WHERE DURUM=0 AND TIP=1)";
            }
            
            return query;
        }

        public static string GetVaryantFiyatQuery(DateTime? guncellemeTarihi)
        {
            if (guncellemeTarihi == null)
            {
                return $"SELECT * FROM [VOW_STOKFIYATENTEGRASYON_ozgurtek] WHERE STOKTYPE='V'";
            }
            else
            {
                return $"SELECT * FROM [VOW_STOKFIYATENTEGRASYON_ozgurtek] WHERE STOKTYPE='V' AND ENSONGUNCELLEME > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
            }

        }

        public static string GetSevkiyatQuery(DateTime? guncellemeTarihi)
        {
            if (guncellemeTarihi.HasValue)
            {
                return $"SELECT * FROM VOW_SIPARISKARGOFIRMAVEBARKOD_ozgurtek WHERE FATURASONDEGISIKLIKTARIHI > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
            }
            else
            {
                return $"SELECT * FROM VOW_SIPARISKARGOFIRMAVEBARKOD_ozgurtek";
            }

        }

        public static string GetSyncQuery(int tip, int durum = 0)
        {
            return $"SELECT * FROM [TBL_B2C_STOKSYNC] WHERE DURUM = {durum} AND TIP={tip}";
        }
        public static string GetSyncCountQuery(int tip, int durum = 0)
        {
            return $"SELECT ISNULL(COUNT(ID),0) FROM [TBL_B2C_STOKSYNC] WHERE DURUM = {durum} AND TIP={tip}";
        }

        public static string SetSyncStatus(string stok_kodu, int tip, int durum)
        {
            return $"UPDATE [TBL_B2C_STOKSYNC] SET DURUM={durum},DURUM_TARIHI=GETDATE() WHERE STOK_KODU='{stok_kodu}' AND TIP={tip}";
        }
    }
}
