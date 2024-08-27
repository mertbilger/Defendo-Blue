using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class _32LocalMAchineRunList : Form
    {
        public _32LocalMAchineRunList()
        {
            InitializeComponent();
            LoadRegistryData();
        }

        private void LoadRegistryData()
        {
            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(subKeyPath))
            {
                if (rk != null)
                {
                    checkedListBox1.Items.Clear(); 

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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(subKeyPath, writable: true))
            {
                if (rk != null)
                {
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        string keyName = item.ToString().Split(':')[0].Trim();

                        rk.DeleteValue(keyName, throwOnMissingValue: false);
                    }

                    LoadRegistryData();
                }
                else
                {
                    MessageBox.Show("Alt anahtar bulunamadı.", "Hata");
                }
            }
        }

    }
}
