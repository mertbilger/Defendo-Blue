using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class WinEventLog : Form
    {
        private NotifyIcon notifyIcon1; // NotifyIcon nesnesini ekleyin
        private DataTable dataTable; // DataTable nesnesini ekleyin

      

        private string watchLog = "Security";
        private string eventFilter = "";

        public WinEventLog()
        {
            InitializeComponent();
       

            dataTable = new DataTable();
            dataTable.Columns.Add("InstanceId", typeof(int));
            dataTable.Columns.Add("Message", typeof(string));

            dataGridView1.DataSource = dataTable;

            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }
        notifyIcon = new NotifyIconEx();
        NotifyIcon.Icon = this.appIcon;
			NotifyIcon.Text = tipText+"("+watchLog+")";
			NotifyIcon.Visible = true;
			NotifyIcon.ContextMenu = this.contextMenu;
			
			cbLogs.SelectedItem = watchLog;
			if (eventFilter.Length > 0)
				cbFilter.SelectedItem = eventFilter;
			else
				cbFilter.SelectedIndex = 0;

			// add event handlers
			NotifyIcon.BalloonClick += new EventHandler(OnClickBalloon);
        NotifyIcon.DoubleClick += new EventHandler(OnDoubleClickIcon);

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateLogEntries();
        }


    }

    private void WinEventLog_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Message"].Width = 400;
        }
        private void GetLogEntryStats(string logName)
        {
            int e = 0;

            EventLog log = new EventLog(logName);
            e = log.Entries.Count - 1;

            logMessage = log.Entries[e].Message;
            logMachine = log.Entries[e].MachineName;
            logSource = log.Entries[e].Source;
            logCategory = log.Entries[e].Category;
            logType = Convert.ToString(log.Entries[e].EntryType);
            eventID = log.Entries[e].EventID.ToString();
            user = log.Entries[e].UserName;
            logTime = log.Entries[e].TimeGenerated.ToShortTimeString();
            log.Close();
        }
        private void OnEntryWritten(object source, EntryWrittenEventArgs e)
        {
            string logName = watchLog;
            GetLogEntryStats(watchLog);

            if (logType == eventFilter || eventFilter.Length == 0)
            {
                // show balloon
                NotifyIcon.ShowBalloon("Event Log Monitor",
                    "An event was written to the " + logName + " event log." +
                    "\nType: " + LogType +
                    "\nSource: " + LogSource +
                    "\nCategory: " + LogCategory +
                    "\nEventID: " + EventID +
                    "\nUser: " + User,
                    NotifyIconEx.NotifyInfoFlags.Info,
                    5000);

                LogNotification();
            }
        }



        private void OnEntryWritten(object source, EntryWrittenEventArgs e)
        {
            UpdateLogEntries();
        }

        private void UpdateLogEntries()
        {
            try
            {
                dataTable.Clear();

                EventLog log = new EventLog(watchLog);

                foreach (EventLogEntry entry in log.Entries)
                {
                    if (entry.InstanceId == 4624) 

                    {
                        dataTable.Rows.Add(entry.InstanceId, entry.Message);
                    }
                }

                log.Close();
            }
            catch (SecurityException ex)
            {
                MessageBox.Show("Access to the event log was denied. Please check your permissions.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the event log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string message = row.Cells["Message"].Value.ToString(); 
                MessageBox.Show(message, "Event Log Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
