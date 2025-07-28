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
    public class NetsisService(ErpSetting erpSetting)
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

            foreach (var item in malzemeList)
            {
                item.EvrakList = DataReader.ReadData<EvrakModel>(connectionString, NetsisQuery.GetEvrakQuery(item.urun_kodu), ref errorMessage);
            }

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

        public void SiparisKaydet(B2BSiparis siparis)
        {
            string errorMessage = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (DataReader.GetExecuteScalarToInt(connectionString, $"SELECT COUNT(*) FROM TBLFATUEK  WITH (READPAST) WHERE  FKOD='6'  AND  ACIK16='{siparis.siparis_id}'", ref errorMessage) == 0)
                    {

                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
