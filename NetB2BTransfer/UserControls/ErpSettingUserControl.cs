using DevExpress.XtraEditors;
using NetB2BTransfer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetB2BTransfer.UserControls
{
    public partial class ErpSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly NetB2BTransferContext _context;
        public ErpSettingUserControl()
        {
            InitializeComponent();
            _context = new NetB2BTransferContext("Data Source=(local);Initial Catalog=B2BENT2;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;");
        }

        private void ErpSettingUserControl_Load(object sender, EventArgs e)
        {
            var erpSetting = _context.ErpSetting.FirstOrDefault();
            if (erpSetting != null)
            {
                cmbErp.Text = erpSetting.Erp;

                switch (erpSetting.Erp)
                {
                    case "Netsis":
                        xtraTabPageNetsis.PageVisible = true;
                        xtraTabPageLogo.PageVisible = false;
                        break;
                    case "Logo":
                        xtraTabPageNetsis.PageVisible = false;
                        xtraTabPageLogo.PageVisible = true;
                        break;
                }
            }
            else
            {
                xtraTabPageNetsis.PageEnabled = false;
                xtraTabPageLogo.PageEnabled = true;
            }
        }

        private void cmbErp_TextChanged(object sender, EventArgs e)
        {
            switch (cmbErp.Text)
            {
                case "Netsis":
                    xtraTabPageNetsis.PageVisible = true;
                    xtraTabPageLogo.PageVisible = false;
                    break;
                case "Logo":
                    xtraTabPageNetsis.PageVisible = false;
                    xtraTabPageLogo.PageVisible = true;
                    break;
            }
        }
    }
}
