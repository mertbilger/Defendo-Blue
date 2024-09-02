﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using Defendo_Blue.Forms;
using System.Net.Sockets;
using System.Net;

namespace Defendo_Blue
{
    public partial class Form1 : Form
    {
        private ContextMenuStrip contextMenu;
        private WinEvent winEventForm;

        static readonly PerformanceCounter IdleCounter = new PerformanceCounter("Processor", "% Idle Time", "_Total");
        static readonly PerformanceCounter RamCounter = new PerformanceCounter("Memory", "Available MBytes");

        private Timer timerUpdate;
        private Point? prevPosition = null;
        private ToolTip tooltip = new ToolTip();


        public Form1()
        {
            InitializeComponent();
            Setup();
            TransparentControls();
            SetupChart();
            SetupTimer();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; 
                this.Hide();  
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winEventForm = new WinEvent();
            winEventForm.Load += (s, args) =>
            {
                winEventForm.Hide();
            };
            winEventForm.Show();

            try
            {
                string localIP = GetLocalIPAddress();

                label3.Text = $"Yerel IP Adresi: {localIP}\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}",
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private string GetLocalIPAddress()
        {
            string localIP = string.Empty;

            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) 
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }

       
        private void Setup()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = new Icon("C:\\Users\\Mert\\Desktop\\MB\\security\\security.ico");

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

            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.Click += NotifyIcon_Click;
        }
        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SetupChart()
        {
            chartHardware.Series.Clear();

            Series cpuSeries = new Series("CPU Kullanımı");
            cpuSeries.ChartType = SeriesChartType.Spline;
            cpuSeries.BorderWidth = 3;
            cpuSeries.Color = Color.FromArgb(150, Color.Blue);
            chartHardware.Series.Add(cpuSeries);

            chartHardware.ChartAreas[0].AxisX.Title = "Zaman (saniye)";
            chartHardware.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            chartHardware.ChartAreas[0].AxisX.Interval = 1;
            chartHardware.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartHardware.ChartAreas[0].AxisX.Minimum = 0;
            chartHardware.ChartAreas[0].AxisX.Maximum = 10;

            chartHardware.ChartAreas[0].AxisY.Title = "CPU Kullanımı (%)";
            chartHardware.ChartAreas[0].AxisY.LabelStyle.Format = "{0}%";

            chartHardware.Parent = picBack;
            chartHardware.BackColor = Color.Transparent;

            chartHardware.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            chartHardware.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;

            chartHardware.MouseMove += chart_MouseMove;
        }

        private void SetupTimer()
        {
            timerUpdate = new Timer();
            timerUpdate.Interval = 1000;
            timerUpdate.Tick += TimerUpdate_Tick;
            timerUpdate.Start();
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            float cpuUsage = 99 - IdleCounter.NextValue();
            Series series = chartHardware.Series["CPU Kullanımı"];
            double currentTime = series.Points.Count;

            series.Points.AddXY(currentTime, cpuUsage);

            int maxPoints = 10;
            if (series.Points.Count > maxPoints)
            {
                series.Points.RemoveAt(0);
                foreach (var point in series.Points)
                {
                    point.XValue -= 1;
                }
            }

            chartHardware.ChartAreas[0].AxisX.Maximum = series.Points.Count;
            chartHardware.ChartAreas[0].AxisX.Minimum = Math.Max(0, chartHardware.ChartAreas[0].AxisX.Maximum - maxPoints);

            float ramAvailable = RamCounter.NextValue();
            label4.Text = $"Kalan Bellek: {ramAvailable} MB";
        }


        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;

            tooltip.RemoveAll();
            prevPosition = pos;

            var results = chartHardware.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var point = result.Object as DataPoint;
                    if (point != null)
                    {
                        tooltip.Show($"Değer: {point.YValues[0]}", chartHardware, pos.X, pos.Y - 15);
                    }
                }
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
            Application.Exit();
        }

        private void TransparentControls()
        {
            picLogo.Parent = picBack;
            picLogo.BackColor = Color.Transparent;

            TagName.Parent = picBack;
            TagName.BackColor = Color.Transparent;

            label4.Parent = picBack;
            label4.BackColor=Color.Transparent;
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

        private void chartHardware_Click(object sender, EventArgs e)
        {
            float cpuUsage = 100 - IdleCounter.NextValue();
            float ramAvailable = RamCounter.NextValue();

            MessageBox.Show($"CPU Kullanımı: {cpuUsage}%\nKalan Bellek : {ramAvailable} MB",
                            "CPU Bilgisi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }


    }
}
