using DevExpress.Data;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using NetB2BTransfer.B2B.Library;
using NetB2BTransfer.Core;
using NetB2BTransfer.Core.Data;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Data;
using NetB2BTransfer.Logo.Library.Class;
using NetB2BTransfer.Logo.Library.Models;
using NetB2BTransfer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetB2BTransfer.UserControls
{
    public partial class TransferUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private ILogger _logger;
        private Timer refreshTimer;
        private BindingList<Log> logs = new BindingList<Log>();

        private readonly NetB2BTransferContext _context;
        private ErpSetting _erpSetting;
        private B2BSetting _b2BSetting;
        private LogoTransferSetting _logoTransferSetting;
        public TransferUserControl()
        {
            InitializeComponent();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddEventLog(settings=>
                {
                    settings.SourceName = "NetB2BTransfer";
                });
            });

            _logger = loggerFactory.CreateLogger<TransferUserControl>();

            _context = new NetB2BTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            Transfer transfer;
            if (_erpSetting.Erp == "Logo")
            {
                transfer = new Transfer(_logger, _erpSetting, _b2BSetting, _logoTransferSetting);
            }
            else if (_erpSetting.Erp == "Netsis")
            {
                transfer = new Transfer(_logger, _erpSetting, _b2BSetting);
            }
            else
            {
                transfer = new Transfer(_logger, _erpSetting, _b2BSetting);
            }

            if (string.IsNullOrEmpty(cmbTransferType.SelectedItem.ToString()))
            {
                MessageBox.Show("Lütfen aktarım türünü seçiniz.");
                return;
            }

            LogStart();
            if (cmbTransferType.SelectedItem.ToString() == "Cari Aktarım")
            {
                await transfer.CariTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Cari Bakiye Aktarım")
            {
                await transfer.CariBakiyeTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Aktarım")
            {
                await transfer.MalzemeTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Stok Aktarım")
            {
                await transfer.MalzemeStokTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Fiyat Aktarım")
            {
                await transfer.MalzemeFiyatTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "Sipariş Aktarım")
            {
                await transfer.SiparisTransfer();
            }
            else if (cmbTransferType.SelectedItem.ToString() == "SanalPos Aktarım")
            {
                await transfer.SanalPosTransfer();
            }

            LogStop();
        }

        private async void TransferUserControl_Load(object sender, EventArgs e)
        {
            _logger.LogInformation("Transfer ayarları yükleniyor. Datetime : {time}", DateTimeOffset.Now);

            _erpSetting = await _context.ErpSetting.FirstAsync();
            _b2BSetting = await _context.B2BSetting.FirstAsync();

            if (_erpSetting.Erp == "Logo")
            {
                if (!_context.LogoTransferSetting.Any())
                {
                    MessageBox.Show("Lütfen logo aktarım ayarlarını tanımlayınız.");
                    return;
                }

                _logoTransferSetting = await _context.LogoTransferSetting.FirstAsync();
            }


            cmbTransferType.Items.Clear();
            if (_b2BSetting.B2B == "B2B")
            {
                cmbTransferType.Items.AddRange(
                    "Cari Aktarım",
                    "Cari Bakiye Aktarım",
                    "Malzeme Aktarım",
                    "Malzeme Stok Aktarım",
                    "Malzeme Fiyat Aktarım",
                    "Sipariş Aktarım",
                    "SanalPos Aktarım"
                );
            }

            if (_b2BSetting.B2B == "Smartstore")
            {
                cmbTransferType.Items.AddRange(
                   "Malzeme Aktarım",
                   "Malzeme Stok Aktarım",
                   "Malzeme Fiyat Aktarım"
               );
            }
        }

        private void LogStart()
        {
            RealTimeSource realTimeSource = new RealTimeSource()
            {
                DataSource = logs
            };

            gridControlLog.DataSource = realTimeSource;

            refreshTimer = new Timer();
            refreshTimer.Interval = 100; // 10 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void LogStop()
        {
            refreshTimer.Stop();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (refreshTimer.Interval == 0)
            {
                refreshTimer.Interval = 10000;
            }
            LoadLog();
        }

        private void LoadLog()
        {
            string filter = "*[System/Provider/@Name=\"NetB2BTransfer\"]";
            EventLogQuery query = new EventLogQuery("Application", PathType.LogName, filter);
            EventLogReader reader = new EventLogReader(query);

            for (EventRecord eventRecord = reader.ReadEvent(); eventRecord != null; eventRecord = reader.ReadEvent())
            {
                if (!logs.Where(m => m.EventId == eventRecord.RecordId.ToString()).Any())
                {

                    var message = eventRecord.FormatDescription().Split("\n");
                    logs.Add(new Log()
                    {
                        Source = eventRecord.ProviderName,
                        EventId = eventRecord.RecordId.ToString(),
                        EventLevel = eventRecord.Level.ToString(),
                        EventMessage = message[message.Length == 1 ? 0 : 3],
                        EventTime = eventRecord.TimeCreated.Value
                    });
                }
            }

        }
    }
}
