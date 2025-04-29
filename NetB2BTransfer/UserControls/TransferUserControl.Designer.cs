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
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            btnTransfer = new DevExpress.XtraEditors.SimpleButton();
            cmbTransferType = new System.Windows.Forms.ComboBox();
            gridControlLog = new DevExpress.XtraGrid.GridControl();
            gridViewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).BeginInit();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(gridControlLog);
            groupControl1.Controls.Add(panelControl1);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1474, 730);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Aktarım";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnTransfer);
            panelControl1.Controls.Add(cmbTransferType);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(2, 28);
            panelControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1470, 66);
            panelControl1.TabIndex = 0;
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new System.Drawing.Point(387, 14);
            btnTransfer.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new System.Drawing.Size(118, 36);
            btnTransfer.TabIndex = 1;
            btnTransfer.Text = "Aktar";
            btnTransfer.Click += btnTransfer_Click;
            // 
            // cmbTransferType
            // 
            cmbTransferType.FormattingEnabled = true;
            cmbTransferType.Items.AddRange(new object[] { "Cari Aktarım", "Cari Bakiye Aktarım", "Malzeme Aktarım", "Malzeme Stok Aktarım", "Malzeme Fiyat Aktarım", "Sipariş Aktarım", "SanalPos Aktarım" });
            cmbTransferType.Location = new System.Drawing.Point(26, 20);
            cmbTransferType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            cmbTransferType.Name = "cmbTransferType";
            cmbTransferType.Size = new System.Drawing.Size(355, 24);
            cmbTransferType.TabIndex = 0;
            // 
            // gridControlLog
            // 
            gridControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControlLog.Location = new System.Drawing.Point(2, 94);
            gridControlLog.MainView = gridViewLog;
            gridControlLog.Name = "gridControlLog";
            gridControlLog.Size = new System.Drawing.Size(1470, 634);
            gridControlLog.TabIndex = 1;
            gridControlLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewLog });
            // 
            // gridViewLog
            // 
            gridViewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn3, gridColumn2 });
            gridViewLog.GridControl = gridControlLog;
            gridViewLog.Name = "gridViewLog";
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Türü";
            gridColumn1.FieldName = "EventLevel";
            gridColumn1.MinWidth = 25;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.OptionsColumn.AllowEdit = false;
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 186;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Mesaj";
            gridColumn3.FieldName = "EventMessage";
            gridColumn3.MinWidth = 25;
            gridColumn3.Name = "gridColumn3";
            gridColumn3.OptionsColumn.AllowEdit = false;
            gridColumn3.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 1;
            gridColumn3.Width = 1158;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "Tarih";
            gridColumn2.FieldName = "EventTime";
            gridColumn2.MinWidth = 25;
            gridColumn2.Name = "gridColumn2";
            gridColumn2.OptionsColumn.AllowEdit = false;
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            gridColumn2.Width = 223;
            // 
            // TransferUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            Name = "TransferUserControl";
            Size = new System.Drawing.Size(1474, 730);
            Load += TransferUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ComboBox cmbTransferType;
        private DevExpress.XtraEditors.SimpleButton btnTransfer;
        private DevExpress.XtraGrid.GridControl gridControlLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLog;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}
