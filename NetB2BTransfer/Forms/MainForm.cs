using DevExpress.XtraBars;
using NetB2BTransfer.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetB2BTransfer.Forms
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();

            TransferUserControl transferUserControl = new TransferUserControl();
            transferUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(transferUserControl);
        }

        private void btnB2BSetting_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();
            B2BSettingUserControl b2BSettingUserControl = new B2BSettingUserControl();
            b2BSettingUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(b2BSettingUserControl);
        }

        private void btnErpSetting_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();
            ErpSettingUserControl erpSettingUserControl = new ErpSettingUserControl();
            erpSettingUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(erpSettingUserControl);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();
            LogUserControl logUserControl = new LogUserControl();
            logUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(logUserControl);
        }
    }
}
