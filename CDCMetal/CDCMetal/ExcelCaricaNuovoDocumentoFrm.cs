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
                btnSalvaDB.Enabled = false;

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
                else
                {
                    lblMessage.Text = "Il file è vuoto";
                    return;
                }

                btnSalvaDB.Enabled = true;

                dgvExcelCaricato.AutoGenerateColumns = true;
                dgvExcelCaricato.DataSource = Contesto.DS;
                dgvExcelCaricato.DataMember = Contesto.DS.CDC_DETTAGLIO.TableName;
                dgvExcelCaricato.Columns["IDDETTAGLIO"].Visible = false;
                dgvExcelCaricato.Columns["IDEXCEL"].Visible = false;

                List<decimal> IDPRENOTAZIONE_DUPLICATO = bll.VerificaExcelCaricato(Contesto.DS);
                if (IDPRENOTAZIONE_DUPLICATO.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Sono state individuate delle righe il cui ID PRENOTAZIONE è già stato acquisito. Si tratta di righe duplicate.");
                    sb.AppendLine("Di seguito gli ID PRENOTAZIONE duplicati:");
                    foreach (decimal id in IDPRENOTAZIONE_DUPLICATO)
                        sb.AppendLine(id.ToString());

                    MessageBox.Show(sb.ToString(), "RIGHE DUPLICATE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                foreach (DataGridViewRow riga in dgvExcelCaricato.Rows)
                {
                    decimal IDPRENOTAZIONE = (decimal)riga.Cells["IDPRENOTAZIONE"].Value;
                    if (IDPRENOTAZIONE_DUPLICATO.Contains(IDPRENOTAZIONE))
                    {
                        riga.Cells["IDPRENOTAZIONE"].Style.BackColor = Color.Yellow;
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN APRI FILE EXCEL", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
        }

        private void btnSalvaDB_Click(object sender, EventArgs e)
        {
            ExcelBLL bll = new ExcelBLL();
            List<decimal> IDPRENOTAZIONE_DUPLICATO = bll.VerificaExcelCaricato(Contesto.DS);
            if (IDPRENOTAZIONE_DUPLICATO.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Ci sono righe che sono già state salvate sul database (doppioni).");
                sb.AppendLine("Le righe duplicate NON saranno salvate.");

                MessageBox.Show(sb.ToString(), "RIGHE DUPLICATE", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                foreach (CDCDS.CDC_DETTAGLIORow dettaglio in Contesto.DS.CDC_DETTAGLIO.Where(x => x.RowState != DataRowState.Deleted && IDPRENOTAZIONE_DUPLICATO.Contains(x.IDPRENOTAZIONE)))
                    dettaglio.Delete();

                Contesto.DS.CDC_DETTAGLIO.AcceptChanges();

            }

            bll.Salva(Contesto.DS);

        }
    }
}
