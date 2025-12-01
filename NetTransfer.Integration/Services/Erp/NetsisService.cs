using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Integration.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using NetTransfer.Opak.Library.Models;
using NetTransfer.Smartstore.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Services.Erp
{
    public class NetsisService(ErpSetting erpSetting, NetsisSetting netsisSetting, ErpDovizTip? erpDovizTip)
    {
        private readonly string connectionString = $"Data Source={erpSetting.SqlServer};Initial Catalog={erpSetting.SqlDatabase};Integrated Security=False;Persist Security Info=False;User ID={erpSetting.SqlUser};Password={erpSetting.SqlPassword};Trust Server Certificate=True;";

        public List<CariModel>? GetCariList(ref string errorMessage)
        {

            var cariList = DataReader.ReadData<CariModel>(connectionString, NetsisQuery.GetCariQuery(), ref errorMessage);
            if (cariList == null)
            {
                return null;
            }

            return cariList;
        }

        public List<CariBakiyeModel>? GetCariBakiyeList(ref string errorMessage)
        {

            var cariBakiyeList = DataReader.ReadData<CariBakiyeModel>(connectionString, NetsisQuery.GetCariBakiyeQuery(), ref errorMessage);
            if (cariBakiyeList == null)
            {
                return null;
            }

            return cariBakiyeList;
        }

        public List<MalzemeModel>? GetMalzemeList(ref string errorMessage)
        {

            var malzemeList = DataReader.ReadData<MalzemeModel>(connectionString, NetsisQuery.GetMalzemeQuery(), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }

            //foreach (var item in malzemeList)
            //{
            //    item.EvrakList = DataReader.ReadData<EvrakModel>(connectionString, NetsisQuery.GetEvrakQuery(item.urun_kodu), ref errorMessage);
            //}

            return malzemeList;
        }

        public List<MalzemeFiyatModel> GetMalzemeFiyatList(ref string errorMessage)
        {
            var data = DataReader.ReadData<MalzemeFiyatModel>(connectionString, NetsisQuery.GetMalzemeFiyatQuery(), ref errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            return data;
        }

        public List<BaseMalzemeStokModel> GetMalzemeStokList(ref string errorMessage)
        {
            List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();

            var data = DataReader.ReadData<MalzemeStokModel>(connectionString, NetsisQuery.GetMalzemeStokQuery(), ref errorMessage);

            if (data == null)
            {
                return new List<BaseMalzemeStokModel>();
            }
            else
            {
                foreach (var item in data)
                {
                    malzemeStokList.Add(new BaseMalzemeStokModel
                    {
                        DepoKodu = item.depo_kodu,
                        DepoAdi = item.depo_baslik,
                        StokKodu = item.urun_kodu,
                        StokMiktari = item.miktar,
                        StokType = ""
                    });
                }
            }

            return malzemeStokList;
        }

        public string SiparisKaydet(B2BSiparis siparis, B2BResponseSiparisDetay siparisDetay,ref string errorMessage)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (DataReader.GetExecuteScalarToInt(connectionString, $"SELECT COUNT(*) FROM TBLFATUEK  WITH (READPAST) WHERE  FKOD='6'  AND  ACIK16='{siparis.siparis_id}'", ref errorMessage) == 0)
                    {
                        string siparisNo = getSiparisNo(ref errorMessage);
                        int subeKodu = getSubeKodu(siparis, ref errorMessage);
                        string carikod = getCariKod(siparis, ref errorMessage);
                        double bruttutar = 0;
                        double sat_iskt = 0;
                        double geniskt1 = 0;
                        double geniskt2 = 0;
                        double geniskt3 = 0;
                        double genisko1 = 0;
                        double genisko2 = 0;
                        double genisko3 = 0;
                        double kdv = 0;
                        double geneltoplam = 0;
                        double doviztipi = 0;
                        double doviztoplam = 0;
                        string aciklama = siparisDetay.ust_bilgiler.siparis_aciklama;
                        string kod1 = netsisSetting.OzelKod1;
                        string kod2 = netsisSetting.OzelKod2;
                        string plasiyerKodu = getPlasiyerKod(siparis, siparisDetay.ust_bilgiler, ref errorMessage);
                        int odemegunu = getOdemeGunu(siparis, ref errorMessage);
                        string proje_kodu = siparisDetay.siparis_kalemler.Where(m => m.kdv_oran == 0).Any() ? netsisSetting.KdvProjeKodu : netsisSetting.ProjeKodu;
                        string kosulkod = "";
                        int isletme_kodu = int.Parse(netsisSetting.IsletmeKodu);
                        string aciklama1 = siparisDetay.ust_bilgiler.aciklama_1;
                        string aciklama2 = siparisDetay.ust_bilgiler.aciklama_2;
                        string aciklama3 = siparisDetay.ust_bilgiler.aciklama_3;
                        string aciklama4 = siparisDetay.ust_bilgiler.aciklama_4;
                        string aciklama5 = siparisDetay.ust_bilgiler.aciklama_5;
                        string aciklama6 = siparisDetay.ust_bilgiler.aciklama_6;
                        string aciklama7 = siparisDetay.ust_bilgiler.aciklama_7;
                        string aciklama8 = siparisDetay.ust_bilgiler.aciklama_8;
                        string aciklama9 = siparisDetay.ust_bilgiler.aciklama_9;
                        string aciklama10 = siparisDetay.ust_bilgiler.aciklama_10;
                        string aciklama11 = siparisDetay.ust_bilgiler.aciklama_11;
                        string aciklama12 = siparisDetay.ust_bilgiler.aciklama_12;
                        string aciklama13 = siparisDetay.ust_bilgiler.aciklama_13;
                        string aciklama14 = siparisDetay.ust_bilgiler.aciklama_14;
                        string aciklama15 = siparisDetay.ust_bilgiler.aciklama_15;
                        string aciklama16 = siparis.siparis_id.ToString();

                        #region TBLSIPAMAS
                        SqlCommand cmd_sipamas = new SqlCommand(
                                                            " INSERT INTO dbo.TBLSIPAMAS " +
                                                              "       (SUBE_KODU                            " +
                                                              "       , FTIRSIP                             " +
                                                              "       , FATIRS_NO                           " +
                                                              "       , CARI_KODU                           " +
                                                              "       , TARIH                               " +
                                                              "       , TIPI                                " +
                                                              "       , BRUTTUTAR                           " +
                                                              "       , SAT_ISKT                            " +
                                                              "       , MFAZ_ISKT                           " +
                                                              "       , GEN_ISK1T                           " +
                                                              "       , GEN_ISK2T                           " +
                                                              "       , GEN_ISK3T                           " +
                                                              "       , GEN_ISK1O                           " +
                                                              "       , GEN_ISK2O                           " +
                                                              "       , GEN_ISK3O                           " +
                                                              "       , KDV                                 " +
                                                              "       , FAT_ALTM1                           " +
                                                              "       , FAT_ALTM2                           " +
                                                              "       , ACIKLAMA                            " +
                                                              "       , KOD1                                " +
                                                              "       , KOD2                                " +
                                                              "       , ODEMEGUNU                           " +
                                                              "       , ODEMETARIHI                         " +
                                                              "       , KDV_DAHILMI                         " +
                                                              "       , FATKALEM_ADEDI                      " +
                                                              "       , SIPARIS_TEST                        " +
                                                              "       , TOPLAM_MIK                          " +
                                                              "       , TOPDEPO                             " +
                                                              "       , YEDEK22                             " +
                                                              "       , CARI_KOD2                           " +
                                                              "       , YEDEK                               " +
                                                              "       , UPDATE_KODU                         " +
                                                              "       , SIRANO                              " +
                                                              "       , KDV_DAHIL_BRUT_TOP                  " +
                                                              "       , KDV_TENZIL                          " +
                                                              "       , MALFAZLASIKDVSI                     " +
                                                              "       , GENELTOPLAM                         " +
                                                              "       , YUVARLAMA                           " +
                                                              "       , SATIS_KOND                          " +
                                                              "       , PLA_KODU                            " +
                                                              "       , DOVIZTIP                            " +
                                                              "       , DOVIZTUT                            " +
                                                              "       , KS_KODU                             " +
                                                              "       , BAG_TUTAR                           " +
                                                              "       , YEDEK2                              " +
                                                              "       , HIZMET_FAT                          " +
                                                              "       , VADEBAZT                            " +
                                                              "       , KAPATILMIS                          " +
                                                              "       , S_YEDEK1                            " +
                                                              "       , S_YEDEK2                            " +
                                                              "       , F_YEDEK3                            " +
                                                              "       , F_YEDEK4                            " +
                                                              "       , F_YEDEK5                            " +
                                                              "       , C_YEDEK6                            " +
                                                              "       , B_YEDEK7                            " +
                                                              "       , I_YEDEK8                            " +
                                                              "       , L_YEDEK9                            " +
                                                              "       , AMBAR_KBLNO                         " +
                                                              "       , D_YEDEK10                           " +
                                                              "       , PROJE_KODU                          " +
                                                              "       , KOSULKODU                           " +
                                                              "       , FIYATTARIHI                         " +
                                                              "       , KOSULTARIHI                         " +
                                                              "       , GENISK1TIP                          " +
                                                              "       , GENISK2TIP                          " +
                                                              "       , GENISK3TIP                          " +
                                                              "       , EXPORTTYPE                          " +
                                                              "       , EXGUMRUKNO                          " +
                                                              "       , EXGUMTARIH                          " +
                                                              "       , EXFIILITARIH                        " +
                                                              "       , EXPORTREFNO                         " +
                                                              "       , KAYITYAPANKUL                       " +
                                                              "       , KAYITTARIHI                         " +
                                                              "       , DUZELTMEYAPANKUL                    " +
                                                              "       , DUZELTMETARIHI                      " +
                                                              "       , GELSUBE_KODU                        " +
                                                              "       , GITSUBE_KODU                        " +
                                                              "       , ONAYTIPI                            " +
                                                              "       , ONAYNUM                             " +
                                                              "       , ISLETME_KODU                        " +
                                                              "       , ODEKOD                              " +
                                                              "       , BRMALIYET                           " +
                                                              "       , KOSVADEGUNU                         " +
                                                              "       , YAPKOD                              " +
                                                              "       , GIB_FATIRS_NO                       " +

                                                              "                                      " +
                                                              "                               " +
                                                              "                           " +
                                                              "       )                          " +
                                                              " VALUES                                      " +
                                                              "       ( @SUBE_KODU                           " +
                                                              "       , @FTIRSIP                            " +
                                                              "       , @FATIRS_NO                          " +
                                                              "       , @CARI_KODU                          " +
                                                              "       , @TARIH                              " +
                                                              "       , @TIPI                               " +
                                                              "       , @BRUTTUTAR                          " +
                                                              "       , @SAT_ISKT                           " +
                                                              "       , @MFAZ_ISKT                          " +
                                                              "       , @GEN_ISK1T                          " +
                                                              "       , @GEN_ISK2T                          " +
                                                              "       , @GEN_ISK3T                          " +
                                                              "       , @GEN_ISK1O                          " +
                                                              "       , @GEN_ISK2O                          " +
                                                              "       , @GEN_ISK3O                          " +
                                                              "       , @KDV                                " +
                                                              "       , @FAT_ALTM1                          " +
                                                              "       , @FAT_ALTM2                          " +
                                                              "       , @ACIKLAMA                           " +
                                                              "       , @KOD1                               " +
                                                              "       , @KOD2                               " +
                                                              "       , @ODEMEGUNU                          " +
                                                              "       , @ODEMETARIHI                        " +
                                                              "       , @KDV_DAHILMI                        " +
                                                              "       , @FATKALEM_ADEDI                     " +
                                                              "       , @SIPARIS_TEST                       " +
                                                              "       , @TOPLAM_MIK                         " +
                                                              "       , @TOPDEPO                            " +
                                                              "       , @YEDEK22                            " +
                                                              "       , @CARI_KOD2                          " +
                                                              "       , @YEDEK                              " +
                                                              "       , @UPDATE_KODU                        " +
                                                              "       , @SIRANO                             " +
                                                              "       , @KDV_DAHIL_BRUT_TOP                 " +
                                                              "       , @KDV_TENZIL                         " +
                                                              "       , @MALFAZLASIKDVSI                    " +
                                                              "       , @GENELTOPLAM                        " +
                                                              "       , @YUVARLAMA                          " +
                                                              "       , @SATIS_KOND                         " +
                                                              "       , @PLA_KODU                           " +
                                                              "       , @DOVIZTIP                           " +
                                                              "       , @DOVIZTUT                           " +
                                                              "       , @KS_KODU                            " +
                                                              "       , @BAG_TUTAR                          " +
                                                              "       , @YEDEK2                             " +
                                                              "       , @HIZMET_FAT                         " +
                                                              "       , @VADEBAZT                           " +
                                                              "       , @KAPATILMIS                         " +
                                                              "       , @S_YEDEK1                           " +
                                                              "       , @S_YEDEK2                           " +
                                                              "       , @F_YEDEK3                           " +
                                                              "       , @F_YEDEK4                           " +
                                                              "       , @F_YEDEK5                           " +
                                                              "       , @C_YEDEK6                           " +
                                                              "       , @B_YEDEK7                           " +
                                                              "       , @I_YEDEK8                           " +
                                                              "       , @L_YEDEK9                           " +
                                                              "       , @AMBAR_KBLNO                        " +
                                                              "       , @D_YEDEK10                          " +
                                                              "       , @PROJE_KODU                         " +
                                                              "       , @KOSULKODU                          " +
                                                              "       , @FIYATTARIHI                        " +
                                                              "       , @KOSULTARIHI                        " +
                                                              "       , @GENISK1TIP                         " +
                                                              "       , @GENISK2TIP                         " +
                                                              "       , @GENISK3TIP                         " +
                                                              "       , @EXPORTTYPE                         " +
                                                              "       , @EXGUMRUKNO                         " +
                                                              "       , @EXGUMTARIH                         " +
                                                              "       , @EXFIILITARIH                       " +
                                                              "       , @EXPORTREFNO                        " +
                                                              "       , @KAYITYAPANKUL                      " +
                                                              "       , @KAYITTARIHI                        " +
                                                              "       , @DUZELTMEYAPANKUL                   " +
                                                              "       , @DUZELTMETARIHI                     " +
                                                              "       , @GELSUBE_KODU                       " +
                                                              "       , @GITSUBE_KODU                       " +
                                                              "       , @ONAYTIPI                           " +
                                                              "       , @ONAYNUM                            " +
                                                              "       , @ISLETME_KODU                       " +
                                                              "       , @ODEKOD                             " +
                                                              "       , @BRMALIYET                          " +
                                                              "       , @KOSVADEGUNU                        " +
                                                              "       , @YAPKOD                             " +
                                                              "       , @GIB_FATIRS_NO                      " +

                                                              "                                     " +
                                                              "                              " +
                                                              "                          " +
                                                              "                                " +
                                                              "       )   ", conn);

                        cmd_sipamas.Parameters.Add("@SUBE_KODU", SqlDbType.Int).Value = subeKodu;
                        cmd_sipamas.Parameters.Add("@FTIRSIP", SqlDbType.NVarChar).Value = "6";
                        cmd_sipamas.Parameters.Add("@FATIRS_NO", SqlDbType.NVarChar).Value = siparisNo;
                        cmd_sipamas.Parameters.Add("@CARI_KODU", SqlDbType.NVarChar).Value = carikod;
                        cmd_sipamas.Parameters.Add("@TARIH", SqlDbType.Date).Value = siparis.siparis_tarih.Date;
                        cmd_sipamas.Parameters.Add("@TIPI", SqlDbType.NVarChar).Value = "2";
                        cmd_sipamas.Parameters.Add("@BRUTTUTAR", SqlDbType.Decimal).Value = bruttutar;
                        cmd_sipamas.Parameters.Add("@SAT_ISKT", SqlDbType.Decimal).Value = sat_iskt;
                        cmd_sipamas.Parameters.Add("@MFAZ_ISKT", SqlDbType.Decimal).Value = 0;
                        cmd_sipamas.Parameters.Add("@GEN_ISK1T", SqlDbType.Decimal).Value = geniskt1;
                        cmd_sipamas.Parameters.Add("@GEN_ISK2T", SqlDbType.Decimal).Value = geniskt2;
                        cmd_sipamas.Parameters.Add("@GEN_ISK3T", SqlDbType.Decimal).Value = geniskt3;
                        cmd_sipamas.Parameters.Add("@GEN_ISK1O", SqlDbType.Decimal).Value = genisko1;
                        cmd_sipamas.Parameters.Add("@GEN_ISK2O", SqlDbType.Decimal).Value = genisko2;
                        cmd_sipamas.Parameters.Add("@GEN_ISK3O", SqlDbType.Decimal).Value = genisko3;
                        cmd_sipamas.Parameters.Add("@KDV", SqlDbType.NVarChar).Value = kdv;
                        cmd_sipamas.Parameters.Add("@FAT_ALTM1", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@FAT_ALTM2", SqlDbType.NVarChar).Value = 0;
                        string aciklamaa = string.IsNullOrEmpty(aciklama) ? "" : aciklama.Substring(0, Math.Min(20, aciklama.Length));
                        cmd_sipamas.Parameters.Add("@ACIKLAMA", SqlDbType.NVarChar, 25).Value = aciklamaa;
                        cmd_sipamas.Parameters.Add("@KOD1", SqlDbType.NVarChar).Value = kod1;
                        cmd_sipamas.Parameters.Add("@KOD2", SqlDbType.NVarChar).Value = kod2;
                        cmd_sipamas.Parameters.Add("@ODEMEGUNU", SqlDbType.NVarChar).Value = odemegunu;
                        cmd_sipamas.Parameters.Add("@ODEMETARIHI", SqlDbType.Date).Value = siparis.siparis_tarih.Date.AddDays(odemegunu);
                        cmd_sipamas.Parameters.Add("@KDV_DAHILMI", SqlDbType.NVarChar).Value = "H";
                        cmd_sipamas.Parameters.Add("@FATKALEM_ADEDI", SqlDbType.Int).Value = siparisDetay.siparis_kalemler.Count;
                        cmd_sipamas.Parameters.Add("@SIPARIS_TEST", SqlDbType.Date).Value = siparis.siparis_tarih.Date;
                        cmd_sipamas.Parameters.Add("@TOPLAM_MIK", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@TOPDEPO", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@YEDEK22", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@CARI_KOD2", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@YEDEK", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@UPDATE_KODU", SqlDbType.NVarChar).Value = "X";
                        cmd_sipamas.Parameters.Add("@SIRANO", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@KDV_DAHIL_BRUT_TOP", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@KDV_TENZIL", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@MALFAZLASIKDVSI", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@GENELTOPLAM", SqlDbType.NVarChar).Value = geneltoplam;
                        cmd_sipamas.Parameters.Add("@YUVARLAMA", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@SATIS_KOND", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@PLA_KODU", SqlDbType.NVarChar).Value = plasiyerKodu;
                        cmd_sipamas.Parameters.Add("@DOVIZTIP", SqlDbType.Int).Value = doviztipi;
                        cmd_sipamas.Parameters.Add("@DOVIZTUT", SqlDbType.Decimal).Value = doviztoplam;
                        cmd_sipamas.Parameters.Add("@KS_KODU", SqlDbType.NVarChar).Value = "100";
                        cmd_sipamas.Parameters.Add("@BAG_TUTAR", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@YEDEK2", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@HIZMET_FAT", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@VADEBAZT", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@KAPATILMIS", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@S_YEDEK1", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@S_YEDEK2", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@F_YEDEK3", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@F_YEDEK4", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@F_YEDEK5", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@C_YEDEK6", SqlDbType.NVarChar).Value = "X";
                        cmd_sipamas.Parameters.Add("@B_YEDEK7", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@I_YEDEK8", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@L_YEDEK9", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@AMBAR_KBLNO", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@D_YEDEK10", SqlDbType.Date).Value = siparis.siparis_tarih.Date;
                        cmd_sipamas.Parameters.Add("@PROJE_KODU", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(proje_kodu) ? DBNull.Value : proje_kodu;
                        cmd_sipamas.Parameters.Add("@KOSULKODU", SqlDbType.NVarChar).Value = kosulkod;
                        cmd_sipamas.Parameters.Add("@FIYATTARIHI", SqlDbType.Date).Value = siparis.siparis_tarih.Date;
                        cmd_sipamas.Parameters.Add("@KOSULTARIHI", SqlDbType.Date).Value = siparis.siparis_tarih.Date;
                        cmd_sipamas.Parameters.Add("@GENISK1TIP", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@GENISK2TIP", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@GENISK3TIP", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@EXPORTTYPE", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@EXGUMRUKNO", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@EXGUMTARIH", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@EXFIILITARIH", SqlDbType.Date).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@EXPORTREFNO", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@KAYITYAPANKUL", SqlDbType.NVarChar).Value = "b2b";
                        cmd_sipamas.Parameters.Add("@KAYITTARIHI", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd_sipamas.Parameters.Add("@DUZELTMEYAPANKUL", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@DUZELTMETARIHI", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd_sipamas.Parameters.Add("@GELSUBE_KODU", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@GITSUBE_KODU", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@ONAYTIPI", SqlDbType.NVarChar).Value = "A";
                        cmd_sipamas.Parameters.Add("@ONAYNUM", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@ISLETME_KODU", SqlDbType.NVarChar).Value = isletme_kodu;
                        cmd_sipamas.Parameters.Add("@ODEKOD", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@BRMALIYET", SqlDbType.NVarChar).Value = 0;
                        cmd_sipamas.Parameters.Add("@KOSVADEGUNU", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@YAPKOD", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd_sipamas.Parameters.Add("@GIB_FATIRS_NO", SqlDbType.NVarChar).Value = DBNull.Value;



                        cmd_sipamas.Transaction = trans;
                        cmd_sipamas.ExecuteNonQuery();
                        #endregion

                        #region TBLFATUEK
                        SqlCommand cmd_fatuek = new SqlCommand(
                                " INSERT INTO dbo.TBLFATUEK            " +
                                "           (SUBE_KODU                 " +
                                "           , FKOD                     " +
                                "           , FATIRSNO                 " +
                                "           , CKOD                     " +
                                "           , ACIK1                    " +
                                "           , ACIK2                    " +
                                "           , ACIK3                    " +
                                "           , ACIK4                    " +
                                "           , ACIK5                    " +
                                "           , ACIK6                    " +
                                "           , ACIK7                    " +
                                "           , ACIK8                    " +
                                "           , ACIK9                    " +
                                "           , ACIK10                   " +
                                "           , ACIK11                   " +
                                "           , ACIK12                   " +
                                "           , ACIK13                   " +
                                "           , ACIK14                   " +
                                "           , ACIK15                   " +
                                "           , ACIK16                   " +
                                 "          )               " +
                                "            VALUES             " +
                                "           ( @SUBE_KODU                " +
                                "           , @FKOD                    " +
                                "           , @FATIRSNO                " +
                                "           , @CKOD                    " +
                                "           , @ACIK1                   " +
                                "           , @ACIK2                   " +
                                "           , @ACIK3                   " +
                                "           , @ACIK4                   " +
                                "           , @ACIK5                   " +
                                "           , @ACIK6                   " +
                                "           , @ACIK7                   " +
                                "           , @ACIK8                   " +
                                "           , @ACIK9                   " +
                                "           , @ACIK10                  " +
                                "           , @ACIK11                  " +
                                "           , @ACIK12                  " +
                                "           , @ACIK13                  " +
                                "           , @ACIK14                  " +
                                "           , @ACIK15                  " +
                                "           , @ACIK16                  " +
                                  "           )              ", conn
                                );
                        cmd_fatuek.Parameters.Add("@SUBE_KODU", SqlDbType.NVarChar).Value = subeKodu;
                        cmd_fatuek.Parameters.Add("@FKOD", SqlDbType.NVarChar).Value = "6";
                        cmd_fatuek.Parameters.Add("@FATIRSNO ", SqlDbType.NVarChar).Value = siparisNo;
                        cmd_fatuek.Parameters.Add("@CKOD", SqlDbType.NVarChar).Value = carikod;
                        cmd_fatuek.Parameters.Add("@ACIK1", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama1) ? "" : aciklama1;
                        cmd_fatuek.Parameters.Add("@ACIK2", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama2) ? "" : aciklama2;
                        cmd_fatuek.Parameters.Add("@ACIK3", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama3) ? "" : aciklama3;
                        cmd_fatuek.Parameters.Add("@ACIK4", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama4) ? "" : aciklama4;
                        cmd_fatuek.Parameters.Add("@ACIK5", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama5) ? "" : aciklama5;
                        cmd_fatuek.Parameters.Add("@ACIK6", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama6) ? "" : aciklama6;
                        cmd_fatuek.Parameters.Add("@ACIK7", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama7) ? "" : aciklama7;
                        cmd_fatuek.Parameters.Add("@ACIK8", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama8) ? "" : aciklama8;
                        cmd_fatuek.Parameters.Add("@ACIK9", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama9) ? "" : aciklama9;
                        cmd_fatuek.Parameters.Add("@ACIK10", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama10) ? "" : aciklama10;
                        cmd_fatuek.Parameters.Add("@ACIK11", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama11) ? "" : aciklama11;
                        cmd_fatuek.Parameters.Add("@ACIK12", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama12) ? "" : aciklama12;
                        cmd_fatuek.Parameters.Add("@ACIK13", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama13) ? "" : aciklama13;
                        cmd_fatuek.Parameters.Add("@ACIK14", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama14) ? "" : aciklama14;
                        cmd_fatuek.Parameters.Add("@ACIK15", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama15) ? "" : aciklama15;
                        cmd_fatuek.Parameters.Add("@ACIK16", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aciklama16) ? "" : aciklama16;
                        cmd_fatuek.Transaction = trans;
                        cmd_fatuek.ExecuteNonQuery();
                        #endregion

                        int sira = 0;
                        foreach (var item in siparisDetay.siparis_kalemler)
                        {

                            //Urun
                            string stokkodu = "";
                            string yapkod = "";

                            if (netsisSetting.NetsisEsnekYap == 1)
                            {
                                stokkodu = item.model_no;
                                yapkod = item.urun_kodu;
                            }
                            else
                            {
                                stokkodu = item.urun_kodu;
                            }

                            if (DataReader.GetExecuteScalarToInt(connectionString, "SELECT COUNT(*) FROM TBLSTSABIT WHERE STOK_KODU='" + NetsisUtils.Cevir(stokkodu) + "'", ref errorMessage) == 0)
                            {
                                throw new Exception($"'{item.urun_kodu}' stok kodu ERP de bulunmadı.");
                            }

                            if (erpDovizTip == null)
                            {
                                throw new Exception("Doviz kodu eşleştirme yapılmamış");
                            }
                            else
                            {
                                if (!(erpDovizTip.Erp == "Netsis" && erpDovizTip.EticaretDovizKodu == item.doviz_kodu))
                                {
                                    throw new Exception("Doviz kodu eşleştirme yapılmamış");
                                }

                                if (int.Parse(erpDovizTip.ErpDovizKodu) == 0)
                                {

                                }
                                else if (DataReader.GetExecuteScalarToInt(connectionString, "SELECT COUNT(*) FROM NETSIS..KUR WHERE SIRA='" + erpDovizTip.ErpDovizKodu + "'", ref errorMessage) == 0)
                                {
                                    throw new Exception("Doviz kodu eşleştirme yapılmamış");
                                }
                            }

                            int depoKodu = getDepoKodu(item, ref errorMessage);
                            double isk_1 = item.iskonto_1;
                            double isk_2 = item.iskonto_2;
                            double isk_3 = item.iskonto_3;
                            double isk_4 = item.iskonto_4;
                            double isk_5 = item.iskonto_5;
                            double isk_6 = item.iskonto_6;
                            int sthar_dovtip = int.Parse(erpDovizTip.ErpDovizKodu.ToString());

                            double doviz_fiyat = 0;
                            double bf_fiyat = 0;
                            double nf_fiyat = 0;
                            double kur = 0;
                            if (sthar_dovtip > 0)
                            {
                                doviz_fiyat = item.liste_fiyat;
                                bf_fiyat = doviz_fiyat * item.kur;
                                nf_fiyat = item.net_fiyat * item.kur;
                            }
                            else
                            {
                                bf_fiyat = item.liste_fiyat;
                                nf_fiyat = item.net_fiyat;
                            }

                            SqlCommand cmd_sipatra = new SqlCommand(

                          "        INSERT INTO dbo.TBLSIPATRA        " +
                          "          (STOK_KODU                     " +
                          "          , FISNO                        " +
                          "          , STHAR_GCMIK                  " +
                          "          , STHAR_GCMIK2                 " +
                          "          , CEVRIM                       " +
                          "          , STHAR_GCKOD                  " +
                          "          , STHAR_TARIH                  " +
                          "          , STHAR_NF                     " +
                          "          , STHAR_BF                     " +
                          "          , STHAR_IAF                    " +
                          "          , STHAR_KDV                    " +
                          "          , DEPO_KODU                    " +
                          "          , STHAR_ACIKLAMA               " +
                          "          , STHAR_SATISK                 " +
                          "          , STHAR_MALFISK                " +
                          "          , STHAR_FTIRSIP                " +
                          "          , STHAR_SATISK2                " +
                          "          , LISTE_FIAT                   " +
                          "          , STHAR_HTUR                   " +
                          "          , STHAR_DOVTIP                 " +
                          "          , PROMASYON_KODU               " +
                          "          , STHAR_DOVFIAT                " +
                          "          , STHAR_ODEGUN                 " +
                          "          , STRA_SATISK3                 " +
                          "          , STRA_SATISK4                 " +
                          "          , STRA_SATISK5                 " +
                          "          , STRA_SATISK6                 " +
                          "          , STHAR_BGTIP                  " +
                          "          , STHAR_KOD1                   " +
                          "          , STHAR_KOD2                   " +
                          "          , STHAR_SIPNUM                 " +
                          "          , STHAR_CARIKOD                " +
                          "          , STHAR_SIP_TURU               " +
                          "          , PLASIYER_KODU                " +
                          "          , EKALAN_NEDEN                 " +
                          "          , EKALAN                       " +
                          "          , EKALAN1                      " +
                          "          , REDMIK                       " +
                          "          , REDNEDEN                     " +
                          "          , SIRA                         " +
                          "          , STRA_SIPKONT                 " +
                          "          , AMBAR_KABULNO                " +
                          "          , FIRMA_DOVTIP                 " +
                          "          , FIRMA_DOVTUT                 " +
                          "          , FIRMA_DOVMAL                 " +
                          "          , UPDATE_KODU                  " +
                          "          , IRSALIYE_NO                  " +
                          "          , IRSALIYE_TARIH               " +
                          "          , KOSULKODU                    " +
                          "          , ECZA_FAT_TIP                 " +
                          "          , STHAR_TESTAR                 " +
                          "          , OLCUBR                       " +
                          "          , VADE_TARIHI                  " +
                          "          , LISTE_NO                     " +
                          "          , BAGLANTI_NO                  " +
                          "          , SUBE_KODU                    " +
                          "          , MUH_KODU                     " +
                          "          , S_YEDEK1                     " +
                          "          , S_YEDEK2                     " +
                          "          , F_YEDEK3                     " +
                          "          , F_YEDEK4                     " +
                          "          , F_YEDEK5                     " +
                          "          , C_YEDEK6                     " +
                          "          , B_YEDEK7                     " +
                          "          , I_YEDEK8                     " +
                          "          , L_YEDEK9                     " +
                          "          , D_YEDEK10                    " +
                          "          , PROJE_KODU                   " +
                          "          , FIYATTARIHI                  " +
                          "          , KOSULTARIHI                  " +
                          "          , SATISK1TIP                   " +
                          "          , SATISK2TIP                   " +
                          "          , SATISK3TIP                   " +
                          "          , SATISK4TIP                   " +
                          "          , SATISK5TIP                   " +
                          "          , SATISK6TIP                   " +
                          "          , EXPORTTYPE                   " +
                          "          , EXPORTMIK                    " +
                          "          , ONAYTIPI                     " +
                          "          , ONAYNUM                      " +
                          "          , KKMALF                       " +
                          "          , STRA_IRSKONT                 " +
                          "          , YAPKOD                       " +
                          "          , MAMYAPKOD                    " +
                          "          , OTVFIYAT                     " +
                          "          , IRS_INCKEYNO                 " +
                          "          , YEDEK11                      " +
                          "          , YEDEK12                      " +
                          "          , YEDEK13                      " +
                          "          , YEDEK14                      " +
                          "          , YEDEK15                      " +
                          "          , YEDEK16                      " +
                          "          , YEDEK17                      " +
                          "          , YEDEK18                      " +
                          "          , YEDEK19                      " +
                          "          , YEDEK20)                     " +
                          "    VALUES                               " +
                          "          ( @STOK_KODU                    " +
                          "          , @FISNO                       " +
                          "          , @STHAR_GCMIK                 " +
                          "          , @STHAR_GCMIK2                " +
                          "          , @CEVRIM                      " +
                          "          , @STHAR_GCKOD                 " +
                          "          , @STHAR_TARIH                 " +
                          "          , @STHAR_NF                    " +
                          "          , @STHAR_BF                    " +
                          "          , @STHAR_IAF                   " +
                          "          , @STHAR_KDV                   " +
                          "          , @DEPO_KODU                   " +
                          "          , @STHAR_ACIKLAMA              " +
                          "          , @STHAR_SATISK                " +
                          "          , @STHAR_MALFISK               " +
                          "          , @STHAR_FTIRSIP               " +
                          "          , @STHAR_SATISK2               " +
                          "          , @LISTE_FIAT                  " +
                          "          , @STHAR_HTUR                  " +
                          "          , @STHAR_DOVTIP                " +
                          "          , @PROMASYON_KODU              " +
                          "          , @STHAR_DOVFIAT               " +
                          "          , @STHAR_ODEGUN                " +
                          "          , @STRA_SATISK3                " +
                          "          , @STRA_SATISK4                " +
                          "          , @STRA_SATISK5                " +
                          "          , @STRA_SATISK6                " +
                          "          , @STHAR_BGTIP                 " +
                          "          , @STHAR_KOD1                  " +
                          "          , @STHAR_KOD2                  " +
                          "          , @STHAR_SIPNUM                " +
                          "          , @STHAR_CARIKOD               " +
                          "          , @STHAR_SIP_TURU              " +
                          "          , @PLASIYER_KODU               " +
                          "          , @EKALAN_NEDEN                " +
                          "          , @EKALAN                      " +
                          "          , @EKALAN1                     " +
                          "          , @REDMIK                      " +
                          "          , @REDNEDEN                    " +
                          "          , @SIRA                        " +
                          "          , @STRA_SIPKONT                " +
                          "          , @AMBAR_KABULNO               " +
                          "          , @FIRMA_DOVTIP                " +
                          "          , @FIRMA_DOVTUT                " +
                          "          , @FIRMA_DOVMAL                " +
                          "          , @UPDATE_KODU                 " +
                          "          , @IRSALIYE_NO                 " +
                          "          , @IRSALIYE_TARIH              " +
                          "          , @KOSULKODU                   " +
                          "          , @ECZA_FAT_TIP                " +
                          "          , @STHAR_TESTAR                " +
                          "          , @OLCUBR                      " +
                          "          , @VADE_TARIHI                 " +
                          "          , @LISTE_NO                    " +
                          "          , @BAGLANTI_NO                 " +
                          "          , @SUBE_KODU                   " +
                          "          , @MUH_KODU                    " +
                          "          , @S_YEDEK1                    " +
                          "          , @S_YEDEK2                    " +
                          "          , @F_YEDEK3                    " +
                          "          , @F_YEDEK4                    " +
                          "          , @F_YEDEK5                    " +
                          "          , @C_YEDEK6                    " +
                          "          , @B_YEDEK7                    " +
                          "          , @I_YEDEK8                    " +
                          "          , @L_YEDEK9                    " +
                          "          , @D_YEDEK10                   " +
                          "          , @PROJE_KODU                  " +
                          "          , @FIYATTARIHI                 " +
                          "          , @KOSULTARIHI                 " +
                          "          , @SATISK1TIP                  " +
                          "          , @SATISK2TIP                  " +
                          "          , @SATISK3TIP                  " +
                          "          , @SATISK4TIP                  " +
                          "          , @SATISK5TIP                  " +
                          "          , @SATISK6TIP                  " +
                          "          , @EXPORTTYPE                  " +
                          "          , @EXPORTMIK                   " +
                          "          , @ONAYTIPI                    " +
                          "          , @ONAYNUM                     " +
                          "          , @KKMALF                      " +
                          "          , @STRA_IRSKONT                " +
                          "          , @YAPKOD                      " +
                          "          , @MAMYAPKOD                   " +
                          "          , @OTVFIYAT                    " +
                          "          , @IRS_INCKEYNO                " +
                          "          , @YEDEK11                     " +
                          "          , @YEDEK12                     " +
                          "          , @YEDEK13                     " +
                          "          , @YEDEK14                     " +
                          "          , @YEDEK15                     " +
                          "          , @YEDEK16                     " +
                          "          , @YEDEK17                     " +
                          "          , @YEDEK18                     " +
                          "          , @YEDEK19                     " +
                          "          , @YEDEK20                     " +
                                    "  )", conn);


                            cmd_sipatra.Parameters.Add("@STOK_KODU", SqlDbType.NVarChar).Value = stokkodu;
                            cmd_sipatra.Parameters.Add("@FISNO", SqlDbType.NVarChar).Value = siparisNo;
                            cmd_sipatra.Parameters.Add("@STHAR_GCMIK", SqlDbType.Decimal).Value = item.miktar;
                            cmd_sipatra.Parameters.Add("@STHAR_GCMIK2", SqlDbType.Decimal).Value = 0;
                            cmd_sipatra.Parameters.Add("@CEVRIM", SqlDbType.Decimal).Value = 0;
                            cmd_sipatra.Parameters.Add("@STHAR_GCKOD", SqlDbType.NVarChar).Value = "C";
                            cmd_sipatra.Parameters.Add("@STHAR_TARIH", SqlDbType.Date).Value = siparis.siparis_tarih;
                            cmd_sipatra.Parameters.Add("@STHAR_NF", SqlDbType.Decimal).Value = nf_fiyat;
                            cmd_sipatra.Parameters.Add("@STHAR_BF", SqlDbType.Decimal).Value = bf_fiyat;
                            cmd_sipatra.Parameters.Add("@STHAR_IAF", SqlDbType.Decimal).Value = 0;
                            cmd_sipatra.Parameters.Add("@STHAR_KDV", SqlDbType.Decimal).Value = item.kdv_oran;
                            cmd_sipatra.Parameters.Add("@DEPO_KODU", SqlDbType.Int).Value = depoKodu;
                            cmd_sipatra.Parameters.Add("@STHAR_ACIKLAMA", SqlDbType.NVarChar).Value = carikod;
                            cmd_sipatra.Parameters.Add("@STHAR_SATISK", SqlDbType.Decimal).Value = isk_1 > 0 ? isk_1 / 100000 : 0;
                            cmd_sipatra.Parameters.Add("@STHAR_MALFISK", SqlDbType.Decimal).Value = 0;
                            cmd_sipatra.Parameters.Add("@STHAR_FTIRSIP", SqlDbType.NVarChar).Value = "6";
                            cmd_sipatra.Parameters.Add("@STHAR_SATISK2", SqlDbType.Decimal).Value = isk_2;
                            cmd_sipatra.Parameters.Add("@LISTE_FIAT", SqlDbType.Int).Value = 1;
                            cmd_sipatra.Parameters.Add("@STHAR_HTUR", SqlDbType.NVarChar).Value = "H";
                            cmd_sipatra.Parameters.Add("@STHAR_DOVTIP", SqlDbType.Int).Value = sthar_dovtip;
                            cmd_sipatra.Parameters.Add("@PROMASYON_KODU", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@STHAR_DOVFIAT", SqlDbType.Decimal).Value = doviz_fiyat;
                            cmd_sipatra.Parameters.Add("@STHAR_ODEGUN", SqlDbType.Int).Value = odemegunu;
                            cmd_sipatra.Parameters.Add("@STRA_SATISK3", SqlDbType.Decimal).Value = isk_3;
                            cmd_sipatra.Parameters.Add("@STRA_SATISK4", SqlDbType.Decimal).Value = isk_4;
                            cmd_sipatra.Parameters.Add("@STRA_SATISK5", SqlDbType.Decimal).Value = isk_5;
                            cmd_sipatra.Parameters.Add("@STRA_SATISK6", SqlDbType.Decimal).Value = isk_6;
                            cmd_sipatra.Parameters.Add("@STHAR_BGTIP", SqlDbType.NVarChar).Value = "I";
                            cmd_sipatra.Parameters.Add("@STHAR_KOD1", SqlDbType.NVarChar).Value = kod1;
                            cmd_sipatra.Parameters.Add("@STHAR_KOD2", SqlDbType.NVarChar).Value = kod2;
                            cmd_sipatra.Parameters.Add("@STHAR_SIPNUM", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@STHAR_CARIKOD", SqlDbType.NVarChar).Value = carikod;
                            cmd_sipatra.Parameters.Add("@STHAR_SIP_TURU", SqlDbType.NVarChar).Value = "B";
                            cmd_sipatra.Parameters.Add("@PLASIYER_KODU", SqlDbType.NVarChar).Value = plasiyerKodu;
                            cmd_sipatra.Parameters.Add("@EKALAN_NEDEN", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@EKALAN", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@EKALAN1", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@REDMIK", SqlDbType.Decimal).Value = 0;
                            cmd_sipatra.Parameters.Add("@REDNEDEN", SqlDbType.Int).Value = 0;
                            cmd_sipatra.Parameters.Add("@SIRA", SqlDbType.Int).Value = sira + 1;
                            cmd_sipatra.Parameters.Add("@STRA_SIPKONT", SqlDbType.Int).Value = sira + 1;
                            cmd_sipatra.Parameters.Add("@AMBAR_KABULNO", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@FIRMA_DOVTIP", SqlDbType.Int).Value = 0;
                            cmd_sipatra.Parameters.Add("@FIRMA_DOVTUT", SqlDbType.Int).Value = 0;
                            cmd_sipatra.Parameters.Add("@FIRMA_DOVMAL", SqlDbType.Int).Value = 0;
                            cmd_sipatra.Parameters.Add("@UPDATE_KODU", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@IRSALIYE_NO", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@IRSALIYE_TARIH", SqlDbType.Date).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@KOSULKODU", SqlDbType.NVarChar).Value = kosulkod;
                            cmd_sipatra.Parameters.Add("@ECZA_FAT_TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@STHAR_TESTAR", SqlDbType.Date).Value = siparis.siparis_tarih;
                            cmd_sipatra.Parameters.Add("@OLCUBR", SqlDbType.Int).Value = 1;
                            cmd_sipatra.Parameters.Add("@VADE_TARIHI", SqlDbType.Date).Value = siparis.siparis_tarih.AddDays(odemegunu);
                            cmd_sipatra.Parameters.Add("@LISTE_NO", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@BAGLANTI_NO", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@SUBE_KODU", SqlDbType.Int).Value = subeKodu;
                            cmd_sipatra.Parameters.Add("@MUH_KODU", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@S_YEDEK1", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@S_YEDEK2", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@F_YEDEK3", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@F_YEDEK4", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@F_YEDEK5", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@C_YEDEK6", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@B_YEDEK7", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@I_YEDEK8", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@L_YEDEK9", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@D_YEDEK10", SqlDbType.Date).Value = siparis.siparis_tarih;
                            if (proje_kodu == "")
                            {
                                cmd_sipatra.Parameters.Add("@PROJE_KODU", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd_sipatra.Parameters.Add("@PROJE_KODU", SqlDbType.NVarChar).Value = proje_kodu;
                            }

                            cmd_sipatra.Parameters.Add("@FIYATTARIHI", SqlDbType.Date).Value = siparis.siparis_tarih;
                            cmd_sipatra.Parameters.Add("@KOSULTARIHI", SqlDbType.Date).Value = siparis.siparis_tarih;
                            cmd_sipatra.Parameters.Add("@SATISK1TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@SATISK2TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@SATISK3TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@SATISK4TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@SATISK5TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@SATISK6TIP", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@EXPORTTYPE", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@EXPORTMIK", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@ONAYTIPI", SqlDbType.NVarChar).Value = "A";
                            cmd_sipatra.Parameters.Add("@ONAYNUM", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@KKMALF", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@STRA_IRSKONT", SqlDbType.NVarChar).Value = 0;

                            if (yapkod == "")
                            {
                                cmd_sipatra.Parameters.Add("@YAPKOD", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd_sipatra.Parameters.Add("@YAPKOD", SqlDbType.NVarChar).Value = yapkod;
                            }

                            cmd_sipatra.Parameters.Add("@MAMYAPKOD", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@OTVFIYAT", SqlDbType.NVarChar).Value = 0;
                            cmd_sipatra.Parameters.Add("@IRS_INCKEYNO", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK11", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK12", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK13", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK14", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK15", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK16", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK17", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK18", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK19", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd_sipatra.Parameters.Add("@YEDEK20", SqlDbType.NVarChar).Value = DBNull.Value;


                            cmd_sipatra.Transaction = trans;
                            cmd_sipatra.ExecuteNonQuery();

                            sira++;

                        }

                        trans.Commit();

                        return siparisNo;
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    trans.Rollback();
                }
                finally
                {
                    conn.Close();
                }

                return "";
            }
        }

        public async Task<bool> OdemeKaydet(B2BOdeme odeme)
        {
            DataTable tblsanalposlog = await DataReader.GetDbDataTable(connectionString,
                            " EXEC "
                            + "SP_B2BSANALPOS_KAYIT" + " " +
                            "'" + NetsisUtils.CevirNetsis(odeme.musteri_erp_kodu) + "'," +
                            "'" + NetsisUtils.CevirNetsis(odeme.odeme_no) + "'," +
                            "'" + NetsisUtils.CevirNetsis(odeme.odeme_notu) + "'," +
                            "'" + NetsisUtils.CevirNetsis(odeme.odeme_sistem) + "'," +
                            "'" + NetsisUtils.CevirNetsis(odeme.doviz_kodu) + "'," +
                            "" + odeme.doviz_kuru + "," +
                            "" + odeme.komisyon_orani + "," +
                            "'" + NetsisUtils.CevirNetsis(odeme.odeme_turu) + "'," +
                            "'" + Convert.ToDateTime(odeme.islem_tarihi).ToString("yyyy-MM-dd") + "'," +
                            "" + odeme.hesaba_islenecek_tutar + "," +
                            "" + 1 + "," +
                            "" + odeme.cekilecek_tutar + "," +
                            "" + odeme.banka_komisyon_orani + "," +
                            "'" + NetsisUtils.CevirNetsis(odeme.durum_mesaj) + "'");

            return tblsanalposlog.Rows[0]["durum"].ToString() == "true";

        }

        private int getOdemeGunu(B2BSiparis siparis, ref string errorMessage)
        {
            return DataReader.GetExecuteScalarToInt(connectionString, "SELECT VADE_GUNU from tblcasabit  WITH (READPAST) where cari_kod='" + NetsisUtils.Cevir(siparis.erp_kodu) + "'", ref errorMessage);
        }

        private int getDepoKodu(B2BSiparisKalem item, ref string errorMessage)
        {
            int depoKodu = 0;
            if (netsisSetting.DepoKoduCaridenSecilsin == "E")
            {
                depoKodu = DataReader.GetExecuteScalarToInt(connectionString, "SELECT DEPO_KODU FROM TBLSTSABIT WHERE STOK_KODU='" + NetsisUtils.Cevir(item.urun_kodu) + "'", ref errorMessage);

            }
            else
            {
                depoKodu = int.Parse(netsisSetting.DepoKodu);
            }

            return depoKodu;
        }

        private string getPlasiyerKod(B2BSiparis siparis, B2BSiparisUstBilgiler b2BSiparisUstBilgiler, ref string errorMessage)
        {
            string plasiyer_kodu = "";
            if (netsisSetting.CariPlasiyerKoduSecilsin == "E")
            {
                plasiyer_kodu = DataReader.GetExecuteScalar(connectionString, "select PLASIYER_KODU from tblcasabit  WITH (READPAST) where cari_kod='" + NetsisUtils.Cevir(siparis.erp_kodu) + "'", ref errorMessage);
            }
            else if (netsisSetting.PlasiyerKodu == "")
            {
                plasiyer_kodu = netsisSetting.PlasiyerKodu;
            }
            else
            {
                plasiyer_kodu = b2BSiparisUstBilgiler.plasiyer;
            }

            return plasiyer_kodu;
        }

        private string getCariKod(B2BSiparis siparis, ref string errorMessage)
        {
            return DataReader.GetExecuteScalar(connectionString, "select CARI_KOD from tblcasabit  WITH (READPAST) where cari_kod='" + NetsisUtils.Cevir(siparis.erp_kodu) + "'", ref errorMessage);
        }

        private string getSiparisNo(ref string errorMessage)
        {
            return DataReader.GetExecuteScalar(connectionString, "SELECT SIPARISNO=ISNULL((SELECT TOP 1  '" + netsisSetting.NumaraSerisi + "' +  RIGHT( '000000000000000000'+CONVERT(VARCHAR, CONVERT(DECIMAL(18,0), RIGHT(FATIRS_NO,15-LEN('" + netsisSetting.NumaraSerisi + "')))+1)  ,15-LEN('" + netsisSetting.NumaraSerisi + "')) FROM TBLSIPAMAS WHERE FTIRSIP='6' AND FATIRS_NO LIKE '" + netsisSetting.NumaraSerisi + "%' AND ISNUMERIC(RIGHT(FATIRS_NO,15-LEN('" + netsisSetting.NumaraSerisi + "')))=1 ORDER BY FATIRS_NO DESC),'" + netsisSetting.NumaraSerisi + "' +RIGHT('0000000000000000001',15-LEN('" + netsisSetting.NumaraSerisi + "')) )", ref errorMessage); ;
        }

        private int getSubeKodu(B2BSiparis siparis, ref string errorMessage)
        {
            int subeKodu = 0;
            try
            {
                string cariSubeKodu = DataReader.GetExecuteScalar(connectionString, "select SUBE_KODU from tblcasabit  WITH (READPAST) where cari_kod='" + NetsisUtils.Cevir(siparis.erp_kodu) + "'", ref errorMessage);
                if (netsisSetting.CariSubeKoduSecilsin == "E")
                {
                    if (int.Parse(cariSubeKodu) >= 0)
                    {
                        subeKodu = int.Parse(cariSubeKodu);
                    }
                }
                else
                {
                    subeKodu = int.Parse(netsisSetting.SubeKodu);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                subeKodu = -1;
            }

            return subeKodu;
        }
    }
}
