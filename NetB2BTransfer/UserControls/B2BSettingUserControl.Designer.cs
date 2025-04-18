namespace NetB2BTransfer.UserControls
{
    partial class B2BSettingUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupControl1 = new DevExpress.XtraEditors.GroupControl();
            simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            simpleButtonTest = new DevExpress.XtraEditors.SimpleButton();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            txtUser = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            txtUrl = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUrl.Properties).BeginInit();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(simpleButtonSave);
            groupControl1.Controls.Add(simpleButtonTest);
            groupControl1.Controls.Add(txtPassword);
            groupControl1.Controls.Add(labelControl3);
            groupControl1.Controls.Add(txtUser);
            groupControl1.Controls.Add(labelControl2);
            groupControl1.Controls.Add(txtUrl);
            groupControl1.Controls.Add(labelControl1);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(889, 376);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "B2B Ayarları";
            // 
            // simpleButtonSave
            // 
            simpleButtonSave.Location = new System.Drawing.Point(628, 136);
            simpleButtonSave.Name = "simpleButtonSave";
            simpleButtonSave.Size = new System.Drawing.Size(118, 36);
            simpleButtonSave.TabIndex = 7;
            simpleButtonSave.Text = "Kaydet";
            simpleButtonSave.Click += simpleButtonSave_Click;
            // 
            // simpleButtonTest
            // 
            simpleButtonTest.Location = new System.Drawing.Point(504, 136);
            simpleButtonTest.Name = "simpleButtonTest";
            simpleButtonTest.Size = new System.Drawing.Size(118, 36);
            simpleButtonTest.TabIndex = 6;
            simpleButtonTest.Text = "Bağlantı Test";
            simpleButtonTest.Click += simpleButtonTest_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(98, 108);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(648, 22);
            txtPassword.TabIndex = 5;
            // 
            // labelControl3
            // 
            labelControl3.Location = new System.Drawing.Point(25, 111);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(27, 16);
            labelControl3.TabIndex = 4;
            labelControl3.Text = "Şifre";
            // 
            // txtUser
            // 
            txtUser.Location = new System.Drawing.Point(98, 80);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(648, 22);
            txtUser.TabIndex = 3;
            // 
            // labelControl2
            // 
            labelControl2.Location = new System.Drawing.Point(25, 83);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(67, 16);
            labelControl2.TabIndex = 2;
            labelControl2.Text = "Kullanıcı adı";
            // 
            // txtUrl
            // 
            txtUrl.Location = new System.Drawing.Point(98, 52);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new System.Drawing.Size(648, 22);
            txtUrl.TabIndex = 1;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(25, 55);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(16, 16);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Url";
            // 
            // B2BSettingUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Name = "B2BSettingUserControl";
            Size = new System.Drawing.Size(889, 376);
            Load += B2BSettingUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUrl.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtUrl;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTest;
    }
}
