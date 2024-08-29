using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Defendo_Blue.Forms
{
    public partial class RegistryForm : Form
    {
        public RegistryForm()
        {
            InitializeComponent();
            InitializeCheckedListBox();
            LoadRegistryData();
            TransparentControl();
        }

        private void InitializeCheckedListBox()
        {
     
            this.Controls.Add(checkedListBox1);
        }

        private void LoadRegistryData()
        {
            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(subKeyPath))
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

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string item = checkedListBox1.Items[e.Index].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LocalMachineRunList localMachineRunList = new LocalMachineRunList();
            localMachineRunList.Show();
        }

        private void bit32_Click(object sender, EventArgs e)
        {
            _32LocalMAchineRunList _32localMachineRunList = new _32LocalMAchineRunList();
            _32localMachineRunList.Show();
        }

        private void TransparentControl()
        {
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
        }

        private void AllUSer_Click(object sender, EventArgs e)
        {
            AllUserRun allUserRun = new AllUserRun();
            allUserRun.Show();
        }

        private void ValidUser_Click(object sender, EventArgs e)
        {
            ValidUserRun validUserRun = new ValidUserRun();
            validUserRun.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Silmek için en az bir öğe seçmelisiniz.", "Uyarı");
                return;
            }

            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
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
                        LoadRegistryData(); 
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
    }
}
