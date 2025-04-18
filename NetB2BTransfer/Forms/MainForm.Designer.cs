namespace NetB2BTransfer.Forms
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
            accordionControlElement6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement7 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement5 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(components);
            ((System.ComponentModel.ISupportInitialize)accordionControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fluentDesignFormControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fluentFormDefaultManager1).BeginInit();
            SuspendLayout();
            // 
            // container
            // 
            container.Dock = System.Windows.Forms.DockStyle.Fill;
            container.Location = new System.Drawing.Point(260, 39);
            container.Name = "container";
            container.Size = new System.Drawing.Size(1254, 601);
            container.TabIndex = 0;
            // 
            // accordionControl1
            // 
            accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement4, accordionControlElement1, accordionControlElement2, accordionControlElement3 });
            accordionControl1.Location = new System.Drawing.Point(0, 39);
            accordionControl1.Name = "accordionControl1";
            accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            accordionControl1.Size = new System.Drawing.Size(260, 601);
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
            accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement6, accordionControlElement7 });
            accordionControlElement1.Expanded = true;
            accordionControlElement1.Name = "accordionControlElement1";
            accordionControlElement1.Text = "Yapılandırma";
            // 
            // accordionControlElement6
            // 
            accordionControlElement6.Name = "accordionControlElement6";
            accordionControlElement6.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            accordionControlElement6.Text = "Ayarlar";
            // 
            // accordionControlElement7
            // 
            accordionControlElement7.Name = "accordionControlElement7";
            accordionControlElement7.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            accordionControlElement7.Text = "B2B Ayarları";
            // 
            // accordionControlElement2
            // 
            accordionControlElement2.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement5 });
            accordionControlElement2.Expanded = true;
            accordionControlElement2.Name = "accordionControlElement2";
            accordionControlElement2.Text = "Sistem";
            // 
            // accordionControlElement5
            // 
            accordionControlElement5.Name = "accordionControlElement5";
            accordionControlElement5.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            accordionControlElement5.Text = "Günlük";
            // 
            // accordionControlElement3
            // 
            accordionControlElement3.Name = "accordionControlElement3";
            accordionControlElement3.Text = "Eklentiler";
            // 
            // fluentDesignFormControl1
            // 
            fluentDesignFormControl1.FluentDesignForm = this;
            fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            fluentDesignFormControl1.Manager = fluentFormDefaultManager1;
            fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            fluentDesignFormControl1.Size = new System.Drawing.Size(1514, 39);
            fluentDesignFormControl1.TabIndex = 2;
            fluentDesignFormControl1.TabStop = false;
            // 
            // fluentFormDefaultManager1
            // 
            fluentFormDefaultManager1.Form = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1514, 640);
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
            ((System.ComponentModel.ISupportInitialize)accordionControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fluentDesignFormControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fluentFormDefaultManager1).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer container;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement6;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement5;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnTransfer;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement7;
    }
}