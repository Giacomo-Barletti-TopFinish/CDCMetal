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
    public partial class AnalisiPiomboDaExcelFrm : BaseChildForm
    {
        public AnalisiPiomboDaExcelFrm()
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
                CDCBLL cdcBll = new CDCBLL();
                string messaggioErrore;
                if (!bll.LeggiExcelAnalisiPiombo(_DS, txtFilePath.Text, Contesto.Utente.FULLNAMEUSER, out messaggioErrore))
                {
                    string messaggio = string.Format("Errore nel caricamento del file excel. Errore: {0}", messaggioErrore);
                    MessageBox.Show(messaggio, "ERRORE LETTURA FILE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblNumeroRigheExcel.Text = _DS.CDC_CERTIFICATIPIOMBO.Count.ToString();
                if (_DS.CDC_CERTIFICATIPIOMBO.Count == 0)
                {
                    lblMessage.Text = "Il file è vuoto";
                    return;
                }

                dgvExcelCaricato.AutoGenerateColumns = true;
                dgvExcelCaricato.DataSource = _DS;
                dgvExcelCaricato.DataMember = _DS.CDC_CERTIFICATIPIOMBO.TableName;
                dgvExcelCaricato.Columns["IDCERTIFICATIPIOMBO"].Visible = false;
                dgvExcelCaricato.Columns["UTENTE"].Visible = false;
                dgvExcelCaricato.Columns["DATAINSERIMENTO"].Visible = false;

                foreach (DataGridViewRow riga in dgvExcelCaricato.Rows)
                {
                    decimal nPd = (decimal)riga.Cells["PBPPM"].Value;
                    Color colore;
                    riga.Cells["ESITO"].Value = cdcBll.CalcolaEsitoAnalisiPiombo(nPd, out colore);
                    riga.Cells["ESITO"].Style.BackColor = colore;
                }
            }
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN APRI FILE EXCEL", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
        }

        private void btnCreaPdf_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Bitmap firma = Properties.Resources.logo_tf_autodichiarazione;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                CDCBLL bll = new CDCBLL();

                StringBuilder files = new StringBuilder();

                foreach (CDCDS.CDC_CERTIFICATIPIOMBORow riga in _DS.CDC_CERTIFICATIPIOMBO)
                {
                    decimal nPd = riga.PBPPM;
                    Color colore;
                    bll.CalcolaEsitoAnalisiPiombo(nPd, out colore);
                    string spessore = string.Empty;
                    string lunghezza = string.Empty;
                    string larghezza = string.Empty;
                    string elemento = string.Empty;
                    if (!riga.IsELEMENTONull())
                        elemento = riga.ELEMENTO.ToString();

                    if (!riga.IsLUNGHEZZANull())
                        lunghezza = riga.LUNGHEZZA.ToString();
                    if (!riga.IsLARGHEZZANull())
                        larghezza = riga.LARGHEZZA.ToString();

                    if (!riga.IsSPESSORENull())
                        spessore = riga.SPESSORE.ToString();
                    string path = bll.CreaPDFCertificatoPiombo(elemento, lunghezza, larghezza, spessore.ToString(), riga.CODICE, riga.LOTTO,
                    riga.ESITO, colore, riga.METODO, riga.DATACERTIFICATO, riga.PBPPM, riga.CDPPM, Contesto.PathAnalisiPiombo, image);
                    files.AppendLine(path);
                    riga.PATHFILE = path.Length > 300 ? path.Substring(0, 300) : path;

                }
                //List<CDCDS.CDC_CERTIFICATIPIOMBORow> certificati = _DS.CDC_CERTIFICATIPIOMBO.Where(x => x.IsLUNGHEZZANull()).ToList();
                //foreach (CDCDS.CDC_CERTIFICATIPIOMBORow certificato in certificati)
                //    certificato.Delete();
                bll.SalvaCertificatiPiombo(_DS);

                MessageBox.Show("Operazionbe eseguita con successo", "INFORMAZIONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN CREA PDF", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }
    }
}
