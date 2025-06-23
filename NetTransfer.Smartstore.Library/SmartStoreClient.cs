using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Headers;
using NetTransfer.Core.Entities;
using Newtonsoft.Json;
using NetTransfer.Smartstore.Library.Models;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Http.Json;
using static Azure.Core.HttpHeader;
namespace NetTransfer.Smartstore.Library
{
    public class SmartStoreClient(VirtualStoreSetting _b2BSetting)
    {
        public async Task<ResponseSmartList<SmartstoreProduct>?> GetProduct(string sku)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/products?count=true&filter=Sku eq '{sku}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProduct>>(json);
                    if (result != null)
                    {
                        result.status = response.IsSuccessStatusCode;
                    }

                    return result;
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProduct>?> GetProducts(List<string> skus)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/products?count=true&select=Id,Sku&filter=Sku in({string.Join(",", skus.Select(n => $"'{Uri.EscapeDataString(n)}'"))}) eq true"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProduct>>(json);
                    if (result != null)
                    {
                        result.status = response.IsSuccessStatusCode;
                    }

                    return result;
                }
            }
        }
        public async Task<SmartstoreProduct?> ProductTransfer(SmartstoreProduct product)
        {

            var json = JsonConvert.SerializeObject(product);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/products"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProduct>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<bool> UpdateProductPublished(int productId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        Published = false,
                        UpdatedOnUtc = DateTime.Now
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
        public async Task<bool> UpdateProductStock(int productId, int stokQuantity)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        StockQuantity = stokQuantity,
                        UpdatedOnUtc = DateTime.Now
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
        public async Task<bool> UpdateProductPrice(int productId, double price)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        Price = price,
                        UpdatedOnUtc = DateTime.Now
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
        public async Task<bool> UpdateProductVariantAttributeCombinationPrice(int productVariantAttributeCombinationId, double price)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/productvariantattributecombinations({productVariantAttributeCombinationId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        Price = price
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
        public async Task<bool> UpdateProductVariantAttributeCombinationStok(int productVariantAttributeCombinationId, int stockQuantity)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/productvariantattributecombinations({productVariantAttributeCombinationId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        StockQuantity = stockQuantity
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
        public async Task<bool> UpdateProductPriceOrSpecialPrice(int productId, double price, double specialPrice)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var jsonContent = new
                    {
                        Price = price,
                        SpecialPrice = specialPrice,
                        UpdatedOnUtc = DateTime.Now
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
        public async Task<bool> UpdateProduct(SmartstoreUpdateProduct updateProduct, int productId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/products({productId})"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // JSON içeriği
                    var json = JsonConvert.SerializeObject(updateProduct);

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
        public async Task<SmartstoreProductManufacturer?> ProductManufacturerTrasnfer(SmartstoreProductManufacturer productManufacturer)
        {
            var json = JsonConvert.SerializeObject(productManufacturer);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productmanufacturers"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductManufacturer>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductCategory?> ProductCategoryTrasnfer(SmartstoreProductCategory productCategory)
        {
            var json = JsonConvert.SerializeObject(productCategory);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productcategories"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductCategory>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductMediaFile?> ProductMediaFileTrasnfer(SmartstoreProductMediaFile productMediaFile)
        {
            var json = JsonConvert.SerializeObject(productMediaFile);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productmediafiles"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductMediaFile>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreManufacturer?> ManufacturerTransfer(SmartstoreManufacturer manufacturer)
        {

            var json = JsonConvert.SerializeObject(manufacturer);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/manufacturers"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreManufacturer>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreCategory?> CategoryTransfer(SmartstoreCategory category)
        {
            var json = JsonConvert.SerializeObject(category);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/categories"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreCategory>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreCategory>?> GetCategory(int? parentId, string name)
        {

            using (var httpClient = new HttpClient())
            {
                string filter = parentId.HasValue ? $"parentId eq {parentId.Value} and Name eq '{name}'" : $"Name eq '{name}'";
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/categories?count=true&filter={filter}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreCategory>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreManufacturer>?> GetManufacturer(string name)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/manufacturers?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreManufacturer>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductManufacturer>?> GetProductManufacturer(int productId, int manufacturerId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productmanufacturers?count=true&filter=ProductId eq {productId} and ManufacturerId eq {manufacturerId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductManufacturer>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductCategory>?> GetProductCategory(int productId, int categoryId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productcategories?count=true&filter=ProductId eq {productId} and CategoryId eq {categoryId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductCategory>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductMediaFile>?> GetProductMediaFile(int productId, int mediaFileId)
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productmediafiles?count=true&filter=ProductId eq {productId} and MediaFileId eq {mediaFileId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductMediaFile>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductAttribute>?> GetProductAttribute(string name)
        {

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productattributes?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductAttribute>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductAttribute?> ProductAttributeTransfer(SmartstoreProductAttribute productAttribute)
        {
            var json = JsonConvert.SerializeObject(productAttribute);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productattributes"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductAttribute>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreFileItemInfo?> MediaFileSave(SmartstoreFile smartstoreFile)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, $"{_b2BSetting.Url}/mediafiles/savefile"))
                {
                    // Authorization ve diğer gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    // Dosya içeriğini yüklemek için Multipart form-data
                    var multipartContent = new MultipartFormDataContent();
                    var fileContent = new ByteArrayContent(smartstoreFile.File);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(smartstoreFile.MimeType);
                    multipartContent.Add(fileContent, "file", smartstoreFile.FileName);
                    multipartContent.Add(new StringContent(smartstoreFile.FileName, Encoding.UTF8), "path");


                    request.Content = multipartContent;

                    // API'ye isteği gönder
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreFileItemInfo>(result);
                    }
                    else
                    {
                        // Hata durumunda null döndür
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreResponse<bool>?> FileExists(string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, $"{_b2BSetting.Url}/mediafiles/fileexists"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

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
                        return JsonConvert.DeserializeObject<SmartstoreResponse<bool>>(result);
                    }
                    else
                    {
                        // Hata durumunda false döndür
                        return new SmartstoreResponse<bool>() { value = false };
                    }
                }
            }
        }
        public async Task<SmartstoreFileItemInfo?> GetFileByPath(string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, $"{_b2BSetting.Url}/mediafiles/getfilebypath"))
                {
                    // Gerekli başlıklar
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

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
                        return JsonConvert.DeserializeObject<SmartstoreFileItemInfo>(result);
                    }
                    else
                    {
                        // Hata durumunda null döndür
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductVariantAttribute>?> GetProductVariantAttribute(int productId, int productAttributeId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productvariantattributes?count=true&filter=ProductId eq {productId} and ProductAttributeId eq {productAttributeId}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductVariantAttribute>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductVariantAttribute>?> GetProductVariantAttributes(int productId, List<int> productAttributeIds)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productvariantattributes?count=true&filter=ProductId eq {productId} and ProductAttributeId in ({string.Join(",", productAttributeIds)}) eq false"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductVariantAttribute>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<bool> DeleteProductVariantAttribute(int productVariantAttributeId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"{_b2BSetting.Url}/productvariantattributes({productVariantAttributeId})"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<SmartstoreProductVariantAttribute?> ProductVariantAttributeUpdate(SmartstoreProductVariantAttribute productVariantAttribute)
        {
            var jsonContent = new
            {
                productVariantAttribute.DisplayOrder
            };

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/productvariantattributes({productVariantAttribute.Id})"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var json = JsonConvert.SerializeObject(jsonContent);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductVariantAttribute>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<SmartstoreProductVariantAttribute?> ProductVariantAttributeTransfer(SmartstoreProductVariantAttribute productVariantAttribute)
        {
            var json = JsonConvert.SerializeObject(productVariantAttribute);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productvariantattributes"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductVariantAttribute>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductTag>?> GetProductTag(string name)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/producttags?count=true&filter=Name eq '{name}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductTag>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductTag?> ProductTagTransfer(SmartstoreProductTag productTag)
        {
            var json = JsonConvert.SerializeObject(productTag);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/producttags"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductTag>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductVariantAttributeValue>?> GetProductVariantAttributeValue(int productVariantAttributeId, string name)
        {
            using (var httpClient = new HttpClient())
            {


                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productvariantattributevalues?count=true&filter=ProductVariantAttributeId eq {productVariantAttributeId} and Name eq '{Uri.EscapeDataString(name)}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductVariantAttributeValue>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductVariantAttributeValue>?> GetProductVariantAttributeValues(int productVariantAttributeId, List<string> names)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productvariantattributevalues?count=true&filter=ProductVariantAttributeId eq {productVariantAttributeId} and Name in({string.Join(",", names.Select(n => $"'{Uri.EscapeDataString(n)}'"))}) eq false"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductVariantAttributeValue>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<bool> DeleteProductVariantAttributeValue(int productVariantAttributeValueId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"{_b2BSetting.Url}/productvariantattributevalues({productVariantAttributeValueId})"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<SmartstoreProductVariantAttributeValue?> ProductVariantAttributeValueTransfer(SmartstoreProductVariantAttributeValue productVariantAttributeValue)
        {
            var json = JsonConvert.SerializeObject(productVariantAttributeValue);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productvariantattributevalues"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductVariantAttributeValue>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductVariantAttributeValue?> ProductVariantAttributeValueUpdate(SmartstoreProductVariantAttributeValue productVariantAttributeValue)
        {
            var jsonContent = new
            {
                productVariantAttributeValue.ValueTypeId,
                productVariantAttributeValue.DisplayOrder
            };

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/productvariantattributevalues({productVariantAttributeValue.Id})"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var json = JsonConvert.SerializeObject(jsonContent);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductVariantAttributeValue>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<ResponseSmartList<SmartstoreProductVariantAttributeCombination>?> GetProductVariantAttributeCombinationSku(string sku)
        {
            using (var httpClient = new HttpClient())
            {


                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productvariantattributecombinations?count=true&filter=Sku eq '{sku}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductVariantAttributeCombination>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductVariantAttributeCombination>?> GetProductVariantAttributeCombination(int productId, int hashCode)
        {
            using (var httpClient = new HttpClient())
            {


                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/productvariantattributecombinations?count=true&filter=ProductId eq {productId} and HashCode eq {hashCode}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductVariantAttributeCombination>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductVariantAttributeCombination?> ProductVariantAttributeCombinationsTransfer(SmartstoreProductVariantAttributeCombination productVariantAttributeValue)
        {
            var json = JsonConvert.SerializeObject(productVariantAttributeValue);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/productvariantattributecombinations"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductVariantAttributeCombination>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreProductVariantAttributeCombination?> ProductVariantAttributeCombinationsUpdate(SmartstoreProductVariantAttributeCombination productVariantAttributeValue)
        {
            var json = JsonConvert.SerializeObject(productVariantAttributeValue);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_b2BSetting.Url}/productvariantattributecombinations({productVariantAttributeValue.Id})"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreProductVariantAttributeCombination>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreProductTag>> UpdateProductTags(int productId, SmartstoreProductTagMapping productTagMapping)
        {
            var json = JsonConvert.SerializeObject(productTagMapping);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/products({productId})/updateproducttags"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreProductTag>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreOrder>?> GetOrders(int orderStatusId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders?count=true&filter=orderStatusId eq 10 or orderStatusId eq 20"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreOrder>>(json);
                        result!.status = true;
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreOrder>?> GetOrdersWithOrderGuid(string orderGuid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders?count=true&filter=OrderGuid eq '{orderGuid}'"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreOrder>>(json);
                        result!.status = true;
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreOrderItem>?> GetOrderItems(int orderId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders({orderId})/orderitems"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreOrderItem>>(json);
                        result!.status = true;

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreBillingAddress?> GetOrderBillingAddress(int orderId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders({orderId})/billingaddress?expand={Uri.EscapeDataString("City,Town,District")}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreBillingAddress>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreShippingAddress?> GetOrderShippingAddress(int orderId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders({orderId})/shippingaddress?expand={Uri.EscapeDataString("City,Town,District")}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreShippingAddress>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreCustomer?> GetOrderCustomer(int orderId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders({orderId})/customer"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreCustomer>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<SmartstoreShipment?> AddShipment(SmartstoreAddShipment shipment, int orderId, string carrier)
        {
            var json = JsonConvert.SerializeObject(shipment);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{_b2BSetting.Url}/orders({orderId})/addshipment?carrier={Uri.EscapeDataString(carrier)}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SmartstoreShipment>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public async Task<ResponseSmartList<SmartstoreShipment>?> GetShipment(int orderId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/orders({orderId})/shipments"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseSmartList<SmartstoreShipment>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<SmartstorePaymentTransaction>?> GetPaymentTransaction(string orderNumber)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{_b2BSetting.Url}/paymenttransactions?count=false&filter=OrderNumber eq {orderNumber}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_b2BSetting.User}:{_b2BSetting.Password}"))}");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<SmartstorePaymentTransaction>>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
