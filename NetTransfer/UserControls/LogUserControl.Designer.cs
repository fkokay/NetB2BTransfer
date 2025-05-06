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
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            btnFilter = new DevExpress.XtraEditors.SimpleButton();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtDate = new DevExpress.XtraEditors.DateEdit();
            txtTime = new DevExpress.XtraEditors.TimeEdit();
            btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewLog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtDate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDate.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTime.Properties).BeginInit();
            SuspendLayout();
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(gridControlLog);
            groupControl1.Controls.Add(panelControl1);
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
            gridControlLog.Location = new System.Drawing.Point(2, 123);
            gridControlLog.MainView = gridViewLog;
            gridControlLog.Name = "gridControlLog";
            gridControlLog.Size = new System.Drawing.Size(1104, 462);
            gridControlLog.TabIndex = 0;
            gridControlLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewLog });
            // 
            // gridViewLog
            // 
            gridViewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn4, gridColumn1, gridColumn3, gridColumn2 });
            gridViewLog.GridControl = gridControlLog;
            gridViewLog.Name = "gridViewLog";
            gridViewLog.OptionsView.ShowGroupPanel = false;
            gridViewLog.RowCountChanged += gridViewLog_RowCountChanged;
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
            // panelControl1
            // 
            panelControl1.Controls.Add(btnClear);
            panelControl1.Controls.Add(labelControl2);
            panelControl1.Controls.Add(btnFilter);
            panelControl1.Controls.Add(labelControl1);
            panelControl1.Controls.Add(txtDate);
            panelControl1.Controls.Add(txtTime);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(2, 28);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1104, 95);
            panelControl1.TabIndex = 1;
            // 
            // labelControl2
            // 
            labelControl2.Location = new System.Drawing.Point(57, 53);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(26, 16);
            labelControl2.TabIndex = 4;
            labelControl2.Text = "Saat";
            // 
            // btnFilter
            // 
            btnFilter.Location = new System.Drawing.Point(381, 25);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new System.Drawing.Size(118, 47);
            btnFilter.TabIndex = 2;
            btnFilter.Text = "Filtrele";
            btnFilter.Click += btnFilter_Click;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(57, 25);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(30, 16);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Tarih";
            // 
            // txtDate
            // 
            txtDate.EditValue = null;
            txtDate.Location = new System.Drawing.Point(93, 22);
            txtDate.Name = "txtDate";
            txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtDate.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            txtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            txtDate.Properties.EditFormat.FormatString = "dd.MM.yyyy";
            txtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            txtDate.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            txtDate.Properties.MaskSettings.Set("mask", "dd.MM.yyyy");
            txtDate.Size = new System.Drawing.Size(270, 22);
            txtDate.TabIndex = 3;
            // 
            // txtTime
            // 
            txtTime.EditValue = null;
            txtTime.Location = new System.Drawing.Point(93, 50);
            txtTime.Name = "txtTime";
            txtTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtTime.Properties.DisplayFormat.FormatString = "HH:mm";
            txtTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            txtTime.Properties.EditFormat.FormatString = "hh:mm";
            txtTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            txtTime.Properties.MaskSettings.Set("mask", "HH:mm");
            txtTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            txtTime.Size = new System.Drawing.Size(270, 22);
            txtTime.TabIndex = 5;
            // 
            // btnClear
            // 
            btnClear.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            btnClear.Appearance.ForeColor = System.Drawing.Color.White;
            btnClear.Appearance.Options.UseBackColor = true;
            btnClear.Appearance.Options.UseForeColor = true;
            btnClear.Location = new System.Drawing.Point(505, 25);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(118, 47);
            btnClear.TabIndex = 6;
            btnClear.Text = "Temizle";
            btnClear.Click += btnClear_Click;
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
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtDate.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDate.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTime.Properties).EndInit();
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
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraEditors.TimeEdit txtTime;
        private DevExpress.XtraEditors.SimpleButton btnClear;
    }
}
