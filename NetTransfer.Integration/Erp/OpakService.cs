using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Opak.Library.Class;
using NetTransfer.Opak.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Erp
{
    public class OpakService(ErpSetting erpSetting)
    {
        private readonly string connectionString = $"Data Source={erpSetting.SqlServer};Initial Catalog={erpSetting.SqlDatabase};Integrated Security=False;Persist Security Info=False;User ID={erpSetting.SqlUser};Password={erpSetting.SqlPassword};Trust Server Certificate=True;";

        public List<OpakMalzeme>? GetMalzemeList(ref string errorMessage)
        {
            var malzemeList = DataReader.ReadData<OpakMalzeme>(connectionString, OpakQuery.GetMalzemeQuery(), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }
            foreach (var item in malzemeList)
            {
                item.MalzemeResimList = DataReader.ReadData<OpakMalzemeResim>(connectionString, OpakQuery.GetMalzemeResimQuery(item.STOK_KODU), ref errorMessage);
                if (item.VARYANTLIURUN > 0)
                {
                    item.MalzemeVaryantList = DataReader.ReadData<OpakVaryant>(connectionString, OpakQuery.GetMalzemeVaryantQuery(item.STOK_KODU), ref errorMessage);
                }
            }

            return malzemeList;
        }
    }
}
