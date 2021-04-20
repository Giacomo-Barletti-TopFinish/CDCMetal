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
            //ddlBrand.SelectedIndex = -1;
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
                if (!bll.LeggiExcelCDC(_DS, txtFilePath.Text, Contesto.Utente.FULLNAMEUSER, out messaggioErrore)) //brand,
                {
                    string messaggio = string.Format("Errore nel caricamento del file excel. Errore: {0}", messaggioErrore);
                    MessageBox.Show(messaggio, "ERRORE LETTURA FILE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblNumeroRigheExcel.Text = _DS.CDC_DETTAGLIO_rel.Count.ToString();
                if (_DS.CDC_DETTAGLIO_rel.Count > 0)
                    lblDataExcel.Text = _DS.CDC_DETTAGLIO_rel.FirstOrDefault().DATACOLLAUDO;
                else
                {
                    lblMessage.Text = "Il file è vuoto";
                    return;
                }

                btnSalvaDB.Enabled = true;



                dgvExcelCaricato.AutoGenerateColumns = true;
                dgvExcelCaricato.DataSource = _DS;
                dgvExcelCaricato.DataMember = _DS.CDC_DETTAGLIO_rel.TableName;
                dgvExcelCaricato.Columns["IDDETTAGLIO"].Visible = false;
                dgvExcelCaricato.Columns["IDEXCEL"].Visible = false;
                dgvExcelCaricato.Columns["IDBRAND"].Visible = false;
                dgvExcelCaricato.ReadOnly = false;
                foreach (DataGridViewColumn col in dgvExcelCaricato.Columns)
                {
                    col.ReadOnly = true;
                }

                if (dgvExcelCaricato.Columns.Contains("BRAND"))
                { dgvExcelCaricato.Columns.Remove("BRAND"); }

                var comboCol = new DataGridViewComboBoxColumn();
                comboCol.HeaderText = "BRAND";
                comboCol.Name = "BRAND";
                comboCol.DataPropertyName = "IDBRAND";
                comboCol.AutoComplete = true;
                comboCol.Width = 100;
                comboCol.DropDownWidth = 100;
                comboCol.DataSource = _DS.CDC_BRANDS;
                comboCol.DisplayMember = "CODICE";
                comboCol.ValueMember = "IDBRAND";
                comboCol.ReadOnly = false;
                dgvExcelCaricato.Columns.Add(comboCol);




                List<decimal> IDPRENOTAZIONE_DUPLICATO = bll.VerificaExcelCaricato(_DS);

                List<decimal> IDPRENOTAZIONE_NOBRAND = bll.VerificaExcelCaricatoBrand(_DS).ToList();

                if (IDPRENOTAZIONE_DUPLICATO.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Sono state individuate delle righe il cui ID PRENOTAZIONE è già stato acquisito. Si tratta di righe duplicate.");
                    sb.AppendLine("Di seguito gli ID PRENOTAZIONE duplicati:");
                    foreach (decimal id in IDPRENOTAZIONE_DUPLICATO)
                        sb.AppendLine(id.ToString());

                    MessageBox.Show(sb.ToString(), "RIGHE DUPLICATE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (IDPRENOTAZIONE_NOBRAND.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Sono state individuate delle righe per le quali non è stato possibile attribuire il BRAND.");
                    sb.AppendLine("Di seguito gli ID PRENOTAZIONE:");
                    foreach (decimal id in IDPRENOTAZIONE_NOBRAND)
                        sb.AppendLine(id.ToString());

                    MessageBox.Show(sb.ToString(), "RIGHE SENZA BRAND", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



                ///ALLA FINE
                foreach (DataGridViewRow riga in dgvExcelCaricato.Rows)
                {
                    decimal IDPRENOTAZIONE = (decimal)riga.Cells["IDPRENOTAZIONE"].Value;

                    if (IDPRENOTAZIONE_NOBRAND.Contains(IDPRENOTAZIONE))
                    {
                        riga.Cells["IDPRENOTAZIONE"].Style.BackColor = Color.Orange;
                    }

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
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblMessage.Text = string.Empty;

                ExcelBLL bll = new ExcelBLL();
                List<decimal> IDPRENOTAZIONE_DUPLICATO = bll.VerificaExcelCaricato(_DS);

                List<CDCDS.CDC_DETTAGLIO_relRow> daCancellareNoBrand = _DS.CDC_DETTAGLIO_rel.Where(x => x.RowState != DataRowState.Deleted && !IDPRENOTAZIONE_DUPLICATO.Contains(x.IDPRENOTAZIONE) && x.IsIDBRANDNull()).ToList();
                //_DS.CDC_DETTAGLIO.AcceptChanges();


                if (daCancellareNoBrand.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Ci sono righe senza Brand.");
                    sb.AppendLine("Assegnare un Brand o cancellarle,");

                    MessageBox.Show(sb.ToString(), "RIGHE SENZA BRAND", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IDPRENOTAZIONE_DUPLICATO.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Ci sono righe che sono già state salvate sul database.");
                    sb.AppendLine("Le righe duplicate NON saranno salvate.");

                    MessageBox.Show(sb.ToString(), "RIGHE DUPLICATE", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    List<CDCDS.CDC_DETTAGLIO_relRow> daCancellare = _DS.CDC_DETTAGLIO_rel.Where(x => x.RowState != DataRowState.Deleted && IDPRENOTAZIONE_DUPLICATO.Contains(x.IDPRENOTAZIONE)).ToList();

                    foreach (CDCDS.CDC_DETTAGLIO_relRow dettaglio in daCancellare)
                        dettaglio.Delete();


                    _DS.CDC_DETTAGLIO_rel.AcceptChanges();
                }

                bll.Salva(_DS);

                lblMessage.Text = "Salvataggio riuscito";
                //ddlBrand.SelectedIndex = -1;

                string message = "operazione riuscita";
                MessageBox.Show(message, "INFORMAZIONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN APRI FILE EXCEL", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void ExcelCaricaNuovoDocumentoFrm_Load(object sender, EventArgs e)
        {
            CDCBLL bll = new CDCBLL();
            bll.CaricaBrands(_DS);
            bll.CaricaEnti(_DS);//JACOPO

            // CaricaDropDownListBrands();
        }

        //private void CaricaDropDownListBrands()
        //{
        //    ddlBrand.Items.AddRange(_DS.CDC_BRANDS.Select(XmlReadMode => XmlReadMode.CODICE).ToArray());
        //}

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExcelCaricaNuovoDocumentoFrm_Load_1(object sender, EventArgs e)
        {

        }

        //private void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}
    }
}
