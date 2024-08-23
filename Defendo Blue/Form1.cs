using Defendo_Blue.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Defendo_Blue
{
    public partial class Form1 : Form
    {
        private ContextMenuStrip contextMenu;
        private WinEvent winEventForm;

        public Form1()
        {
            InitializeComponent();
            Setup();
            transparentControls();
            this.Load += new EventHandler(Form1_Load);
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

            button1.Click += new EventHandler(button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = "OK";
            notifyIcon1.ShowBalloonTip(1000);
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
    }
}
