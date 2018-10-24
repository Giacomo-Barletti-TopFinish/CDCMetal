using CDCMetal.BLL;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDCMetal
{
    public partial class ExcelCaricaNuovoDocumentoFrm : BaseChildForm
    {
        public ExcelCaricaNuovoDocumentoFrm()
        {
            InitializeComponent();
        }

        private void btnCercaFile_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN CERCA FILE EXCEL", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
        }

        private void btnApriFile_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                if (string.IsNullOrEmpty(txtFilePath.Text))
                {
                    lblMessage.Text = "Selezionare un file";
                    return;
                }

                if (!File.Exists(txtFilePath.Text))
                {
                    lblMessage.Text = "Il file specificato non esiste";
                    return;
                }

                ExcelBLL bll = new ExcelBLL();
                string messaggioErrore;
                if (!bll.LeggiExcelCDC(Contesto.DS, txtFilePath.Text, Contesto.Utente.FULLNAMEUSER, out messaggioErrore))
                {
                    string messaggio = string.Format("Errore nel caricamento del file excel. Errore: {0}", messaggioErrore);
                    MessageBox.Show(messaggio, "ERRORE LETTURA FILE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblNumeroRigheExcel.Text = Contesto.DS.CDC_DETTAGLIO.Count.ToString();
                if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
                    lblDataExcel.Text = Contesto.DS.CDC_DETTAGLIO.FirstOrDefault().DATACOLLAUDO;

                dgvExcelCaricato.AutoGenerateColumns = true;
                dgvExcelCaricato.DataSource = Contesto.DS;
                dgvExcelCaricato.DataMember = Contesto.DS.CDC_DETTAGLIO.TableName;
                dgvExcelCaricato.Columns["IDDETTAGLIO"].Visible = false;
                dgvExcelCaricato.Columns["IDEXCEL"].Visible = false;


            }
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN APRI FILE EXCEL", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
        }

    }
}
