using DevExpress.DataAccess.UI.Native.DataFederation;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
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
            if (container.Controls.Count > 0)
            {
                if (!(container.Controls[0] is TransferUserControl))
                {
                    container.Controls.Clear();

                    TransferUserControl transferUserControl = new TransferUserControl();
                    transferUserControl.Dock = DockStyle.Fill;

                    container.Controls.Add(transferUserControl);
                }
            }
        }

        private void btnB2BSetting_Click(object sender, EventArgs e)
        {
            if (TransferUserControl())
            {
                container.Controls.Clear();
                VirtualStoreSettingUserControl b2BSettingUserControl = new VirtualStoreSettingUserControl();
                b2BSettingUserControl.Dock = DockStyle.Fill;

                container.Controls.Add(b2BSettingUserControl);
            }
        }

        private bool TransferUserControl()
        {
            if (container.Controls.Count > 0)
            {
                if (container.Controls[0] is TransferUserControl)
                {
                    var isTransfer = ((TransferUserControl)container.Controls[0]).IsTransfer();
                    if (isTransfer)
                    {
                        XtraMessageBox.Show("Transfer işlemi devam ediyor. Lütfen bekleyin yada aktarım işlenini iptal edin");
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnErpSetting_Click(object sender, EventArgs e)
        {
            if (TransferUserControl())
            {
                container.Controls.Clear();
                ErpSettingUserControl erpSettingUserControl = new ErpSettingUserControl();
                erpSettingUserControl.Dock = DockStyle.Fill;

                container.Controls.Add(erpSettingUserControl);
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (TransferUserControl())
            {
                container.Controls.Clear();
                LogUserControl logUserControl = new LogUserControl();
                logUserControl.Dock = DockStyle.Fill;

                container.Controls.Add(logUserControl);
            }
        }

        private void btnServiceSetting_Click(object sender, EventArgs e)
        {
            if (TransferUserControl())
            {
                container.Controls.Clear();
                ServiceUserControl serviceUserControl = new ServiceUserControl();
                serviceUserControl.Dock = DockStyle.Fill;

                container.Controls.Add(serviceUserControl);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HomeInitialize();

            Timer timer = new Timer();
            timer.Interval = 30000;
            timer.Tick += (s, args) =>
            {
                ServiceInitialize();
            };
            timer.Start();
            ServiceInitialize();
        }

        private void HomeInitialize()
        {
            container.Controls.Clear();
            HomeUserControl homeUserControl = new HomeUserControl();
            homeUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(homeUserControl);
        }

        public void ServiceInitialize()
        {
            try
            {
                var result = ServiceInstaller.GetServiceStatus("NetTransferService");
                if (result == ServiceState.Unknown || result == ServiceState.NotFound)
                {
                    txtServiceStatus.Caption = "Servis Kurulu Değil";
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Red;
                }
                else if (result == ServiceState.Running)
                {
                    txtServiceStatus.Caption = "Servis Çalışıyor";
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Green;
                }
                else if (result == ServiceState.Stopped)
                {
                    txtServiceStatus.Caption = "Servis Durduruldu";
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Red;
                }
                else
                {
                    txtServiceStatus.Caption = "Servis Durumu Bilinmiyor";
                    txtServiceStatus.ItemAppearance.Normal.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnQueryBuilder_Click(object sender, EventArgs e)
        {
        }

        private void btnExcelTransfer_Click(object sender, EventArgs e)
        {
            container.Controls.Clear();
            ExcelUserControl excelUserControl = new ExcelUserControl();
            excelUserControl.Dock = DockStyle.Fill;

            container.Controls.Add(excelUserControl);
        }
    }
}
