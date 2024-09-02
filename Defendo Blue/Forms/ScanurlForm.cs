using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;

namespace Defendo_Blue.Forms
{
    public partial class ScanUrlForm : Form
    {
        private readonly string apiKey = "77fd1f6a20cd3fb04fe63493fc40d4628f661aed7367fb63ab8018e1e423bb31";

        public ScanUrlForm()
        {
            InitializeComponent();
        }

        private void PrintScan(UrlReport urlReport)
        {
            StringBuilder resultMessage = new StringBuilder();
            resultMessage.AppendLine("Tarama Sonuçları\n");
            resultMessage.AppendLine($"Scan ID: {urlReport.ScanId}");
            resultMessage.AppendLine($"Mesaj: {urlReport.VerboseMsg}\n");
            resultMessage.AppendLine(string.Format("{0,-30} {1}", "Antivirüs Motoru", "Durum"));
            resultMessage.AppendLine(new string('-', 50));

            if (urlReport.ResponseCode == UrlReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, UrlScanEngine> scan in urlReport.Scans)
                {
                    string detectionStatus = scan.Value.Detected ? "Tespit Edildi" : "Tespit Edilmedi";
                    resultMessage.AppendLine(string.Format("{0,-30} {1}", scan.Key, detectionStatus));
                }
            }

            ScanResultForm resultForm = new ScanResultForm(resultMessage.ToString());
            resultForm.ShowDialog();
        }

        private void PrintScan(UrlScanResult urlResult)
        {
            StringBuilder resultMessage = new StringBuilder();
            resultMessage.AppendLine("Tarama Sonuçları\n");
            resultMessage.AppendLine($"Scan ID: {urlResult.ScanId}");
            resultMessage.AppendLine($"Mesaj: {urlResult.VerboseMsg}\n");

            resultMessage.AppendLine("Taranan URL bilgisi başarıyla alındı. Detaylar aşağıda:");

            ScanResultForm resultForm = new ScanResultForm(resultMessage.ToString());
            resultForm.ShowDialog();
        }


        private async void CheckUrl_Click(object sender, EventArgs e)
        {
            string urlToScan = textBox1.Text;

            if (string.IsNullOrWhiteSpace(urlToScan))
            {
                MessageBox.Show("Lütfen bir URL girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VirusTotal oVirusTotal = new VirusTotal(apiKey);
            oVirusTotal.UseTLS = true;

            UrlReport urlReport = await oVirusTotal.GetUrlReportAsync(urlToScan);
            bool hasUrlBeenScannedBefore = urlReport.ResponseCode == UrlReportResponseCode.Present;

            if (hasUrlBeenScannedBefore)
            {
                MessageBox.Show("URL daha önce tarandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PrintScan(urlReport);
            }
            else
            {
                MessageBox.Show("URL daha önce taranmamış, şimdi taranıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UrlScanResult urlResult = await oVirusTotal.ScanUrlAsync(urlToScan);

                string debugInfo = $"ScanId: {urlResult.ScanId}, VerboseMsg: {urlResult.VerboseMsg}";
                MessageBox.Show(debugInfo, "Debug Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PrintScan(urlResult);
            }
        }

    }
}
