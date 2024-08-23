using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    public partial class ScanResultForm : Form
    {
        public ScanResultForm(string resultMessage)
        {
            InitializeComponent();
            DisplayResults(resultMessage);
        }

        private void DisplayResults(string resultMessage)
        {
            resultTextBox.Text = resultMessage;
        }




    }
}
