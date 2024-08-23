using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Defendo_Blue.Forms
{
    public partial class ScanUrlForm : Form
    {
        private readonly string apiKey = "77fd1f6a20cd3fb04fe63493fc40d4628f661aed7367fb63ab8018e1e423bb31";

        public ScanUrlForm()
        {
            InitializeComponent();
            this.CheckUrl.Click += new EventHandler(CheckUrlButton_Click);
        }

        private async void CheckUrlButton_Click(object sender, EventArgs e)
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
                PrintScan(urlResult);
            }
        }

        private void PrintScan(UrlReport urlReport)
        {
            string resultMessage = "Tarama Sonuçları:\n\n";

            resultMessage += $"Scan ID: {urlReport.ScanId}\n";
            resultMessage += $"Message: {urlReport.VerboseMsg}\n\n";

            if (urlReport.ResponseCode == UrlReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, UrlScanEngine> scan in urlReport.Scans)
                {
                    resultMessage += $"{scan.Key,-25} Detected: {scan.Value.Detected}\n";
                }
            }

            MessageBox.Show(resultMessage, "Tarama Sonuçları", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PrintScan(UrlScanResult urlResult)
        {
            string resultMessage = "Tarama Sonuçları:\n\n";

            resultMessage += $"Scan ID: {urlResult.ScanId}\n";
            resultMessage += $"Message: {urlResult.VerboseMsg}\n";

            MessageBox.Show(resultMessage, "Tarama Sonuçları", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
