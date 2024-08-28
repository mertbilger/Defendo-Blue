using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Defendo_Blue.Forms
{
    public partial class FastScan : Form
    {
        public FastScan()
        {
            InitializeComponent();
            transparentControls();
        }
  
        private bool CheckForMacros(string filePath)
        {
            try
            {
                string extension = System.IO.Path.GetExtension(filePath).ToLower();

                switch (extension)
                {
                    case ".xlsm":
                        return CheckForMacrosInExcel(filePath);
                    case ".docm":
                        return CheckForMacrosInWord(filePath);
                    case ".pptm":
                        return CheckForMacrosInPowerPoint(filePath);
                    default:
                        MessageBox.Show("Desteklenmeyen dosya türü.", "Hata");
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
                return false;
            }
        }

        private bool CheckForMacrosInExcel(string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
            {
                return document.WorkbookPart.VbaProjectPart != null;
            }
        }

        private bool CheckForMacrosInWord(string filePath)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open(filePath, false))
            {
                return document.MainDocumentPart.VbaProjectPart != null;
            }
        }

        private bool CheckForMacrosInPowerPoint(string filePath)
        {
            using (PresentationDocument document = PresentationDocument.Open(filePath, false))
            {
                return document.PresentationPart.VbaProjectPart != null;
            }
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Office Dosyaları (*.xlsm; *.docm; *.pptm)|*.xlsm;*.docm;*.pptm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    bool hasMacros = CheckForMacros(filePath);

                    if (hasMacros)
                    {
                        MessageBox.Show("Makro tespit edildi.", "Sonuç");
                    }
                    else
                    {
                        MessageBox.Show("Makro bulunamadı.", "Sonuç");
                    }
                }
            }

        }
        private void transparentControls()
        {
            addFile.Parent = pictureBox1;
            addFile.BackColor = Color.Transparent;

            content1.Parent = pictureBox1;
            content1.BackColor = Color.Transparent;
        }
    }
}
 
    

