using Microsoft.IdentityModel.Tokens;
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
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.Services.Erp
{
    public class OpakService(ErpSetting erpSetting, SmartstoreParameter smartstoreParameter)
    {
        private readonly string connectionString = $"Data Source={erpSetting.SqlServer};Initial Catalog={erpSetting.SqlDatabase};Integrated Security=False;Persist Security Info=False;User ID={erpSetting.SqlUser};Password={erpSetting.SqlPassword};Trust Server Certificate=True;";

        public List<OpakMalzeme>? GetMalzemeList(ref string errorMessage)
        {
            var malzemeList = DataReader.ReadData<OpakMalzeme>(connectionString, OpakQuery.GetMalzemeQuery(smartstoreParameter.ProductSync), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }
            foreach (var item in malzemeList)
            {
                if (item.AKTIF == "E")
                {
                    item.MalzemeResimList = DataReader.ReadData<OpakMalzemeResim>(connectionString, OpakQuery.GetMalzemeResimQuery(item.STOK_KODU), ref errorMessage);
                    if (item.VARYANTLIURUN == "E")
                    {
                        item.MalzemeVaryantList = DataReader.ReadData<OpakVaryant>(connectionString, OpakQuery.GetMalzemeVaryantQuery(item.STOK_KODU), ref errorMessage);
                        foreach (var varyant in item.MalzemeVaryantList)
                        {
                            item.MalzemeResimList.AddRange(DataReader.ReadData<OpakMalzemeResim>(connectionString, OpakQuery.GetMalzemeResimQuery(varyant.KOD), ref errorMessage));
                            varyant.MalzemeResimList = DataReader.ReadData<OpakMalzemeResim>(connectionString, OpakQuery.GetMalzemeResimQuery(varyant.KOD), ref errorMessage);
                        }
                    }
                }
            }
            ;
            var pasifMalzemeList = GetPasifMalzemeList(ref errorMessage);
            if (pasifMalzemeList != null)
            {
                malzemeList.AddRange(pasifMalzemeList);
            }

            return malzemeList;
        }

        public List<OpakMalzeme>? GetPasifMalzemeList(ref string errorMessage)
        {
            var malzemeList = DataReader.ReadData<OpakMalzeme>(connectionString, OpakQuery.GetPasifMalzemeQuery(smartstoreParameter.ProductSync), ref errorMessage);
            if (malzemeList == null)
            {
                return null;
            }

            return malzemeList;
        }

        public List<BaseMalzemeStokModel> GetMalzemeStokList(ref string errorMessage)
        {
            List<BaseMalzemeStokModel> malzemeStokList = new List<BaseMalzemeStokModel>();

            var data = DataReader.ReadData<OpakMalzemeStok>(connectionString, OpakQuery.GetMalzemeStokQuery(smartstoreParameter.ProductStockSync), ref errorMessage);

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
                        StokType = item.STOKTYPE,
                        StokKodu = item.STOKKOD,
                        DepoAdi = item.DEPOADI,
                        StokMiktari = Convert.ToInt32(item.BAKIYE),
                    });
                }
            }

            return malzemeStokList;
        }

        public List<BaseMalzemeFiyatModel> GetMalzemeFiyatList(ref string errorMessage)
        {
            List<BaseMalzemeFiyatModel> malzemeFiyatList = new List<BaseMalzemeFiyatModel>();

            var data = DataReader.ReadData<OpakMalzemeFiyat>(connectionString, OpakQuery.GetMalzemeFiyatQuery(smartstoreParameter.ProductPriceSync), ref errorMessage);
            if (data == null)
            {
                return new List<BaseMalzemeFiyatModel>();
            }
            else
            {
                var varyantData = DataReader.ReadData<OpakMalzemeFiyat>(connectionString, OpakQuery.GetVaryantFiyatQuery(), ref errorMessage);

                foreach (var item in data)
                {
                    double fiyat = 0;
                    if (item.STOKTYPE == "S")
                    {
                        if (item.VARYANTLIURUN == "E")
                        {
                            fiyat = varyantData.Where(m => m.STOKTYPE == "V" && m.ANASTOKKOD == item.STOKKOD).OrderBy(m => m.FIYAT).Select(m => m.FIYAT).FirstOrDefault(0);
                        }
                        else
                        {
                            fiyat = item.FIYAT;
                        }
                    }
                    else
                    {
                        fiyat = item.FIYAT;


                        if (!malzemeFiyatList.Where(m => m.StokKodu == item.ANASTOKKOD).Any())
                        {
                            malzemeFiyatList.Add(new BaseMalzemeFiyatModel
                            {
                                StokType = "S",
                                StokKodu = item.ANASTOKKOD,
                                Fiyat = varyantData.Where(m => m.STOKTYPE == "V" && m.ANASTOKKOD == item.ANASTOKKOD).OrderBy(m => m.FIYAT).Select(m => m.FIYAT).FirstOrDefault(0),
                                IndirimliFiyat = 0
                            });
                        }
                    }


                    malzemeFiyatList.Add(new BaseMalzemeFiyatModel
                    {
                        StokType = item.STOKTYPE,
                        StokKodu = item.STOKKOD,
                        Fiyat = fiyat,
                        IndirimliFiyat = 0
                    });
                }
            }

            return malzemeFiyatList;
        }

        public List<OpakSevkiyat> GetSevkiyatList(ref string errorMessage)
        {
            List<OpakSevkiyat> sevkiyatList = new List<OpakSevkiyat>();

            var data = DataReader.ReadData<OpakSevkiyat>(connectionString, OpakQuery.GetSevkiyatQuery(smartstoreParameter.OrderShipmentLastTransfer), ref errorMessage);
            if (data == null)
            {
                return new List<OpakSevkiyat>();
            }
            else
            {
                sevkiyatList = data;
            }

            return sevkiyatList;
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
                        try
                        {
                            string cariKod = item.OrderCustomer == null ? "H01" : string.IsNullOrEmpty(item.OrderCustomer.CustomerNumber) ? "H01" : item.OrderCustomer.CustomerNumber;
                            string odemeTuru = "";
                            if (item.PaymentMethodSystemName == "Payments.CreditCard" || item.PaymentMethodSystemName == "Payments.Iyzico")
                            {
                                if (item.PaymentTransaction == null)
                                {
                                    odemeTuru = $"Kredi Kartı:1";
                                }
                                else
                                {
                                    odemeTuru = $"Kredi Kartı:{item.PaymentTransaction!.Installment}";
                                }
                            }
                            else
                            {
                                odemeTuru = "Nakit";
                            }

                            string aciklama = "";
                            if (!string.IsNullOrEmpty(item.CustomerOrderComment))
                            {
                                aciklama += item.CustomerOrderComment;
                            }

                            if (item.OrderItems.Where(m => m.AttributeDescription.Length > 0).Any())
                            {
                                bool stringAttribute = false;
                                string stringAttributeSku = "";
                                foreach (var orderItem in item.OrderItems)
                                {
                                    if (!orderItem.RawAttributes.IsNullOrEmpty())
                                    {
                                        var data = JsonConvert.DeserializeObject<SmartstoreAttribute>(orderItem.RawAttributes);
                                        foreach (var attribute in data.Attributes)
                                        {
                                            foreach (var value in attribute.Value)
                                            {
                                                int valueInt = 0;
                                                if (!int.TryParse(value, out valueInt))
                                                {
                                                    stringAttribute = true;
                                                    stringAttributeSku = orderItem.Sku;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (stringAttribute)
                                {
                                    if (!aciklama.IsNullOrEmpty())
                                        aciklama += "-";

                                    aciklama += string.Join(",", item.OrderItems.Where(m => m.Sku == stringAttributeSku).Select(m => m.Sku + ":" + WebUtility.HtmlDecode(m.AttributeDescription)).ToArray());
                                }
                            }

                            if (item.ShippingAddress.Address1 != item.BillingAddress.Address1)
                            {
                                if (!aciklama.IsNullOrEmpty())
                                    aciklama += "-";

                                aciklama += $"{item.ShippingAddress.Address1} {item.ShippingAddress.City!.Name}/{item.ShippingAddress.Town!.Name}";
                            }

                            if (!aciklama.IsNullOrEmpty())
                                aciklama += "-";

                            aciklama += odemeTuru;

                            if (!aciklama.IsNullOrEmpty())
                                aciklama += "-";

                            aciklama += item.ShippingMethod;

                            OpakSiparis opakSiparis = new OpakSiparis();
                            opakSiparis.ID = 0;
                            opakSiparis.SUBEID = 1;
                            opakSiparis.DEPOID = 1;
                            opakSiparis.CARIKOD = cariKod;
                            opakSiparis.CARIADI = "";
                            opakSiparis.ALTHESAP = "001"; // 001-HÜNERİŞ 
                            opakSiparis.BELGENO = $"B2C{item.OrderNumber ?? item.Id.ToString()}";
                            opakSiparis.TARIH = item.CreatedOnUtc.ToString("yyyy-MM-dd");
                            opakSiparis.SAAT = item.CreatedOnUtc.ToString("HH:mm:ss");
                            opakSiparis.ACIKLAMA1 = aciklama;
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

                            string adsoyad = item.BillingAddress.FirstName + " " + item.BillingAddress.LastName;
                            if (!string.IsNullOrEmpty(item.BillingAddress.Company))
                            {
                                adsoyad += $" ({item.BillingAddress.Company})";
                            }

                            opakSiparis.ADSOYAD = adsoyad;
                            if (item.BillingAddress != null)
                            {
                                opakSiparis.TEL = item.BillingAddress.PhoneNumber;
                                opakSiparis.FAX = "";
                                opakSiparis.CEPTEL = item.BillingAddress.PhoneNumber;
                                opakSiparis.MAIL = item.BillingAddress.Email;
                                opakSiparis.ADRES = item.BillingAddress.Address1;
                                opakSiparis.ILCE = item.BillingAddress.Town == null ? "" : item.BillingAddress.Town.Name;
                                opakSiparis.IL = item.BillingAddress.City == null ? "" : item.BillingAddress.City.Name;
                                opakSiparis.VERGIDAIRESI = item.BillingAddress.TaxOffice.IsNullOrEmpty() ? "" : item.BillingAddress.TaxOffice;
                                opakSiparis.VERGINO = item.BillingAddress.TaxNumber.IsNullOrEmpty() ? "" : item.BillingAddress.TaxNumber.Length == 10 ? item.BillingAddress.TaxNumber : "";
                                opakSiparis.TCNO = item.BillingAddress.TaxNumber.IsNullOrEmpty() ? "" : item.BillingAddress.TaxNumber.Length == 10 ? "" : item.BillingAddress.TaxNumber;
                            }
                            if (item.PaymentTransaction != null && item.PaymentTransaction.Installment > 1)
                            {
                                opakSiparis.KARGOBEDELI = item.OrderShippingExclTax + item.OrderShippingExclTax / 100 * 7;
                            }
                            else
                            {
                                opakSiparis.KARGOBEDELI = item.OrderShippingExclTax;
                            }

                            foreach (var orderItem in item.OrderItems)
                            {
                                var brutFiyat = orderItem.UnitPriceExclTax;
                                var netFiyat = orderItem.UnitPriceInclTax;

                                if (item.PaymentTransaction != null)
                                {
                                    brutFiyat = item.PaymentTransaction.Installment > 1 ? orderItem.UnitPriceExclTax + orderItem.UnitPriceExclTax / 100 * 7 : orderItem.UnitPriceExclTax;
                                    netFiyat = item.PaymentTransaction.Installment > 1 ? orderItem.UnitPriceInclTax + orderItem.UnitPriceInclTax / 100 * 7 : orderItem.UnitPriceInclTax;
                                }

                                var siparisKalem = new OpakSiparisKalem();
                                siparisKalem.ID = 0;
                                siparisKalem.SIPID = 0;
                                siparisKalem.USTUUID = item.OrderGuid;
                                siparisKalem.KALEMUID = orderItem.OrderItemGuid;
                                siparisKalem.STOKKOD = orderItem.Sku;
                                siparisKalem.STOKADI = "";
                                siparisKalem.KDVORANI = 20;
                                siparisKalem.MIKTAR = orderItem.Quantity;
                                siparisKalem.BRUTFIYAT = brutFiyat;
                                siparisKalem.ISK1 = 0;
                                siparisKalem.ISK2 = 0;
                                siparisKalem.ISK3 = 0;
                                siparisKalem.ISK4 = 0;
                                siparisKalem.ISK5 = 0;
                                siparisKalem.ISK6 = 0;
                                siparisKalem.NETFIYAT = netFiyat;
                                siparisKalem.BIRIM = "ADET";
                                siparisKalem.DOVIZADI = "TL";
                                siparisKalem.TARIH = item.CreatedOnUtc;
                                siparisKalem.KUR = 0;
                                siparisKalem.ACIKLAMA1 = WebUtility.HtmlDecode(orderItem.AttributeDescription);

                                opakSiparis.STOKLISTESI.Add(siparisKalem);
                            }

                            opakSiparis.KALEMSAYISI = opakSiparis.STOKLISTESI.Count;

                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Adı",
                            //    DEGER = "",
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Adı
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Kimlik No",
                            //    DEGER = item.BillingAddress.TaxNumber.IsNullOrEmpty() ? "" : item.BillingAddress.TaxNumber.Length == 10 ? "" : item.BillingAddress.TaxNumber,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Kimlik No
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Vergi No",
                            //    DEGER = item.BillingAddress.TaxNumber.IsNullOrEmpty() ? "" : item.BillingAddress.TaxNumber.Length == 10 ? item.BillingAddress.TaxNumber : "",
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Vergi No
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Soyadı",
                            //    DEGER = "",
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Soyadı
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Vergi Dairesi",
                            //    DEGER = item.BillingAddress.TaxOffice.IsNullOrEmpty() ? "" : item.BillingAddress.TaxOffice,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Vergi Dairesi
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Adres",
                            //    DEGER = item.BillingAddress.Address1,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Adres
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "İlçe",
                            //    DEGER = item.BillingAddress.Town.Name,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//İlçe
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "İl",
                            //    DEGER = item.BillingAddress.City.Name,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//İl
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "E-Mail",
                            //    DEGER = item.BillingAddress.Email,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//E-Mail
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Web Sipariş No",
                            //    DEGER = item.OrderNumber ?? item.Id.ToString(),
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Web Sipariş No
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Adı Soyadı",
                            //    DEGER = item.BillingAddress.FirstName + " " + item.BillingAddress.LastName,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Adı Soyadı
                            //opakSiparis.SIPARISEKPARAM.Add(new OpakSiparisParam()
                            //{
                            //    ID = 0,
                            //    UUID = item.OrderGuid,
                            //    ACIKLAMA = "Telefon",
                            //    DEGER = item.BillingAddress.PhoneNumber,
                            //    ZORUNLU = false,
                            //    TIP = 0
                            //});//Telefon

                            if (item.PaymentMethodSystemName == "Payments.CreditCard" || item.PaymentMethodSystemName == "Payments.Iyzico")
                            {
                                opakSiparis.SIPARISODEME.Add(new OpakSiparisOdeme()
                                {
                                    ID = 0,
                                    ODEMETURU = "Nakit",
                                    BANKAKOD = "9",
                                    TUTAR = item.OrderTotal,
                                    TAKSIT = item.PaymentTransaction == null ? 1 : item.PaymentTransaction.Installment,
                                    AKTARILDIMI = "H",
                                    TARIH = item.PaidDateUtc.HasValue ? item.PaidDateUtc.Value : item.CreatedOnUtc
                                });
                            }

                            opakOrderList.Add(opakSiparis);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                    break;
            }

            return opakOrderList;
        }

        public bool IsSync(int tip, int durum, ref string errorMessage)
        {
            var value = DataReader.GetExecuteScalarToInt(connectionString, OpakQuery.GetSyncCountQuery(tip, durum), ref errorMessage);
            return value > 0;
        }

        public bool UpdateSync(string stok_kodu, int tip, int durum, ref string errorMessage)
        {
            var result = DataReader.ExecuteNonQuery(connectionString, OpakQuery.SetSyncStatus(stok_kodu, tip, durum), ref errorMessage);
            return result > 0;

        }


    }
}
