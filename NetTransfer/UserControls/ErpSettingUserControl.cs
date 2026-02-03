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
    public partial class ErpSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetTransferContext _context;
        public ErpSettingUserControl()
        {
            InitializeComponent();
            _context = new NetTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
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
                txtFirmaNo.Text = erpSetting.FirmNo;
                txtPeriodNo.Text = erpSetting.PeriodNo;
                txtDefaultBankCode.Text = erpSetting.DefaultBankCode;
                txtDefaultCashierCode.Text = erpSetting.DefaultCashierCode;

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
            erpSetting.FirmNo = txtFirmaNo.Text;
            erpSetting.PeriodNo = txtPeriodNo.Text;
            erpSetting.DefaultBankCode = erpSetting.DefaultBankCode;
            erpSetting.DefaultCashierCode = erpSetting.DefaultCashierCode;

            _context.SaveChanges();

            XtraMessageBox.Show("ERP ayarları başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbErp_TextChanged(object sender, EventArgs e)
        {
            if (cmbErp.Text == "Logo")
            {
                lblFirmaNo.Visible = true;
                txtFirmaNo.Visible = true;

                lblPeriodNo.Visible = true;
                txtPeriodNo.Visible = true;

                lblDefaultBankCode.Visible = true;
                txtDefaultBankCode.Visible = true;

                lblDefaultCashierCode.Visible = true;
                txtDefaultCashierCode.Visible = true;
            }
            else
            {
                lblFirmaNo.Visible = false;
                txtFirmaNo.Visible = false;

                lblPeriodNo.Visible = false;
                txtPeriodNo.Visible = false;

                lblDefaultBankCode.Visible = false;
                txtDefaultBankCode.Visible = false;

                lblDefaultCashierCode.Visible = false;
                txtDefaultCashierCode.Visible = false;
            }
        }
    }
}
