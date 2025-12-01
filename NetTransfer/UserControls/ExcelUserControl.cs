using DevExpress.XtraEditors;
using ExcelDataReader;
using Microsoft.Extensions.Logging;
using NetTransfer.B2B.Library;
using NetTransfer.B2B.Library.Models;
using NetTransfer.Core.Entities;
using NetTransfer.Core.Utils;
using NetTransfer.Data;
using NetTransfer.Integration.Services.VirtualStore;
using NetTransfer.Smartstore.Library;
using NetTransfer.Smartstore.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class ExcelUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private NetTransferContext _db;
        private SmartStoreClient _smartStoreClient;
        private SmartstoreService _smartstoreTransfer;
        private B2BClient _b2bClient;
        private B2BService _b2bService;
        private ILogger _logger;

        private List<SmartstoreProduct> _products = new List<SmartstoreProduct>();
        private ILoggerFactory _loggerFactory;

        public ExcelUserControl()
        {
            InitializeComponent();
            InitializeContext();
            InitializeLogger();

            var virtualStoreSetting = _db.VirtualStoreSetting.FirstOrDefault(v => v.VirtualStore == "Smartstore");
            _smartStoreClient = new SmartStoreClient(virtualStoreSetting);
            _smartstoreTransfer = new SmartstoreService(_logger, _smartStoreClient);

            var virtualStoreSettingB2B = _db.VirtualStoreSetting.FirstOrDefault(v => v.VirtualStore == "B2B");
            _b2bClient = new B2BClient(virtualStoreSettingB2B);
            
        }


        private void InitializeContext()
        {
            _db = new NetTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private void InitializeLogger()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddEventLog(settings =>
                {
                    settings.SourceName = "NetTransfer";
                });
            });
            _logger = _loggerFactory.CreateLogger<TransferUserControl>();
        }

        private async void btnFile_ButtonClickAsync(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel Dosyaları|*.xls;*.xlsx;*.xlsm",
                Title = "Bir Excel Dosyası Seçin"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;

                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                IExcelDataReader excelReader;
                if (Path.GetExtension(filePath).ToLower() == ".xls")
                {
                    //Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                int index = 0;
                while (excelReader.Read())
                {
                    if (index > 0)
                    {
                        var image = await getUrlToImage("https://b2b.demirbeyotomotiv.com.tr/" + excelReader.GetString(2));
                        if (image != null)
                        {
                            B2BUrunResim b2BUrunResim = new B2BUrunResim();
                            b2BUrunResim.Image = image;
                            b2BUrunResim.UrunKodu = excelReader.GetString(1);

                            _ = await _b2bClient.UrunResimTransferAsync(b2BUrunResim);
                        }

                        //SmartstoreProduct product = new SmartstoreProduct();
                        //product.ProductTypeId = 5;
                        //product.ParentGroupedProductId = 0;
                        //product.Visibility = "Full";
                        //product.Condition = "New";
                        //product.Name = excelReader.GetString(2);
                        //product.ShortDescription = "";
                        //product.FullDescription = excelReader.GetString(4);
                        //product.AdminComment = "";
                        //product.ProductTemplateId = 1;
                        //product.ShowOnHomePage = false;
                        //product.HomePageDisplayOrder = 0;
                        //product.MetaKeywords = "";
                        //product.MetaTitle = excelReader.GetString(2);
                        //product.MetaDescription = excelReader.GetString(2);
                        //product.AllowCustomerReviews = true;
                        //product.ApprovedRatingSum = 0;
                        //product.NotApprovedRatingSum = 0;
                        //product.ApprovedTotalReviews = 0;
                        //product.NotApprovedTotalReviews = 0;
                        //product.SubjectToAcl = false;
                        //product.LimitedToStores = false;
                        //product.Sku = excelReader.GetString(1);
                        //product.ManufacturerPartNumber = "";
                        //product.Gtin = "";
                        //product.IsGiftCard = false;
                        //product.GiftCardTypeId = 0;
                        //product.RequireOtherProducts = false;
                        //product.RequiredProductIds = null;
                        //product.AutomaticallyAddRequiredProducts = false;
                        //product.IsDownload = false;
                        //product.UnlimitedDownloads = true;
                        //product.MaxNumberOfDownloads = 10;
                        //product.DownloadExpirationDays = null;
                        //product.DownloadActivationTypeId = 1;
                        //product.HasSampleDownload = false;
                        //product.SampleDownloadId = null;
                        //product.HasUserAgreement = false;
                        //product.UserAgreementText = null;
                        //product.IsRecurring = false;
                        //product.RecurringCycleLength = 100;
                        //product.RecurringCyclePeriodId = 0;
                        //product.RecurringTotalCycles = 10;
                        //product.IsShippingEnabled = true;
                        //product.IsFreeShipping = false;
                        //product.AdditionalShippingCharge = 0;
                        //product.IsTaxExempt = false;
                        //product.IsEsd = false;
                        //product.TaxCategoryId = 1;
                        //product.ManageInventoryMethodId = 0;
                        //product.StockQuantity = 0;
                        //product.DisplayStockAvailability = false;
                        //product.DisplayStockQuantity = false;
                        //product.MinStockQuantity = 0;
                        //product.LowStockActivityId = 0;
                        //product.NotifyAdminForQuantityBelow = 0;
                        //product.BackorderModeId = 0;
                        //product.AllowBackInStockSubscriptions = false;
                        //product.OrderMinimumQuantity = 1;
                        //product.OrderMaximumQuantity = int.MaxValue;
                        //product.QuantityStep = 1;
                        //product.QuantityControlType = "Spinner";
                        //product.HideQuantityControl = false;
                        //product.AllowedQuantities = null;
                        //product.DisableBuyButton = false;
                        //product.DisableWishlistButton = false;
                        //product.AvailableForPreOrder = false;
                        //product.CallForPrice = false;
                        //product.Price = 0;
                        //product.ComparePrice = 0;
                        //product.ComparePriceLabelId = null;
                        //product.SpecialPrice = null;
                        //product.SpecialPriceStartDateTimeUtc = null;
                        //product.SpecialPriceEndDateTimeUtc = null;
                        //product.CustomerEntersPrice = false;
                        //product.MinimumCustomerEnteredPrice = 0;
                        //product.MaximumCustomerEnteredPrice = 1000;
                        //product.HasTierPrices = false;
                        //product.LowestAttributeCombinationPrice = null;
                        //product.AttributeCombinationRequired = false;
                        //product.AttributeChoiceBehaviour = "GrayOutUnavailable";
                        //product.Weight = 0;
                        //product.Length = 0;
                        //product.Width = 0;
                        //product.Height = 0;
                        //product.AvailableStartDateTimeUtc = null;
                        //product.AvailableEndDateTimeUtc = null;
                        //product.DisplayOrder = 0;
                        //product.Published = true;
                        //product.IsSystemProduct = false;
                        //product.SystemName = "";
                        //product.CreatedOnUtc = DateTimeOffset.UtcNow;
                        //product.UpdatedOnUtc = DateTimeOffset.UtcNow;
                        //product.DeliveryTimeId = null;
                        //product.QuantityUnitId = null;
                        //product.CustomsTariffNumber = "";
                        //product.CountryOfOriginId = 77;
                        //product.BasePriceEnabled = false;
                        //product.BasePriceMeasureUnit = null;
                        //product.BasePriceAmount = null;
                        //product.BasePriceBaseAmount = null;
                        //product.BundleTitleText = null;
                        //product.BundlePerItemShipping = false;
                        //product.BundlePerItemPricing = false;
                        //product.BundlePerItemShoppingCart = false;
                        //product.MainPictureId = null;
                        //product.HasPreviewPicture = false;
                        //product.HasDiscountsApplied = false;
                        //product.Id = 0;
                        //product.GroupedProductConfiguration = null;
                        //product.ManufacturerCode = "";
                        //product.ManufacturerName = "";
                        //product.Categories = excelReader.GetString(6).Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries)
                        //                        .Select(c => c.Trim()).ToList();

                        //var images = excelReader.GetString(5).Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                        //                        .Select(i => i.Trim()).ToList();

                        //foreach (var item in images)
                        //{
                        //    try
                        //    {
                        //        using (var client = new HttpClient())
                        //        {
                        //            using (var response = await client.GetAsync(item))
                        //            {
                        //                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                        //                product.Files.Add(new SmartstoreFile()
                        //                {
                        //                    File = imageBytes,
                        //                    FileName = $"catalog/{Path.GetFileName(item)}",
                        //                    MimeType = MainUtils.GetMimeType(Path.GetExtension(item)!)
                        //                });
                        //            }
                        //        }
                        //    }
                        //    catch (Exception)
                        //    {

                        //    }
                        //}

              
                    }
                   
                    index++;    
                }

                excelReader.Close();

                MessageBox.Show("Veriler Hazırlandı");
            }
        }

        private async Task<byte[]?> getUrlToImage(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(url))
                    {
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                        return imageBytes;  
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            //foreach (var item in _products)
            //{
            //    _ = await _smartstoreTransfer.CreateProduct(item,true);
            //}
        }
    }
}
