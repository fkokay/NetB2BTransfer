using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Integration.Models;
using NetTransfer.Logo.Library.Models;
using NetTransfer.Netsis.Library.Class;
using NetTransfer.Netsis.Library.Models;
using NetTransfer.Opak.Library.Models;
using NetTransfer.Smartstore.Library;
using NetTransfer.Smartstore.Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetTransfer.Integration.VirtualStore
{
    public class SmartstoreService
    {
        private readonly ILogger _logger;
        private readonly SmartStoreClient _smartStoreClient;
        public SmartstoreService(ILogger logger, SmartStoreClient smartStoreClient)
        {
            _logger = logger;
            _smartStoreClient = smartStoreClient;
        }

        public async Task<bool> ProductUpdateWithNotPublished(List<string> productSkus)
        {
            bool result = true;

            int count = PaginationBuilder.GetPageCount(productSkus, 50);
            for (int i = 1; i <= count; i++)
            {
                var skus = PaginationBuilder.GetPage(productSkus, i, 50).ToList();
                var products = await _smartStoreClient.GetProducts(skus);
                if (products != null)
                {
                    foreach (var item in products.value)
                    {
                        if (!await _smartStoreClient.UpdateProductPublished(item.Id))
                        {
                            _logger.LogError($"Ürün güncellenmedi - UpdateProductPublished : {item.Id} - {item.Sku}");
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public async Task<SmartstoreProduct?> CreateProduct(SmartstoreProduct product)
        {
            #region Product
            _logger.LogInformation($"Ürün transferi başladı : {product.Name}");

            SmartstoreProduct? result = null;

            if (product.Id > 0)
            {
                result = product;

                SmartstoreUpdateProduct updateProduct = new SmartstoreUpdateProduct();
                updateProduct.Name = product.Name;
                updateProduct.ShortDescription = product.ShortDescription;
                updateProduct.FullDescription = product.FullDescription;
                //updateProduct.StockQuantity = product.StockQuantity;
                //updateProduct.Price = product.Price;
                updateProduct.ShowOnHomePage = product.ShowOnHomePage;
                updateProduct.Weight = product.Weight;
                updateProduct.ManageInventoryMethodId = product.ManageInventoryMethodId;
                updateProduct.Published = product.Published;
                updateProduct.UpdatedOnUtc = DateTime.UtcNow;

                await _smartStoreClient.UpdateProduct(updateProduct, result.Id);

            }
            else
            {
                if (product.Published)
                {
                    result = await _smartStoreClient.ProductTransfer(product);

                    _logger.LogError("ProductTransfer Is Null");
                }
            }

            if (result == null)
            {
                return null;
            }
            #endregion

            #region Manufacturer
            SmartstoreManufacturer? manufacturer = null;
            if (!string.IsNullOrEmpty(product.ManufacturerName))
            {
                manufacturer = await CreateManufacturer(MappingManufacturer(product.ManufacturerName));
            }

            if (manufacturer != null)
            {
                var productManufacturersResult = await _smartStoreClient.GetProductManufacturer(result.Id);
                if (productManufacturersResult != null)
                {
                    foreach (var item in productManufacturersResult.value)
                    {
                        if (item.ManufacturerId != manufacturer.Id)
                        {
                            _ = await _smartStoreClient.DeleteProductManufacturer(item.Id);
                        }
                    }
                }

                SmartstoreProductManufacturer smartstoreProductManufacturer = new SmartstoreProductManufacturer();
                smartstoreProductManufacturer.ProductId = result.Id;
                smartstoreProductManufacturer.ManufacturerId = manufacturer.Id;
                smartstoreProductManufacturer.IsFeaturedProduct = false;
                smartstoreProductManufacturer.DisplayOrder = 0;

                _ = await CreateProductManufacturer(smartstoreProductManufacturer);
            }
            else
            {
                _ = await DeleteProductManufacturer(result.Id);
            }
            #endregion

            #region Category
            int? parentId = null;
            List<SmartstoreCategory> categories = new List<SmartstoreCategory>();
            foreach (var item in product.Categories)
            {
                var resultCateogry = await CreateCategory(MappingCategory(item, parentId));
                if (resultCateogry != null)
                {
                    parentId = resultCateogry.Id;
                    categories.Add(resultCateogry);
                }
            }
            categories.Reverse();
            foreach (var item in categories)
            {
                SmartstoreProductCategory data = new SmartstoreProductCategory();
                data.ProductId = result.Id;
                data.CategoryId = item.Id;
                data.IsFeaturedProduct = false;
                data.DisplayOrder = 0;
                data.IsSystemMapping = false;
                data.Id = 0;

                var resultProductCategory = await CreateProductCategory(data);
            }

            SmartstoreCategory? category1 = null;
            if (!string.IsNullOrEmpty(product.Category1))
            {
                category1 = await CreateCategory(MappingCategory(product.Category1, null));
            }

            SmartstoreCategory? category2 = null;
            if (category1 != null)
            {
                if (!string.IsNullOrEmpty(product.Category2))
                {
                    category2 = await CreateCategory(MappingCategory(product.Category2, category1.Id));
                }
            }

            SmartstoreProductCategory? productCategory2 = null;
            if (category2 != null)
            {
                SmartstoreProductCategory data = new SmartstoreProductCategory();
                data.ProductId = result.Id;
                data.CategoryId = category2.Id;
                data.IsFeaturedProduct = false;
                data.DisplayOrder = 0;
                data.IsSystemMapping = false;
                data.Id = 0;

                productCategory2 = await CreateProductCategory(data);
            }

            SmartstoreProductCategory? productCategory1 = null;
            if (category1 != null)
            {
                SmartstoreProductCategory data = new SmartstoreProductCategory();
                data.ProductId = result.Id;
                data.CategoryId = category1.Id;
                data.IsFeaturedProduct = false;
                data.DisplayOrder = productCategory2 == null ? 0 : 1;
                data.IsSystemMapping = false;
                data.Id = 0;

                productCategory1 = await CreateProductCategory(data);
            }
            #endregion

            #region Image
            if (product.Files != null)
            {
                _ = await DeleteProductMediaFile(result.Id);

                foreach (var file in product.Files)
                {
                    SmartstoreFileItemInfo? fileItemInfo = await CreateMediaFile(file);

                    int i = 0;
                    if (fileItemInfo != null)
                    {
                        SmartstoreProductMediaFile smartstoreProductMediaFile = new SmartstoreProductMediaFile();
                        smartstoreProductMediaFile.ProductId = result.Id;
                        smartstoreProductMediaFile.MediaFileId = fileItemInfo.Id;
                        smartstoreProductMediaFile.DisplayOrder = i;

                        _ = await CreateProductMediaFile(smartstoreProductMediaFile);

                        i++;
                    }
                }
            }
            else
            {
                _ = await DeleteProductMediaFile(result.Id);
            }
            #endregion

            #region Variant
            if (product.ProductAttributes != null)
            {
                if (product.ProductAttributes.Any())
                {
                    foreach (var attribute in product.ProductAttributes)
                    {
                        SmartstoreProductAttribute? productAttribute = await CreateProductAttribute(attribute);
                        if (productAttribute != null)
                        {
                            attribute.Id = productAttribute.Id; ;
                        }
                    }

                    var productAttributeIds = product.ProductAttributes.Select(m => m.Id).ToList();

                    var productAttributesResult = await _smartStoreClient.GetProductVariantAttributes(result.Id, productAttributeIds);
                    if (productAttributesResult != null)
                    {
                        foreach (var item in productAttributesResult.value)
                        {
                            await _smartStoreClient.DeleteProductVariantAttribute(item.Id);
                        }
                    }

                    int index = 0;
                    foreach (var attribute in product.ProductAttributes)
                    {
                        SmartstoreProductVariantAttribute productVariantAttributeModel = new SmartstoreProductVariantAttribute();
                        productVariantAttributeModel.ProductId = result.Id;
                        productVariantAttributeModel.ProductAttributeId = attribute.Id;
                        productVariantAttributeModel.TextPrompt = null;
                        productVariantAttributeModel.CustomData = null;
                        productVariantAttributeModel.IsRequired = true;
                        productVariantAttributeModel.AttributeControlTypeId = 40;
                        productVariantAttributeModel.DisplayOrder = index;

                        SmartstoreProductVariantAttribute? productVariantAttribute = await CreateProductVariantAttribute(productVariantAttributeModel);
                        if (productVariantAttribute != null)
                        {
                            product.ProductVariantAttributes.Add(productVariantAttribute);

                            var productAttributeValueNames = product.ProductVariantAttributeValues.Where(m => m.Attribute == attribute.Name).Select(m => m.Name).Distinct().ToList();
                            var productAttributeValuesResult = await _smartStoreClient.GetProductVariantAttributeValues(productVariantAttribute.Id, productAttributeValueNames);
                            if (productAttributeValuesResult != null)
                            {
                                foreach (var item in productAttributeValuesResult.value)
                                {
                                    await _smartStoreClient.DeleteProductVariantAttributeValue(item.Id);
                                }
                            }

                            foreach (var value in product.ProductVariantAttributeValues.Where(m => m.Attribute == attribute.Name))
                            {
                                value.ProductVariantAttributeId = productVariantAttribute.Id;

                                SmartstoreProductVariantAttributeValue? productVariantAttributeValue = await CreateProductVariantAttributeValues(value);
                                if (productVariantAttributeValue != null)
                                {
                                    value.Id = productVariantAttributeValue.Id;
                                }
                            }
                        }


                        index++;
                    }

                    foreach (var combination in product.ProductVariantAttributeCombinations.Select(m => new { m.Sku, m.Price, m.IsActive, m.StockQuantity, m.AllowOutOfStockOrders, m.Weight }).Distinct().ToList())
                    {
                        SmartstoreProductVariantAttributeCombination model = new SmartstoreProductVariantAttributeCombination();
                        model.ProductId = result.Id;
                        model.Sku = combination.Sku;
                        model.Price = combination.Price;
                        model.IsActive = combination.IsActive;
                        model.StockQuantity = combination.StockQuantity;
                        model.AllowOutOfStockOrders = combination.AllowOutOfStockOrders;
                        model.Weight = combination.Weight;

                        var files = product.ProductVariantAttributeCombinations.Where(m => m.Sku == combination.Sku).First().Files;

                        List<int> mediaFileIds = new List<int>();
                        foreach (var file in files)
                        {
                            SmartstoreFileItemInfo? fileItemInfo = await CreateMediaFile(file);
                            if (fileItemInfo != null)
                            {
                                mediaFileIds.Add(fileItemInfo.Id);
                            }
                        }

                        model.AssignedMediaFileIds = string.Join(",", mediaFileIds);


                        var list = product.ProductVariantAttributeCombinations.Where(m => m.Sku == combination.Sku);
                        List<KeyValuePair<int, ICollection<object>>> attributes = new List<KeyValuePair<int, ICollection<object>>>();
                        foreach (var attribute in list)
                        {
                            var productAttribute = product.ProductAttributes.Where(m => m.Name == attribute.Variant).FirstOrDefault();
                            var productVariantAttribute = product.ProductVariantAttributes.Where(m => m.ProductAttributeId == productAttribute.Id).FirstOrDefault();
                            var productVariantAttributeValues = product.ProductVariantAttributeValues.Where(m => m.ProductVariantAttributeId == productVariantAttribute.Id && m.Name == attribute.Value).ToList();

                            attributes.Add(new KeyValuePair<int, ICollection<object>>(productVariantAttribute.Id, productVariantAttributeValues.Select(m => m.Id as object).ToList()));
                        }


                        RawAttribute rawAttribute = new RawAttribute();
                        rawAttribute.Attributes = attributes;

                        model.RawAttributes = JsonConvert.SerializeObject(rawAttribute);
                        model.HashCode = GetHashCode(rawAttribute);

                        SmartstoreProductVariantAttributeCombination? smartstoreProductVariantAttributeCombination = await CreateProductVariantAttributeCombination(model);
                    }
                }
            }
            #endregion

            #region Tag
            if (product.ProductTags != null)
            {
                foreach (var item in product.ProductTags)
                {
                    SmartstoreProductTag? smartstoreProductTag = await CreateProductTag(item);
                }

                SmartstoreProductTagMapping productTagMapping = new SmartstoreProductTagMapping();
                productTagMapping.tagNames = product.ProductTags.Select(m => m.Name).ToList();

                await CreateProductTagMapping(result.Id, productTagMapping);
            }
            #endregion

            _logger.LogInformation($"Ürün transferi tamamlandı : {product.Name}");
            return result;
        }

        private async Task<ResponseSmartList<SmartstoreProductTag>> CreateProductTagMapping(int productId, SmartstoreProductTagMapping productTagMapping)
        {
            return await _smartStoreClient.UpdateProductTags(productId, productTagMapping);
        }
        private async Task<SmartstoreProductTag?> CreateProductTag(SmartstoreProductTag productTag)
        {
            var productTagResult = await _smartStoreClient.GetProductTag(productTag.Name);
            if (productTagResult != null)
            {
                if (productTagResult.value.Any())
                {
                    return productTagResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductTagTransfer(productTag);
                }
            }

            return null;
        }
        private int GetHashCode(RawAttribute rawAttribute)
        {
            var combiner = HashCodeCombiner.Start();
            var attributes = rawAttribute.Attributes.OrderBy(x => x.Key).ToArray();

            for (var i = 0; i < attributes.Length; ++i)
            {
                var attribute = attributes[i];

                combiner.Add(attribute.Key);

                var values = attribute.Value
                    .Select(x => x.ToString())
                    .OrderBy(x => x)
                    .ToArray();

                for (var j = 0; j < values.Length; ++j)
                {
                    combiner.Add(values[j]);
                }
            }

            return combiner.CombinedHash;
        }
        private async Task<SmartstoreProductVariantAttributeCombination?> CreateProductVariantAttributeCombination(SmartstoreProductVariantAttributeCombination combination)
        {
            var productVariantAttributeCombination = await _smartStoreClient.GetProductVariantAttributeCombination(combination.ProductId, combination.HashCode);
            if (productVariantAttributeCombination != null)
            {
                if (productVariantAttributeCombination.value.Any())
                {
                    var result = productVariantAttributeCombination.value.First();
                    combination.Id = result.Id;

                    await _smartStoreClient.ProductVariantAttributeCombinationsUpdate(combination);

                    return result;
                }
                else
                {
                    return await _smartStoreClient.ProductVariantAttributeCombinationsTransfer(combination);
                }
            }

            return null;
        }
        private async Task<SmartstoreProductVariantAttributeValue?> CreateProductVariantAttributeValues(SmartstoreProductVariantAttributeValue productVariantAttributeValue)
        {
            var productAttributeResult = await _smartStoreClient.GetProductVariantAttributeValue(productVariantAttributeValue.ProductVariantAttributeId, productVariantAttributeValue.Name);
            if (productAttributeResult != null)
            {
                if (productAttributeResult.value.Any())
                {
                    var result = productAttributeResult.value.First();
                    productVariantAttributeValue.Id = result.Id;

                    await _smartStoreClient.ProductVariantAttributeValueUpdate(productVariantAttributeValue);

                    return result;
                }
                else
                {
                    return await _smartStoreClient.ProductVariantAttributeValueTransfer(productVariantAttributeValue);
                }
            }

            return null;
        }
        private async Task<SmartstoreProductVariantAttribute?> CreateProductVariantAttribute(SmartstoreProductVariantAttribute productVariantAttribute)
        {
            var productAttributeResult = await _smartStoreClient.GetProductVariantAttribute(productVariantAttribute.ProductId, productVariantAttribute.ProductAttributeId);
            if (productAttributeResult != null)
            {
                if (productAttributeResult.value.Any())
                {
                    var result = productAttributeResult.value.First();
                    productVariantAttribute.Id = result.Id;

                    await _smartStoreClient.ProductVariantAttributeUpdate(productVariantAttribute);

                    return result;
                }
                else
                {
                    return await _smartStoreClient.ProductVariantAttributeTransfer(productVariantAttribute);
                }
            }

            return null;
        }
        private async Task<SmartstoreProductAttribute?> CreateProductAttribute(SmartstoreProductAttribute productAttribute)
        {
            var productAttributeResult = await _smartStoreClient.GetProductAttribute(productAttribute.Name);
            if (productAttributeResult != null)
            {
                if (productAttributeResult.value.Any())
                {
                    return productAttributeResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductAttributeTransfer(productAttribute);
                }
            }

            return null;
        }
        public async Task<bool> UpdateProductPrice(BaseMalzemeFiyatModel malzemeFiyat)
        {
            var productResult = await _smartStoreClient.GetProductSelectId(malzemeFiyat.StokKodu);
            if (productResult != null)
            {
                var product = productResult.value.FirstOrDefault();
                if (product != null)
                {
                    var priceResult = await _smartStoreClient.UpdateProductPrice(product.Id, malzemeFiyat.Fiyat);

                    return priceResult;
                }
            }

            return false;
        }

        public async Task<bool> UpdateProductVariantCombinationPrice(BaseMalzemeFiyatModel malzemeFiyat)
        {
            var productVariantResult = await _smartStoreClient.GetProductVariantAttributeCombinationSku(malzemeFiyat.StokKodu);
            if (productVariantResult != null)
            {
                var productVariant = productVariantResult.value.FirstOrDefault();

                if (productVariant != null)
                {
                    var priceResult = await _smartStoreClient.UpdateProductVariantAttributeCombinationPrice(productVariant.Id, malzemeFiyat.Fiyat);
                    return priceResult;
                }
            }

            return false;
        }
        public async Task UpdateProductStock(List<BaseMalzemeStokModel> malzemeFiyatList)
        {
            foreach (var item in malzemeFiyatList)
            {
                var productResult = await _smartStoreClient.GetProduct(item.StokKodu);
                if (productResult != null)
                {
                    if (productResult.value.Any())
                    {
                        var product = productResult.value.First();

                        var stokResult = await _smartStoreClient.UpdateProductStock(product.Id, Convert.ToInt32(item.StokMiktari));
                        if (stokResult)
                        {
                            _logger.LogInformation($"Ürün stok güncellendi - Stok Kodu : {item.StokKodu} - Stok : {item.StokMiktari}");
                        }
                        else
                        {
                            _logger.LogError($"Ürün stok güncellenemedi - Stok Kodu : {item.StokKodu} - Stok : {item.StokMiktari}");
                        }
                    }
                    else
                    {
                        _logger.LogError($"Ürün bulunamadı - Stok Kodu : {item.StokKodu}");
                    }
                }
            }
        }
        public async Task UpdateProductVariantCombinationStok(List<BaseMalzemeStokModel> malzemeStokList)
        {
            foreach (var item in malzemeStokList)
            {
                var productResult = await _smartStoreClient.GetProductVariantAttributeCombinationSku(item.StokKodu);
                if (productResult != null)
                {
                    if (productResult.value.Any())
                    {
                        var product = productResult.value.First();

                        var stokResult = await _smartStoreClient.UpdateProductVariantAttributeCombinationStok(product.Id, item.StokMiktari);
                        if (stokResult)
                        {
                            _logger.LogInformation($"Ürün varyant stok güncellendi - Stok Kodu : {item.StokKodu} - Stok : {item.StokMiktari}");
                        }
                        else
                        {
                            _logger.LogError($"Ürün varyant stok güncellenemedi - Stok Kodu : {item.StokKodu} - Stok : {item.StokMiktari}");
                        }
                    }
                    else
                    {
                        _logger.LogError($"Ürün stok bulunamadı - Stok Kodu : {item.StokKodu}");
                    }
                }
                else
                {
                    _logger.LogError($"Ürün stok bulunamadı - Stok Kodu : {item.StokKodu}");
                }
            }
        }
        public async Task<SmartstoreManufacturer?> CreateManufacturer(SmartstoreManufacturer smartstoreManufacturer)
        {
            var manufacuterResult = await _smartStoreClient.GetManufacturer(smartstoreManufacturer.Name);
            if (manufacuterResult != null)
            {
                if (manufacuterResult.value.Any())
                {
                    return manufacuterResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ManufacturerTransfer(smartstoreManufacturer);
                }
            }

            return null;

        }
        public async Task<SmartstoreProductManufacturer?> CreateProductManufacturer(SmartstoreProductManufacturer smartstoreProductManufacturer)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductManufacturer(smartstoreProductManufacturer.ProductId, smartstoreProductManufacturer.ManufacturerId);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    return productManufacturerResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductManufacturerTrasnfer(smartstoreProductManufacturer);
                }
            }

            return null;
        }

        public async Task<bool> DeleteProductManufacturer(int productId)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductManufacturer(productId);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    foreach (var item in productManufacturerResult.value)
                    {
                        await _smartStoreClient.DeleteProductManufacturer(item.Id);
                    }
                }
            }

            return true;
        }
        public async Task<SmartstoreCategory?> CreateCategory(SmartstoreCategory smartstoreCategory)
        {
            var categoryResult = await _smartStoreClient.GetCategory(smartstoreCategory.ParentId, smartstoreCategory.Name);
            if (categoryResult != null)
            {
                if (categoryResult.value.Any())
                {
                    return categoryResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.CategoryTransfer(smartstoreCategory);
                }
            }

            return null;
        }
        public async Task<SmartstoreProductCategory?> CreateProductCategory(SmartstoreProductCategory productCategory)
        {
            var productCategoryResult = await _smartStoreClient.GetProductCategory(productCategory.ProductId, productCategory.CategoryId);
            if (productCategoryResult != null)
            {
                if (productCategoryResult.value.Any())
                {
                    return productCategoryResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductCategoryTrasnfer(productCategory);
                }
            }

            return null;


        }
        public async Task<SmartstoreFileItemInfo?> CreateMediaFile(SmartstoreFile smartstoreFile)
        {
            var response = await _smartStoreClient.FileExists(smartstoreFile.FileName);
            if (response != null)
            {
                if (response.value)
                {
                    return await _smartStoreClient.GetFileByPath(smartstoreFile.FileName);
                }
            }

            return await _smartStoreClient.MediaFileSave(smartstoreFile);
        }
        public async Task<SmartstoreProductMediaFile?> CreateProductMediaFile(SmartstoreProductMediaFile smartstoreProductMediaFile)
        {
            var productManufacturerResult = await _smartStoreClient.GetProductMediaFile(smartstoreProductMediaFile.ProductId, smartstoreProductMediaFile.MediaFileId);
            if (productManufacturerResult != null)
            {
                if (productManufacturerResult.value.Any())
                {
                    return productManufacturerResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.ProductMediaFileTrasnfer(smartstoreProductMediaFile);
                }
            }

            return null;


        }
        public async Task<List<SmartstoreOrder>?> GetSmartstoreOrder(string orderStatusId)
        {
            try
            {
                var ordersResult = await _smartStoreClient.GetOrders(orderStatusId);
                if (ordersResult != null)
                {
                    if (ordersResult.status)
                    {
                        foreach (var item in ordersResult.value)
                        {
                            var orderCustomerResult = await _smartStoreClient.GetOrderCustomer(item.Id);
                            var orderItemsResult = await _smartStoreClient.GetOrderItems(item.Id);
                            var orderShippingAddressResult = await _smartStoreClient.GetOrderShippingAddress(item.Id);
                            var orderBillingAddressResult = await _smartStoreClient.GetOrderBillingAddress(item.Id);

                            if (orderCustomerResult != null)
                                item.OrderCustomer = orderCustomerResult;

                            if (orderItemsResult!.status)
                                item.OrderItems = orderItemsResult.value;

                            if (orderShippingAddressResult != null)
                                item.ShippingAddress = orderShippingAddressResult;

                            if (orderBillingAddressResult != null)
                                item.BillingAddress = orderBillingAddressResult;


                            if (item.PaymentMethodSystemName == "Payments.CreditCard" || item.PaymentMethodSystemName == "Payments.Iyzico")
                            {
                                var paymentTransactionResult = await _smartStoreClient.GetPaymentTransaction(item.OrderGuid);
                                if (paymentTransactionResult != null)
                                {
                                    item.PaymentTransaction = paymentTransactionResult.FirstOrDefault();
                                }
                            }
                        }

                        return ordersResult.value;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSmartstoreOrder method failed.");

                return null;
            }
        }
        public async Task<SmartstoreShipment?> AddShipment(SmartstoreAddShipment smartstoreShipment, int orderId, string carrier)
        {
            var orderShipmentResult = await _smartStoreClient.GetShipment(orderId);
            if (orderShipmentResult != null)
            {
                if (orderShipmentResult.value.Any())
                {
                    return orderShipmentResult.value.First();
                }
                else
                {
                    return await _smartStoreClient.AddShipment(smartstoreShipment, orderId, carrier);
                }
            }


            return null;
        }

        public async Task<bool> DeleteProductMediaFile(int productId)
        {
            var result = await _smartStoreClient.GetProductMediaFile(productId);
            if (result != null)
            {
                if (result.value.Any())
                {
                    foreach (var item in result.value)
                    {
                        await _smartStoreClient.DeleteProductMediaFile(item.Id);
                    }
                }
            }

            return true;
        }

        #region MappingProduct
        public List<SmartstoreProduct>? MappingProduct(string erp, object? malzemeList)
        {
            switch (erp)
            {
                case "Logo":
                    return MappingProductLogo(malzemeList as List<ItemModel>);
                case "Netsis":
                    return MappingProductNetsis(malzemeList as List<MalzemeModel>);
                case "Opak":
                    return MappingProductOpak(malzemeList as List<OpakMalzeme>);
            }

            return null;
        }
        private List<SmartstoreProduct> MappingProductLogo(List<ItemModel>? malzemeList)
        {
            List<SmartstoreProduct> productList = new List<SmartstoreProduct>();
            foreach (var item in malzemeList)
            {
                SmartstoreProduct product = new SmartstoreProduct();
                product.ProductTypeId = 5;
                product.ParentGroupedProductId = 0;
                product.Visibility = "Full";
                product.Condition = "New";
                product.Name = item.NAME;
                product.ShortDescription = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                product.FullDescription = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                product.AdminComment = "";
                product.ProductTemplateId = 1;
                product.ShowOnHomePage = false;
                product.HomePageDisplayOrder = 0;
                product.MetaKeywords = "";
                product.MetaTitle = item.NAME;
                product.MetaDescription = item.NAME2 + " " + item.NAME3 + " " + item.NAME4 + " " + item.NAME;
                product.AllowCustomerReviews = true;
                product.ApprovedRatingSum = 0;
                product.NotApprovedRatingSum = 0;
                product.ApprovedTotalReviews = 0;
                product.NotApprovedTotalReviews = 0;
                product.SubjectToAcl = false;
                product.LimitedToStores = false;
                product.Sku = item.CODE;
                product.ManufacturerPartNumber = "";
                product.Gtin = "";
                product.IsGiftCard = false;
                product.GiftCardTypeId = 0;
                product.RequireOtherProducts = false;
                product.RequiredProductIds = null;
                product.AutomaticallyAddRequiredProducts = false;
                product.IsDownload = false;
                product.UnlimitedDownloads = true;
                product.MaxNumberOfDownloads = 10;
                product.DownloadExpirationDays = null;
                product.DownloadActivationTypeId = 1;
                product.HasSampleDownload = false;
                product.SampleDownloadId = null;
                product.HasUserAgreement = false;
                product.UserAgreementText = null;
                product.IsRecurring = false;
                product.RecurringCycleLength = 100;
                product.RecurringCyclePeriodId = 0;
                product.RecurringTotalCycles = 10;
                product.IsShippingEnabled = true;
                product.IsFreeShipping = false;
                product.AdditionalShippingCharge = 0;
                product.IsTaxExempt = false;
                product.IsEsd = false;
                product.TaxCategoryId = 1;
                product.ManageInventoryMethodId = 0;
                product.StockQuantity = 10000;
                product.DisplayStockAvailability = false;
                product.DisplayStockQuantity = false;
                product.MinStockQuantity = 0;
                product.LowStockActivityId = 0;
                product.NotifyAdminForQuantityBelow = 0;
                product.BackorderModeId = 0;
                product.AllowBackInStockSubscriptions = false;
                product.OrderMinimumQuantity = 1;
                product.OrderMaximumQuantity = int.MaxValue;
                product.QuantityStep = 1;
                product.QuantityControlType = "Spinner";
                product.HideQuantityControl = false;
                product.AllowedQuantities = null;
                product.DisableBuyButton = false;
                product.DisableWishlistButton = false;
                product.AvailableForPreOrder = false;
                product.CallForPrice = false;
                product.Price = item.PRICE;
                product.ComparePrice = 0;
                product.ComparePriceLabelId = null;
                product.SpecialPrice = null;
                product.SpecialPriceStartDateTimeUtc = null;
                product.SpecialPriceEndDateTimeUtc = null;
                product.CustomerEntersPrice = false;
                product.MinimumCustomerEnteredPrice = 0;
                product.MaximumCustomerEnteredPrice = 1000;
                product.HasTierPrices = false;
                product.LowestAttributeCombinationPrice = null;
                product.AttributeCombinationRequired = false;
                product.AttributeChoiceBehaviour = "GrayOutUnavailable";
                product.Weight = 0;
                product.Length = 0;
                product.Width = 0;
                product.Height = 0;
                product.AvailableStartDateTimeUtc = null;
                product.AvailableEndDateTimeUtc = null;
                product.DisplayOrder = 0;
                product.Published = true;
                product.IsSystemProduct = false;
                product.SystemName = "";
                product.CreatedOnUtc = DateTimeOffset.UtcNow;
                product.UpdatedOnUtc = DateTimeOffset.UtcNow;
                product.DeliveryTimeId = null;
                product.QuantityUnitId = null;
                product.CustomsTariffNumber = "";
                product.CountryOfOriginId = 77;
                product.BasePriceEnabled = false;
                product.BasePriceMeasureUnit = null;
                product.BasePriceAmount = null;
                product.BasePriceBaseAmount = null;
                product.BundleTitleText = null;
                product.BundlePerItemShipping = false;
                product.BundlePerItemPricing = false;
                product.BundlePerItemShoppingCart = false;
                product.MainPictureId = null;
                product.HasPreviewPicture = false;
                product.HasDiscountsApplied = false;
                product.Id = 0;
                product.GroupedProductConfiguration = null;
                product.ManufacturerCode = item.STGRPCODE;
                product.ManufacturerName = item.STGRPNAME;
                product.Category1 = item.STGRPNAME;

                productList.Add(product);
            }
            return productList;
        }
        private List<SmartstoreProduct> MappingProductNetsis(List<MalzemeModel>? malzemeList)
        {
            List<SmartstoreProduct> productList = new List<SmartstoreProduct>();
            foreach (var item in malzemeList)
            {
                SmartstoreProduct product = new SmartstoreProduct();
                product.ProductTypeId = 5;
                product.ParentGroupedProductId = 0;
                product.Visibility = "Full";
                product.Condition = "New";
                product.Name = NetsisUtils.CevirNetsis(item.STOK_ADI);
                product.ShortDescription = "";
                product.FullDescription = "";
                product.AdminComment = "";
                product.ProductTemplateId = 1;
                product.ShowOnHomePage = false;
                product.HomePageDisplayOrder = 0;
                product.MetaKeywords = "";
                product.MetaTitle = NetsisUtils.CevirNetsis(item.STOK_ADI);
                product.MetaDescription = "";
                product.AllowCustomerReviews = true;
                product.ApprovedRatingSum = 0;
                product.NotApprovedRatingSum = 0;
                product.ApprovedTotalReviews = 0;
                product.NotApprovedTotalReviews = 0;
                product.SubjectToAcl = false;
                product.LimitedToStores = false;
                product.Sku = NetsisUtils.CevirNetsis(item.STOK_KODU);
                product.ManufacturerPartNumber = "";
                product.Gtin = "";
                product.IsGiftCard = false;
                product.GiftCardTypeId = 0;
                product.RequireOtherProducts = false;
                product.RequiredProductIds = null;
                product.AutomaticallyAddRequiredProducts = false;
                product.IsDownload = false;
                product.UnlimitedDownloads = true;
                product.MaxNumberOfDownloads = 10;
                product.DownloadExpirationDays = null;
                product.DownloadActivationTypeId = 1;
                product.HasSampleDownload = false;
                product.SampleDownloadId = null;
                product.HasUserAgreement = false;
                product.UserAgreementText = null;
                product.IsRecurring = false;
                product.RecurringCycleLength = 100;
                product.RecurringCyclePeriodId = 0;
                product.RecurringTotalCycles = 10;
                product.IsShippingEnabled = true;
                product.IsFreeShipping = false;
                product.AdditionalShippingCharge = 0;
                product.IsTaxExempt = false;
                product.IsEsd = false;
                product.TaxCategoryId = 1;
                product.ManageInventoryMethodId = 0;
                product.StockQuantity = 10000;
                product.DisplayStockAvailability = false;
                product.DisplayStockQuantity = false;
                product.MinStockQuantity = 0;
                product.LowStockActivityId = 0;
                product.NotifyAdminForQuantityBelow = 0;
                product.BackorderModeId = 0;
                product.AllowBackInStockSubscriptions = false;
                product.OrderMinimumQuantity = 1;
                product.OrderMaximumQuantity = 50;
                product.QuantityStep = 1;
                product.QuantityControlType = "Spinner";
                product.HideQuantityControl = false;
                product.AllowedQuantities = null;
                product.DisableBuyButton = false;
                product.DisableWishlistButton = false;
                product.AvailableForPreOrder = false;
                product.CallForPrice = false;
                product.Price = item.SATIS_FIAT1;
                product.ComparePrice = 0;
                product.ComparePriceLabelId = null;
                product.SpecialPrice = null;
                product.SpecialPriceStartDateTimeUtc = null;
                product.SpecialPriceEndDateTimeUtc = null;
                product.CustomerEntersPrice = false;
                product.MinimumCustomerEnteredPrice = 0;
                product.MaximumCustomerEnteredPrice = 1000;
                product.HasTierPrices = false;
                product.LowestAttributeCombinationPrice = null;
                product.AttributeCombinationRequired = false;
                product.AttributeChoiceBehaviour = "GrayOutUnavailable";
                product.Weight = 0;
                product.Length = 0;
                product.Width = 0;
                product.Height = 0;
                product.AvailableStartDateTimeUtc = null;
                product.AvailableEndDateTimeUtc = null;
                product.DisplayOrder = 0;
                product.Published = true;
                product.IsSystemProduct = false;
                product.SystemName = "";
                product.CreatedOnUtc = DateTimeOffset.UtcNow;
                product.UpdatedOnUtc = DateTimeOffset.UtcNow;
                product.DeliveryTimeId = null;
                product.QuantityUnitId = null;
                product.CustomsTariffNumber = "";
                product.CountryOfOriginId = 77;
                product.BasePriceEnabled = false;
                product.BasePriceMeasureUnit = null;
                product.BasePriceAmount = null;
                product.BasePriceBaseAmount = null;
                product.BundleTitleText = null;
                product.BundlePerItemShipping = false;
                product.BundlePerItemPricing = false;
                product.BundlePerItemShoppingCart = false;
                product.MainPictureId = null;
                product.HasPreviewPicture = false;
                product.HasDiscountsApplied = false;
                product.Id = 0;
                product.GroupedProductConfiguration = null;
                product.ManufacturerCode = item.KOD_5;
                product.ManufacturerName = item.KOD_5_ISIM;
                product.Category1 = item.KOD_3_ISIM;
                product.Category2 = item.KOD_4_ISIM;

                if (item.EVRAK_LIST != null)
                {
                    product.Files = new List<SmartstoreFile>();
                    foreach (var evrak in item.EVRAK_LIST)
                    {
                        if (evrak.BILGI == null || evrak.BILGI.Length == 0)
                            continue;

                        SmartstoreFile smartstoreFile = new SmartstoreFile();
                        smartstoreFile.FileName = $"catalog/{Path.GetFileName(evrak.DOSYAADI)}";
                        smartstoreFile.File = evrak.BILGI;
                        smartstoreFile.MimeType = MainUtils.GetMimeType(Path.GetExtension(evrak.DOSYAADI)!);

                        product.Files.Add(smartstoreFile);
                    }
                }

                productList.Add(product);
            }
            return productList;
        }
        private List<SmartstoreProduct> MappingProductOpak(List<OpakMalzeme>? malzemeList)
        {
            List<SmartstoreProduct> productList = new List<SmartstoreProduct>();
            foreach (var item in malzemeList)
            {
                SmartstoreProduct product = new SmartstoreProduct();
                product.ProductTypeId = 5;
                product.ParentGroupedProductId = 0;
                product.Visibility = "Full";
                product.Condition = "New";
                product.Name = item.STOK_ADI;
                product.ShortDescription = "";
                product.FullDescription = item.ACIKLAMA;
                product.AdminComment = "";
                product.ProductTemplateId = 1;
                product.ShowOnHomePage = item.ANASAYFA == "E";
                product.HomePageDisplayOrder = 0;
                if (!item.KISAACIKLAMA.IsNullOrEmpty())
                {
                    product.MetaKeywords = item.KISAACIKLAMA.Length > 400 ? item.KISAACIKLAMA.Substring(400) : item.KISAACIKLAMA;
                }
                else
                {
                    product.MetaKeywords = "";
                }
                product.MetaTitle = item.STOK_ADI;
                product.MetaDescription = item.STOK_ADI;
                product.AllowCustomerReviews = true;
                product.ApprovedRatingSum = 0;
                product.NotApprovedRatingSum = 0;
                product.ApprovedTotalReviews = 0;
                product.NotApprovedTotalReviews = 0;
                product.SubjectToAcl = false;
                product.LimitedToStores = false;
                product.Sku = item.STOK_KODU;
                product.ManufacturerPartNumber = "";
                product.Gtin = "";
                product.IsGiftCard = false;
                product.GiftCardTypeId = 0;
                product.RequireOtherProducts = false;
                product.RequiredProductIds = null;
                product.AutomaticallyAddRequiredProducts = false;
                product.IsDownload = false;
                product.UnlimitedDownloads = true;
                product.MaxNumberOfDownloads = 10;
                product.DownloadExpirationDays = null;
                product.DownloadActivationTypeId = 1;
                product.HasSampleDownload = false;
                product.SampleDownloadId = null;
                product.HasUserAgreement = false;
                product.UserAgreementText = null;
                product.IsRecurring = false;
                product.RecurringCycleLength = 100;
                product.RecurringCyclePeriodId = 0;
                product.RecurringTotalCycles = 10;
                product.IsShippingEnabled = true;
                product.IsFreeShipping = false;
                product.AdditionalShippingCharge = 0;
                product.IsTaxExempt = false;
                product.IsEsd = false;
                product.TaxCategoryId = 1;
                product.ManageInventoryMethodId = item.STOKMIKTAR == "E" ? (item.VARYANTLIURUN == "E" ? 2 : 1) : 0;
                product.StockQuantity = Convert.ToInt32(item.MIKTAR);
                product.DisplayStockAvailability = false;
                product.DisplayStockQuantity = false;
                product.MinStockQuantity = 0;
                product.LowStockActivityId = 0;
                product.NotifyAdminForQuantityBelow = 0;
                product.BackorderModeId = 0;
                product.AllowBackInStockSubscriptions = false;
                product.OrderMinimumQuantity = 1;
                product.OrderMaximumQuantity = int.MaxValue;
                product.QuantityStep = 1;
                product.QuantityControlType = "Spinner";
                product.HideQuantityControl = false;
                product.AllowedQuantities = null;
                product.DisableBuyButton = false;
                product.DisableWishlistButton = false;
                product.AvailableForPreOrder = false;
                product.CallForPrice = false;
                product.Price = item.FIYAT;
                product.ComparePrice = 0;
                product.ComparePriceLabelId = null;
                product.SpecialPrice = null;
                product.SpecialPriceStartDateTimeUtc = null;
                product.SpecialPriceEndDateTimeUtc = null;
                product.CustomerEntersPrice = false;
                product.MinimumCustomerEnteredPrice = 0;
                product.MaximumCustomerEnteredPrice = 1000;
                product.HasTierPrices = false;
                product.LowestAttributeCombinationPrice = null;
                product.AttributeCombinationRequired = false;
                product.AttributeChoiceBehaviour = "GrayOutUnavailable";
                product.Weight = item.BIRIMAGIRLIK;
                product.Length = 0;
                product.Width = 0;
                product.Height = 0;
                product.AvailableStartDateTimeUtc = null;
                product.AvailableEndDateTimeUtc = null;
                product.DisplayOrder = 0;
                product.Published = item.AKTIF == "E";
                product.IsSystemProduct = false;
                product.SystemName = "";
                product.CreatedOnUtc = DateTimeOffset.UtcNow;
                product.UpdatedOnUtc = DateTimeOffset.UtcNow;
                product.DeliveryTimeId = null;
                product.QuantityUnitId = null;
                product.CustomsTariffNumber = "";
                product.CountryOfOriginId = 77;
                product.BasePriceEnabled = false;
                product.BasePriceMeasureUnit = null;
                product.BasePriceAmount = null;
                product.BasePriceBaseAmount = null;
                product.BundleTitleText = null;
                product.BundlePerItemShipping = false;
                product.BundlePerItemPricing = false;
                product.BundlePerItemShoppingCart = false;
                product.MainPictureId = null;
                product.HasPreviewPicture = false;
                product.HasDiscountsApplied = false;
                product.Id = 0;
                product.GroupedProductConfiguration = null;
                product.ManufacturerCode = item.MARKA;
                product.ManufacturerName = item.MARKA;
                product.Category1 = item.KOD1;
                product.Category2 = item.KOD2;

                if (item.MalzemeResimList != null)
                {
                    product.Files = new List<SmartstoreFile>();
                    foreach (var resim in item.MalzemeResimList)
                    {
                        SmartstoreFile smartstoreFile = new SmartstoreFile();
                        smartstoreFile.FileName = $"catalog/{resim.KOD}_{resim.ID}.jpg";
                        smartstoreFile.File = resim.RESIM;
                        smartstoreFile.MimeType = "image/jpeg";

                        product.Files.Add(smartstoreFile);
                    }
                }

                if (item.MalzemeVaryantList != null)
                {
                    if (item.MalzemeVaryantList.Any())
                    {
                        product.Price = item.MalzemeVaryantList.OrderBy(m => m.FIYAT4).Select(m => m.FIYAT4).First();
                        product.AttributeCombinationRequired = true;
                    }


                    product.ProductAttributes = new List<SmartstoreProductAttribute>();
                    product.ProductVariantAttributeCombinations = new List<SmartstoreProductVariantAttributeCombination>();
                    product.ProductVariantAttributeValues = new List<SmartstoreProductVariantAttributeValue>();

                    var attributes = item.MalzemeVaryantList.Select(m => m.OZELLIK).Distinct().ToList();

                    int attributeIndex = 0;
                    foreach (var attribute in attributes.OrderBy(m => m))
                    {
                        SmartstoreProductAttribute smartstoreProductAttribute = new SmartstoreProductAttribute();
                        smartstoreProductAttribute.Name = attribute;
                        smartstoreProductAttribute.Description = null;
                        smartstoreProductAttribute.Alias = "";
                        smartstoreProductAttribute.AllowFiltering = true;
                        smartstoreProductAttribute.DisplayOrder = attributeIndex;
                        smartstoreProductAttribute.FacetTemplateHint = "Boxes";
                        smartstoreProductAttribute.IndexOptionNames = false;
                        smartstoreProductAttribute.ExportMappings = null;

                        product.ProductAttributes.Add(smartstoreProductAttribute);

                        var values = item.MalzemeVaryantList.Where(m => m.OZELLIK == attribute).OrderBy(m => m.SIRA).Select(m => new { m.DEGER }).Distinct().ToList();
                        int attributeValueIndex = 0;
                        foreach (var value in values)
                        {
                            SmartstoreProductVariantAttributeValue smartstoreProductVariantAttributeValue = new SmartstoreProductVariantAttributeValue();
                            smartstoreProductVariantAttributeValue.ProductVariantAttributeId = 0;
                            smartstoreProductVariantAttributeValue.Attribute = attribute;
                            smartstoreProductVariantAttributeValue.Name = value.DEGER;
                            smartstoreProductVariantAttributeValue.Alias = "";
                            smartstoreProductVariantAttributeValue.MediaFileId = 0;
                            smartstoreProductVariantAttributeValue.Color = null;
                            smartstoreProductVariantAttributeValue.PriceAdjustment = 0;
                            smartstoreProductVariantAttributeValue.WeightAdjustment = 0;
                            smartstoreProductVariantAttributeValue.IsPreSelected = false;
                            smartstoreProductVariantAttributeValue.DisplayOrder = attributeValueIndex;
                            smartstoreProductVariantAttributeValue.ValueTypeId = 0;
                            smartstoreProductVariantAttributeValue.LinkedProductId = 0;
                            smartstoreProductVariantAttributeValue.Quantity = 1;

                            product.ProductVariantAttributeValues.Add(smartstoreProductVariantAttributeValue);

                            attributeValueIndex++;
                        }

                        attributeIndex++;
                    }

                    foreach (var varyant in item.MalzemeVaryantList.OrderBy(m => m.SIRA))
                    {
                        SmartstoreProductVariantAttributeCombination smartstoreProductVariantAttributeCombination = new SmartstoreProductVariantAttributeCombination();
                        smartstoreProductVariantAttributeCombination.Variant = varyant.OZELLIK;
                        smartstoreProductVariantAttributeCombination.Value = varyant.DEGER;
                        smartstoreProductVariantAttributeCombination.Sku = varyant.KOD;
                        smartstoreProductVariantAttributeCombination.Price = Convert.ToDecimal(varyant.FIYAT4);
                        smartstoreProductVariantAttributeCombination.IsActive = true;
                        smartstoreProductVariantAttributeCombination.StockQuantity = varyant.MIKTAR;
                        smartstoreProductVariantAttributeCombination.Weight = varyant.BIRIMAGIRLIK;
                        smartstoreProductVariantAttributeCombination.AllowOutOfStockOrders = true;
                        foreach (var resim in varyant.MalzemeResimList)
                        {
                            SmartstoreFile smartstoreFile = new SmartstoreFile();
                            smartstoreFile.FileName = $"catalog/{resim.KOD}_{resim.ID}.jpg";
                            smartstoreFile.File = resim.RESIM;
                            smartstoreFile.MimeType = "image/jpeg";

                            smartstoreProductVariantAttributeCombination.Files.Add(smartstoreFile);
                        }


                        product.ProductVariantAttributeCombinations.Add(smartstoreProductVariantAttributeCombination);
                    }

                }

                if (!string.IsNullOrEmpty(item.KISAACIKLAMA))
                {
                    var tags = item.KISAACIKLAMA.Trim().Split(',').Select(m => m.Trim()).ToList();
                    foreach (var tag in tags)
                    {
                        product.ProductTags.Add(new SmartstoreProductTag()
                        {
                            Id = 0,
                            Name = tag,
                            Published = true,
                        });
                    }

                }
                productList.Add(product);
            }
            return productList;
        }
        #endregion

        #region MappingManufacturer
        public SmartstoreManufacturer MappingManufacturer(string manufacturer)
        {
            SmartstoreManufacturer smartstoreManufacturer = new SmartstoreManufacturer();
            smartstoreManufacturer.Name = manufacturer;
            smartstoreManufacturer.Description = "";
            smartstoreManufacturer.BottomDescription = "";
            smartstoreManufacturer.ManufacturerTemplateId = 0;
            smartstoreManufacturer.MetaKeywords = "";
            smartstoreManufacturer.MetaDescription = "";
            smartstoreManufacturer.MetaTitle = manufacturer;
            smartstoreManufacturer.MediaFileId = null;
            smartstoreManufacturer.PageSize = 10;
            smartstoreManufacturer.AllowCustomersToSelectPageSize = false;
            smartstoreManufacturer.PageSizeOptions = "10,20,50";
            smartstoreManufacturer.LimitedToStores = false;
            smartstoreManufacturer.SubjectToAcl = false;
            smartstoreManufacturer.DisplayOrder = 0;
            smartstoreManufacturer.Published = true;
            smartstoreManufacturer.CreatedOnUtc = DateTimeOffset.UtcNow;
            smartstoreManufacturer.UpdatedOnUtc = DateTimeOffset.UtcNow;
            smartstoreManufacturer.HasDiscountsApplied = false;
            smartstoreManufacturer.Id = 0;

            return smartstoreManufacturer;
        }
        #endregion

        #region MappingCategory
        public SmartstoreCategory MappingCategory(string category, int? parentId)
        {
            SmartstoreCategory smartstoreCategory = new SmartstoreCategory();
            smartstoreCategory.Alias = "";
            smartstoreCategory.AllowCustomersToSelectPageSize = false;
            smartstoreCategory.BadgeStyle = 0;
            smartstoreCategory.BadgeText = "";
            smartstoreCategory.BottomDescription = "";
            smartstoreCategory.CategoryTemplateId = 0;
            smartstoreCategory.CreatedOnUtc = DateTime.Now;
            smartstoreCategory.TreePath = "/";
            smartstoreCategory.FullName = category;
            smartstoreCategory.DefaultViewMode = "Grid";
            smartstoreCategory.Description = "";
            smartstoreCategory.DisplayOrder = 0;
            smartstoreCategory.UpdatedOnUtc = DateTime.Now;
            smartstoreCategory.SubjectToAcl = false;
            smartstoreCategory.ShowOnHomePage = false;
            smartstoreCategory.Published = true;
            smartstoreCategory.ParentId = parentId;
            smartstoreCategory.PageSizeOptions = "10,20,50";
            smartstoreCategory.PageSize = 20;
            smartstoreCategory.Name = category;
            smartstoreCategory.MetaTitle = category;
            smartstoreCategory.MetaKeywords = "";
            smartstoreCategory.MetaDescription = "";
            smartstoreCategory.MediaFileId = null;
            smartstoreCategory.LimitedToStores = false;
            smartstoreCategory.IgnoreInMenus = false;
            smartstoreCategory.Id = 0;
            smartstoreCategory.HasDiscountsApplied = false;
            smartstoreCategory.ExternalLink = "";

            return smartstoreCategory;
        }
        #endregion
    }

    public class RawAttribute
    {
        public List<KeyValuePair<int, ICollection<object>>> Attributes { get; set; }
    }

    public struct HashCodeCombiner
    {
        const long _globalSeed = 0x1505L;

        private long _combinedHash64;

        /// <summary>
        /// Initializes the <see cref="HashCodeCombiner"/> with zero seed.
        /// </summary>
        public HashCodeCombiner()
        {
        }

        /// <summary>
        /// Initializes the <see cref="HashCodeCombiner"/> with the given <paramref name="seed"/>.
        /// </summary>
        public HashCodeCombiner(long seed)
        {
            _combinedHash64 = seed;
        }

        /// <summary>
        /// Initializes a deterministic <see cref="HashCodeCombiner"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashCodeCombiner Start()
        {
            return new HashCodeCombiner(_globalSeed);
        }

        /// <summary>
        /// Initializes a non-deterministic <see cref="HashCodeCombiner"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashCodeCombiner StartNonDeterministic()
        {
            return new HashCodeCombiner(CurrentSeed);
        }

        public int CombinedHash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _combinedHash64.GetHashCode(); }
        }

        public string CombinedHashString
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _combinedHash64.GetHashCode().ToString("x", CultureInfo.InvariantCulture); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(HashCodeCombiner self)
        {
            return self.CombinedHash;
        }

        internal static long GlobalSeed { get; } = _globalSeed;
        internal static long CurrentSeed { get; } = GenerateRandomInteger(min: int.MinValue);

        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            return Random.Shared.Next(min, max);
        }

        public HashCodeCombiner AddSequence<T>(IEnumerable<T> sequence, IEqualityComparer<T>? comparer = null)
            where T : notnull
        {
            if (sequence is not null)
            {
                var count = 0;
                foreach (var o in sequence)
                {
                    Add(o, comparer);
                    count++;
                }

                Append(count);
            }

            return this;
        }

        public HashCodeCombiner AddDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
            where TKey : notnull
            where TValue : notnull
        {
            if (dictionary is not null)
            {
                foreach (var kvp in dictionary.OrderBy(x => x.Key))
                {
                    Add(kvp.Key);
                    Add(kvp.Value);
                }
            }

            return this;
        }

        public HashCodeCombiner Add<TStruct>(TStruct? value)
            where TStruct : struct
        {
            // Optimization: for value types, we can avoid boxing "value" by skipping the null check
            if (value.HasValue)
            {
                Append(value.GetHashCode());
            }

            return this;
        }

        public HashCodeCombiner Add<TStruct>(TStruct value)
            where TStruct : struct
        {
            // Optimization: for value types, we can avoid boxing "value" by skipping the null check
            Append(value.GetHashCode());

            return this;
        }

        public HashCodeCombiner Add<T>(T value, IEqualityComparer<T>? comparer = null)
        {
            if (value is string str)
            {
                // XxHash3 is faster than Marvin
                Append((long)XxHash3.HashToUInt64(Encoding.UTF8.GetBytes(str)));
            }
            else if (value is not null)
            {
                Append(comparer?.GetHashCode(value) ?? value.GetHashCode());
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Append(long hash)
        {
            if (hash != 0)
            {
                _combinedHash64 = ((_combinedHash64 << 5) + _combinedHash64) ^ hash;
            }
        }
    }
}
