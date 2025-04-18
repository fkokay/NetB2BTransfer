using DevExpress.XtraEditors;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetB2BTransfer.UserControls
{
    public partial class B2BSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetB2BTransferContext _context;
        public B2BSettingUserControl()
        {
            InitializeComponent();
            _context = new NetB2BTransferContext("Data Source=(local);Initial Catalog=B2BENT2;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;");
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var b2bSetting = _context.B2BSetting.FirstOrDefault();
            if (b2bSetting != null)
            {
                b2bSetting.Url = txtUrl.Text;
                b2bSetting.User = txtUser.Text;
                b2bSetting.Password = txtPassword.Text;
            }
            else
            {
                b2bSetting = new B2BSetting
                {
                    Url = txtUrl.Text,
                    User = txtUser.Text,
                    Password = txtPassword.Text
                };
                _context.B2BSetting.Add(b2bSetting);
            }

            _context.SaveChanges();
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

                    var response = await client.PostAsync(txtUrl.Text+"/entegrasyon/giris", formData);
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
            var b2bSetting = _context.B2BSetting.FirstOrDefault();
            if (b2bSetting != null)
            {
                txtUrl.Text = b2bSetting.Url;
                txtPassword.Text = b2bSetting.Password;
                txtUser.Text = b2bSetting.User;

            }
        }
    }
}
