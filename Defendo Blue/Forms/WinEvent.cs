using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class WinEvent : Form
    {
        private DataTable dataTable;
        private string watchLog = "Security";
        private EventLog eventLog;

        public WinEvent()
        {
            InitializeComponent();
            TransparentControl();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Visible = true;

            dataTable = new DataTable();
            dataTable.Columns.Add("InstanceId", typeof(int));
            dataTable.Columns.Add("Message", typeof(string));

            dataGridView1.DataSource = dataTable;
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);

            eventLog = new EventLog(watchLog);
            eventLog.EntryWritten += new EntryWrittenEventHandler(OnEntryWritten);
            eventLog.EnableRaisingEvents = true;
        }

        private void OnEntryWritten(object source, EntryWrittenEventArgs e)
        {
            if (e.Entry.InstanceId == 4624)
            {
                UpdateLogEntries();
                ShowNotification(e.Entry);
            }
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
                        
                        string userName = "Bilinmeyen Kullanıcı";
                        string ipAddress = "Bilinmiyor";

                        if (entry.ReplacementStrings.Length > 5) 
                        {
                            userName = entry.ReplacementStrings[5]; 
                        }

                        if (entry.ReplacementStrings.Length > 18) 
                        {
                            ipAddress = entry.ReplacementStrings[18]; 
                        }

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

        private void ShowNotification(EventLogEntry entry)
        {
            string userName = "Bilinmeyen Kullanıcı";
            string ipAddress = "Bilinmiyor";

            if (entry.ReplacementStrings.Length > 5) 
            {
                userName = entry.ReplacementStrings[5];
            }

            if (entry.ReplacementStrings.Length > 20) 
            {
                ipAddress = entry.ReplacementStrings[20];
            }

            string logonTime = entry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss");

            string notificationText = $"Uzak Masaüstü Bağlantısı gerçekleşti\n" +
                                       $"Kullanıcı: {userName}\n" +
                                       $"Saat: {logonTime}\n" +
                                       $"IP Adresi: {ipAddress}";

            notifyIcon.ShowBalloonTip(5000, "Event Log Monitor", notificationText, ToolTipIcon.Info);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int instanceId = Convert.ToInt32(row.Cells["InstanceId"].Value);
                string message = row.Cells["Message"].Value.ToString();

                string userName = "Bilinmeyen Kullanıcı";
                string ipAddress = "Bilinmiyor";
                string logonTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                foreach (EventLogEntry entry in new EventLog(watchLog).Entries)
                {
                    if (entry.InstanceId == instanceId)
                    {
                        if (entry.ReplacementStrings.Length > 5)
                        {
                            userName = entry.ReplacementStrings[5];
                        }

                        if (entry.ReplacementStrings.Length > 18)
                        {
                            ipAddress = entry.ReplacementStrings[18];
                        }

                        logonTime = entry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    }
                }

                string details = $"Tarih ve Saat: {logonTime}\n" +
                                 $"Kullanıcı: {userName}\n" +
                                 $"IP Adresi: {ipAddress}\n";
                                 

                MessageBox.Show(details, "Event Log Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            UpdateLogEntries();
        }

        private void TransparentControl()
        {
            button4.Parent = pictureBox1;
            button4.BackColor = Color.Transparent;

            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
        }
    }
}
