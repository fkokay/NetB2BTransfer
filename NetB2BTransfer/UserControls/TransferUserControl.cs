using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using NetB2BTransfer.B2B.Library;
using NetB2BTransfer.Core;
using NetB2BTransfer.Core.Data;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Data;
using NetB2BTransfer.Logo.Library.Class;
using NetB2BTransfer.Logo.Library.Models;
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

namespace NetB2BTransfer.UserControls
{
    public partial class TransferUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetB2BTransferContext _context;
        private ErpSetting _erpSetting;
        private B2BSetting _b2BSetting;
        private LogoTransferSetting _logoTransferSetting;
        public TransferUserControl()
        {
            InitializeComponent();
            _context = new NetB2BTransferContext("Data Source=(local);Initial Catalog=B2BENT2;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;");
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            Transfer transfer;
            if (_erpSetting.Erp == "Logo")
            {
                transfer = new Transfer(_erpSetting, _b2BSetting, _logoTransferSetting);
            }
            else if (_erpSetting.Erp == "Netsis")
            {
                transfer = new Transfer(_erpSetting, _b2BSetting);
            }
            else
            {
                transfer = new Transfer(_erpSetting, _b2BSetting);
            }

            if (string.IsNullOrEmpty(cmbTransferType.SelectedItem.ToString()))
            {
                MessageBox.Show("Lütfen aktarım türünü seçiniz.");
                return;
            }

            if (cmbTransferType.SelectedItem.ToString() == "Cari Aktarım")
            {
                await transfer.CariTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Cari Bakiye Aktarım")
            {
                await transfer.CariBakiyeTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Aktarım")
            {
                await transfer.MalzemeTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Stok Aktarım")
            {
                await transfer.MalzemeStokTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Fiyat Aktarım")
            {
                await transfer.MalzemeFiyatTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Sipariş Aktarım")
            {
                await transfer.SiparisTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "SanalPos Aktarım")
            {
                await transfer.SanalPosTransfer();
            }
        }

        private async void TransferUserControl_Load(object sender, EventArgs e)
        {
            _erpSetting = await _context.ErpSetting.FirstAsync();
            _b2BSetting = await _context.B2BSetting.FirstAsync();

            if (_erpSetting.Erp == "Logo")
            {
                if (!_context.LogoTransferSetting.Any())
                {
                    MessageBox.Show("Lütfen logo aktarım ayarlarını tanımlayınız.");
                    return;
                }

                _logoTransferSetting = await _context.LogoTransferSetting.FirstAsync();
            }
        }
    }
}
