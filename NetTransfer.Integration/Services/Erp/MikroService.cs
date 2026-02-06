using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Integration.Models;
using NetTransfer.Mikro.Library.Class;
using NetTransfer.Mikro.Library.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Services.Erp
{
    public class MikroService(ErpSetting erpSetting)
    {
        private readonly string connectionString = $"Data Source={erpSetting.SqlServer};Initial Catalog={erpSetting.SqlDatabase};Integrated Security=False;Persist Security Info=False;User ID={erpSetting.SqlUser};Password={erpSetting.SqlPassword};Trust Server Certificate=True;";
        public List<MikroCariModel>? GetCariList(ref string errorMessage)
        {

            var cariList = DataReader.ReadData<MikroCariModel>(connectionString, MikroQuery.GetCariQuery(), ref errorMessage);
            if (cariList == null)
            {
                return null;
            }

            return cariList;
        }

        public List<MikroCariBakiyeModel>? GetCariBakiyeList(ref string errorMessage)
        {

            var cariBakiyeList = DataReader.ReadData<MikroCariBakiyeModel>(connectionString, MikroQuery.GetCariBakiyeQuery(), ref errorMessage);
            if (cariBakiyeList == null)
            {
                return null;
            }

            return cariBakiyeList;
        }

        public List<MikroMalzemeModel>? GetMalzemeList(ref string errorMessage)
        {

            var malzemeList = DataReader.ReadData<MikroMalzemeModel>(connectionString, MikroQuery.GetMalzemeQuery(), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }

            return malzemeList;
        }

        public List<MikroMalzemeFiyatModel> GetMalzemeFiyatList(ref string errorMessage)
        {
            var data = DataReader.ReadData<MikroMalzemeFiyatModel>(connectionString, MikroQuery.GetMalzemeFiyatQuery(), ref errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            return data;
        }

        public List<BaseMalzemeStokModel> GetMalzemeStokList(ref string errorMessage)
        {
            List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();

            var data = DataReader.ReadData<MikroMalzemeStokModel>(connectionString, MikroQuery.GetMalzemeStokQuery(), ref errorMessage);
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
    }
}
