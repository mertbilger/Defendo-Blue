using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class AllUserRun : Form
    {
        public AllUserRun()
        {
            InitializeComponent();
            this.Load += new EventHandler(AllUserRun_Load);
        }

        private void AllUserRun_Load(object sender, EventArgs e)
        {
            string path = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp";

            try
            {
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
