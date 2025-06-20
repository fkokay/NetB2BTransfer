namespace NetTransfer.UserControls
{
    partial class ExcelUserControl
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
            btnFile = new DevExpress.XtraEditors.ButtonEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            btnTransfer = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)btnFile.Properties).BeginInit();
            SuspendLayout();
            // 
            // btnFile
            // 
            btnFile.Location = new System.Drawing.Point(51, 63);
            btnFile.Name = "btnFile";
            btnFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            btnFile.Size = new System.Drawing.Size(335, 22);
            btnFile.TabIndex = 0;
            btnFile.ButtonClick += btnFile_ButtonClickAsync;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(51, 41);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(59, 16);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "Dosya Seç";
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new System.Drawing.Point(262, 91);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new System.Drawing.Size(124, 33);
            btnTransfer.TabIndex = 2;
            btnTransfer.Text = "Aktarımı Başlat";
            btnTransfer.Click += btnTransfer_Click;
            // 
            // ExcelUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(btnTransfer);
            Controls.Add(labelControl1);
            Controls.Add(btnFile);
            Name = "ExcelUserControl";
            Size = new System.Drawing.Size(1549, 810);
            ((System.ComponentModel.ISupportInitialize)btnFile.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit btnFile;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnTransfer;
    }
}
