namespace NetTransfer.UserControls
{
    partial class LogUserControl
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
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).BeginInit();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(gridControlLog);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl1.Location = new System.Drawing.Point(0, 0);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(1108, 587);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "Günlük";
            // 
            // gridControlLog
            // 
            gridControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControlLog.Location = new System.Drawing.Point(2, 28);
            gridControlLog.MainView = gridViewLog;
            gridControlLog.Name = "gridControlLog";
            gridControlLog.Size = new System.Drawing.Size(1104, 557);
            gridControlLog.TabIndex = 0;
            gridControlLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewLog });
            // 
            // gridViewLog
            // 
            gridViewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn4, gridColumn1, gridColumn3, gridColumn2 });
            gridViewLog.GridControl = gridControlLog;
            gridViewLog.Name = "gridViewLog";
            gridViewLog.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "Kaynak";
            gridColumn4.FieldName = "Source";
            gridColumn4.MinWidth = 25;
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 0;
            gridColumn4.Width = 157;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Türü";
            gridColumn1.FieldName = "EventLevel";
            gridColumn1.MinWidth = 25;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.OptionsColumn.AllowEdit = false;
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1;
            gridColumn1.Width = 108;
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
            gridColumn3.VisibleIndex = 2;
            gridColumn3.Width = 678;
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
            gridColumn2.VisibleIndex = 3;
            gridColumn2.Width = 133;
            // 
            // LogUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl1);
            Name = "LogUserControl";
            Size = new System.Drawing.Size(1108, 587);
            Load += LogUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControlLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLog;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}
