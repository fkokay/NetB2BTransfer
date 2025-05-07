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
    public partial class B2BParameterUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetTransferContext _context;
        public B2BParameterUserControl()
        {
            InitializeComponent();
            _context = new NetTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private void B2BParameterUserControl_Load(object sender, EventArgs e)
        {
            var b2BParameter = _context.B2BParameter.FirstOrDefault();
            if (b2BParameter != null)
            {
                txtCustomerTransferMinute.Value = b2BParameter.CustomerTransferMinute;
                txtCustomerFilter.Text = b2BParameter.CustomerFilter;
                txtCustomerLastTransfer.Text = b2BParameter.CustomerLastTransfer?.ToString("dd.MM.yyyy HH:mm:ss");

                txtProductTransferMinute.Value = b2BParameter.ProductTransferMinute;
                txtProductFilter.Text = b2BParameter.ProductFilter;
                txtProductLastTransfer.Text = b2BParameter.ProductLastTransfer?.ToString("dd.MM.yyyy HH:mm:ss");

                txtProductStockTransferMinute.Value = b2BParameter.ProductStockTransferMinute;
                txtProductStockFilter.Text = b2BParameter.ProductStockFilter;
                txtProductStockLastTransfer.Text = b2BParameter.ProductStockLastTransfer?.ToString("dd.MM.yyyy HH:mm:ss");

                txtProductPriceTransferMinute.Value = b2BParameter.ProductPriceTransferMinute;
                txtProductPriceFilter.Text = b2BParameter.ProductPriceFilter;
                txtProductPriceLastTransfer.Text = b2BParameter.ProductPriceLastTransfer?.ToString("dd.MM.yyyy HH:mm:ss");
            }
        }

        public void Save()
        {
            var b2BParameter = _context.B2BParameter.FirstOrDefault();
            if (b2BParameter != null)
            {
                b2BParameter.CustomerTransferMinute = (int)txtCustomerTransferMinute.Value;
                b2BParameter.CustomerFilter = txtCustomerFilter.Text;
                b2BParameter.ProductTransferMinute = (int)txtProductTransferMinute.Value;
                b2BParameter.ProductFilter = txtProductFilter.Text;
                b2BParameter.ProductStockTransferMinute = (int)txtProductStockTransferMinute.Value;
                b2BParameter.ProductStockFilter = txtProductStockFilter.Text;
                b2BParameter.ProductPriceTransferMinute = (int)txtProductPriceTransferMinute.Value;
                b2BParameter.ProductPriceFilter = txtProductPriceFilter.Text;
            }
            else
            {
                b2BParameter = new B2BParameter
                {
                    CustomerTransferMinute = (int)txtCustomerTransferMinute.Value,
                    CustomerFilter = txtCustomerFilter.Text,
                    ProductTransferMinute = (int)txtProductTransferMinute.Value,
                    ProductFilter = txtProductFilter.Text,
                    ProductStockTransferMinute = (int)txtProductStockTransferMinute.Value,
                    ProductStockFilter = txtProductStockFilter.Text,
                    ProductPriceTransferMinute = (int)txtProductPriceTransferMinute.Value,
                    ProductPriceFilter = txtProductPriceFilter.Text,
                };
                _context.B2BParameter.Add(b2BParameter);
            }
            _context.SaveChanges();
        }
    }
}
