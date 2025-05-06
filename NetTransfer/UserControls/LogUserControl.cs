using DevExpress.Data;
using DevExpress.XtraEditors;
using NetTransfer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class LogUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private DateTime startDatetime;
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private BindingList<Log> logs = new BindingList<Log>();

        public LogUserControl()
        {
            InitializeComponent();
        }

        private void LogUserControl_Load(object sender, EventArgs e)
        {
            txtDate.DateTime = DateTime.Now;
            txtTime.Time = DateTime.Now.AddMinutes(-15);

            RealTimeSource realTimeSource = new RealTimeSource()
            {
                DataSource = logs
            };

            gridControlLog.DataSource = realTimeSource;

            startDatetime = txtDate.DateTime.Date.AddHours(txtTime.Time.Hour).AddMinutes(txtTime.Time.Minute);

            Task.Run(() =>
            {
                LoadLog();
            }, cancellation.Token);
        }



        private void LoadLog()
        {
            while (true)
            {
                string filter = "*[System/Provider/@Name=\"NetTransferService\"] and *[System[TimeCreated[@SystemTime >= '" + startDatetime.ToUniversalTime().ToString("o") + "']]]";
                EventLogQuery query = new EventLogQuery("Application", PathType.LogName, filter);
                EventLogReader reader = new EventLogReader(query);

                for (EventRecord eventRecord = reader.ReadEvent(); eventRecord != null; eventRecord = reader.ReadEvent())
                {
                    lock (logs)
                    {
                        if (!logs.Where(m => m.EventId == eventRecord.RecordId.ToString()).Any())
                        {

                            if (!string.IsNullOrEmpty(eventRecord.FormatDescription()))
                            {
                                var message = eventRecord.FormatDescription().Split("\n");
                                logs.Insert(0, new Log()
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            cancellation.Cancel();

            startDatetime = txtDate.DateTime.Date.AddHours(txtTime.Time.Hour).AddMinutes(txtTime.Time.Minute);
            logs.Clear();

            Task.Run(() =>
            {
                LoadLog();
            }, cancellation.Token);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cancellation.Cancel();

            var eventLog = new EventLog("Application", System.Environment.MachineName);
            eventLog.Clear();

            logs.Clear();

            XtraMessageBox.Show("Loglar temizlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Task.Run(() =>
            {
                LoadLog();
            }, cancellation.Token); Task.Run(() =>
            {
                LoadLog();
            }, cancellation.Token);
        }
    }


}
