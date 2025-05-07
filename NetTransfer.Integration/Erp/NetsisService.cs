using Microsoft.Data.SqlClient;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Data;
using NetTransfer.Core.Utils;
using NetTransfer.Integration.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using NetTransfer.Smartstore.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Erp
{
    public class NetsisService()
    {
        public List<MalzemeModel>? GetMalzemeList(ref string errorMessage)
        {
            var malzemeList = DataReader.ReadData<MalzemeModel>(NetsisQuery.GetMalzemeQuery(), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }

            foreach (var item in malzemeList)
            {
                item.EVRAK_LIST = DataReader.ReadData<EvrakModel>(NetsisQuery.GetEvrakQuery(item.STOK_KODU), ref errorMessage);
            }

            return malzemeList;
        }

        public List<BaseMalzemeFiyatModel> GetMalzemeFiyatList(ref string errorMessage)
        {
            List<BaseMalzemeFiyatModel> malzemeFiyatList = new List<BaseMalzemeFiyatModel>();

            var data = DataReader.ReadData<MalzemeFiyatModel>(NetsisQuery.GetMalzemeFiyatQuery(), ref errorMessage);
            if (data == null)
            {
                return new List<BaseMalzemeFiyatModel>();
            }
            else
            {
                foreach (var item in data)
                {
                    malzemeFiyatList.Add(new BaseMalzemeFiyatModel
                    {
                        StokKodu = item.STOK_KODU,
                        Fiyat = item.LISTE_FIYATI,
                        IndirimliFiyat = item.INDIRIMLI_FIYATI
                    });
                }
            }

            return malzemeFiyatList;
        }

        public List<BaseMalzemeStokModel> GetMalzemeStokList(ref string errorMessage)
        {
            List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();

            var data = DataReader.ReadData<MalzemeStokModel>(NetsisQuery.GetMalzemeStokQuery(), ref errorMessage);

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
                        StokKodu = item.STOK_KODU,
                        StokMiktari = item.STOK_MIKTARI,
                    });
                }
            }

            return malzemeStokList;
        }

    }
}
