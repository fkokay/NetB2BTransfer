using NetB2BTransfer.B2B.Library.Models;
using NetB2BTransfer.Core.Data;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Logo.Library.Class;
using NetB2BTransfer.Logo.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.B2B.Library
{
    public class Transfer
    {
        private readonly B2BClient _b2BClient;
        private readonly ErpSetting _erpSetting;

        public Transfer(ErpSetting erpSetting,B2BSetting b2BSetting)
        {
            _erpSetting = erpSetting;
            _b2BClient = new B2BClient(b2BSetting);
        }

        public async Task MusteriTransfer()
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


                foreach (var item in arpList)
                {
                    var musteri = new Musteri
                    {
                        musteri_ozellik="kurumsal",
                        unvan = item.DEFINITION_,
                        cari_kod = item.CODE,
                        adi = item.CUSTNAME,
                        soyadi = item.CUSTSURNAME,
                        telefon = item.TELNRS1,
                        adres = item.ADDR1+" "+item.ADDR2,
                        il = item.CITY,
                        ilce = item.TOWN,
                        vergi_dairesi = item.TAXOFFICE,
                        vergi_no = item.TAXNR,
                        tc_no = item.TCKNO,
                        plasiyer = item.CYPHCODE,
                        depo_kodu ="",
                        erp_kodu= item.CODE,
                        odeme_sekilleri = new List<string>(),
                        musteri_kosul_kodu = item.SPECODE,
                        grup_kodu = "TURKUAZ",
                        fiyat_listesi_kodu = "",
                        email = item.CODE + "@turkuaz.com",
                        kullanici_adi = item.CODE,
                        sifre = item.CODE.Substring(6),
                        email_durum_bildirimi ="H",
                        musteri_durumu="1",
                    };
                    var result = await _b2BClient.MusteriTransferAsync(musteri);
                    if (result.Code != 0)
                    {
                        throw new Exception(result.Message);
                    }
                }

            }
        }
    }
}
