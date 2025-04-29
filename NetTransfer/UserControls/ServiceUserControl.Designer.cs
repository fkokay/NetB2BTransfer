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
            btnUnistall = new DevExpress.XtraEditors.SimpleButton();
            btnInstall = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(btnUnistall);
            groupControl1.Controls.Add(btnInstall);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1371, 714);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Servis Ayarları";
            // 
            // btnUnistall
            // 
            btnUnistall.Location = new System.Drawing.Point(158, 61);
            btnUnistall.Name = "btnUnistall";
            btnUnistall.Size = new System.Drawing.Size(118, 36);
            btnUnistall.TabIndex = 1;
            btnUnistall.Text = "Kaldır";
            btnUnistall.Click += btnUnistall_Click;
            // 
            // btnInstall
            // 
            btnInstall.Location = new System.Drawing.Point(34, 61);
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
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnUnistall;
        private DevExpress.XtraEditors.SimpleButton btnInstall;
    }
}
