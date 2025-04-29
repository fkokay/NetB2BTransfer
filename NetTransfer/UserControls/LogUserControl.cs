using DevExpress.Data;
using DevExpress.XtraEditors;
using NetTransfer.B2B.Library.B2B.Models;
using NetTransfer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTransfer.UserControls
{
    public partial class LogUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private Timer refreshTimer;
        private BindingList<Log> logs = new BindingList<Log>();

        public LogUserControl()
        {
            InitializeComponent();
        }

        private void LogUserControl_Load(object sender, EventArgs e)
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
            string filter = "*[System/Provider/@Name=\"NetTransferService\" or System/Provider/@Name=\"NetTransfer\"]";
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
