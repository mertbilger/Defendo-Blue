using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;

namespace Defendo_Blue.Forms
{
    public partial class ScanfileForm : Form
    {
        private readonly string apiKey = "77fd1f6a20cd3fb04fe63493fc40d4628f661aed7367fb63ab8018e1e423bb31";
        private OpenFileDialog openFileDialog;

        public ScanfileForm()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form_DragEnter);
            this.DragDrop += new DragEventHandler(Form_DragDrop);
            transparentControls();

            openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*",
                Title = "Dosya Seç"
            };

            addFile.Click += AddFile_Click;
        }

        private async void AddFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(filePath);

                VirusTotal virusTotal = new VirusTotal(apiKey);
                virusTotal.UseTLS = true;

                FileReport fileReport = await virusTotal.GetFileReportAsync(fileBytes);

                bool hasFileBeenScannedBefore = fileReport.ResponseCode == FileReportResponseCode.Present;

                if (hasFileBeenScannedBefore)
                {
                    MessageBox.Show("Dosya daha önce tarandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PrintScan(fileReport);
                }
                else
                {
                    MessageBox.Show("Dosya daha önce taranmamış, şimdi taranıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ScanResult scanResult = await virusTotal.ScanFileAsync(fileBytes, Path.GetFileName(filePath));
                    MessageBox.Show("Tarama kuyruğa alındı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void Form_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                string filePath = files[0];
                byte[] fileBytes = File.ReadAllBytes(filePath);

                VirusTotal virusTotal = new VirusTotal(apiKey);
                virusTotal.UseTLS = true;

                FileReport fileReport = await virusTotal.GetFileReportAsync(fileBytes);

                bool hasFileBeenScannedBefore = fileReport.ResponseCode == FileReportResponseCode.Present;

                if (hasFileBeenScannedBefore)
                {
                    MessageBox.Show("Dosya daha önce tarandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PrintScan(fileReport);
                }
                else
                {
                    MessageBox.Show("Dosya daha önce taranmamış, şimdi taranıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void PrintScan(FileReport fileReport)
        {
            StringBuilder resultMessage = new StringBuilder();
            resultMessage.AppendLine("Tarama Sonuçları\n");
            resultMessage.AppendLine($"Scan ID: {fileReport.ScanId}");
            resultMessage.AppendLine($"Mesaj: {fileReport.VerboseMsg}\n");
            resultMessage.AppendLine(string.Format("{0,-30} {1}", "Antivirüs Motoru", "Durum"));
            resultMessage.AppendLine(new string('-', 50));

            if (fileReport.ResponseCode == FileReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, ScanEngine> scan in fileReport.Scans)
                {
                    string detectionStatus = scan.Value.Detected ? "Tespit Edildi" : "Tespit Edilmedi";
                    resultMessage.AppendLine(string.Format("{0,-30} {1}", scan.Key, detectionStatus));
                }
            }

            ScanResultForm resultForm = new ScanResultForm(resultMessage.ToString());
            resultForm.ShowDialog();
        }



        private void transparentControls()
        {
            addFile.Parent = bgFileBack;
            addFile.BackColor = Color.Transparent;

            content1.Parent = bgFileBack;
            content1.BackColor = Color.Transparent;
        }
    }
}
