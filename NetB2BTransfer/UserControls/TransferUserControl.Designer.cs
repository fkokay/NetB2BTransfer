namespace NetB2BTransfer.UserControls
{
    partial class TransferUserControl
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
            gridControlLog = new DevExpress.XtraGrid.GridControl();
            gridViewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            btnTransfer = new DevExpress.XtraEditors.SimpleButton();
            cmbTransferType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(gridControlLog);
            groupControl1.Controls.Add(panelControl1);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1263, 593);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Aktarım";
            // 
            // gridControlLog
            // 
            gridControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControlLog.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            gridControlLog.Location = new System.Drawing.Point(2, 77);
            gridControlLog.MainView = gridViewLog;
            gridControlLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            gridControlLog.Name = "gridControlLog";
            gridControlLog.Size = new System.Drawing.Size(1259, 514);
            gridControlLog.TabIndex = 1;
            gridControlLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewLog });
            // 
            // gridViewLog
            // 
            gridViewLog.DetailHeight = 284;
            gridViewLog.GridControl = gridControlLog;
            gridViewLog.Name = "gridViewLog";
            gridViewLog.OptionsEditForm.PopupEditFormWidth = 686;
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnTransfer);
            panelControl1.Controls.Add(cmbTransferType);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(2, 23);
            panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1259, 54);
            panelControl1.TabIndex = 0;
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new System.Drawing.Point(332, 11);
            btnTransfer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new System.Drawing.Size(101, 29);
            btnTransfer.TabIndex = 1;
            btnTransfer.Text = "Aktar";
            btnTransfer.Click += btnTransfer_Click;
            // 
            // cmbTransferType
            // 
            cmbTransferType.FormattingEnabled = true;
            cmbTransferType.Items.AddRange(new object[] { "Cari Aktarım", "Cari Bakiye Aktarım", "Malzeme Aktarım", "Malzeme Stok Aktarım", "Malzeme Fiyat Aktarım", "Sipariş Aktarım", "SanalPos Aktarım" });
            cmbTransferType.Location = new System.Drawing.Point(22, 16);
            cmbTransferType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            cmbTransferType.Name = "cmbTransferType";
            cmbTransferType.Size = new System.Drawing.Size(305, 21);
            cmbTransferType.TabIndex = 0;
            // 
            // TransferUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "TransferUserControl";
            Size = new System.Drawing.Size(1263, 593);
            Load += TransferUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ComboBox cmbTransferType;
        private DevExpress.XtraGrid.GridControl gridControlLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLog;
        private DevExpress.XtraEditors.SimpleButton btnTransfer;
    }
}
