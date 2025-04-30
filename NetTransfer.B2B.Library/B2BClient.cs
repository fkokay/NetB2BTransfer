using Azure.Core;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task<B2BResponse?> MusteriBakiyeTransferAsync(List<B2BUrun> urunler)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(urunler);
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/urunler/toplu/urun/tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    B2BResponse? result = JsonConvert.DeserializeObject<B2BResponse>(value: value);


                    return result;
                }

                return null;
            }
        }
    }

}