using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Defendo_Blue.Forms
{
    public partial class RegistryForm : Form
    {
        private DataTable dataTable;
        
        public RegistryForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadRegistryData();
            TransparentControl();
        }
   
        private void InitializeDataGridView()
        {

            dataTable = new DataTable();
            dataTable.Columns.Add("Anahtar Adı", typeof(string));
            dataTable.Columns.Add("Değer", typeof(string));

            dataGridView1.DataSource = dataTable;

            this.Controls.Add(dataGridView1);

            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void LoadRegistryData()
        {
            string subKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(subKeyPath))
            {
                if (rk != null)
                {
                    dataTable.Clear();

                    foreach (string valueName in rk.GetValueNames())
                    {
                        object value = rk.GetValue(valueName);
                        
                        dataTable.Rows.Add(valueName, value);
                    }
                }
                else
                {
                    MessageBox.Show("Alt anahtar bulunamadı.", "Hata");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string keyName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string value = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            }
        }

        private void TransparentControl()
        {

        }
    }
}
