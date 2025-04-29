using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Headers;
using NetTransfer.B2B.Library.SmartStore.Models;
using NetTransfer.Core.Entities;
using Newtonsoft.Json;
namespace NetTransfer.B2B.Library.SmartStore
{
    public class SmartStoreClient(VirtualStoreSetting _b2BSetting)
    {
        public async Task<ResponseSmartList<Product>?> GetProduct(string sku)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://erbabogludtm.com/odata/v1/products?count=true&filter=Sku eq '{sku}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<Product>>(result);
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
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://erbabogludtm.com/odata/v1/products"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

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
        public async Task<bool> UpdateProductStock(int productId, int stokQuantity)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"https://erbabogludtm.com/odata/v1/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        StockQuantity = stokQuantity,
                        Id = productId
                    };
                    var json = JsonConvert.SerializeObject(jsonContent);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    // API'ye isteği gönder
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<bool> UpdateProductPrice(int productId, double price,double specialPrice)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"https://erbabogludtm.com/odata/v1/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        Price = price,
                        SpecialPrice = specialPrice,
                        Id = productId
                    };
                    var json = JsonConvert.SerializeObject(jsonContent);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    // API'ye isteği gönder
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<ProductManufacturer?> ProductManufacturerTrasnfer(ProductManufacturer productManufacturer)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(productManufacturer);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://erbabogludtm.com/odata/v1/productmanufacturers"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");
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
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://erbabogludtm.com/odata/v1/productcategories"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");
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
        public async Task<ProductMediaFile?> ProductMediaFileTrasnfer(ProductMediaFile productMediaFile)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(productMediaFile);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://erbabogludtm.com/odata/v1/productmediafiles"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ProductMediaFile>(result);
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
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://erbabogludtm.com/odata/v1/manufacturers"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

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
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://erbabogludtm.com/odata/v1/categories"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

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
        public async Task<ResponseSmartList<Category>?> GetCategory(string name)
        {

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://erbabogludtm.com/odata/v1/categories?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<Category>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<Manufacturer>?> GetManufacturer(string name)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://erbabogludtm.com/odata/v1/manufacturers?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<Manufacturer>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<ProductManufacturer>?> GetProductManufacturer(int productId, int manufacturerId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://erbabogludtm.com/odata/v1/productmanufacturers?count=true&filter=ProductId eq {productId} and ManufacturerId eq {manufacturerId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<ProductManufacturer>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<ProductCategory>?> GetProductCategory(int productId, int categoryId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://erbabogludtm.com/odata/v1/productcategories?count=true&filter=ProductId eq {productId} and CategoryId eq {categoryId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<ProductCategory>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<ProductMediaFile>?> GetProductMediaFile(int productId, int mediaFileId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://erbabogludtm.com/odata/v1/productmediafiles?count=true&filter=ProductId eq {productId} and MediaFileId eq {mediaFileId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<ProductMediaFile>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<FileItemInfo?> MediaFileSave(byte[] file, string fileName, string mimeType)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://erbabogludtm.com/odata/v1/mediafiles/savefile"))
                {
                    // Authorization ve diğer gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    // Dosya içeriğini yüklemek için Multipart form-data
                    var multipartContent = new MultipartFormDataContent();
                    var fileContent = new ByteArrayContent(file);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mimeType);
                    multipartContent.Add(fileContent, "file", fileName);

                    request.Content = multipartContent;

                    // API'ye isteği gönder
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<FileItemInfo>(result);
                    }
                    else
                    {
                        // Hata durumunda null döndür
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmart<bool>?> FileExists(string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://erbabogludtm.com/odata/v1/mediafiles/fileexists"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        path = filePath
                    };
                    var json = JsonConvert.SerializeObject(jsonContent);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    // API'ye isteği gönder
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmart<bool>>(result);
                    }
                    else
                    {
                        // Hata durumunda false döndür
                        return new ResponseSmart<bool>() { value = false };
                    }
                }
            }
        }
        public async Task<FileItemInfo?> GetFileByPath(string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://erbabogludtm.com/odata/v1/mediafiles/getfilebypath"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic YjUwM2ZkYmIyODIzZmY4NGE1NDk4MmIzMGE3MmVhMDU6ZTRmZmM5ODM3MGFmNjVjN2NkZDg0NjJiNDE1NTAyODg=");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        path = filePath
                    };
                    var json = JsonConvert.SerializeObject(jsonContent);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    // API'ye isteği gönder
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<FileItemInfo>(result);
                    }
                    else
                    {
                        // Hata durumunda null döndür
                        return null;
                    }
                }
            }
        }
    }
}
