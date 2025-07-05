using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using NetTransfer.Core;
using NetTransfer.Core.Data;
using NetTransfer.Core.Entities;
using NetTransfer.Data;
using NetTransfer.Integration;
using NetTransfer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class TransferUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private NetTransferContext _context;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private Transfer transfer;

        private DateTime startDatetime;

        private CancellationTokenSource cancellationLog = new CancellationTokenSource();
        private CancellationTokenSource cancellationTransfer = new CancellationTokenSource();
        private BindingList<Log> logs = new BindingList<Log>();
        private ErpSetting _erpSetting;
        private VirtualStoreSetting _virtualStoreSetting;
        private B2BParameter _b2BParameter;
        private SmartstoreParameter _smartstoreParameter;


        public TransferUserControl()
        {
            InitializeComponent();
            InitializeLogger();
            InitializeContext();
            InitializeTransfer();
        }

        private async void InitializeTransfer()
        {
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            _erpSetting = await _context.ErpSetting.FirstAsync();
            _virtualStoreSetting = await _context.VirtualStoreSetting.FirstAsync();

            cmbTransferType.Properties.Items.Clear();
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                _b2BParameter = _context.B2BParameter.FirstOrDefault();
                cmbTransferType.Properties.Items.AddRange(
                    new string[]
                    {
                        "Cari Aktarım",
                        "Cari Bakiye Aktarım",
                        "Malzeme Aktarım",
                        "Malzeme Stok Aktarım",
                        "Malzeme Fiyat Aktarım",
                        "Sipariş Aktarım",
                        "SanalPos Aktarım"
                    }
                );

                transfer = new Transfer(_logger, conn, _erpSetting, _virtualStoreSetting, _b2BParameter);
            }

            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                _smartstoreParameter = _context.SmartstoreParameter.FirstOrDefault();
                cmbTransferType.Properties.Items.AddRange(
                   new string[]
                   {
                    "Malzeme Aktarım",
                    "Malzeme Stok Aktarım",
                    "Malzeme Fiyat Aktarım",
                    "Sipariş Aktarım",
                    "Sevkiyat Aktarım"
                   }
               );

                transfer = new Transfer(_logger, conn, _erpSetting, _virtualStoreSetting, _smartstoreParameter);
            }
        }

        private void InitializeContext()
        {
            _context = new NetTransferContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private void InitializeLogger()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddEventLog(settings =>
                {
                    settings.SourceName = "NetTransfer";
                });
            });
            _logger = _loggerFactory.CreateLogger<TransferUserControl>();
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            if (cmbTransferType.SelectedItem == null)
            {
                MessageBox.Show("Lütfen aktarım türünü seçiniz.");
                return;
            }

            LogStart();

            cancellationTransfer = new CancellationTokenSource();
            btnCancel.Enabled = true;
            btnTransfer.Enabled = false;
            if (transfer != null)
            {
                if (cmbTransferType.SelectedItem.ToString() == "Cari Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.CariTransfer();
                    }, cancellationTransfer.Token);

                }
                else if (cmbTransferType.SelectedItem.ToString() == "Cari Bakiye Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.CariBakiyeTransfer();
                    }, cancellationTransfer.Token);
                }
                else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.MalzemeTransfer();
                    }, cancellationTransfer.Token);
                }
                else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Stok Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.MalzemeStokTransfer();
                    }, cancellationTransfer.Token);
                }
                else if (cmbTransferType.SelectedItem.ToString() == "Malzeme Fiyat Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.MalzemeFiyatTransfer();
                    }, cancellationTransfer.Token);
                }
                else if (cmbTransferType.SelectedItem.ToString() == "Sipariş Aktarım")
                {
                    _logger.LogWarning("Sipariş Aktarım");
                    await Task.Run(async () =>
                    {
                        await transfer.SiparisTransfer();
                    }, cancellationTransfer.Token);
                }
                else if (cmbTransferType.SelectedItem.ToString() == "Sevkiyat Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.SevkiyatTransfer();
                    }, cancellationTransfer.Token);
                }
                else if (cmbTransferType.SelectedItem.ToString() == "SanalPos Aktarım")
                {
                    await Task.Run(async () =>
                    {
                        await transfer.SanalPosTransfer();
                    }, cancellationTransfer.Token);
                }
            }
            btnCancel.Enabled = false;
            btnTransfer.Enabled = true;

            cancellationTransfer.Cancel();
        }

        private void TransferUserControl_Load(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            btnTransfer.Enabled = true;
        }

        private bool IsLogStart = false;

        private void LogStart()
        {
            if (!IsLogStart)
            {
                cancellationLog = new CancellationTokenSource();
                RealTimeSource realTimeSource = new RealTimeSource()
                {
                    DataSource = logs
                };

                gridControlLog.DataSource = realTimeSource;

                startDatetime = DateTime.Now.AddMinutes(-1);

                Task.Run(() =>
                {
                    LoadLogWhile();
                }, cancellationLog.Token); 
            }

            IsLogStart = true;
        }

        private void LoadLogWhile()
        {
            while (!cancellationLog.IsCancellationRequested)
            {
                string filter = "*[System/Provider/@Name=\"NetTransfer\"] and *[System[TimeCreated[@SystemTime >= '" + startDatetime.AddMinutes(-1).ToUniversalTime().ToString("o") + "']]]";
                EventLogQuery query = new EventLogQuery("Application", PathType.LogName, filter);
                EventLogReader reader = new EventLogReader(query);

                for (EventRecord eventRecord = reader.ReadEvent(); eventRecord != null; eventRecord = reader.ReadEvent())
                {
                    if (!logs.Where(m => m.EventId == eventRecord.RecordId.ToString()).Any())
                    {
                        logs.Insert(0, new Log()
                        {
                            Source = eventRecord.ProviderName,
                            EventId = eventRecord.RecordId.ToString(),
                            EventLevel = eventRecord.Level.ToString(),
                            EventMessage = eventRecord.FormatDescription().Replace("\r\n", "").Replace("Category: NetTransfer.UserControls.TransferUserControlEventId: 0",""),
                            EventTime = eventRecord.TimeCreated.Value
                        });
                    }
                }

                startDatetime = DateTime.Now;

                Thread.Sleep(100);
            }

        }

        private void gridViewLog_RowCountChanged(object sender, EventArgs e)
        {
            gridViewLog.BeginUpdate();
            gridViewLog.FocusedRowHandle = 0;
            gridViewLog.TopRowIndex = 0;
            gridViewLog.EndUpdate();
        }

        public bool IsTransfer()
        {
            if (btnTransfer.Enabled)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTransfer.Cancel();
            btnCancel.Enabled = false;
            btnTransfer.Enabled = true;
        }

        private void btnLastTransferClear_Click(object sender, EventArgs e)
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                if (_b2BParameter == null)
                {
                    return;
                }
                _b2BParameter.CustomerLastTransfer = null;
                _b2BParameter.ProductLastTransfer = null;
                _b2BParameter.ProductStockLastTransfer = null;
                _b2BParameter.ProductPriceLastTransfer = null;
                _context.B2BParameter.Update(_b2BParameter);
                _context.SaveChanges();
            }

            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                if (_smartstoreParameter == null)
                {
                    return;
                }

                _smartstoreParameter.ProductStockLastTransfer = null;
                _smartstoreParameter.OrderShipmentLastTransfer = null;
                _context.SmartstoreParameter.Update(_smartstoreParameter);
                _context.SaveChanges();
            }

            MessageBox.Show("Son aktarım tarihi sıfırlandı.");
        }
    }
}
