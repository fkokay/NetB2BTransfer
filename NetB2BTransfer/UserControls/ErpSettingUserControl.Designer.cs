namespace NetB2BTransfer.UserControls
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
            xtraTabPageNetsis = new DevExpress.XtraTab.XtraTabPage();
            xtraTabPageLogo = new DevExpress.XtraTab.XtraTabPage();
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
            xtraTabControl.SelectedTabPage = xtraTabPageNetsis;
            xtraTabControl.Size = new System.Drawing.Size(1276, 512);
            xtraTabControl.TabIndex = 2;
            xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { xtraTabPageNetsis, xtraTabPageLogo });
            // 
            // xtraTabPageNetsis
            // 
            xtraTabPageNetsis.Name = "xtraTabPageNetsis";
            xtraTabPageNetsis.Size = new System.Drawing.Size(1274, 482);
            xtraTabPageNetsis.Text = "Netsis";
            // 
            // xtraTabPageLogo
            // 
            xtraTabPageLogo.Name = "xtraTabPageLogo";
            xtraTabPageLogo.Size = new System.Drawing.Size(1274, 482);
            xtraTabPageLogo.Text = "Logo";
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
            cmbErp.Items.AddRange(new object[] { "Netsis", "Logo" });
            cmbErp.Location = new System.Drawing.Point(54, 24);
            cmbErp.Name = "cmbErp";
            cmbErp.Size = new System.Drawing.Size(255, 24);
            cmbErp.TabIndex = 1;
            cmbErp.TextChanged += cmbErp_TextChanged;
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
        private DevExpress.XtraTab.XtraTabPage xtraTabPageNetsis;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageLogo;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Button btnSave;
    }
}
