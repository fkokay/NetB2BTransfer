using NetTransfer.Netsis.Library.Models;
using NetOpenX50;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library
{
    public class Netsis
    {
        public static bool MusteriSiparisiKaydet(NetOpenXModel netOpenX,FaturaModel faturaData, ref string errorMessage, out string fatIrsNo)
        {
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            Fatura fatura = default(Fatura);
            FatUst fatUst = default(FatUst);
            FatKalem fatKalem = default(FatKalem);
            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL, netOpenX.DatabaseName, netOpenX.DatabaseUserName, netOpenX.DatabasePassword, netOpenX.NetsisUserName, netOpenX.NetsisPassword, 0);
                fatura = kernel.yeniFatura(sirket, TFaturaTip.ftSSip);

                fatura.StokKartinaGoreHesapla = true;

                fatUst = fatura.Ust();
                fatUst.FATIRS_NO = faturaData.FATIRS_NO;
                fatUst.CariKod = faturaData.CARI_KODU;
                fatUst.Tarih = faturaData.TARIH;
                fatUst.ENTEGRE_TRH = DateTime.Now;
                fatUst.FiiliTarih = DateTime.Now;
                fatUst.SIPARIS_TEST = DateTime.Now;
                fatUst.TIPI = TFaturaTipi.ft_YurtIci;
                fatUst.PLA_KODU = faturaData.PLA_KODU;
                fatUst.Proje_Kodu = faturaData.PROJE_KODU;
                fatUst.KDV_DAHILMI = faturaData.KDV_DAHILMI;


                foreach (var item in faturaData.FATURA_KALEMLER)
                {
                    fatKalem = fatura.kalemYeni(item.STOK_KODU);
                    fatKalem.DEPO_KODU = item.DEPO_KODU;
                    fatKalem.STra_GCMIK = item.STRA_GCMIK;
                    fatKalem.STra_NF = item.STRA_NF;
                    fatKalem.STra_BF = item.STRA_BF;
                    if (!string.IsNullOrEmpty(item.SATIR_BAZI_ACIKLAMA))
                    {
                        fatKalem.SatirBaziAcik[1] = item.SATIR_BAZI_ACIKLAMA;
                    }
                }
               

                fatura.kayitYeni();

                fatIrsNo = fatUst.FATIRS_NO;

                return true;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                fatIrsNo = string.Empty;
                return false;
            }
            finally
            {
                Marshal.ReleaseComObject(fatKalem);
                Marshal.ReleaseComObject(fatUst);
                Marshal.ReleaseComObject(fatura);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }

        }

        public static bool CariKontrol(NetOpenXModel netOpenX, string cariKod, ref string errorMessage)
        {
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            Cari cari = default(Cari);
            CariTemelBilgi caritemel = default(CariTemelBilgi);
            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL, netOpenX.DatabaseName, netOpenX.DatabaseUserName, netOpenX.DatabasePassword, netOpenX.NetsisUserName, netOpenX.NetsisPassword, 0);
                if (sirket == null)
                {
                    errorMessage = "Netsis bağlantısı sağlanamadı.";
                    return false;
                }
                else
                {
                    cari = kernel.yeniCari(sirket);
                    caritemel = cari.TemelBilgi();
                    var result =cari.kayitOku(TOkumaTipi.otAc, "CASABIT.CARI_KOD='Cari.01'");
                    {
                        cari.kayitOku(TOkumaTipi.otIlk);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            finally
            {
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }
        }

        public static bool CariKaydet(NetOpenXModel netOpenX,CariModel cariData,ref string errorMessage)
        {
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            Cari cari = default(Cari);
            CariTemelBilgi cariTmlBlg = default(CariTemelBilgi);
            CariEkBilgi cariEkBlg = default(CariEkBilgi);


            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL, netOpenX.DatabaseName, netOpenX.DatabaseUserName, netOpenX.DatabasePassword, netOpenX.NetsisUserName, netOpenX.NetsisPassword, 0);
                cari = kernel.yeniCari(sirket);

                cariTmlBlg = cari.TemelBilgi();
                cariEkBlg = cari.EkBilgi();
                cariTmlBlg.CARI_KOD = cariData.CARI_KOD;
                cariTmlBlg.CARI_ISIM = cariData.CARI_ISIM;
                cariTmlBlg.CARI_TIP = cariData.CARI_TIP;
                cariTmlBlg.VERGI_DAIRESI = cariData.VERGI_DAIRESI;
                cariTmlBlg.VERGI_NUMARASI = cariData.VERGI_NUMARASI;
                cariTmlBlg.CARI_ADRES = cariData.CARI_ADRES;
                cariTmlBlg.EMAIL = cariData.EMAIL;
                cariTmlBlg.CARI_TEL = cariData.CARI_TEL;
              

                cariEkBlg.CARI_KOD = cariTmlBlg.CARI_KOD;
                cariEkBlg.TcKimlikNo = cariData.TCKIMLIK_NO;
                cari.kayitYeni();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            finally
            {
                Marshal.ReleaseComObject(cariEkBlg);
                Marshal.ReleaseComObject(cariTmlBlg);
                Marshal.ReleaseComObject(cari);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }
        }
    }
}
