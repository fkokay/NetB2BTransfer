namespace NetTransfer.UserControls
{
    partial class ErpSettingUserControl
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
            xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            xtraTabPageErp = new DevExpress.XtraTab.XtraTabPage();
            txtErpPassword = new DevExpress.XtraEditors.TextEdit();
            labelControl8 = new DevExpress.XtraEditors.LabelControl();
            txtErpUser = new DevExpress.XtraEditors.TextEdit();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            txtRestUrl = new DevExpress.XtraEditors.TextEdit();
            labelControl6 = new DevExpress.XtraEditors.LabelControl();
            txtSqlPassword = new DevExpress.XtraEditors.TextEdit();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            txtSqlUser = new DevExpress.XtraEditors.TextEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            txtSqlDatabase = new DevExpress.XtraEditors.TextEdit();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            txtSqlServer = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            panelControl3 = new DevExpress.XtraEditors.PanelControl();
            btnSave = new System.Windows.Forms.Button();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            cmbErp = new System.Windows.Forms.ComboBox();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xtraTabControl).BeginInit();
            xtraTabControl.SuspendLayout();
            xtraTabPageErp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtErpPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtErpUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtRestUrl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlDatabase.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlServer.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl3).BeginInit();
            panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(xtraTabControl);
            groupControl1.Controls.Add(panelControl2);
            groupControl1.Controls.Add(panelControl1);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1280, 667);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Erp Ayarları";
            // 
            // xtraTabControl
            // 
            xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            xtraTabControl.Location = new System.Drawing.Point(2, 100);
            xtraTabControl.Name = "xtraTabControl";
            xtraTabControl.SelectedTabPage = xtraTabPageErp;
            xtraTabControl.Size = new System.Drawing.Size(1276, 512);
            xtraTabControl.TabIndex = 2;
            xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { xtraTabPageErp });
            // 
            // xtraTabPageErp
            // 
            xtraTabPageErp.Controls.Add(txtErpPassword);
            xtraTabPageErp.Controls.Add(labelControl8);
            xtraTabPageErp.Controls.Add(txtErpUser);
            xtraTabPageErp.Controls.Add(labelControl7);
            xtraTabPageErp.Controls.Add(txtRestUrl);
            xtraTabPageErp.Controls.Add(labelControl6);
            xtraTabPageErp.Controls.Add(txtSqlPassword);
            xtraTabPageErp.Controls.Add(labelControl5);
            xtraTabPageErp.Controls.Add(txtSqlUser);
            xtraTabPageErp.Controls.Add(labelControl4);
            xtraTabPageErp.Controls.Add(txtSqlDatabase);
            xtraTabPageErp.Controls.Add(labelControl3);
            xtraTabPageErp.Controls.Add(txtSqlServer);
            xtraTabPageErp.Controls.Add(labelControl2);
            xtraTabPageErp.Name = "xtraTabPageErp";
            xtraTabPageErp.Size = new System.Drawing.Size(1274, 482);
            xtraTabPageErp.Text = "Genel Ayarlar";
            // 
            // txtErpPassword
            // 
            txtErpPassword.Location = new System.Drawing.Point(127, 188);
            txtErpPassword.Name = "txtErpPassword";
            txtErpPassword.Size = new System.Drawing.Size(455, 22);
            txtErpPassword.TabIndex = 13;
            // 
            // labelControl8
            // 
            labelControl8.Location = new System.Drawing.Point(29, 191);
            labelControl8.Name = "labelControl8";
            labelControl8.Size = new System.Drawing.Size(78, 16);
            labelControl8.TabIndex = 12;
            labelControl8.Text = "Erp Password";
            // 
            // txtErpUser
            // 
            txtErpUser.Location = new System.Drawing.Point(127, 160);
            txtErpUser.Name = "txtErpUser";
            txtErpUser.Size = new System.Drawing.Size(455, 22);
            txtErpUser.TabIndex = 11;
            // 
            // labelControl7
            // 
            labelControl7.Location = new System.Drawing.Point(29, 163);
            labelControl7.Name = "labelControl7";
            labelControl7.Size = new System.Drawing.Size(49, 16);
            labelControl7.TabIndex = 10;
            labelControl7.Text = "Erp User";
            // 
            // txtRestUrl
            // 
            txtRestUrl.Location = new System.Drawing.Point(127, 132);
            txtRestUrl.Name = "txtRestUrl";
            txtRestUrl.Size = new System.Drawing.Size(455, 22);
            txtRestUrl.TabIndex = 9;
            // 
            // labelControl6
            // 
            labelControl6.Location = new System.Drawing.Point(29, 135);
            labelControl6.Name = "labelControl6";
            labelControl6.Size = new System.Drawing.Size(41, 16);
            labelControl6.TabIndex = 8;
            labelControl6.Text = "RestUrl";
            // 
            // txtSqlPassword
            // 
            txtSqlPassword.Location = new System.Drawing.Point(127, 104);
            txtSqlPassword.Name = "txtSqlPassword";
            txtSqlPassword.Size = new System.Drawing.Size(455, 22);
            txtSqlPassword.TabIndex = 7;
            // 
            // labelControl5
            // 
            labelControl5.Location = new System.Drawing.Point(29, 107);
            labelControl5.Name = "labelControl5";
            labelControl5.Size = new System.Drawing.Size(77, 16);
            labelControl5.TabIndex = 6;
            labelControl5.Text = "Sql Password";
            // 
            // txtSqlUser
            // 
            txtSqlUser.Location = new System.Drawing.Point(127, 76);
            txtSqlUser.Name = "txtSqlUser";
            txtSqlUser.Size = new System.Drawing.Size(455, 22);
            txtSqlUser.TabIndex = 5;
            // 
            // labelControl4
            // 
            labelControl4.Location = new System.Drawing.Point(29, 79);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(48, 16);
            labelControl4.TabIndex = 4;
            labelControl4.Text = "Sql User";
            // 
            // txtSqlDatabase
            // 
            txtSqlDatabase.Location = new System.Drawing.Point(127, 48);
            txtSqlDatabase.Name = "txtSqlDatabase";
            txtSqlDatabase.Size = new System.Drawing.Size(455, 22);
            txtSqlDatabase.TabIndex = 3;
            // 
            // labelControl3
            // 
            labelControl3.Location = new System.Drawing.Point(29, 51);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(53, 16);
            labelControl3.TabIndex = 2;
            labelControl3.Text = "Database";
            // 
            // txtSqlServer
            // 
            txtSqlServer.Location = new System.Drawing.Point(127, 20);
            txtSqlServer.Name = "txtSqlServer";
            txtSqlServer.Size = new System.Drawing.Size(455, 22);
            txtSqlServer.TabIndex = 1;
            // 
            // labelControl2
            // 
            labelControl2.Location = new System.Drawing.Point(29, 23);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(38, 16);
            labelControl2.TabIndex = 0;
            labelControl2.Text = "Server";
            // 
            // panelControl2
            // 
            panelControl2.Controls.Add(panelControl3);
            panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelControl2.Location = new System.Drawing.Point(2, 612);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1276, 53);
            panelControl2.TabIndex = 1;
            // 
            // panelControl3
            // 
            panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl3.Controls.Add(btnSave);
            panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            panelControl3.Location = new System.Drawing.Point(1108, 2);
            panelControl3.Name = "panelControl3";
            panelControl3.Padding = new System.Windows.Forms.Padding(5);
            panelControl3.Size = new System.Drawing.Size(166, 49);
            panelControl3.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            btnSave.Location = new System.Drawing.Point(5, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(156, 39);
            btnSave.TabIndex = 0;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(cmbErp);
            panelControl1.Controls.Add(labelControl1);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(2, 28);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1276, 72);
            panelControl1.TabIndex = 0;
            // 
            // cmbErp
            // 
            cmbErp.FormattingEnabled = true;
            cmbErp.Items.AddRange(new object[] { "Netsis", "Logo", "Opak" });
            cmbErp.Location = new System.Drawing.Point(54, 24);
            cmbErp.Name = "cmbErp";
            cmbErp.Size = new System.Drawing.Size(255, 24);
            cmbErp.TabIndex = 1;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(29, 27);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(19, 16);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Erp";
            // 
            // ErpSettingUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Name = "ErpSettingUserControl";
            Size = new System.Drawing.Size(1280, 667);
            Load += ErpSettingUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)xtraTabControl).EndInit();
            xtraTabControl.ResumeLayout(false);
            xtraTabPageErp.ResumeLayout(false);
            xtraTabPageErp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtErpPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtErpUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtRestUrl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlDatabase.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSqlServer.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl3).EndInit();
            panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cmbErp;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Button btnSave;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageErp;
        private DevExpress.XtraEditors.TextEdit txtErpUser;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtRestUrl;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtSqlPassword;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtSqlUser;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtSqlDatabase;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSqlServer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtErpPassword;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}
