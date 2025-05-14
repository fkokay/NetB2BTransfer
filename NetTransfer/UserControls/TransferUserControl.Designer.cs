namespace NetTransfer.UserControls
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
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            btnLastTransferClear = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            cmbTransferType = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            btnTransfer = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbTransferType.Properties).BeginInit();
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
            gridViewLog.OptionsView.ShowGroupPanel = false;
            gridViewLog.RowCountChanged += gridViewLog_RowCountChanged;
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
            gridColumn2.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridColumn2.FieldName = "EventTime";
            gridColumn2.MinWidth = 25;
            gridColumn2.Name = "gridColumn2";
            gridColumn2.OptionsColumn.AllowEdit = false;
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2;
            gridColumn2.Width = 223;
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnLastTransferClear);
            panelControl1.Controls.Add(btnCancel);
            panelControl1.Controls.Add(cmbTransferType);
            panelControl1.Controls.Add(labelControl1);
            panelControl1.Controls.Add(btnTransfer);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(2, 28);
            panelControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1470, 66);
            panelControl1.TabIndex = 0;
            // 
            // btnLastTransferClear
            // 
            btnLastTransferClear.Location = new System.Drawing.Point(639, 5);
            btnLastTransferClear.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            btnLastTransferClear.Name = "btnLastTransferClear";
            btnLastTransferClear.Size = new System.Drawing.Size(118, 45);
            btnLastTransferClear.TabIndex = 5;
            btnLastTransferClear.Text = "Son Aktarım \r\nTarihlerini Temizle";
            btnLastTransferClear.Click += btnLastTransferClear_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(513, 4);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(118, 45);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "İptal Et";
            btnCancel.Click += btnCancel_Click;
            // 
            // cmbTransferType
            // 
            cmbTransferType.Location = new System.Drawing.Point(24, 27);
            cmbTransferType.Name = "cmbTransferType";
            cmbTransferType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbTransferType.Properties.NullText = "Aktarım Türü Seçiniz";
            cmbTransferType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbTransferType.Size = new System.Drawing.Size(342, 22);
            cmbTransferType.TabIndex = 3;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(24, 5);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(75, 16);
            labelControl1.TabIndex = 2;
            labelControl1.Text = "Aktarım Türü";
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new System.Drawing.Point(387, 5);
            btnTransfer.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new System.Drawing.Size(118, 45);
            btnTransfer.TabIndex = 1;
            btnTransfer.Text = "Aktar";
            btnTransfer.Click += btnTransfer_Click;
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
            ((System.ComponentModel.ISupportInitialize)gridControlLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbTransferType.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnTransfer;
        private DevExpress.XtraGrid.GridControl gridControlLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLog;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTransferType;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnLastTransferClear;
    }
}
