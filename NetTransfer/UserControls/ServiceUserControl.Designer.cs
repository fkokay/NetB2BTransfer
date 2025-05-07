namespace NetTransfer.UserControls
{
    partial class ServiceUserControl
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
            btnStop = new DevExpress.XtraEditors.SimpleButton();
            btnStart = new DevExpress.XtraEditors.SimpleButton();
            txtStatus = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            btnUnistall = new DevExpress.XtraEditors.SimpleButton();
            btnInstall = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtStatus.Properties).BeginInit();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(btnStop);
            groupControl1.Controls.Add(btnStart);
            groupControl1.Controls.Add(txtStatus);
            groupControl1.Controls.Add(labelControl1);
            groupControl1.Controls.Add(btnUnistall);
            groupControl1.Controls.Add(btnInstall);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1371, 714);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Servis Ayarları";
            // 
            // btnStop
            // 
            btnStop.Location = new System.Drawing.Point(400, 116);
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(118, 36);
            btnStop.TabIndex = 5;
            btnStop.Text = "Durdur";
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new System.Drawing.Point(276, 116);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(118, 36);
            btnStart.TabIndex = 4;
            btnStart.Text = "Başlat";
            btnStart.Click += btnStart_Click;
            // 
            // txtStatus
            // 
            txtStatus.Location = new System.Drawing.Point(28, 80);
            txtStatus.Name = "txtStatus";
            txtStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            txtStatus.Properties.Appearance.Options.UseFont = true;
            txtStatus.Properties.ReadOnly = true;
            txtStatus.Size = new System.Drawing.Size(490, 30);
            txtStatus.TabIndex = 3;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(28, 58);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(84, 16);
            labelControl1.TabIndex = 2;
            labelControl1.Text = "Servis Durumu";
            // 
            // btnUnistall
            // 
            btnUnistall.Location = new System.Drawing.Point(152, 116);
            btnUnistall.Name = "btnUnistall";
            btnUnistall.Size = new System.Drawing.Size(118, 36);
            btnUnistall.TabIndex = 1;
            btnUnistall.Text = "Kaldır";
            btnUnistall.Click += btnUnistall_Click;
            // 
            // btnInstall
            // 
            btnInstall.Location = new System.Drawing.Point(28, 116);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new System.Drawing.Size(118, 36);
            btnInstall.TabIndex = 0;
            btnInstall.Text = "Kur";
            btnInstall.Click += btnInstall_Click;
            // 
            // ServiceUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Name = "ServiceUserControl";
            Size = new System.Drawing.Size(1371, 714);
            Load += ServiceUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtStatus.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnUnistall;
        private DevExpress.XtraEditors.SimpleButton btnInstall;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
