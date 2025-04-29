namespace NetTransfer.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            container = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnTransfer = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnB2BSetting = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnErpSetting = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnServiceSetting = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnLog = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            txtServiceStatus = new DevExpress.XtraBars.BarStaticItem();
            fluentFormDefaultManager = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(components);
            ((System.ComponentModel.ISupportInitialize)accordionControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fluentDesignFormControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fluentFormDefaultManager).BeginInit();
            SuspendLayout();
            // 
            // container
            // 
            container.Dock = System.Windows.Forms.DockStyle.Fill;
            container.Location = new System.Drawing.Point(260, 39);
            container.Name = "container";
            container.Size = new System.Drawing.Size(1015, 655);
            container.TabIndex = 0;
            // 
            // accordionControl1
            // 
            accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement4, accordionControlElement1, btnB2BSetting, btnErpSetting, btnServiceSetting, accordionControlElement2, accordionControlElement3 });
            accordionControl1.Location = new System.Drawing.Point(0, 39);
            accordionControl1.Name = "accordionControl1";
            accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            accordionControl1.Size = new System.Drawing.Size(260, 655);
            accordionControl1.TabIndex = 1;
            accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement4
            // 
            accordionControlElement4.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { btnTransfer });
            accordionControlElement4.Expanded = true;
            accordionControlElement4.Name = "accordionControlElement4";
            accordionControlElement4.Text = "İşlemler";
            // 
            // btnTransfer
            // 
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnTransfer.Text = "Aktarım";
            btnTransfer.Click += btnTransfer_Click;
            // 
            // accordionControlElement1
            // 
            accordionControlElement1.Expanded = true;
            accordionControlElement1.Name = "accordionControlElement1";
            accordionControlElement1.Text = "Yapılandırma";
            // 
            // btnB2BSetting
            // 
            btnB2BSetting.Name = "btnB2BSetting";
            btnB2BSetting.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnB2BSetting.Text = "Sanal Mağaza Ayarları";
            btnB2BSetting.Click += btnB2BSetting_Click;
            // 
            // btnErpSetting
            // 
            btnErpSetting.Name = "btnErpSetting";
            btnErpSetting.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnErpSetting.Text = "Erp Ayarları";
            btnErpSetting.Click += btnErpSetting_Click;
            // 
            // btnServiceSetting
            // 
            btnServiceSetting.Name = "btnServiceSetting";
            btnServiceSetting.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnServiceSetting.Text = "Servis Ayarları";
            btnServiceSetting.Click += btnServiceSetting_Click;
            // 
            // accordionControlElement2
            // 
            accordionControlElement2.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { btnLog });
            accordionControlElement2.Expanded = true;
            accordionControlElement2.Name = "accordionControlElement2";
            accordionControlElement2.Text = "Sistem";
            // 
            // btnLog
            // 
            btnLog.Name = "btnLog";
            btnLog.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnLog.Text = "Günlük";
            btnLog.Click += btnLog_Click;
            // 
            // accordionControlElement3
            // 
            accordionControlElement3.Expanded = true;
            accordionControlElement3.Name = "accordionControlElement3";
            accordionControlElement3.Text = "Eklentiler";
            // 
            // fluentDesignFormControl1
            // 
            fluentDesignFormControl1.FluentDesignForm = this;
            fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { txtServiceStatus });
            fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            fluentDesignFormControl1.Manager = fluentFormDefaultManager;
            fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            fluentDesignFormControl1.Size = new System.Drawing.Size(1275, 39);
            fluentDesignFormControl1.TabIndex = 2;
            fluentDesignFormControl1.TabStop = false;
            fluentDesignFormControl1.TitleItemLinks.Add(txtServiceStatus);
            // 
            // txtServiceStatus
            // 
            txtServiceStatus.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            txtServiceStatus.Caption = "Yükleniyor";
            txtServiceStatus.Id = 0;
            txtServiceStatus.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            txtServiceStatus.ItemAppearance.Normal.Options.UseFont = true;
            txtServiceStatus.Name = "txtServiceStatus";
            // 
            // fluentFormDefaultManager
            // 
            fluentFormDefaultManager.Form = this;
            fluentFormDefaultManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { txtServiceStatus });
            fluentFormDefaultManager.MaxItemId = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1275, 694);
            ControlContainer = container;
            Controls.Add(container);
            Controls.Add(accordionControl1);
            Controls.Add(fluentDesignFormControl1);
            FluentDesignFormControl = fluentDesignFormControl1;
            Name = "MainForm";
            NavigationControl = accordionControl1;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Net B2B Transfer v1.0.0";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)accordionControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fluentDesignFormControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fluentFormDefaultManager).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer container;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnErpSetting;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnLog;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnTransfer;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnB2BSetting;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnServiceSetting;
        private DevExpress.XtraBars.BarStaticItem txtServiceStatus;
    }
}