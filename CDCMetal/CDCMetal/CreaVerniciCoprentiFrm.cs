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
    public partial class CreaVerniciCoprentiFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "VERNICICOPRENTI";

        public CreaVerniciCoprentiFrm()
        {
            InitializeComponent();
        }

        private void CreaDsPerVerniciCoprenti()
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
            dtCartelle.Columns.Add("FORNITORE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("TURBULA", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("QUADRETTATURA", Type.GetType("System.Boolean"));


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in Contesto.DS.CDC_DETTAGLIO)
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

                CDCDS.CDC_VERNICICOPRENTIRow vCoprente = Contesto.DS.CDC_VERNICICOPRENTI.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO).FirstOrDefault();
                if (vCoprente != null)
                {
                    riga[9] = vCoprente.DATATEST;
                    riga[10] = vCoprente.NUMEROCAMPIONI;
                    riga[11] = vCoprente.FORNITORE;
                    riga[12] = vCoprente.TURBULA == "S" ? true : false;
                    riga[13] = vCoprente.QUADRETTATURA == "S" ? true : false;
                }
                else
                {
                    riga[9] = DateTime.Today.AddDays(-2);
                    riga[10] = 2;
                    riga[11] = "Top Finish";
                    riga[12] = true;
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

            Contesto.DS = new Entities.CDCDS();

            bll.LeggiCollaudoDaData(Contesto.DS, dataSelezionata);


            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = Contesto.DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_VERNICICOPRENTI(Contesto.DS, IDDETTAGLIO);
                bll.CDC_PDF(Contesto.DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerVerniciCoprenti();

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

        private void CreaVerniciCoprentiFrm_Load(object sender, EventArgs e)
        {
            PopolaDDLDate();
        }

        private void PopolaDDLDate()
        {
            ddlDataCollaudo.Items.AddRange(CaricaDateCollaudo().ToArray());
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
                    string fornitore = ConvertiInStringa(riga[11]);
                    fornitore = fornitore.Length > 25 ? fornitore.Substring(0, 25) : fornitore;

                    CDCDS.CDC_VERNICICOPRENTIRow vCoprenteRow = Contesto.DS.CDC_VERNICICOPRENTI.Where(x => x.IDDETTAGLIO == iddettaglio).FirstOrDefault();
                    if (vCoprenteRow == null)
                    {
                        vCoprenteRow = Contesto.DS.CDC_VERNICICOPRENTI.NewCDC_VERNICICOPRENTIRow();
                        vCoprenteRow.IDDETTAGLIO = iddettaglio;
                        vCoprenteRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        vCoprenteRow.DATAINSERIMENTO = DateTime.Now;
                        vCoprenteRow.DATATEST = (DateTime)riga[9];
                        vCoprenteRow.NUMEROCAMPIONI = (Decimal)riga[10];
                        vCoprenteRow.FORNITORE = fornitore;
                        vCoprenteRow.TURBULA = ConvertiBoolInStringa(riga[12]);
                        vCoprenteRow.QUADRETTATURA = ConvertiBoolInStringa(riga[13]);

                        Contesto.DS.CDC_VERNICICOPRENTI.AddCDC_VERNICICOPRENTIRow(vCoprenteRow);
                    }
                    else
                    {
                        vCoprenteRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        vCoprenteRow.DATAINSERIMENTO = DateTime.Now;
                        vCoprenteRow.DATATEST = (DateTime)riga[9];
                        vCoprenteRow.NUMEROCAMPIONI = (decimal)riga[10];
                        vCoprenteRow.FORNITORE = fornitore;
                        vCoprenteRow.TURBULA = ConvertiBoolInStringa(riga[12]);
                        vCoprenteRow.QUADRETTATURA = ConvertiBoolInStringa(riga[13]);
                    }

                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiVerniciaturaCoprente(Contesto.DS);
                Contesto.DS.CDC_VERNICICOPRENTI.AcceptChanges();

                Bitmap firma = Properties.Resources.logo_spessori_v2;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                fileCreati = bll.CreaPDFVerniceCoprente(idPerPDF, Contesto.DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand));
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
