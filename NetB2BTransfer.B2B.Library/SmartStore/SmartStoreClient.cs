using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Headers;
using NetB2BTransfer.B2B.Library.SmartStore.Models;
using NetB2BTransfer.Core.Entities;
using Newtonsoft.Json;
namespace NetB2BTransfer.B2B.Library.SmartStore
{
    public class SmartStoreClient(B2BSetting b2BSetting)
    {
        public async Task<ResponseSmart<Product>?> GetProduct(string sku)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://localhost:5000/odata/v1/products?count=true&filter=Sku eq '{sku}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmart<Product>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<Product?> ProductTransfer(Product product)
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(product);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:5000/odata/v1/products"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ProductManufacturer?> ProductManufacturerTrasnfer(ProductManufacturer productManufacturer)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(productManufacturer);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:5000/odata/v1/productmanufacturers"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ProductManufacturer>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ProductCategory?> ProductCategoryTrasnfer(ProductCategory productCategory)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(productCategory);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:5000/odata/v1/productcategories"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ProductCategory>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<Manufacturer?> ManufacturerTransfer(Manufacturer manufacturer)
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(manufacturer);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:5000/odata/v1/manufacturers"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<Manufacturer>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<Category?> CategoryTransfer(Category category)
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(category);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:5000/odata/v1/categories"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmart<Category>?> GetCategory(string name)
        {

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://localhost:5000/odata/v1/categories?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmart<Category>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<ResponseSmart<Manufacturer>?> GetManufacturer(string name)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://localhost:5000/odata/v1/manufacturers?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmart<Manufacturer>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmart<ProductManufacturer>?> GetProductManufacturer(int productId,int manufacturerId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://localhost:5000/odata/v1/productmanufacturers?count=true&filter=ProductId eq {productId} and ManufacturerId eq {manufacturerId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmart<ProductManufacturer>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmart<ProductCategory>?> GetProductCategory(int productId, int categoryId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://localhost:5000/odata/v1/productcategories?count=true&filter=ProductId eq {productId} and CategoryId eq {categoryId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmart<ProductCategory>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task GetStores()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "http://localhost:5000/odata/v1/stores?%24count=true"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic NzQ3YThjYzI0Mjc3ZGM3NDJiNjFiNmNmYzZiMTlmYmQ6NzUyOTdhYzQxMDczMGM4MzBmZDkzN2JiMzlhODlmMjc=");

                    var response = await httpClient.SendAsync(request);
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
