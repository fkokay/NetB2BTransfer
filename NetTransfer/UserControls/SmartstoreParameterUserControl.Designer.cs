namespace NetTransfer.UserControls
{
    partial class SmartstoreParameterUserControl
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
            groupControlCustomer = new DevExpress.XtraEditors.GroupControl();
            txtProductLastTransfer = new DevExpress.XtraEditors.TextEdit();
            txtProductTransferMinute = new DevExpress.XtraEditors.SpinEdit();
            labelControl11 = new DevExpress.XtraEditors.LabelControl();
            txtProductFilter = new DevExpress.XtraEditors.TextEdit();
            labelControl10 = new DevExpress.XtraEditors.LabelControl();
            labelControl9 = new DevExpress.XtraEditors.LabelControl();
            groupControl1 = new DevExpress.XtraEditors.GroupControl();
            txtProductStockLastTransfer = new DevExpress.XtraEditors.TextEdit();
            txtProductStockTransferMinute = new DevExpress.XtraEditors.SpinEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtProductStockFilter = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            groupControl2 = new DevExpress.XtraEditors.GroupControl();
            txtProductPriceLastTransfer = new DevExpress.XtraEditors.TextEdit();
            txtProductPriceTransferMinute = new DevExpress.XtraEditors.SpinEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            txtProductPriceFilter = new DevExpress.XtraEditors.TextEdit();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            labelControl6 = new DevExpress.XtraEditors.LabelControl();
            groupControl4 = new DevExpress.XtraEditors.GroupControl();
            txtOrderStatusId = new DevExpress.XtraEditors.TextEdit();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            txtOrderTransferMinute = new DevExpress.XtraEditors.SpinEdit();
            labelControl15 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)groupControlCustomer).BeginInit();
            groupControlCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductLastTransfer.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProductTransferMinute.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProductFilter.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductStockLastTransfer.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProductStockTransferMinute.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProductStockFilter.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
            groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductPriceLastTransfer.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProductPriceTransferMinute.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProductPriceFilter.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupControl4).BeginInit();
            groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtOrderStatusId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtOrderTransferMinute.Properties).BeginInit();
            SuspendLayout();
            // 
            // groupControlCustomer
            // 
            groupControlCustomer.Controls.Add(txtProductLastTransfer);
            groupControlCustomer.Controls.Add(txtProductTransferMinute);
            groupControlCustomer.Controls.Add(labelControl11);
            groupControlCustomer.Controls.Add(txtProductFilter);
            groupControlCustomer.Controls.Add(labelControl10);
            groupControlCustomer.Controls.Add(labelControl9);
            groupControlCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            groupControlCustomer.Location = new System.Drawing.Point(0, 0);
            groupControlCustomer.Name = "groupControlCustomer";
            groupControlCustomer.Size = new System.Drawing.Size(963, 141);
            groupControlCustomer.TabIndex = 2;
            groupControlCustomer.Text = "Malzeme Aktarım Ayarları";
            // 
            // txtProductLastTransfer
            // 
            txtProductLastTransfer.Location = new System.Drawing.Point(223, 103);
            txtProductLastTransfer.Name = "txtProductLastTransfer";
            txtProductLastTransfer.Properties.ReadOnly = true;
            txtProductLastTransfer.Size = new System.Drawing.Size(455, 22);
            txtProductLastTransfer.TabIndex = 9;
            // 
            // txtProductTransferMinute
            // 
            txtProductTransferMinute.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            txtProductTransferMinute.Location = new System.Drawing.Point(223, 46);
            txtProductTransferMinute.Name = "txtProductTransferMinute";
            txtProductTransferMinute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtProductTransferMinute.Size = new System.Drawing.Size(252, 24);
            txtProductTransferMinute.TabIndex = 8;
            // 
            // labelControl11
            // 
            labelControl11.Location = new System.Drawing.Point(16, 106);
            labelControl11.Name = "labelControl11";
            labelControl11.Size = new System.Drawing.Size(107, 16);
            labelControl11.TabIndex = 6;
            labelControl11.Text = "Son Aktarım Tarihi";
            // 
            // txtProductFilter
            // 
            txtProductFilter.Location = new System.Drawing.Point(223, 75);
            txtProductFilter.Name = "txtProductFilter";
            txtProductFilter.Size = new System.Drawing.Size(455, 22);
            txtProductFilter.TabIndex = 5;
            // 
            // labelControl10
            // 
            labelControl10.Location = new System.Drawing.Point(16, 78);
            labelControl10.Name = "labelControl10";
            labelControl10.Size = new System.Drawing.Size(29, 16);
            labelControl10.TabIndex = 4;
            labelControl10.Text = "Filtre";
            // 
            // labelControl9
            // 
            labelControl9.Location = new System.Drawing.Point(16, 50);
            labelControl9.Name = "labelControl9";
            labelControl9.Size = new System.Drawing.Size(135, 16);
            labelControl9.TabIndex = 2;
            labelControl9.Text = "Aktarım Süresi (Dakika)";
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(txtProductStockLastTransfer);
            groupControl1.Controls.Add(txtProductStockTransferMinute);
            groupControl1.Controls.Add(labelControl1);
            groupControl1.Controls.Add(txtProductStockFilter);
            groupControl1.Controls.Add(labelControl2);
            groupControl1.Controls.Add(labelControl3);
            groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            groupControl1.Location = new System.Drawing.Point(0, 141);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(963, 141);
            groupControl1.TabIndex = 3;
            groupControl1.Text = "Malzeme Stok Aktarım Ayarları";
            // 
            // txtProductStockLastTransfer
            // 
            txtProductStockLastTransfer.Location = new System.Drawing.Point(223, 98);
            txtProductStockLastTransfer.Name = "txtProductStockLastTransfer";
            txtProductStockLastTransfer.Properties.ReadOnly = true;
            txtProductStockLastTransfer.Size = new System.Drawing.Size(455, 22);
            txtProductStockLastTransfer.TabIndex = 15;
            // 
            // txtProductStockTransferMinute
            // 
            txtProductStockTransferMinute.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            txtProductStockTransferMinute.Location = new System.Drawing.Point(223, 41);
            txtProductStockTransferMinute.Name = "txtProductStockTransferMinute";
            txtProductStockTransferMinute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtProductStockTransferMinute.Size = new System.Drawing.Size(252, 24);
            txtProductStockTransferMinute.TabIndex = 14;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(16, 101);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(107, 16);
            labelControl1.TabIndex = 13;
            labelControl1.Text = "Son Aktarım Tarihi";
            // 
            // txtProductStockFilter
            // 
            txtProductStockFilter.Location = new System.Drawing.Point(223, 70);
            txtProductStockFilter.Name = "txtProductStockFilter";
            txtProductStockFilter.Size = new System.Drawing.Size(455, 22);
            txtProductStockFilter.TabIndex = 12;
            // 
            // labelControl2
            // 
            labelControl2.Location = new System.Drawing.Point(16, 73);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(29, 16);
            labelControl2.TabIndex = 11;
            labelControl2.Text = "Filtre";
            // 
            // labelControl3
            // 
            labelControl3.Location = new System.Drawing.Point(16, 45);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(135, 16);
            labelControl3.TabIndex = 10;
            labelControl3.Text = "Aktarım Süresi (Dakika)";
            // 
            // groupControl2
            // 
            groupControl2.Controls.Add(txtProductPriceLastTransfer);
            groupControl2.Controls.Add(txtProductPriceTransferMinute);
            groupControl2.Controls.Add(labelControl4);
            groupControl2.Controls.Add(txtProductPriceFilter);
            groupControl2.Controls.Add(labelControl5);
            groupControl2.Controls.Add(labelControl6);
            groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            groupControl2.Location = new System.Drawing.Point(0, 282);
            groupControl2.Name = "groupControl2";
            groupControl2.Size = new System.Drawing.Size(963, 141);
            groupControl2.TabIndex = 4;
            groupControl2.Text = "Malzeme Fiyat Aktarım Ayarları";
            // 
            // txtProductPriceLastTransfer
            // 
            txtProductPriceLastTransfer.Location = new System.Drawing.Point(223, 97);
            txtProductPriceLastTransfer.Name = "txtProductPriceLastTransfer";
            txtProductPriceLastTransfer.Properties.ReadOnly = true;
            txtProductPriceLastTransfer.Size = new System.Drawing.Size(455, 22);
            txtProductPriceLastTransfer.TabIndex = 15;
            // 
            // txtProductPriceTransferMinute
            // 
            txtProductPriceTransferMinute.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            txtProductPriceTransferMinute.Location = new System.Drawing.Point(223, 40);
            txtProductPriceTransferMinute.Name = "txtProductPriceTransferMinute";
            txtProductPriceTransferMinute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtProductPriceTransferMinute.Size = new System.Drawing.Size(252, 24);
            txtProductPriceTransferMinute.TabIndex = 14;
            // 
            // labelControl4
            // 
            labelControl4.Location = new System.Drawing.Point(16, 100);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(107, 16);
            labelControl4.TabIndex = 13;
            labelControl4.Text = "Son Aktarım Tarihi";
            // 
            // txtProductPriceFilter
            // 
            txtProductPriceFilter.Location = new System.Drawing.Point(223, 69);
            txtProductPriceFilter.Name = "txtProductPriceFilter";
            txtProductPriceFilter.Size = new System.Drawing.Size(455, 22);
            txtProductPriceFilter.TabIndex = 12;
            // 
            // labelControl5
            // 
            labelControl5.Location = new System.Drawing.Point(16, 72);
            labelControl5.Name = "labelControl5";
            labelControl5.Size = new System.Drawing.Size(29, 16);
            labelControl5.TabIndex = 11;
            labelControl5.Text = "Filtre";
            // 
            // labelControl6
            // 
            labelControl6.Location = new System.Drawing.Point(16, 44);
            labelControl6.Name = "labelControl6";
            labelControl6.Size = new System.Drawing.Size(135, 16);
            labelControl6.TabIndex = 10;
            labelControl6.Text = "Aktarım Süresi (Dakika)";
            // 
            // groupControl4
            // 
            groupControl4.Controls.Add(txtOrderStatusId);
            groupControl4.Controls.Add(labelControl7);
            groupControl4.Controls.Add(txtOrderTransferMinute);
            groupControl4.Controls.Add(labelControl15);
            groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            groupControl4.Location = new System.Drawing.Point(0, 423);
            groupControl4.Name = "groupControl4";
            groupControl4.Size = new System.Drawing.Size(963, 121);
            groupControl4.TabIndex = 9;
            groupControl4.Text = "Sipariş Aktarım Ayarları";
            // 
            // txtOrderStatusId
            // 
            txtOrderStatusId.EditValue = "10";
            txtOrderStatusId.Location = new System.Drawing.Point(223, 70);
            txtOrderStatusId.Name = "txtOrderStatusId";
            txtOrderStatusId.Size = new System.Drawing.Size(455, 22);
            txtOrderStatusId.TabIndex = 16;
            // 
            // labelControl7
            // 
            labelControl7.Location = new System.Drawing.Point(16, 73);
            labelControl7.Name = "labelControl7";
            labelControl7.Size = new System.Drawing.Size(96, 16);
            labelControl7.TabIndex = 15;
            labelControl7.Text = "Sipariş Durum Id";
            // 
            // txtOrderTransferMinute
            // 
            txtOrderTransferMinute.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            txtOrderTransferMinute.Location = new System.Drawing.Point(223, 40);
            txtOrderTransferMinute.Name = "txtOrderTransferMinute";
            txtOrderTransferMinute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtOrderTransferMinute.Size = new System.Drawing.Size(252, 24);
            txtOrderTransferMinute.TabIndex = 14;
            // 
            // labelControl15
            // 
            labelControl15.Location = new System.Drawing.Point(16, 44);
            labelControl15.Name = "labelControl15";
            labelControl15.Size = new System.Drawing.Size(135, 16);
            labelControl15.TabIndex = 10;
            labelControl15.Text = "Aktarım Süresi (Dakika)";
            // 
            // SmartstoreParameterUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupControl4);
            Controls.Add(groupControl2);
            Controls.Add(groupControl1);
            Controls.Add(groupControlCustomer);
            Name = "SmartstoreParameterUserControl";
            Size = new System.Drawing.Size(963, 689);
            Load += SmartstoreParameterUserControl_Load;
            ((System.ComponentModel.ISupportInitialize)groupControlCustomer).EndInit();
            groupControlCustomer.ResumeLayout(false);
            groupControlCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductLastTransfer.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProductTransferMinute.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProductFilter.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductStockLastTransfer.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProductStockTransferMinute.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProductStockFilter.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
            groupControl2.ResumeLayout(false);
            groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtProductPriceLastTransfer.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProductPriceTransferMinute.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProductPriceFilter.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupControl4).EndInit();
            groupControl4.ResumeLayout(false);
            groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtOrderStatusId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtOrderTransferMinute.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlCustomer;
        private DevExpress.XtraEditors.TextEdit txtProductLastTransfer;
        private DevExpress.XtraEditors.SpinEdit txtProductTransferMinute;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txtProductFilter;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtProductStockLastTransfer;
        private DevExpress.XtraEditors.SpinEdit txtProductStockTransferMinute;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtProductStockFilter;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtProductPriceLastTransfer;
        private DevExpress.XtraEditors.SpinEdit txtProductPriceTransferMinute;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtProductPriceFilter;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.TextEdit txtOrderStatusId;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SpinEdit txtOrderTransferMinute;
        private DevExpress.XtraEditors.LabelControl labelControl15;
    }
}
