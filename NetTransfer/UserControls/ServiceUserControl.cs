using DevExpress.XtraEditors;
using NetTransfer.Forms;
using NetTransfer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class ServiceUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public ServiceUserControl()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                string applicationPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string servicePath = "Service";
                string appFileName = "NetTransferService.exe";
                string path = Path.Combine(applicationPath, servicePath, appFileName);

                ServiceInstaller.InstallAndStart("NetTransferService", "NetTransfer Service", path);

                XtraMessageBox.Show("NetTransferService başarıyla kuruldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ServiceInitialize();
            }
        }

        private void btnUnistall_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInstaller.Uninstall("NetTransferService");

                XtraMessageBox.Show("NetTransferService başarıyla kaldırıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ServiceInitialize();
            }
        }

        private void ServiceUserControl_Load(object sender, EventArgs e)
        {
            ServiceInitialize();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInstaller.StartService("NetTransferService");

                XtraMessageBox.Show("NetTransferService başarıyla başlatıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ServiceInitialize();
            }
        }

        private void ServiceInitialize()
        {
            try
            {
                var result = ServiceInstaller.GetServiceStatus("NetTransferService");
                if (result == ServiceState.Unknown || result == ServiceState.NotFound)
                {
                    btnInstall.Enabled = true;
                    btnUnistall.Enabled = false;
                    btnStart.Enabled = false;
                    btnStop.Enabled = false;

                    txtStatus.Text = "Servis Kurulu Değil";
                }
                else if (result == ServiceState.Running)
                {
                    btnInstall.Enabled = false;
                    btnUnistall.Enabled = true;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    txtStatus.Text = "Servis Çalışıyor";
                }
                else if (result == ServiceState.Stopped)
                {
                    btnInstall.Enabled = false;
                    btnUnistall.Enabled = true;
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    txtStatus.Text = "Servis Durduruldu";
                }
                else
                {
                    txtStatus.Text = "Kontrol Ediliyor";
                }

                (this.Parent.Parent as MainForm).ServiceInitialize();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInstaller.StopService("NetTransferService");

                XtraMessageBox.Show("NetTransferService başarıyla durduruldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { ServiceInitialize(); }
        }
    }
}
