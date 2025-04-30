using DevExpress.XtraBars;
using NetTransfer.Models;
using NetTransfer.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            VirtualStoreSettingUserControl b2BSettingUserControl = new VirtualStoreSettingUserControl();
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
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Green;
                }
                else if (result == ServiceState.Unknown || result == ServiceState.NotFound)
                {
                    txtServiceStatus.Caption = "Servis Kurulu Değil";
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Red;
                }
                else
                {
                    txtServiceStatus.Caption = "Servis Durduruldu";
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
