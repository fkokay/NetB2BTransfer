using Microsoft.Data.SqlClient;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Integration.Models;
using NetTransfer.Logo.Library.Class;
using NetTransfer.Logo.Library.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Erp
{
    public class LogoService(ErpSetting erpSetting, B2BParameter b2BParameter)
    {
        private readonly string connectionString = $"Data Source={erpSetting.SqlServer};Initial Catalog={erpSetting.SqlDatabase};Integrated Security=False;Persist Security Info=False;User ID={erpSetting.SqlUser};Password={erpSetting.SqlPassword};Trust Server Certificate=True;";

        public List<LogoMusteriModel> GetArps(ref string errorMessage)
        {
            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = erpSetting.FirmNo!.ToString(),
                periodnr = erpSetting.PeriodNo!.ToString(),
                filter = b2BParameter.CustomerFilter!,
                limit = "-1",
                offset = "0",
                orderbyfieldname = "CLCARD.CODE",
                ascdesc = "ASC",
            };

            if (b2BParameter.CustomerLastTransfer != null)
            {
                param.filter += " AND ((CLCARD.CAPIBLOCK_CREADEDDATE >= '" + b2BParameter.CustomerLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (CLCARD.CAPIBLOCK_MODIFIEDDATE >= '" + b2BParameter.CustomerLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
            }

            var arpList = DataReader.ReadData<LogoMusteriModel>(connectionString, LogoQuery.GetArpQuery(param), ref errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);

            return arpList;
        }
        public List<LogoMusteriModel> GetArpBalances(ref string errorMessage)
        {
            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = erpSetting.FirmNo!.ToString(),
                periodnr = erpSetting.PeriodNo!.ToString(),
                filter = b2BParameter.CustomerFilter!,
                limit = "-1",
                offset = "0",
                orderbyfieldname = "CLCARD.CODE",
                ascdesc = "ASC",
            };

            var arpList = DataReader.ReadData<LogoMusteriModel>(connectionString, LogoQuery.GetArpBalanceQuery(param), ref errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);

            return arpList;
        }
        public List<ItemModel>? GetMalzemeList(ref string errorMessage)
        {
            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = erpSetting.FirmNo!.ToString(),
                periodnr = erpSetting.PeriodNo!.ToString(),
                filter = b2BParameter.ProductFilter!.ToString(),
                limit = "-1",
                offset = "0",
                orderbyfieldname = "ITEMS.CODE",
                ascdesc = "ASC",
            };

            if (b2BParameter.ProductLastTransfer != null)
            {
                param.filter += " AND ((ITEMS.CAPIBLOCK_CREADEDDATE >= '" + b2BParameter.ProductLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ITEMS.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (ITEMS.CAPIBLOCK_MODIFIEDDATE >= '" + b2BParameter.ProductLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ITEMS.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
            }

            var data = DataReader.ReadData<ItemModel>(connectionString, LogoQuery.GetItemsQuery(param), ref errorMessage);

            return data;
        }
        public List<BaseMalzemeStokModel> GetMalzemeStokList(ref string errorMessage)
        {
            List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();

            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = erpSetting.FirmNo!.ToString(),
                periodnr = erpSetting.PeriodNo!.ToString(),
                filter = b2BParameter.ProductStockFilter!.ToString(),
                limit = "-1",
                offset = "0",
                orderbyfieldname = "ITEMS.CODE",
                ascdesc = "ASC",
            };

            var data = DataReader.ReadData<ItemModel>(connectionString, LogoQuery.GetItemStockQuery(param), ref errorMessage);

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
                        StokKodu = item.CODE,
                        StokMiktari = Convert.ToInt32(item.GNTOTSTOCK),
                    });
                }
            }

            return malzemeStokList;
        }
        public List<BaseMalzemeFiyatModel> GetMalzemeFiyatList(ref string errorMessage)
        {
            List<BaseMalzemeFiyatModel> malzemeStokList = new List<BaseMalzemeFiyatModel>();

            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = erpSetting.FirmNo!.ToString(),
                periodnr = erpSetting.PeriodNo!.ToString(),
                filter = b2BParameter.ProductPriceFilter!.ToString(),
                limit = "-1",
                offset = "0",
                orderbyfieldname = "ITEMS.CODE",
                ascdesc = "ASC",
            };

            if (b2BParameter.ProductPriceLastTransfer != null)
            {
                param.filter += " AND ((PRCLIST.CAPIBLOCK_CREADEDDATE >= '" + b2BParameter.ProductPriceLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND PRCLIST.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (PRCLIST.CAPIBLOCK_MODIFIEDDATE >= '" + b2BParameter.ProductPriceLastTransfer.Value.ToString("yyyy-MM-dd HH:mm") + "' AND PRCLIST.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
            }

            var data = DataReader.ReadData<ItemPriceModel>(connectionString, LogoQuery.GetItemsPriceQuery(param), ref errorMessage);

            if (data == null)
            {
                return new List<BaseMalzemeFiyatModel>();
            }
            else
            {
                foreach (var item in data)
                {
                    malzemeStokList.Add(new BaseMalzemeFiyatModel
                    {
                        StokKodu = item.urun_kodu,
                        Fiyat = item.liste_fiyati,
                        IndirimliFiyat = 0
                    });
                }
            }

            return malzemeStokList;
        }
        public int SiparisKaydet(B2BResponseSiparisDetay siparisDetayResult, ref string errorMessage)
        {
            try
            {
                double TOTALDISCOUNTS = siparisDetayResult.siparis_toplamlar.Where(m => m.toplam_tipi_id == 2 || m.toplam_tipi_id == 3).Sum(m => m.tutar * -1);
                double TOTALDISCOUNTED = siparisDetayResult.siparis_toplamlar.Where(m => m.toplam_tipi_id == 6).Sum(m => m.tutar);
                double TOTALVAT = siparisDetayResult.siparis_toplamlar.Where(m => m.toplam_tipi_id == 7).Sum(m => m.tutar);
                double GROSSTOTAL = siparisDetayResult.siparis_toplamlar.Where(m => m.toplam_tipi_id == 8).Sum(m => m.tutar);
                double NETTOTAL = siparisDetayResult.siparis_toplamlar.Where(m => m.toplam_tipi_id == 6).Sum(m => m.tutar); ;

                string query = "DECLARE @SiparisID INT; " +
                                      "EXEC SAVE_ORFICHE " +
                                      "     @DATE_, " +
                                      "     @CARIKOD, " +
                                      "     @TOTALDISCOUNTS, " +
                                      "     @TOTALDISCOUNTED, " +
                                      "     @TOTALVAT, " +
                                      "     @GROSSTOTAL, " +
                                      "     @NETTOTAL, " +
                                      "     @REPORTNET, " +
                                      "     @GENEXP1, @GENEXP2, @GENEXP3, @GENEXP4, " +
                                      "     @createDBY, " +
                                      "     @id OUTPUT; " +  // OUTPUT parametresi burada tanımlı
                                      "SELECT @id AS SiparisKayitID; ";
                using (var connect = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                    {
                        connect.Open();

                        cmd.Parameters.Add("@DATE_", SqlDbType.Date).Value = siparisDetayResult.ust_bilgiler.siparis_tarih;
                        cmd.Parameters.Add("@CARIKOD", SqlDbType.NVarChar).Value = siparisDetayResult.ust_bilgiler.erp_kodu;
                        cmd.Parameters.Add("@TOTALDISCOUNTS", SqlDbType.Decimal).Value = TOTALDISCOUNTS;
                        cmd.Parameters.Add("@TOTALDISCOUNTED", SqlDbType.Decimal).Value = TOTALDISCOUNTED;
                        cmd.Parameters.Add("@TOTALVAT", SqlDbType.Decimal).Value = TOTALVAT;
                        cmd.Parameters.Add("@GROSSTOTAL", SqlDbType.Decimal).Value = GROSSTOTAL;
                        cmd.Parameters.Add("@NETTOTAL", SqlDbType.Decimal).Value = NETTOTAL;
                        cmd.Parameters.Add("@REPORTNET", SqlDbType.Decimal).Value = NETTOTAL;

                        // Açıklamalar
                        cmd.Parameters.Add("@GENEXP1", SqlDbType.NVarChar, 50).Value = siparisDetayResult.ust_bilgiler.siparis_notu;
                        cmd.Parameters.Add("@GENEXP2", SqlDbType.NVarChar, 50).Value = "";
                        cmd.Parameters.Add("@GENEXP3", SqlDbType.NVarChar, 50).Value = "";
                        cmd.Parameters.Add("@GENEXP4", SqlDbType.NVarChar, 50).Value = siparisDetayResult.ust_bilgiler.siparis_id.ToString();

                        // Kullanıcı ID (CREATE BY)
                        cmd.Parameters.Add("@createDBY", SqlDbType.Int).Value = 1;

                        // OUTPUT parametresini almak için SqlParameter ekleme
                        SqlParameter siparisIdParam = new SqlParameter("@id", SqlDbType.Int);
                        siparisIdParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(siparisIdParam);

                        // Komutu çalıştır
                        cmd.ExecuteNonQuery();

                        return (int)siparisIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return 0;
            }
        }
        public int SiparisKalemKaydet(B2BSiparisUstBilgiler ustBilgiler, B2BSiparisKalem kalem, int siparisId, int index, ref string errorMessage)
        {

            try
            {
                string query = "EXEC SAVE_Orfline " +
                                                  "     @STOCKREF, " +
                                                  "     @ORDFICHEREF, " +
                                                  "     @CLIENTREF, " +
                                                  "     @LINETYPE, " +
                                                  "     @LINENO_, " +
                                                  "     @DATE_, " +
                                                  "     @AMOUNT, " +
                                                  "     @PRICE, " +
                                                  "     @TOTAL, " +
                                                  "     @DISCPER, " +
                                                  "     @DISTCOST, " +
                                                  "     @DISTDISC, " +
                                                  "     @VAT, " +
                                                  "     @VATAMNT, " +
                                                  "     @VATMATRAH, " +
                                                  "     @LINEEXP, " +
                                                  "     @LINENET, " +
                                                  "     @PARENTLNREF, " +
                                                  "     @id OUTPUT; " +
                                                  "";
                using (var connect = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                    {
                        connect.Open();

                        cmd.Parameters.Add("@STOCKREF", SqlDbType.NVarChar).Value = kalem.urun_kodu;
                        cmd.Parameters.Add("@ORDFICHEREF", SqlDbType.Int).Value = siparisId; // Önceki sipariş ID’si
                        cmd.Parameters.Add("@CLIENTREF", SqlDbType.NVarChar).Value = ustBilgiler.erp_kodu;
                        cmd.Parameters.Add("@LINETYPE", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@LINENO_", SqlDbType.Int).Value = index;
                        cmd.Parameters.Add("@DATE_", SqlDbType.Date).Value = ustBilgiler.siparis_tarih;
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = kalem.miktar;
                        cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = kalem.liste_fiyat;
                        cmd.Parameters.Add("@TOTAL", SqlDbType.Decimal).Value = kalem.liste_fiyat * kalem.miktar;
                        cmd.Parameters.Add("@DISCPER", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@DISTCOST", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@DISTDISC", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@VAT", SqlDbType.Decimal).Value = kalem.kdv_oran;
                        cmd.Parameters.Add("@VATAMNT", SqlDbType.Decimal).Value = kalem.kdv_dahil_tutar;
                        cmd.Parameters.Add("@VATMATRAH", SqlDbType.Decimal).Value = kalem.net_tutar;
                        cmd.Parameters.Add("@LINEEXP", SqlDbType.NVarChar, 200).Value = kalem.siparis_hareket_id.ToString();
                        cmd.Parameters.Add("@LINENET", SqlDbType.Decimal).Value = kalem.net_tutar;
                        cmd.Parameters.Add("@PARENTLNREF", SqlDbType.Int).Value = 0;

                        // OUTPUT parametresi için SqlParameter tanımla
                        SqlParameter satirIdParam = new SqlParameter("@id", SqlDbType.Int);
                        satirIdParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(satirIdParam);

                        // Komutu çalıştır
                        cmd.ExecuteNonQuery();

                        return (int)satirIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return 0;
            }
        }
        public void SiparisKalemIskontoKaydet(B2BSiparisUstBilgiler ustBilgiler, B2BSiparisKalem kalem, int siparisId, int index, int satirId, double iskontoTutar, double iskontoOran, ref string errorMessage)
        {

            try
            {
                string query = "EXEC SAVE_Orfline " +
                                                  "     @STOCKREF, " +
                                                  "     @ORDFICHEREF, " +
                                                  "     @CLIENTREF, " +
                                                  "     @LINETYPE, " +
                                                  "     @LINENO_, " +
                                                  "     @DATE_, " +
                                                  "     @AMOUNT, " +
                                                  "     @PRICE, " +
                                                  "     @TOTAL, " +
                                                  "     @DISCPER, " +
                                                  "     @DISTCOST, " +
                                                  "     @DISTDISC, " +
                                                  "     @VAT, " +
                                                  "     @VATAMNT, " +
                                                  "     @VATMATRAH, " +
                                                  "     @LINEEXP, " +
                                                  "     @LINENET, " +
                                                  "     @PARENTLNREF " +
                                                  "";
                using (var connect = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                    {
                        connect.Open();

                        cmd.Parameters.Add("@STOCKREF", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@ORDFICHEREF", SqlDbType.Int).Value = siparisId; // Önceki sipariş ID’si
                        cmd.Parameters.Add("@CLIENTREF", SqlDbType.NVarChar).Value = ustBilgiler.erp_kodu;
                        cmd.Parameters.Add("@LINETYPE", SqlDbType.Int).Value = 2;
                        cmd.Parameters.Add("@LINENO_", SqlDbType.Int).Value = index;
                        cmd.Parameters.Add("@DATE_", SqlDbType.Date).Value = ustBilgiler.siparis_tarih;
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@TOTAL", SqlDbType.Decimal).Value = iskontoTutar;
                        cmd.Parameters.Add("@DISCPER", SqlDbType.Decimal).Value = iskontoOran;
                        cmd.Parameters.Add("@DISTCOST", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@DISTDISC", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@VAT", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@VATAMNT", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@VATMATRAH", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@LINEEXP", SqlDbType.NVarChar, 200).Value = kalem.siparis_hareket_id.ToString();
                        cmd.Parameters.Add("@LINENET", SqlDbType.Decimal).Value = 0;
                        cmd.Parameters.Add("@PARENTLNREF", SqlDbType.Int).Value = satirId;


                        // Komutu çalıştır
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public string GetSipariNo(int id, ref string errorMessage)
        {
            LogoQueryParam param = new LogoQueryParam();
            param.DbName = erpSetting.SqlDatabase!;
            param.firmnr = erpSetting.FirmNo!.ToString();
            param.periodnr = erpSetting.PeriodNo!.ToString();
            param.datareference = id.ToString();

            return DataReader.GetExecuteScalar(connectionString, LogoQuery.GetOrderFicheNoQuery(param), ref errorMessage);
        }
    }
}
