using NetTransfer.Core.Interfaces;
using NetTransfer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.CommerceWriters.SmartStore
{
    public class SmartStoreProductWriter : ICommerceProductWriter
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey; // Token ya da Basic Auth olabilir

        public SmartStoreProductWriter(HttpClient httpClient, string apiBaseUrl, string apiKey)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apiBaseUrl.TrimEnd('/');
            _apiKey = apiKey;
        }

        public Task<bool> SendProductsAsync(List<ProductDto> products)
        {
            throw new NotImplementedException();
        }
    }
}
