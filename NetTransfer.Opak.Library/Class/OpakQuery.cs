using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Opak.Library.Class
{
    public static class OpakQuery
    {
        public static string GetMalzemeQuery(string? aktif,DateTime? guncellemeTarihi)
        {
            if (string.IsNullOrEmpty(aktif))
            {
                if (guncellemeTarihi.HasValue)
                {
                    return $"SELECT * FROM VOW_STOKLAR_ozgurtek WHERE BIRIMAGIRLIK > 0";
                    return $"SELECT * FROM VOW_STOKLAR_ozgurtek WHERE GUNCELLEMETARIH > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
                }
                else
                {
                    return $"SELECT * FROM VOW_STOKLAR_ozgurtek";
                }
            }
            else
            {
                if (guncellemeTarihi.HasValue)
                {
                    return $"SELECT * FROM VOW_STOKLAR_ozgurtek WHERE AKTIF='{aktif}' AND GUNCELLEMETARIH > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
                }
                else
                {
                    return $"SELECT * FROM VOW_STOKLAR_ozgurtek WHERE AKTIF='{aktif}'";
                }
            }
        }

        public static string GetMalzemeStokQuery(DateTime? guncellemeTarihi)
        {
            if (guncellemeTarihi == null)
            {
                return $"SELECT KOD,DEPOADI,BAKIYE FROM [VOW_STOKBAKIYEENTEGRASYON_ozgurtek]";
            }
            else
            {
                return $"SELECT KOD,DEPOADI,BAKIYE FROM [VOW_STOKBAKIYEENTEGRASYON_ozgurtek] WHERE GUNCELLEMETARIH > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
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

        public static string GetMalzemeFiyatQuery(DateTime? guncellemeTarihi)
        {
            if (guncellemeTarihi == null)
            {
                return "SELECT KOD,KDV,SFIYAT1,SFIYAT2,SFIYAT3 FROM [VOW_STOKFIYATENTEGRASYON_ozgurtek]";
            }
            else
            {
                return "SELECT KOD,KDV,SFIYAT1,SFIYAT2,SFIYAT3 FROM [VOW_STOKFIYATENTEGRASYON_ozgurtek] WHERE GUNCELLEMETARIH > '{guncellemeTarihi.Value:yyyy-MM-dd HH:mm:ss}'";
            }

        }

        public static string GetSevkiyatQuery(DateTime? guncellemeTarihi)
        {
            return "SELECT * FROM VOW_SIPARISKARGOFIRMAVEBARKOD_ozgurtek";
        }
    }
}
