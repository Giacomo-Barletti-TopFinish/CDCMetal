using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDCMetal
{
    public partial class ExcelCaricaNuovoDocumentoFrm : Form
    {
        public ExcelCaricaNuovoDocumentoFrm()
        {
            InitializeComponent();
        }

        private void btnCercaFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}
