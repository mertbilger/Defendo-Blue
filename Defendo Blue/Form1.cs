using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using Defendo_Blue.Forms;

namespace Defendo_Blue
{
    public partial class Form1 : Form
    {
        private ContextMenuStrip contextMenu;
        private WinEvent winEventForm;

        static readonly PerformanceCounter IdleCounter = new PerformanceCounter("Processor", "% Idle Time", "_Total");
        static readonly PerformanceCounter RamCounter = new PerformanceCounter("Memory", "Available MBytes");

        private Timer timerUpdate;

        public Form1()
        {
            InitializeComponent();
            Setup();
            transparentControls();
            SetupChart();
            SetupTimer(); // Timer'ı başlatmak için bu yöntemi çağırdık
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winEventForm = new WinEvent();
            winEventForm.Load += (s, args) =>
            {
                winEventForm.Hide();
            };
            winEventForm.Show();
        }

        private void Setup()
        {
            notifyIcon1 = new NotifyIcon();
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = new Icon("C:\\Users\\Mert\\Desktop\\MB\\security\\security.ico");

            contextMenu = new ContextMenuStrip();

            ToolStripMenuItem option1 = new ToolStripMenuItem("Dosya Tara");
            option1.BackColor = Color.FromArgb(52, 52, 52);
            option1.ForeColor = Color.White;
            option1.Font = new Font("Arial", 10, FontStyle.Regular);
            option1.Click += Scan_Files;

            ToolStripMenuItem option2 = new ToolStripMenuItem("URL Tara");
            option2.BackColor = Color.FromArgb(52, 52, 52);
            option2.ForeColor = Color.White;
            option2.Font = new Font("Arial", 10, FontStyle.Regular);
            option2.Click += Scan_Url;

            ToolStripMenuItem option3 = new ToolStripMenuItem("Olay Günlüğü Göster");
            option3.BackColor = Color.FromArgb(52, 52, 52);
            option3.ForeColor = Color.White;
            option3.Font = new Font("Arial", 10, FontStyle.Regular);
            option3.Click += WinEvent_Logs;

            ToolStripMenuItem option4 = new ToolStripMenuItem("Çıkış");
            option4.BackColor = Color.FromArgb(52, 52, 52);
            option4.ForeColor = Color.White;
            option4.Font = new Font("Arial", 10, FontStyle.Regular);
            option4.Click += Exit;

            contextMenu.Items.Add(option1);
            contextMenu.Items.Add(option2);
            contextMenu.Items.Add(option3);
            contextMenu.Items.Add(option4);

            notifyIcon1.ContextMenuStrip = contextMenu;
        }

        private void SetupChart()
        {
            chartHardware.Series.Clear();

            chartHardware.Series.Clear();

            Series cpuSeries = new Series("CPU Kullanımı");
            cpuSeries.ChartType = SeriesChartType.Spline;
            cpuSeries.BorderWidth = 3;
            cpuSeries.Color = Color.FromArgb(150, Color.Blue); // Şeffaf renk ekledik
            chartHardware.Series.Add(cpuSeries);

            chartHardware.ChartAreas[0].AxisX.Title = "Zaman (saniye)";
            chartHardware.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            chartHardware.ChartAreas[0].AxisX.Interval = 1;
            chartHardware.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartHardware.ChartAreas[0].AxisX.Minimum = 0; // Minimum X ekseni değeri
            chartHardware.ChartAreas[0].AxisX.Maximum = 10; // Maksimum X ekseni değeri

            chartHardware.ChartAreas[0].AxisY.Title = "CPU Kullanımı (%)";
            chartHardware.ChartAreas[0].AxisY.LabelStyle.Format = "{0}%";
        }



        private void SetupTimer()
        {
            timerUpdate = new Timer();
            timerUpdate.Interval = 1000; // 1 saniyede bir güncelle
            timerUpdate.Tick += TimerUpdate_Tick;
            timerUpdate.Start();
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            float cpuUsage = 100 - IdleCounter.NextValue();

            Series series = chartHardware.Series["CPU Kullanımı"];
            double currentTime = (DateTime.Now - DateTime.Today).TotalSeconds; // Günün başlangıcından itibaren geçen saniye

            if (series.Points.Count == 0 || series.Points[series.Points.Count - 1].XValue < currentTime)
            {
                series.Points.AddXY(currentTime, cpuUsage);
            }
            else
            {
                // En son noktayı güncelle
                series.Points[series.Points.Count - 1].YValues[0] = cpuUsage;
            }

            int maxPoints = 10; // Maksimum gösterilecek nokta sayısı
            if (series.Points.Count > maxPoints)
            {
                series.Points.RemoveAt(0);
            }

            // X ekseninin maksimum değeri güncelle
            if (currentTime > chartHardware.ChartAreas[0].AxisX.Maximum)
            {
                chartHardware.ChartAreas[0].AxisX.Maximum = currentTime;
                chartHardware.ChartAreas[0].AxisX.Minimum = currentTime - 10;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FastScan hızlıTarama = new FastScan();
            hızlıTarama.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ScanfileForm scanfileForm = new ScanfileForm();
            scanfileForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WinEvent winEventLog = new WinEvent();
            winEventLog.Show();
        }

        private void Scan_Files(object sender, EventArgs e)
        {
            ScanfileForm scanfileForm = new ScanfileForm();
            scanfileForm.Show();
        }

        private void Scan_Url(object sender, EventArgs e)
        {
            ScanUrlForm scanurlForm = new ScanUrlForm();
            scanurlForm.Show();
        }

        private void WinEvent_Logs(object sender, EventArgs e)
        {
            if (winEventForm != null)
            {
                winEventForm.Show();
                winEventForm.BringToFront();
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void transparentControls()
        {
            picLogo.Parent = picBack;
            picLogo.BackColor = Color.Transparent;

            TagName.Parent = picBack;
            TagName.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScanUrlForm scanurlForm = new ScanUrlForm();
            scanurlForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryForm registryForm = new RegistryForm();
            registryForm.Show();
        }

        private void ShowHardwareInfo()
        {
            float cpuUsage = 100 - IdleCounter.NextValue();
            float ramAvailable = RamCounter.NextValue();

            MessageBox.Show($"CPU Kullanımı: {cpuUsage}%\n",
                            "Donanım Bilgileri",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowHardwareInfo();
        }
    }
}
