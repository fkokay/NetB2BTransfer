using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using NetB2BTransfer.Core;
using NetB2BTransfer.Core.Data;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Data;
using NetB2BTransfer.Logo.Library.Class;
using NetB2BTransfer.Logo.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetB2BTransfer.UserControls
{
    public partial class TransferUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetB2BTransferContext _context;
        private ErpSetting _erpSetting;
        public TransferUserControl()
        {
            InitializeComponent();
            _context = new NetB2BTransferContext("Data Source=(local);Initial Catalog=B2BENT2;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;");
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            Transfer transfer = new Transfer(_erpSetting);
            if (cmbTransferType.SelectedItem.ToString() == "Cari Aktarım")
            {
                transfer.MusteriTransfer();
            }
        }

        private async void TransferUserControl_Load(object sender, EventArgs e)
        {
            _erpSetting = await _context.ErpSetting.FirstAsync();   
        }
    }
}
