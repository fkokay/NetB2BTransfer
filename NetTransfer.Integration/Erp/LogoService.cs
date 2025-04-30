using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Logo.Library.Class;
using NetTransfer.Logo.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Erp
{
    public class LogoService(ErpSetting erpSetting,B2BParameter b2BParameter)
    {
        public List<ArpModel> GetArps(ref string errorMessage)
        {
            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = "200",
                periodnr = "1",
                filter = b2BParameter.CustomerFilter!,
                limit = "-1",
                offset = "0",
                orderbyfieldname = "CLCARD.CODE",
                ascdesc = "ASC",
            };

            if (erpSetting.LastTransferDate != null)
            {
                param.filter += " AND ((CLCARD.CAPIBLOCK_CREADEDDATE >= '" + erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (CLCARD.CAPIBLOCK_MODIFIEDDATE >= '" + erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
            }

            var arpList = DataReader.ReadData<ArpModel>(LogoQuery.GetArpsQuery(param), ref errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);

            return arpList;
        }
        public List<ItemModel>? GetMalzemeList(ref string errorMessage)
        {
            LogoQueryParam param = new LogoQueryParam
            {
                DbName = erpSetting.SqlDatabase!,
                firmnr = "200",
                periodnr = "1",
                filter = " AND (ITEMS.CODE NOT LIKE 'ÿ') AND (LEN(ITEMS.NAME) > 0) AND (ITEMS.NAME NOT IN ('AS 396/A', 'SP 453 M'))",
                limit = "-1",
                offset = "0",
                orderbyfieldname = "ITEMS.CODE",
                ascdesc = "ASC",
            };

            if (erpSetting.LastTransferDate != null)
            {
                param.filter += " AND ((ITEMS.CAPIBLOCK_CREADEDDATE >= '" + erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ITEMS.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (ITEMS.CAPIBLOCK_MODIFIEDDATE >= '" + erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ITEMS.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
            }

            var data = DataReader.ReadData<ItemModel>(LogoQuery.GetItemsQuery(param), ref errorMessage);
            
            return data;
        }

    }
}
