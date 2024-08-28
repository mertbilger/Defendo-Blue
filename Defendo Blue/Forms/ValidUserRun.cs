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
            this.Load += new EventHandler(AllUserRun_Load); // Form yüklendiğinde event handler'ı bağla
        }

        private void AllUserRun_Load(object sender, EventArgs e)
        {
            try
            {
                // Geçerli kullanıcı adını al
                string username = Environment.UserName;

                // Kullanıcıya ait Startup dizininin yolunu oluştur
                string path = $@"C:\Users\{username}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";

                // Dosyaların yolunu al
                string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

                // Dosya isimlerini al ve checkedListBox1'e ekle
                checkedListBox1.Items.Clear(); // Mevcut öğeleri temizle
                checkedListBox1.Items.AddRange(files.Select(file => Path.GetFileName(file)).ToArray());
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi ver
                MessageBox.Show("Dosyalar alınırken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
