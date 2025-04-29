using DevExpress.XtraEditors;
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

                MessageBox.Show("Servis başarıyla yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

            }
        }

        private void btnUnistall_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInstaller.Uninstall("NetTransferService");

                MessageBox.Show("Servis başarıyla kaldırıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void ServiceUserControl_Load(object sender, EventArgs e)
        {
            var result = ServiceInstaller.GetServiceStatus("NetTransferService");
            if (result == ServiceState.Unknown || result == ServiceState.NotFound)
            {
                btnInstall.Enabled = true;
                btnUnistall.Enabled = false;
            }
            else
            {

                btnInstall.Enabled = false;
                btnUnistall.Enabled = true;
            }
        }
    }
}
