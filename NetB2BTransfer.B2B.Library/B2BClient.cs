using Azure.Core;
using NetB2BTransfer.B2B.Library.Models;
using NetB2BTransfer.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.B2B.Library
{
    public class B2BClient
    {
        private readonly B2BSetting _b2BSetting;
        private string _accessToken;
        public B2BClient(B2BSetting b2BSetting)
        {
            _b2BSetting = b2BSetting;
        }

        public async Task<Person?> GetAccessTokenAsync()
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
                        Person? result = JsonConvert.DeserializeObject<Person>(value: value);


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
        public async Task<ResponseData?> MusteriTransferAsync(Musteri musteri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(new List<Musteri>() { musteri });
                var response = await client.PostAsync(_b2BSetting.Url + "/entegrasyon/musteriler/tanim", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string value = await response.Content.ReadAsStringAsync();
                    ResponseData? result = JsonConvert.DeserializeObject<ResponseData>(value: value);


                    return result;
                }

                return null;
            }
        }
    }

}