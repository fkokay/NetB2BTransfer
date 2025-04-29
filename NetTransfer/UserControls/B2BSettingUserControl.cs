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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class B2BSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetTransferContext _context;
        public B2BSettingUserControl()
        {
            InitializeComponent();
            _context = new NetTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var b2bSetting = _context.VirtualStoreSetting.FirstOrDefault();
            if (b2bSetting != null)
            {
                b2bSetting.Url = txtUrl.Text;
                b2bSetting.User = txtUser.Text;
                b2bSetting.Password = txtPassword.Text;
            }
            else
            {
                b2bSetting = new VirtualStoreSetting
                {
                    Url = txtUrl.Text,
                    User = txtUser.Text,
                    Password = txtPassword.Text
                };
                _context.VirtualStoreSetting.Add(b2bSetting);
            }

            _context.SaveChanges();

            if (cmbB2B.SelectedText == "B2B")
            {

            }
            else if (cmbB2B.SelectedText == "Smartstore")
            {
                (groupControlParameter.Controls[0] as SmartstoreParameterUserControl).Save();
            }
            XtraMessageBox.Show("Ayarlar başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void simpleButtonTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(txtUrl.Text);
                    var formData = new MultipartFormDataContent
                    {
                        { new StringContent(txtUser.Text), "kullanici_adi" },
                        { new StringContent(txtPassword.Text), "sifre" }
                    };

                    var response = await client.PostAsync(txtUrl.Text + "/entegrasyon/giris", formData);
                    if (response.IsSuccessStatusCode)
                    {
                        XtraMessageBox.Show("Bağlantı başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show($"Bağlantı başarısız. Hata kodu: {response.StatusCode}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bağlantı sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void B2BSettingUserControl_Load(object sender, EventArgs e)
        {
            var b2bSetting = _context.VirtualStoreSetting.FirstOrDefault();
            if (b2bSetting != null)
            {
                cmbB2B.Text = b2bSetting.VirtualStore;
                txtUrl.Text = b2bSetting.Url;
                txtPassword.Text = b2bSetting.Password;
                txtUser.Text = b2bSetting.User;

                cmbB2B_SelectedValueChanged(sender, e);
            }
        }

        private void cmbB2B_SelectedValueChanged(object sender, EventArgs e)
        {
            groupControlParameter.Controls.Clear();
            if (cmbB2B.Text == "B2B")
            {
                B2BParameterUserControl control = new B2BParameterUserControl();
                control.Dock = DockStyle.Fill;

                groupControlParameter.Controls.Add(control);
            }

            if (cmbB2B.Text == "Smartstore")
            {
                SmartstoreParameterUserControl control = new SmartstoreParameterUserControl();
                control.Dock = DockStyle.Fill;

                groupControlParameter.Controls.Add(control);
            }
        }
    }
}
