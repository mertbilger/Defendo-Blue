using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            Setup();

            dataTable = new DataTable();
            dataTable.Columns.Add("Message", typeof(string));

            dataGridView1.DataSource = dataTable;
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);

            eventLog = new EventLog(watchLog);
            eventLog.EntryWritten += new EntryWrittenEventHandler(OnEntryWritten);
            eventLog.EnableRaisingEvents = true;

            this.FormClosing += new FormClosingEventHandler(WinEvent_FormClosing);
        }

        private void Setup()
        {
        }

        public void OnEntryWritten(object source, EntryWrittenEventArgs e)
        {
            if (e.Entry.InstanceId == 4624)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateLogEntries();
                }));
            }
        }

        public void UpdateLogEntries()
        {
            try
            {
                dataTable.Clear();

                EventLog log = new EventLog(watchLog);

                var entries = log.Entries.Cast<EventLogEntry>()
                    .Where(entry => entry.InstanceId == 4624)
                    .OrderByDescending(entry => entry.TimeGenerated)
                    .ToList();

                foreach (EventLogEntry entry in entries)
                {
                    dataTable.Rows.Add(entry.Message);
                }

                log.Close();
            }
            catch (SecurityException)
            {
                MessageBox.Show("Event loguna erişim reddedildi. Lütfen izinlerinizi kontrol edin.", "İzin Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Event log okunurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string message = row.Cells["Message"].Value.ToString();

                string userName = "Bilinmeyen Kullanıcı";
                string ipAddress = "Bilinmiyor";
                string logtype = "Bilinmiyor";
                string logonTime = "Bilinmiyor";

                foreach (EventLogEntry entry in new EventLog(watchLog).Entries)
                {
                    if (entry.Message == message)
                    {
                        if (entry.ReplacementStrings.Length > 5)
                        {
                            userName = entry.ReplacementStrings[5];
                        }

                        if (entry.ReplacementStrings.Length > 18)
                        {
                            ipAddress = entry.ReplacementStrings[18];
                        }

                        if (entry.ReplacementStrings.Length > 8)
                        {
                            logtype = entry.ReplacementStrings[8];
                        }

                        logtype = logtype == "2" ? "Kullanıcı oturum açtı" : "Sistem";
                        logonTime = entry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    }
                }

                string details = $"Kullanıcı : {userName}\n" +
                                 $"IP Adresi : {ipAddress}\n" +
                                 $"Saat : {logonTime}\n" +
                                 $"Giriş Tipi : {logtype}";

                MessageBox.Show(details, "Event Log Detayı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

   

        private void TransparentControl()
        {
            button4.Parent = pictureBox1;
            button4.BackColor = Color.Transparent;

            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
        }

        private void WinEvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (eventLog != null)
            {
                eventLog.EnableRaisingEvents = false;
                eventLog.Dispose();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateLogEntries();
        }
    }
}
