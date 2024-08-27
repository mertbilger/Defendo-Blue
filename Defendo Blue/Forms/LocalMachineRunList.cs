using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class LocalMachineRunList : Form
    {
        public LocalMachineRunList()
        {
            InitializeComponent();
            LoadRegistryDataMachine();

            deleteButton.Click += DeleteButton_Click;
            closeButton.Click += CloseButton_Click;

        }

        private void LoadRegistryDataMachine()
        {
            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            checkedListBox1.Items.Clear();

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (RegistryKey rk = baseKey.OpenSubKey(subKeyPath))
                {
                    if (rk != null)
                    {
                        foreach (string valueName in rk.GetValueNames())
                        {
                            object value = rk.GetValue(valueName);
                            checkedListBox1.Items.Add($"{valueName}: {value}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Alt anahtar bulunamadı.", "Hata");
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Erişim hatası: " + ex.Message, "Hata");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Silmek için en az bir öğe seçmelisiniz.", "Uyarı");
                return;
            }

            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (RegistryKey rk = baseKey.OpenSubKey(subKeyPath, true))
                {
                    if (rk != null)
                    {
                        foreach (var item in checkedListBox1.CheckedItems.OfType<string>().ToList())
                        {
                            string valueName = item.Split(':')[0].Trim();
                            rk.DeleteValue(valueName, false);
                        }

                        MessageBox.Show("Seçili öğeler başarıyla silindi.", "Bilgi");
                        LoadRegistryDataMachine(); 
                    }
                    else
                    {
                        MessageBox.Show("Alt anahtar bulunamadı.", "Hata");
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Erişim hatası: " + ex.Message, "Hata");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

  
    }
}
