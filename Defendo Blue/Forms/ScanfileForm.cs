using LiteDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;
using Defendo_Blue.Models; 

namespace Defendo_Blue.Forms
{
    public partial class ScanfileForm : Form
    {
        private readonly string apiKey = "77fd1f6a20cd3fb04fe63493fc40d4628f661aed7367fb63ab8018e1e423bb31";
        private OpenFileDialog openFileDialog;
        private string databasePath = @"C:\Users\mertb\source\repos\mertbilger\Defendo-Blue\Defendo Blue\ScannedFiles.db";

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

                    SaveScannedFileToDatabase(Path.GetFileName(filePath), filePath, IsFileMalicious(fileReport), fileReport.ScanId);
                }
                else
                {
                    MessageBox.Show("Dosya daha önce taranmamış, şimdi taranıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ScanResult scanResult = await virusTotal.ScanFileAsync(fileBytes, Path.GetFileName(filePath));
                    MessageBox.Show("Tarama kuyruğa alındı, 10 dakika sonra sonuç için tekrar deneyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SaveScannedFileToDatabase(Path.GetFileName(filePath), filePath, false, scanResult.ScanId);
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

                    SaveScannedFileToDatabase(Path.GetFileName(filePath), filePath, IsFileMalicious(fileReport), fileReport.ScanId);
                }
                else
                {
                    MessageBox.Show("Dosya daha önce taranmamış, şimdi taranıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ScanResult scanResult = await virusTotal.ScanFileAsync(fileBytes, Path.GetFileName(filePath));
                    MessageBox.Show("Tarama kuyruğa alındı, 10 dakika sonra sonuç için tekrar deneyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SaveScannedFileToDatabase(Path.GetFileName(filePath), filePath, false, scanResult.ScanId);
                }
            }
        }

        private void SaveScannedFileToDatabase(string fileName, string filePath, bool isMalicious, string scanId)
        {
            try
            {
                using (var db = new LiteDatabase(databasePath))
                {
                    var scannedFilesCollection = db.GetCollection<ScannedFileDB>("scannedFiles");

                    var scannedFile = new ScannedFileDB
                    {
                        FileName = fileName,
                        FilePath = filePath,
                        IsMalicious = isMalicious,
                        ScanId = scanId,
                        ScanDate = DateTime.Now
                    };

                    scannedFilesCollection.Insert(scannedFile);
                    MessageBox.Show("Dosya başarıyla veritabanına kaydedildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanına veri eklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsFileMalicious(FileReport fileReport)
        {
            string[] trustedEngines = new string[]
            {
                "Kaspersky",
                "BitDefender",
                "Microsoft",
                "Avira",
                "McAfee",
                "F-Secure",
                "ESET-NOD32",
                "Symantec",
                "Malwarebytes",
                "AVG",
                "Avast"
            };

            foreach (var scan in fileReport.Scans)
            {
                if (trustedEngines.Contains(scan.Key) && scan.Value.Detected)
                {
                    return true;
                }
            }

            return false;
        }

        private void PrintScan(FileReport pFileReport)
        {
            StringBuilder resultMessage = new StringBuilder();
            resultMessage.AppendLine("Tarama Sonuçları\n");
            resultMessage.AppendLine($"Scan ID: {pFileReport.ScanId}");
            resultMessage.AppendLine($"Mesaj: {pFileReport.VerboseMsg}\n");
            resultMessage.AppendLine(string.Format("{0,-30} {1}", "Antivirüs Motoru", "Durum"));
            resultMessage.AppendLine(new string('-', 50));

            if (pFileReport.ResponseCode == FileReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, ScanEngine> scan in pFileReport.Scans)
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
