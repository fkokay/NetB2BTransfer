using Azure.Core;
using Microsoft.Data.SqlClient;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Entities;
using Newtonsoft.Json;
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
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(txtUrl.Text);
                    var formData = new MultipartFormDataContent
                    {
                        { new StringContent(_b2BSetting.User), "kullanici_adi" },
                        { new StringContent(_b2BSetting.Password), "sifre" }
                    };

                    var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/giris", formData);
                    if (response.IsSuccessStatusCode)
                    {
                        string value = await response.Content.ReadAsStringAsync();
                        B2BPerson? result = JsonConvert.DeserializeObject<B2BPerson>(value: value);


                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void SetAccessToken(string token)
        {
            _accessToken = token;
        }   
        public async Task<B2BResponse?> MusteriTransferAsync(B2BMusteri musteri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(new List<B2BMusteri>() { musteri });
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/musteriler/tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }

                return null;
            }
        }
        public async Task<B2BResponse?> MusteriBakiyeTransferAsync(List<B2BMusteriBakiye> musteriBakiyeleri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(musteriBakiyeleri);
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/musteriler/cari_bakiye_tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }

                return null;
            }
        }
        public async Task<B2BResponse?> UrunTransferAsync(B2BUrun urun)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(urun);
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/ekle", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }

                return null;
            }
        }
        public async Task<B2BResponse?> UrunStokTransferAsync(B2BDepoMiktar depoMiktar)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(depoMiktar);
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/toplu/depo_miktar/tanimlama", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }

                return null;
            }
        }
        public async Task<B2BResponse?> UrunFiyatTrasnferAsync(B2BUrunFiyat b2BUrunFiyat)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(b2BUrunFiyat);
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/fiyat_listeleri/tanimlama", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }

                return null;
            }
        }
        public async Task<B2BResponseList<B2BSiparis>?> Siparisler(int siparisDurumu)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(_b2BSetting.Url + $"/entegrasyon/siparisler/list?siparis_durumu={siparisDurumu}");
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponseList<B2BSiparis>? result = JsonConvert.DeserializeObject<B2BResponseList<B2BSiparis>>(value: value);


                    return result;
                }

                return null;
            }
        }
        public async Task<B2BResponseSiparisDetay?> SiparisDetay(int sip_id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(_b2BSetting.Url + $"/entegrasyon/siparisler/detay/{sip_id}");
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponseSiparisDetay? result = JsonConvert.DeserializeObject<B2BResponseSiparisDetay>(value: value);

                    

                    return result;
                }

                return null;
            }
        }
    }

}