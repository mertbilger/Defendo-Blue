using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class ValidUserRun : Form
    {
        public ValidUserRun()
        {
            InitializeComponent();
            this.Load += new EventHandler(AllUserRun_Load);
        }

        private void AllUserRun_Load(object sender, EventArgs e)
        {
            try
            {
                string username = Environment.UserName;
                string path = $@"C:\Users\{username}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";

                string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

                var visibleFiles = files.Where(file =>
                {
                    FileAttributes attributes = File.GetAttributes(file);
                    return !attributes.HasFlag(FileAttributes.Hidden) && !attributes.HasFlag(FileAttributes.System);
                }).Select(file => Path.GetFileName(file)).ToArray();

                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(visibleFiles);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosyalar alınırken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
