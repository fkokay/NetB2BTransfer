using DevExpress.XtraBars;
using NetB2BTransfer.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetB2BTransfer.Forms
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            TransferUserControl transferUserControl = new TransferUserControl();
            transferUserControl.Dock = DockStyle.Fill;
            
            container.Controls.Add(transferUserControl);
        }
    }
}
