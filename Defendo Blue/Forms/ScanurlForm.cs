using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;
using System.Configuration; 

namespace Defendo_Blue.Forms
{
    public partial class ScanUrlForm : Form
    {
        private readonly string apiKey;

        private readonly string urlPattern = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

        public ScanUrlForm()
        {
            InitializeComponent();
            apiKey = ConfigurationManager.AppSettings["VirusTotalapiKey"];
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

        private bool IsUrlValid(string url)
        {
            return Regex.IsMatch(url, urlPattern);
        }

        private async void CheckUrl_Click(object sender, EventArgs e)
        {
            string urlToScan = textBox1.Text;

            if (string.IsNullOrWhiteSpace(urlToScan))
            {
                MessageBox.Show("Lütfen bir URL girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsUrlValid(urlToScan))
            {
                MessageBox.Show("Geçersiz URL formatı. Lütfen geçerli bir URL girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
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
                    MessageBox.Show("URL daha önce taranmamış, şimdi taranıyor... Lütfen 5-10 dk sonra tekrar deneyiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UrlScanResult urlResult = await oVirusTotal.ScanUrlAsync(urlToScan);
                    string debugInfo = $"ScanId: {urlResult.ScanId}, VerboseMsg: {urlResult.VerboseMsg}";

                    MessageBox.Show("Tarama isteği gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
