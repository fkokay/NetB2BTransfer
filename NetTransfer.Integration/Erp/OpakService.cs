using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Integration.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using NetTransfer.Opak.Library.Class;
using NetTransfer.Opak.Library.Models;
using NetTransfer.Smartstore.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
                    foreach (var varyant in item.MalzemeVaryantList)
                    {
                        item.MalzemeResimList.AddRange(DataReader.ReadData<OpakMalzemeResim>(connectionString, OpakQuery.GetMalzemeResimQuery(varyant.KOD), ref errorMessage));
                        varyant.MalzemeResimList = DataReader.ReadData<OpakMalzemeResim>(connectionString, OpakQuery.GetMalzemeResimQuery(varyant.KOD), ref errorMessage);
                    }
                }
            }

            return malzemeList;
        }

        public List<BaseMalzemeStokModel> GetMalzemeStokList(ref string errorMessage)
        {
            List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();

            var data = DataReader.ReadData<OpakMalzeme>(connectionString, OpakQuery.GetMalzemeStokQuery(), ref errorMessage);

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
                        StokMiktari = item.MIKTAR,
                    });
                }
            }

            return malzemeStokList;
        }

        public List<BaseMalzemeFiyatModel> GetMalzemeFiyatList(ref string errorMessage)
        {
            List<BaseMalzemeFiyatModel> malzemeFiyatList = new List<BaseMalzemeFiyatModel>();

            var data = DataReader.ReadData<OpakMalzeme>(connectionString, OpakQuery.GetMalzemeFiyatQuery(), ref errorMessage);
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
                        Fiyat = item.SATIS_FIAT1,
                        IndirimliFiyat = 0
                    });
                }
            }

            return malzemeFiyatList;
        }

        public async Task<OpakResponse?> SaveOrderAsync(OpakSiparis order)
        {
            try
            {
                var json = JsonConvert.SerializeObject(order);

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"http://localhost:8188/api/siparis/save"))
                    {
                        request.Headers.TryAddWithoutValidation("accept", "application/json");

                        request.Content = new StringContent(json);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var response = await httpClient.SendAsync(request);
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<OpakResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OpakSiparis>? MappingOrder(VirtualStoreSetting b2bSetting, object? orderList)
        {
            List<OpakSiparis>? opakOrderList = new List<OpakSiparis>();

            if (orderList == null)
                return opakOrderList;

            switch (b2bSetting.VirtualStore)
            {
                case "B2B":
                    break;
                case "Smartstore":
                    foreach (var item in orderList as List<SmartstoreOrder>)
                    {
                        string cariKod = string.IsNullOrEmpty(item.OrderCustomer.CustomerNumber) ? "H01" : item.OrderCustomer.CustomerNumber;

                        OpakSiparis opakSiparis = new OpakSiparis();
                        opakSiparis.ID = 0;
                        opakSiparis.SUBEID = 1;
                        opakSiparis.DEPOID = 1;
                        opakSiparis.CARIKOD = cariKod;
                        opakSiparis.CARIADI = "";
                        opakSiparis.ALTHESAP = "001";
                        opakSiparis.BELGENO = item.OrderNumber ?? item.Id.ToString();
                        opakSiparis.TARIH = item.CreatedOnUtc.ToString("yyyy-MM-dd");
                        opakSiparis.SAAT = item.CreatedOnUtc.ToString("HH:mm:ss");
                        opakSiparis.ACIKLAMA1 = item.CustomerOrderComment ?? "";
                        opakSiparis.ACIKLAMA2 = "";
                        opakSiparis.ACIKLAMA3 = "";
                        opakSiparis.ACIKLAMA4 = "";
                        opakSiparis.ACIKLAMA5 = "";
                        opakSiparis.VADEGUNU = 0;
                        opakSiparis.VADETARIHI = item.CreatedOnUtc.ToString("yyyy-MM-dd");
                        opakSiparis.KDVDAHIL = "E";
                        opakSiparis.KUR = 0;
                        opakSiparis.ALTISKORAN = 0;
                        opakSiparis.ALTISKTUTAR = 0;
                        opakSiparis.AKTARILDIMI = "H";
                        opakSiparis.UUID = item.OrderGuid;
                        opakSiparis.ISLEMTIPI = 0;
                        opakSiparis.PLASIYERKOD = "";
                        opakSiparis.TESLIMTARIHI = item.CreatedOnUtc.ToString("yyyy-MM-dd");
                        opakSiparis.ADSOYAD = item.BillingAddress.FirstName + " " + item.BillingAddress.LastName;
                        opakSiparis.TEL = item.BillingAddress.PhoneNumber;
                        opakSiparis.FAX = "";
                        opakSiparis.CEPTEL = item.BillingAddress.PhoneNumber;
                        opakSiparis.MAIL = item.BillingAddress.Email;
                        opakSiparis.ADRES = item.BillingAddress.Address1 + " " + item.BillingAddress.Address2;
                        opakSiparis.ILCE = "";
                        opakSiparis.IL = item.BillingAddress.City;
                        opakSiparis.VERGIDAIRESI = "";
                        opakSiparis.VERGINO = "";
                        opakSiparis.TCNO = "";
                        opakSiparis.KARGOBEDELI = 0;

                        foreach (var orderItem in item.OrderItems)
                        {
                            var siparisKalem = new OpakSiparisKalem();
                            siparisKalem.ID = 0;
                            siparisKalem.SIPID = 0;
                            siparisKalem.USTUUID = item.OrderGuid;
                            siparisKalem.KALEMUID = orderItem.OrderItemGuid;
                            siparisKalem.STOKKOD = orderItem.Sku;
                            siparisKalem.STOKADI = "";
                            siparisKalem.KDVORANI = 20;
                            siparisKalem.MIKTAR = orderItem.Quantity;
                            siparisKalem.BRUTFIYAT = orderItem.UnitPriceExclTax;
                            siparisKalem.ISK1 = 0;
                            siparisKalem.ISK2 = 0;
                            siparisKalem.ISK3 = 0;
                            siparisKalem.ISK4 = 0;
                            siparisKalem.ISK5 = 0;
                            siparisKalem.ISK6 = 0;
                            siparisKalem.NETFIYAT = orderItem.UnitPriceInclTax;
                            siparisKalem.BIRIM = "ADET";
                            siparisKalem.DOVIZADI = "TL";
                            siparisKalem.TARIH = item.CreatedOnUtc;
                            siparisKalem.KUR = 0;
                            siparisKalem.ACIKLAMA1 = "";

                            opakSiparis.STOKLISTESI.Add(siparisKalem);
                        }

                        opakSiparis.KALEMSAYISI = opakSiparis.STOKLISTESI.Count;

                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Adı",
                            DEGER = "",
                            ZORUNLU = false,
                            TIP = 0
                        });//Adı
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Kimlik No",
                            DEGER = "",
                            ZORUNLU = false,
                            TIP = 0
                        });//Kimlik No
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Vergi No",
                            DEGER = "",
                            ZORUNLU = false,
                            TIP = 0
                        });//Vergi No
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Soyadı",
                            DEGER = "",
                            ZORUNLU = false,
                            TIP = 0
                        });//Soyadı
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Vergi Dairesi",
                            DEGER = "",
                            ZORUNLU = false,
                            TIP = 0
                        });//Vergi Dairesi
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Adres",
                            DEGER = item.BillingAddress.Address1 + " " + item.BillingAddress.Address2,
                            ZORUNLU = false,
                            TIP = 0
                        });//Adres
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "İlçe",
                            DEGER = "",
                            ZORUNLU = false,
                            TIP = 0
                        });//İlçe
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "İl",
                            DEGER = item.BillingAddress.City,
                            ZORUNLU = false,
                            TIP = 0
                        });//İl
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "E-Mail",
                            DEGER = item.BillingAddress.Email,
                            ZORUNLU = false,
                            TIP = 0
                        });//E-Mail
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Web Sipariş No",
                            DEGER = item.OrderNumber ?? item.Id.ToString(),
                            ZORUNLU = false,
                            TIP = 0
                        });//Web Sipariş No
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Adı Soyadı",
                            DEGER = item.BillingAddress.FirstName + " " + item.BillingAddress.LastName,
                            ZORUNLU = false,
                            TIP = 0
                        });//Adı Soyadı
                        opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                        {
                            ID = 0,
                            UUID = item.OrderGuid,
                            ACIKLAMA = "Telefon",
                            DEGER = item.BillingAddress.PhoneNumber,
                            ZORUNLU = false,
                            TIP = 0
                        });//Telefon

                        opakSiparis.SIPARISODEME.Add(new OpakSiparisOdeme()
                        {
                            ID = 0,
                            ODEMETURU = "Nakit",
                            BANKAKOD = "9",
                            TUTAR = item.OrderTotal,
                            TAKSIT = 0,
                            AKTARILDIMI = "H",
                            TARIH = item.PaidDateUtc.HasValue ? item.PaidDateUtc.Value : item.CreatedOnUtc
                        });

                        opakOrderList.Add(opakSiparis);
                    }

                    break;
            }

            return opakOrderList;
        }
    }
}
