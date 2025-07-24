using DevExpress.XtraEditors;
using NetTransfer.Core.Entities;
using NetTransfer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class SmartstoreParameterUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetTransferContext _context;
        public SmartstoreParameterUserControl()
        {
            InitializeComponent();
            _context = new NetTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private void SmartstoreParameterUserControl_Load(object sender, EventArgs e)
        {
            var smartStoreParameter = _context.SmartstoreParameter.FirstOrDefault();
            if (smartStoreParameter != null)
            {
                txtProductTransferMinute.Value = smartStoreParameter.ProductTransferMinute;
                txtProductFilter.Text = smartStoreParameter.ProductFilter;
                toggleSwitchProduct.IsOn = smartStoreParameter.ProductSync;

                txtProductStockTransferMinute.Value = smartStoreParameter.ProductStockTransferMinute;
                txtProductStockFilter.Text = smartStoreParameter.ProductStockFilter;
                toggleSwitchProductStock.IsOn = smartStoreParameter.ProductStockSync;

                txtProductPriceTransferMinute.Value = smartStoreParameter.ProductPriceTransferMinute;
                txtProductPriceFilter.Text = smartStoreParameter.ProductPriceFilter;
                toggleSwitchProductPrice.IsOn = smartStoreParameter.ProductPriceSync;

                txtOrderTransferMinute.Value = smartStoreParameter.OrderTransferMinute;
                txtOrderStatusId.Text = smartStoreParameter.OrderStatusId.ToString();
            }
        }

        public void Save()
        {
            var smartStoreParameter = _context.SmartstoreParameter.FirstOrDefault();
            if (smartStoreParameter != null)
            {
                smartStoreParameter.ProductTransferMinute = (int)txtProductTransferMinute.Value;
                smartStoreParameter.ProductFilter = txtProductFilter.Text;
                smartStoreParameter.ProductSync = toggleSwitchProduct.IsOn;
                smartStoreParameter.ProductStockTransferMinute = (int)txtProductStockTransferMinute.Value;
                smartStoreParameter.ProductStockFilter = txtProductStockFilter.Text;
                smartStoreParameter.ProductStockSync = toggleSwitchProductStock.IsOn;
                smartStoreParameter.ProductPriceTransferMinute = (int)txtProductPriceTransferMinute.Value;
                smartStoreParameter.ProductPriceFilter = txtProductPriceFilter.Text;
                smartStoreParameter.ProductPriceSync = toggleSwitchProductPrice.IsOn;
                smartStoreParameter.OrderTransferMinute = (int)txtOrderTransferMinute.Value;
                smartStoreParameter.OrderStatusId = txtOrderStatusId.Text;
            }
            else
            {
                smartStoreParameter = new SmartstoreParameter
                {
                    ProductTransferMinute = (int)txtProductTransferMinute.Value,
                    ProductFilter = txtProductFilter.Text,
                    ProductSync = toggleSwitchProduct.IsOn,
                    ProductStockTransferMinute = (int)txtProductStockTransferMinute.Value,
                    ProductStockFilter = txtProductStockFilter.Text,
                    ProductStockSync = toggleSwitchProductStock.IsOn,
                    ProductPriceTransferMinute = (int)txtProductPriceTransferMinute.Value,
                    ProductPriceFilter = txtProductPriceFilter.Text,
                    ProductPriceSync = toggleSwitchProductPrice.IsOn,
                    OrderTransferMinute = (int)txtOrderTransferMinute.Value,
                    OrderStatusId = txtOrderStatusId.Text,
                };
                _context.SmartstoreParameter.Add(smartStoreParameter);
            }
            _context.SaveChanges();
        }
    }
}
