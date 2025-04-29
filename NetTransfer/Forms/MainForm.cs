using DevExpress.XtraBars;
using NetTransfer.B2B.Library.SmartStore;
using NetTransfer.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetTransfer.Forms
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

        private void btnServiceSetting_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();
            ServiceUserControl serviceUserControl = new ServiceUserControl();
            serviceUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(serviceUserControl);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ServiceInitialize();
        }

        private void ServiceInitialize()
        {
            try
            {
                var result = ServiceInstaller.GetServiceStatus("NetTransferService");
                if (result == ServiceState.Running)
                {
                    txtServiceStatus.Caption = "Servis Çalışıyor";
                }
                else if (result == ServiceState.Unknown || result == ServiceState.NotFound)
                {
                    txtServiceStatus.Caption = "Servis Kurulu Değil";
                }
                else
                {
                    txtServiceStatus.Caption = "Servis Durduruldu";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
