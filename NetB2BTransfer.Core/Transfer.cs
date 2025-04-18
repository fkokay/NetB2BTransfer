using NetB2BTransfer.Core.Data;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Logo.Library.Class;
using NetB2BTransfer.Logo.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core
{
    public class Transfer
    {
        private readonly ErpSetting _erpSetting;

        public Transfer(ErpSetting erpSetting)
        {
            _erpSetting = erpSetting;
        }

        public void MusteriTransfer()
        {
            string errorMessage = string.Empty;

            if (_erpSetting.Erp == "Logo")
            {
                LogoQueryParam param = new LogoQueryParam
                {
                    DbName = "LOGODB",
                    firmnr = "200",
                    periodnr = "1",
                    filter = string.Empty,
                    limit = "-1",
                    offset = "0",
                    orderbyfieldname = "CLCARD.CODE",
                    ascdesc = "ASC",
                };

                if (_erpSetting.LastTransferDate != null)
                {
                    param.filter += " AND ((CLCARD.CAPIBLOCK_CREADEDDATE >= '" + _erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NULL) OR (CLCARD.CAPIBLOCK_MODIFIEDDATE >= '" + _erpSetting.LastTransferDate.Value.ToString("yyyy-MM-dd HH:mm") + "' AND CLCARD.CAPIBLOCK_MODIFIEDDATE IS NOT NULL)) ";
                }

                var arpList = DataReader.ReadData<ArpModel>(LogoQuery.GetArpsQuery(param), ref errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                    throw new Exception(errorMessage);


            }
        }
    }
}
