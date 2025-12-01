using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library
{
    public class B2BClient
    {
        private readonly VirtualStoreSetting _b2BSetting;
        private string _accessToken;
        public B2BClient(VirtualStoreSetting b2BSetting)
        {
            _b2BSetting = b2BSetting;
        }

        public async Task<B2BPerson?> GetAccessTokenAsync()
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                using (var client = new HttpClient(handler))
                {
                    //client.BaseAddress = new Uri(txtUrl.Text);
                    var formData = new MultipartFormDataContent
                    {
                        { new StringContent(_b2BSetting.User), "kullanici_adi" },
                        { new StringContent(_b2BSetting.Password), "sifre" }
                    };

                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/giris", formData);
                    string value = await response.Content.ReadAsStringAsync();
                    B2BPerson? result = JsonConvert.DeserializeObject<B2BPerson>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void SetAccessToken(string token)
        {
            _accessToken = token;
        }
        public bool IsAccessToken()
        {
            return string.IsNullOrEmpty(_accessToken) ? false : true;
        }
        public async Task<B2BResponse?> MusteriTransferAsync(B2BMusteri musteri)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = @" {  ""data"": [ " + JsonConvert.SerializeObject(new List<B2BMusteri>() { musteri }) + " ] }  ";
                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/musteriler/tanim", new StringContent(json, Encoding.UTF8, "application/json"));

                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<B2BResponse?> MusteriTopluTransferAsync(List<B2BMusteri> musteriAll)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = @" {  ""data"":  " + JsonConvert.SerializeObject(musteriAll) + "  }  ";
                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/musteriler/tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<B2BResponse?> MusteriBakiyeTransferAsync(List<B2BMusteriBakiye> musteriBakiyeleri)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = @"{""data"" :" + JsonConvert.SerializeObject(musteriBakiyeleri) + "}";
                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/musteriler/cari_bakiye_tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<B2BResponse?> UrunTopluTransferAsync(List<B2BUrun> urunler)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.SslProtocols =
         System.Security.Authentication.SslProtocols.Tls12 |
         System.Security.Authentication.SslProtocols.Tls13;
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = @" {  ""data"":  " + JsonConvert.SerializeObject(urunler) + "  }  ";

                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/toplu/urun/tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<B2BResponse?> UrunResimTransferAsync(B2BUrunResim b2BUrunResim)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;
                using (var client = new HttpClient(handler))
                {
                    if (!IsAccessToken())
                    {
                       var token = await GetAccessTokenAsync();
                        _accessToken = token?.token;
                    }
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var multipartContent = new MultipartFormDataContent();
                    var fileContent = new ByteArrayContent(b2BUrunResim.Image);
                    multipartContent.Add(fileContent, "resimler[]", "image.png");
                    string[] urunKodlari = { b2BUrunResim.UrunKodu };

                    multipartContent.Add(new StringContent(JsonConvert.SerializeObject(urunKodlari, Formatting.Indented)), "urun_kodlari");

                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/toplu/resim/yukleme", multipartContent);
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<B2BResponse?> UrunStokTransferAsync(B2BDepoMiktar depoMiktar)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = JsonConvert.SerializeObject(depoMiktar);
                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/toplu/depo_miktar/tanimlama", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<B2BResponse?> UrunFiyatTrasnferAsync(B2BUrunFiyat b2BUrunFiyat)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = JsonConvert.SerializeObject(b2BUrunFiyat);
                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/fiyat_listeleri/tanimlama", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<B2BResponseList<B2BSiparis>?> Siparisler(int siparisDurumu)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_b2BSetting.Url + $"/entegrasyon/siparisler/list?siparis_durumu={siparisDurumu}");
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponseList<B2BSiparis>? result = JsonConvert.DeserializeObject<B2BResponseList<B2BSiparis>>(value: value);


                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<B2BResponseSiparisDetay?> SiparisDetay(int sip_id)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_b2BSetting.Url + $"/entegrasyon/siparisler/detay/{sip_id}");
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponseSiparisDetay? result = JsonConvert.DeserializeObject<B2BResponseSiparisDetay>(value: value);



                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<B2BResponse?> SiparisDurumGunncelle(int sip_id, string siparis_no)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var data = new JObject(
                     new JProperty("siparis_id", sip_id),
                     new JProperty("siparis_durum_id", 3),
                      new JProperty("email_bildirimi", "H"),
                      new JProperty("icerik", siparis_no)
                                                );

                    string json = data.ToString(Newtonsoft.Json.Formatting.None);

                    var response = await client.PostAsync(_b2BSetting.Url + $"/entegrasyon/siparisler/durum/guncelle", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);



                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<B2BResponseList<B2BOdeme>?> Odemeler()
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_b2BSetting.Url + $"/entegrasyon/odemeler/list?durum=basarili&erp_aktarma_durumu=0");
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponseList<B2BOdeme>? result = JsonConvert.DeserializeObject<B2BResponseList<B2BOdeme>>(value: value);

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<B2BResponse?> OdemeDurumGunncelle(string odeme_no, bool durum)
        {
            try
            {
                var handler = new SocketsHttpHandler
                {
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    }
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var data = new JObject(
                        new JProperty("data",
                            new JArray(
                                new JObject(
                                    new JProperty("odeme_no", odeme_no),
                                    new JProperty("durum", durum)
                                )
                            )
                        )
                    );



                    string json = data.ToString(Newtonsoft.Json.Formatting.None);

                    var response = await client.PostAsync(_b2BSetting.Url + $"/entegrasyon/odemeler/erp_aktarma_durumu", new StringContent(json, Encoding.UTF8, "application/json"));
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);



                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}