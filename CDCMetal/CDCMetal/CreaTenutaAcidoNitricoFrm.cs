using CDCMetal.BLL;
using CDCMetal.Entities;
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
    public partial class CreaTenutaAcidoNitricoFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "TENUTAACIDONITRICO";

        public CreaTenutaAcidoNitricoFrm()
        {
            InitializeComponent();
        }

        private void CreaTenutaAcidoNitricoFrm_Load(object sender, EventArgs e)
        {
            PopolaDDLDate();
        }

        private void PopolaDDLDate()
        {
            ddlDataCollaudo.Items.AddRange(CaricaDateCollaudo().ToArray());
        }

        private void CreaDsPerTenutaAcidoNitrico()
        {

            _dsServizio = new DataSet();
            DataTable dtCartelle = _dsServizio.Tables.Add();
            dtCartelle.TableName = tableName;
            dtCartelle.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
            dtCartelle.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal")).ReadOnly = true;
            dtCartelle.Columns.Add("ACCESSORISTA", Type.GetType("System.String")).ReadOnly = true;

            dtCartelle.Columns.Add("DATACOLLAUDO", Type.GetType("System.DateTime")).ReadOnly = true;
            dtCartelle.Columns.Add("PREFISSO", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("PARTE", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("COLORE", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("COMMESSA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("QUANTITA", Type.GetType("System.String")).ReadOnly = true;

            dtCartelle.Columns.Add("DATATEST", Type.GetType("System.DateTime"));
            dtCartelle.Columns.Add("NUMEROCAMPIONI", Type.GetType("System.Decimal"));
            dtCartelle.Columns.Add("BOLLA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("DATABOLLA", Type.GetType("System.DateTime"));
            dtCartelle.Columns.Add("ESITO", Type.GetType("System.Boolean"));


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in _DS.CDC_DETTAGLIO)
            {
                DataRow riga = dtCartelle.NewRow();

                riga[0] = dettaglio.IDDETTAGLIO;
                riga[1] = dettaglio.IDPRENOTAZIONE;
                riga[2] = dettaglio.ACCESSORISTA;
                riga[3] = dettaglio.DATACOLLAUDO;
                riga[4] = dettaglio.PREFISSO;
                riga[5] = dettaglio.PARTE;
                riga[6] = dettaglio.COLORE;
                riga[7] = dettaglio.COMMESSAORDINE;
                riga[8] = dettaglio.QUANTITA;

                CDCDS.CDC_TENUTACIDONITRICORow tenuta = _DS.CDC_TENUTACIDONITRICO.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO).FirstOrDefault();
                if (tenuta != null)
                {
                    riga[9] = tenuta.DATATEST;
                    riga[10] = tenuta.NUMEROCAMPIONI;
                    riga[11] = tenuta.IsBOLLANull()?string.Empty:tenuta.BOLLA;
                    if(!tenuta.IsDATADDTNull())
                        riga[12] = tenuta.DATADDT;
                    riga[13] = tenuta.ESITO == "S" ? true : false;
                }
                else
                {
                    riga[9] = DateTime.Today;
                    riga[10] = 1;
                    riga[11] = string.Empty;
                    //     riga[12] = string.Empty;
                    riga[13] = true;
                }

                dtCartelle.Rows.Add(riga);
            }

        }

        private void btnLeggiDati_Click(object sender, EventArgs e)
        {
            btnCreaPDF.Enabled = false;

            lblMessaggio.Text = string.Empty;
            if (ddlDataCollaudo.SelectedIndex == -1)
            {
                lblMessaggio.Text = "Selezionare una data";
                return;
            }

            DataCollaudo dataSelezionata = (DataCollaudo)ddlDataCollaudo.SelectedItem;

            CDCBLL bll = new CDCBLL();

            _DS = new Entities.CDCDS();

            bll.LeggiCollaudoDaData(_DS, dataSelezionata);


            if (_DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_TENUTACIDONITRICO(_DS, IDDETTAGLIO);
                bll.CDC_PDF(_DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerTenutaAcidoNitrico();

            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;

            dgvDettaglio.Columns[0].Frozen = true;
            dgvDettaglio.Columns[0].Visible = false;
            dgvDettaglio.Columns[1].Frozen = true;
            dgvDettaglio.Columns[2].Frozen = true;
            dgvDettaglio.Columns[3].Frozen = true;
            dgvDettaglio.Columns[4].Frozen = true;
            dgvDettaglio.Columns[5].Frozen = true;
            dgvDettaglio.Columns[6].Frozen = true;
            dgvDettaglio.Columns[7].Frozen = true;
            dgvDettaglio.Columns[7].Width = 130;
            dgvDettaglio.Columns[8].Frozen = true;
            dgvDettaglio.Columns[9].Width = 90;
            dgvDettaglio.Columns[10].Width = 130;
            dgvDettaglio.Columns[13].Width = 130;
        }

        private void btnCreaPDF_Click(object sender, EventArgs e)
        {
            string fileCreati = string.Empty;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                bool esito = true;
                lblMessaggio.Text = "";

                List<decimal> idPerPDF = new List<decimal>();
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    decimal iddettaglio = (decimal)riga[0];
                    idPerPDF.Add(iddettaglio);

                    string stringa = ConvertiInStringa(riga[11]);
                    stringa = stringa.Length > 25 ? stringa.Substring(0, 25) : stringa;


                    CDCDS.CDC_TENUTACIDONITRICORow tenutaRow = _DS.CDC_TENUTACIDONITRICO.Where(x => x.IDDETTAGLIO == iddettaglio).FirstOrDefault();
                    if (tenutaRow == null)
                    {
                        tenutaRow = _DS.CDC_TENUTACIDONITRICO.NewCDC_TENUTACIDONITRICORow();
                        tenutaRow.IDDETTAGLIO = iddettaglio;
                        tenutaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        tenutaRow.DATAINSERIMENTO = DateTime.Now;
                        tenutaRow.DATATEST = (DateTime)riga[9];
                        tenutaRow.NUMEROCAMPIONI = (Decimal)riga[10];
                        tenutaRow.BOLLA = stringa;
                        if (riga[12] != DBNull.Value)
                            tenutaRow.DATADDT = (DateTime)riga[12];
                        tenutaRow.ESITO = ConvertiBoolInStringa(riga[13]);

                        _DS.CDC_TENUTACIDONITRICO.AddCDC_TENUTACIDONITRICORow(tenutaRow);
                    }
                    else
                    {
                        tenutaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        tenutaRow.DATAINSERIMENTO = DateTime.Now;
                        tenutaRow.DATATEST = (DateTime)riga[9];
                        tenutaRow.NUMEROCAMPIONI = (decimal)riga[10];
                        tenutaRow.BOLLA = stringa;
                        if (riga[12] != DBNull.Value)
                            tenutaRow.DATADDT = (DateTime)riga[12];
                        tenutaRow.ESITO = ConvertiBoolInStringa(riga[13]);
                    }

                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiTenutaAcidoNitrico(_DS);
                _DS.CDC_TENUTACIDONITRICO.AcceptChanges();

                Bitmap firma = Properties.Resources.logo_spessori_v2;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                fileCreati = bll.CreaPDFTenutaAcidoNitrico(idPerPDF, _DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand));
                btnLeggiDati_Click(null, null);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sono stati creati i seguenti file:");
            sb.Append(fileCreati);

            MessageBox.Show(sb.ToString(), "FILE PDF CREATI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
