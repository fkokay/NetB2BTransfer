using NetTransfer.Core.Data;
using NetTransfer.Opak.Library.Class;
using NetTransfer.Opak.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Erp
{
    public class OpakService
    {
        public List<OpakMalzeme>? GetMalzemeList(ref string errorMessage)
        {
            var malzemeList = DataReader.ReadData<OpakMalzeme>(OpakQuery.GetMalzemeQuery(), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }

            foreach (var item in malzemeList)
            {
                item.MalzemeResimList = DataReader.ReadData<OpakMalzemeResim>(OpakQuery.GetMalzemeResimQuery(item.STOK_KODU), ref errorMessage);

                if (item.VARYANTLIURUN > 0)
                {
                    item.MalzemeVaryantList = DataReader.ReadData<OpakVaryant>(OpakQuery.GetMalzemeVaryantQuery(item.STOK_KODU), ref errorMessage);
                }
            }

            return malzemeList;
        }
    }
}
