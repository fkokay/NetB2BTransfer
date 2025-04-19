using DevExpress.XtraEditors;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetB2BTransfer.UserControls
{
    public partial class ErpSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetB2BTransferContext _context;
        public ErpSettingUserControl()
        {
            InitializeComponent();
            _context = new NetB2BTransferContext("Data Source=(local);Initial Catalog=B2BENT2;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;");
        }

        private void ErpSettingUserControl_Load(object sender, EventArgs e)
        {
            var erpSetting = _context.ErpSetting.FirstOrDefault();
            if (erpSetting != null)
            {
                cmbErp.Text = erpSetting.Erp;
                txtSqlServer.Text = erpSetting.SqlServer;
                txtSqlUser.Text = erpSetting.SqlUser;
                txtSqlPassword.Text = erpSetting.SqlPassword;
                txtSqlDatabase.Text = erpSetting.SqlDatabase;
                txtRestUrl.Text = erpSetting.RestUrl;
                txtErpUser.Text = erpSetting.ErpUser;
                txtErpPassword.Text = erpSetting.ErpPassword;

                switch (erpSetting.Erp)
                {
                    case "Netsis":
                        xtraTabPageNetsis.PageVisible = true;
                        xtraTabPageLogo.PageVisible = false;
                        break;
                    case "Logo":
                        xtraTabPageNetsis.PageVisible = false;
                        xtraTabPageLogo.PageVisible = true;

                        LoadLogoTransferSetting();
                        break;
                }
            }
            else
            {
                xtraTabPageNetsis.PageEnabled = false;
                xtraTabPageLogo.PageEnabled = true;
            }


        }

        private void cmbErp_TextChanged(object sender, EventArgs e)
        {
            switch (cmbErp.Text)
            {
                case "Netsis":
                    xtraTabPageNetsis.PageVisible = true;
                    xtraTabPageLogo.PageVisible = false;
                    break;
                case "Logo":
                    xtraTabPageNetsis.PageVisible = false;
                    xtraTabPageLogo.PageVisible = true;
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var erpSetting = _context.ErpSetting.FirstOrDefault();
            if (erpSetting == null)
            {
                erpSetting = new ErpSetting();
                erpSetting.Erp = cmbErp.Text;
                erpSetting.Active = true;
                _context.ErpSetting.Add(erpSetting);
            }

            erpSetting.Erp = cmbErp.Text;
            erpSetting.SqlServer = txtSqlServer.Text;
            erpSetting.SqlUser = txtSqlUser.Text;
            erpSetting.SqlPassword = txtSqlPassword.Text;
            erpSetting.SqlDatabase = txtSqlDatabase.Text;
            erpSetting.RestUrl = txtRestUrl.Text;
            erpSetting.ErpUser = txtErpUser.Text;
            erpSetting.ErpPassword = txtErpPassword.Text;

            _context.SaveChanges();

            SaveLogoTransferSetting();



            XtraMessageBox.Show("ERP ayarları başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SaveLogoTransferSetting()
        {
            var logoTransferSetting = _context.LogoTransferSetting.FirstOrDefault();
            if (logoTransferSetting == null)
            {
                logoTransferSetting = new LogoTransferSetting
                {
                    CustomerTransferMinute = int.TryParse(txtCustomerTransferMinute.Text, out var minute) ? minute : 0,
                    CustomerFilter = txtCustomerFilter.Text,
                    CustomerLastTransfer = null
                };
                _context.LogoTransferSetting.Add(logoTransferSetting);
            }
            else
            {
                logoTransferSetting.CustomerTransferMinute = int.TryParse(txtCustomerTransferMinute.Text, out var minute) ? minute : 0;
                logoTransferSetting.CustomerFilter = txtCustomerFilter.Text;
            }

            _context.SaveChanges();
        }

        private void LoadLogoTransferSetting()
        {
            var logoTransferSetting = _context.LogoTransferSetting.FirstOrDefault();
            if (logoTransferSetting != null)
            {
                txtCustomerTransferMinute.Text = logoTransferSetting.CustomerTransferMinute.ToString();
                txtCustomerFilter.Text = logoTransferSetting.CustomerFilter;
                if (logoTransferSetting.CustomerLastTransfer.HasValue)
                {
                    txtCustomerLastTransfer.Text = logoTransferSetting.CustomerLastTransfer.Value.ToString("dd.MM.yyyy HH:mm:ss");
                }
            }
            else
            {
                txtCustomerTransferMinute.Text = string.Empty;
                txtCustomerFilter.Text = string.Empty;
            }
        }
    }
}
