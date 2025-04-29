namespace NetTransfer.UserControls
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
            groupControlParameter = new DevExpress.XtraEditors.GroupControl();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            cmbB2B = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtUrl = new DevExpress.XtraEditors.TextEdit();
            simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            simpleButtonTest = new DevExpress.XtraEditors.SimpleButton();
            txtUser = new DevExpress.XtraEditors.TextEdit();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupControlParameter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbB2B.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUrl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(groupControlParameter);
            groupControl1.Controls.Add(panelControl1);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1285, 746);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Sanal Mağaza Ayarları";
            // 
            // groupControlParameter
            // 
            groupControlParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControlParameter.Location = new System.Drawing.Point(2, 232);
            groupControlParameter.Name = "groupControlParameter";
            groupControlParameter.Padding = new System.Windows.Forms.Padding(10);
            groupControlParameter.Size = new System.Drawing.Size(1281, 512);
            groupControlParameter.TabIndex = 11;
            groupControlParameter.Text = "Parametreler";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(labelControl4);
            panelControl1.Controls.Add(cmbB2B);
            panelControl1.Controls.Add(labelControl1);
            panelControl1.Controls.Add(txtUrl);
            panelControl1.Controls.Add(simpleButtonSave);
            panelControl1.Controls.Add(labelControl2);
            panelControl1.Controls.Add(simpleButtonTest);
            panelControl1.Controls.Add(txtUser);
            panelControl1.Controls.Add(txtPassword);
            panelControl1.Controls.Add(labelControl3);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(2, 28);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1281, 204);
            panelControl1.TabIndex = 10;
            // 
            // labelControl4
            // 
            labelControl4.Location = new System.Drawing.Point(34, 39);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(80, 16);
            labelControl4.TabIndex = 8;
            labelControl4.Text = "Sanal Mağaza";
            // 
            // cmbB2B
            // 
            cmbB2B.Location = new System.Drawing.Point(118, 36);
            cmbB2B.Name = "cmbB2B";
            cmbB2B.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbB2B.Properties.Items.AddRange(new object[] { "Smartstore", "B2B" });
            cmbB2B.Size = new System.Drawing.Size(254, 22);
            cmbB2B.TabIndex = 9;
            cmbB2B.SelectedValueChanged += cmbB2B_SelectedValueChanged;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(34, 67);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(16, 16);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Url";
            // 
            // txtUrl
            // 
            txtUrl.Location = new System.Drawing.Point(118, 64);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new System.Drawing.Size(637, 22);
            txtUrl.TabIndex = 1;
            // 
            // simpleButtonSave
            // 
            simpleButtonSave.Location = new System.Drawing.Point(637, 148);
            simpleButtonSave.Name = "simpleButtonSave";
            simpleButtonSave.Size = new System.Drawing.Size(118, 36);
            simpleButtonSave.TabIndex = 7;
            simpleButtonSave.Text = "Kaydet";
            simpleButtonSave.Click += simpleButtonSave_Click;
            // 
            // labelControl2
            // 
            labelControl2.Location = new System.Drawing.Point(34, 95);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(67, 16);
            labelControl2.TabIndex = 2;
            labelControl2.Text = "Kullanıcı adı";
            // 
            // simpleButtonTest
            // 
            simpleButtonTest.Location = new System.Drawing.Point(513, 148);
            simpleButtonTest.Name = "simpleButtonTest";
            simpleButtonTest.Size = new System.Drawing.Size(118, 36);
            simpleButtonTest.TabIndex = 6;
            simpleButtonTest.Text = "Bağlantı Test";
            simpleButtonTest.Click += simpleButtonTest_Click;
            // 
            // txtUser
            // 
            txtUser.Location = new System.Drawing.Point(118, 92);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(637, 22);
            txtUser.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(118, 120);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(637, 22);
            txtPassword.TabIndex = 5;
            // 
            // labelControl3
            // 
            labelControl3.Location = new System.Drawing.Point(34, 123);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(27, 16);
            labelControl3.TabIndex = 4;
            labelControl3.Text = "Şifre";
            // 
            // B2BSettingUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Name = "B2BSettingUserControl";
            Size = new System.Drawing.Size(1285, 746);
            Load += B2BSettingUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupControlParameter).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbB2B.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUrl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
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
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbB2B;
        private DevExpress.XtraEditors.GroupControl groupControlParameter;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
