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
    public partial class CreaSpessoreFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "SPESSORE";
        private string tblAggregati = "AGGREGATI";
        private Random _random = new Random(DateTime.Now.Millisecond);

        private enum COLONNEAGGREGATI { Media = 0, StdDev, Pct, Range, Minimo, Massimo };

        private CDCDS.CDC_DETTAGLIORow _dettaglio;
        public CreaSpessoreFrm()
        {
            InitializeComponent();
        }

        private void CreaSpessoreFrm_Load(object sender, EventArgs e)
        {
            CaricaDateCollaudo();
            CaricaTabelleSpessori();
        }

        private void CaricaDateCollaudo()
        {
            CDCBLL bll = new CDCBLL();
            List<DateTime> date = bll.LeggiDateCollaudo();
            foreach (DateTime dt in date)
                ddlDataCollaudo.Items.Add(dt);

        }

        private void CaricaTabelleSpessori()
        {
            CDCBLL bll = new CDCBLL();
            bll.LeggiTabelleSpessori(Contesto.DS);

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

            DateTime dataSelezionata = (DateTime)ddlDataCollaudo.SelectedItem;

            CDCBLL bll = new CDCBLL();

            Contesto.DS.CDC_EXCEL.Clear();
            Contesto.DS.CDC_DETTAGLIO.Clear();
            _dettaglio = null;

            bll.LeggiCollaudoDaData(Contesto.DS, dataSelezionata);


            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = Contesto.DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_GALVANICA(Contesto.DS, IDDETTAGLIO);
                bll.CDC_PDF(Contesto.DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }


            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = Contesto.DS;
            dgvDettaglio.DataMember = Contesto.DS.CDC_DETTAGLIO.TableName;

            dgvDettaglio.Columns[0].Visible = false;
            dgvDettaglio.Columns[2].Visible = false;
            dgvDettaglio.Columns[3].Visible = false;
            dgvDettaglio.Columns[9].Visible = false;
            dgvDettaglio.Columns[10].Visible = false;
            dgvDettaglio.Columns[11].Width = 130;
            dgvDettaglio.Columns[12].Visible = false;
            dgvDettaglio.Columns[13].Visible = false;
            dgvDettaglio.Columns[15].Visible = false;
            dgvDettaglio.Columns[16].Visible = false;
            dgvDettaglio.Columns[17].Visible = false;
            dgvDettaglio.Columns[18].Visible = false;
            dgvDettaglio.Columns[19].Visible = false;
            dgvDettaglio.Columns[20].Visible = false;
            dgvDettaglio.Columns[22].Visible = false;
            dgvDettaglio.Columns[22].Visible = false;
            dgvDettaglio.Columns[23].Visible = false;
            dgvDettaglio.Columns[24].Visible = false;
            dgvDettaglio.Columns[25].Visible = false;
            dgvDettaglio.Columns[26].Visible = false;
            dgvDettaglio.Columns[27].Visible = false;
            dgvDettaglio.Columns[28].Visible = false;

            foreach (DataGridViewColumn column in dgvDettaglio.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ImpostaApplicazione(string colore)
        {
            ddlCodiceExcel.Items.Clear();
            txtApplicazione.Text = string.Empty;
            List<CDCDS.CDC_APPLICAZIONERow> applicazioni = Contesto.DS.CDC_APPLICAZIONE.Where(x => x.COLORE == colore).ToList();
            if (applicazioni.Count > 0)
            {
                foreach (CDCDS.CDC_APPLICAZIONERow applicazione in applicazioni)
                {
                    ddlItems ddl = new ddlItems()
                    {
                        Applicazione = applicazione.APPLICAZIONE,
                        Codice = applicazione.CODICEEXCEL
                    };
                    ddlCodiceExcel.Items.Add(ddl);
                }
            }

            if (ddlCodiceExcel.Items.Count > 0)
            {
                ddlCodiceExcel.SelectedIndex = 0;
                txtApplicazione.Text = (ddlCodiceExcel.Items[ddlCodiceExcel.SelectedIndex] as ddlItems).Applicazione;

            }
        }

        private void ImpostaSpessore(string parte, string colore)
        {
            CDCDS.CDC_SPESSORERow spessore = Contesto.DS.CDC_SPESSORE.Where(x => x.COLORE == colore && x.PARTE == parte).FirstOrDefault();
            if (spessore != null)
            {
                decimal sp = Decimal.Parse(spessore.SPESSORE);
                nSpessore.Value = sp;
            }
        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblMessaggio.Text = string.Empty;

            if (e.RowIndex == -1) return;
            DataRow r = Contesto.DS.CDC_DETTAGLIO.Rows[e.RowIndex];
            decimal IDDETTAGLIO = (decimal)r[0];
            _dettaglio = Contesto.DS.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();

            ImpostaApplicazione(_dettaglio.COLORE);
            ImpostaSpessore(_dettaglio.PARTE, _dettaglio.COLORE);

            CalcolaNumeroCampioni();

            CDCDS.CDC_GALVANICARow galvanica = Contesto.DS.CDC_GALVANICA.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
            if (galvanica != null)
            {
                decimal aux = Decimal.Parse(galvanica.SPESSORE);
                nSpessore.Value = aux;

                txtApplicazione.Text = galvanica.APPLICAZIONE;
                nMisurePerCampione.Value = galvanica.MISURECAMPIONE;
                CaricaCampioniMisuraPrecedente(galvanica.IDGALVANICA);
            }
        }

        private void CaricaCampioniMisuraPrecedente(decimal IDGALVANICA)
        {

            List<CDCDS.CDC_MISURERow> misurePrecedenti = Contesto.DS.CDC_MISURE.Where(x => x.IDGALVANICA == IDGALVANICA).OrderBy(x => x.NMISURA).ThenBy(x => x.NCOLONNA).ToList();

            int numeroCampioni = (int)misurePrecedenti.Max(x => x.NMISURA) + 1;
            txtNumeroCampioni.Text = numeroCampioni.ToString();
            int numeroColonne = misurePrecedenti.Where(x => x.NMISURA == 1).Count();

            _dsServizio = new DataSet();

            DataTable dtDimensioni = _dsServizio.Tables.Add();
            dtDimensioni.TableName = tableName;
            dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("MISURA", Type.GetType("System.Int32"));

            DataTable dtAggregati = _dsServizio.Tables.Add();
            dtAggregati.TableName = tblAggregati;
            dtAggregati.Columns.Add("DATO", Type.GetType("System.String"));

            foreach (CDCDS.CDC_MISURERow mColore in misurePrecedenti.Where(x => x.NMISURA == 1))
            {
                dtDimensioni.Columns.Add(mColore.TIPOMISURA, Type.GetType("System.Decimal"));
                dtAggregati.Columns.Add(mColore.TIPOMISURA, Type.GetType("System.String"));
            }

            for (int i = 0; i < numeroCampioni; i++)
            {
                DataRow riga = dtDimensioni.NewRow();
                riga[0] = _dettaglio.IDPRENOTAZIONE;
                riga[1] = _dettaglio.IDDETTAGLIO;
                riga[2] = i;

                for (int j = 0; j < numeroColonne; j++)
                {
                    CDCDS.CDC_MISURERow misura = misurePrecedenti.Where(x => x.NMISURA == i && x.NCOLONNA == j).FirstOrDefault();
                    if (misura != null)
                    {
                        riga[j + 3] = Decimal.Parse(misura.VALORE);
                    }
                }

                dtDimensioni.Rows.Add(riga);
            }

            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tableName;

            dgvMisure.Columns[0].Visible = false;
            dgvMisure.Columns[1].Visible = false;
            dgvMisure.Columns[2].ReadOnly = true;

            CalcolaValoriAggregati();
        }

        private void btnCreaCampioni_Click(object sender, EventArgs e)
        {
            lblMessaggio.Text = string.Empty;
            if (_dettaglio == null)
            {
                lblMessaggio.Text = "Selezionare una riga dalla tabella Schede Collaudo";
                return;
            }

            if (nMisurePerCampione.Value == 0)
            {
                lblMessaggio.Text = "Indicare il numero di misure per campione";
                return;
            }

            if (ddlCodiceExcel.SelectedIndex == -1)
            {
                lblMessaggio.Text = "Selezionare un Codice Excel";
                return;
            }

            int numeroCampioni = int.Parse(txtNumeroCampioni.Text);

            string codiceExcel = (ddlCodiceExcel.Items[ddlCodiceExcel.SelectedIndex] as ddlItems).Codice;

            List<CDCDS.CDC_MISURA_COLORERow> misuraColore = Contesto.DS.CDC_MISURA_COLORE.Where(x => x.CODICEEXCEL == codiceExcel).ToList();

            _dsServizio = new DataSet();

            DataTable dtDimensioni = _dsServizio.Tables.Add();
            dtDimensioni.TableName = tableName;
            dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("MISURA", Type.GetType("System.Int32"));

            DataTable dtAggregati = _dsServizio.Tables.Add();
            dtAggregati.TableName = tblAggregati;
            dtAggregati.Columns.Add("DATO", Type.GetType("System.String"));

            foreach (CDCDS.CDC_MISURA_COLORERow mColore in misuraColore)
            {
                dtDimensioni.Columns.Add(mColore.TIPOMISURA, Type.GetType("System.Decimal"));
                dtAggregati.Columns.Add(mColore.TIPOMISURA, Type.GetType("System.String"));
            }

            for (int i = 0; i < numeroCampioni; i++)
            {
                DataRow riga = dtDimensioni.NewRow();
                riga[0] = _dettaglio.IDPRENOTAZIONE;
                riga[1] = _dettaglio.IDDETTAGLIO;
                riga[2] = i;

                int j = 0;
                foreach (CDCDS.CDC_MISURA_COLORERow mColore in misuraColore)
                {
                    int massimo = (int)mColore.MASSIMO;
                    int minimo = (int)mColore.MINIMO;
                    decimal denominatore = mColore.DENOMINATORE;

                    if (denominatore == 0) denominatore = 1;

                    int numero = _random.Next(minimo, massimo);
                    decimal valore = (decimal)numero;
                    valore = valore / denominatore;
                    riga[3 + j] = valore;
                    j++;
                }
                dtDimensioni.Rows.Add(riga);
            }

            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tableName;

            dgvMisure.Columns[0].Visible = false;
            dgvMisure.Columns[1].Visible = false;
            dgvMisure.Columns[2].ReadOnly = true;

            CalcolaValoriAggregati();
        }

        private void nMisurePerCampione_ValueChanged(object sender, EventArgs e)
        {
            CalcolaNumeroCampioni();
        }

        private void CalcolaNumeroCampioni()
        {
            int numeroCampioni = 0;
            if (_dettaglio == null) return;

            if (_dettaglio.QUANTITA < 25)
            {
                lblMessaggio.Text = "Quantita inferiore a 26. Nessuna misura richiesta";
                return;
            }

            if (_dettaglio.QUANTITA >= 25 && _dettaglio.QUANTITA <= 150)
                numeroCampioni = 3 * (int)nMisurePerCampione.Value;

            if (_dettaglio.QUANTITA >= 151 && _dettaglio.QUANTITA <= 1200)
                numeroCampioni = 5 * (int)nMisurePerCampione.Value;

            if (_dettaglio.QUANTITA >= 1201 && _dettaglio.QUANTITA <= 10000)
                numeroCampioni = 8 * (int)nMisurePerCampione.Value;

            if (_dettaglio.QUANTITA > 10001)
            {
                lblMessaggio.Text = "Quantita superiore a 10001. Nessuna misura impostata";
                return;
            }

            txtNumeroCampioni.Text = numeroCampioni.ToString();
        }

        private void CalcolaValoriAggregati()
        {
            DataTable tbl = _dsServizio.Tables[tblAggregati];
            DataTable tblCampioni = _dsServizio.Tables[tableName];

            tbl.Clear();

            int numeroColonne = tblCampioni.Columns.Count - 3;
            int numeroCampioni = Int32.Parse(txtNumeroCampioni.Text);

            decimal[] massimi = new decimal[numeroColonne];
            decimal[] minimi = new decimal[numeroColonne];

            DataRow riga = tbl.NewRow();
            riga[0] = "Media";
            for (int i = 0; i < numeroColonne; i++)
            {
                List<decimal> array = GetList(tblCampioni, i + 3);
                decimal media = array.Average();
                riga[i + 1] = Math.Round(media, 2).ToString();
            }
            tbl.Rows.Add(riga);


            riga = tbl.NewRow();
            riga[0] = "Std Dev";
            for (int i = 0; i < numeroColonne; i++)
            {
                List<decimal> array = GetList(tblCampioni, i + 3);
                double media = (double)array.Average();

                double stdDev = Math.Sqrt(array.Sum(x => Math.Pow((double)x - media, 2)) / array.Count);

                riga[i + 1] = Math.Round(stdDev, 3).ToString();
            }
            tbl.Rows.Add(riga);

            riga = tbl.NewRow();
            riga[0] = "Pct(%) Dev";
            for (int i = 0; i < numeroColonne; i++)
            {
                double pct = 0;
                double media = (double)Double.Parse((string)tbl.Rows[0][i + 1]);
                double stddev = (double)Double.Parse((string)tbl.Rows[1][i + 1]);
                if (media != 0) pct = stddev / media;
                riga[i + 1] = Math.Round(pct * 100, 1).ToString();

            }
            tbl.Rows.Add(riga);

            riga = tbl.NewRow();
            riga[0] = "Massimo";
            for (int i = 0; i < numeroColonne; i++)
            {
                decimal massimo = GetMassimo(tblCampioni, i + 3);
                riga[i + 1] = massimo.ToString();
                massimi[i] = massimo;
            }
            tbl.Rows.Add(riga);

            riga = tbl.NewRow();
            riga[0] = "Minimo";
            for (int i = 0; i < numeroColonne; i++)
            {
                decimal minimo = GetMinimo(tblCampioni, i + 3);
                riga[i + 1] = minimo.ToString();
                minimi[i] = minimo;
            }
            tbl.Rows.Add(riga);

            riga = tbl.NewRow();
            riga[0] = "Range";
            for (int i = 0; i < numeroColonne; i++)
            {
                decimal minimo = minimi[i];
                decimal massimo = massimi[i];
                riga[i + 1] = (massimo - minimo).ToString();
            }
            tbl.Rows.Add(riga);

            dgvAggregati.AutoGenerateColumns = true;
            dgvAggregati.DataSource = _dsServizio;
            dgvAggregati.DataMember = tblAggregati;
        }

        private List<decimal> GetList(DataTable dt, int column)
        {
            decimal[] array = new decimal[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
                array[i] = (decimal)dt.Rows[i][column];
            return array.ToList();
        }


        private decimal GetMassimo(DataTable dt, int column)
        {
            decimal massimo = (decimal)dt.Rows[0][column];

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                decimal valore = (decimal)dt.Rows[i][column];
                if (valore > massimo) massimo = valore;
            }
            return massimo;
        }

        private decimal GetMinimo(DataTable dt, int column)
        {
            decimal minimo = (decimal)dt.Rows[0][column];

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                decimal valore = (decimal)dt.Rows[i][column];
                if (valore < minimo) minimo = valore;
            }
            return minimo;
        }

        private double GetVariance(decimal[] nums)
        {
            double media = 0;
            for (int j = 0; j < nums.Length; j++)
            {
                double aux = (double)nums[j];
                media = media + aux;
            }
            media = media / nums.Length;

            if (nums.Length > 1)
            {

                double sumOfSquares = 0.0;
                foreach (int num in nums)
                {
                    sumOfSquares += Math.Pow((num - media), 2.0);
                }
                return sumOfSquares / (double)(nums.Length - 1);
            }
            else { return 0.0; }
        }

        private double GetStandardDeviation(double variance)
        {
            return Math.Sqrt(variance);
        }

        private void btnCreaPDF_Click(object sender, EventArgs e)
        {
            lblMessaggio.Text = string.Empty;
            if (nSpessore.Value == 0)
            {
                lblMessaggio.Text = "Impostare lo spessore richiesto";
                return;
            }

            if (_dsServizio.Tables[tblAggregati].Rows.Count == 0)
            {
                lblMessaggio.Text = "Creare i campioni per la misura";
                return;
            }

            if (_dettaglio == null)
            {
                lblMessaggio.Text = "Selezionare un collaudo";
                return;
            }

            if (string.IsNullOrEmpty(txtApplicazione.Text))
            {
                lblMessaggio.Text = "Il campo applicazione è vuoto impossibile procedere";
                return;
            }

            CDCDS.CDC_SPESSORERow spessore = Contesto.DS.CDC_SPESSORE.Where(x => x.COLORE == _dettaglio.COLORE).FirstOrDefault();
            if (spessore == null)
            {
                spessore = Contesto.DS.CDC_SPESSORE.NewCDC_SPESSORERow();
                spessore.COLORE = _dettaglio.COLORE;
                spessore.SPESSORE = nSpessore.Value.ToString();
                Contesto.DS.CDC_SPESSORE.AddCDC_SPESSORERow(spessore);
            }
            else
            {
                spessore.SPESSORE = nSpessore.Value.ToString();
            }

            decimal IDDETTAGLIO = _dettaglio.IDDETTAGLIO;

            CDCBLL bll = new CDCBLL();
            int misurePerCampione = (int)nMisurePerCampione.Value;
            decimal IDGALVANICA = bll.InserisciCDCGalvanica(Contesto.DS, nSpessore.Value, IDDETTAGLIO, txtApplicazione.Text, Contesto.StrumentoSpessore, misurePerCampione, Contesto.Utente.FULLNAMEUSER);

            foreach (CDCDS.CDC_MISURERow row in Contesto.DS.CDC_MISURE.Where(x => x.IDGALVANICA == IDGALVANICA))
                row.Delete();

            foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
            {

                for (int j = 0; j < _dsServizio.Tables[tableName].Columns.Count - 3; j++)
                {
                    CDCDS.CDC_MISURERow misura = Contesto.DS.CDC_MISURE.NewCDC_MISURERow();
                    int nMisura = (int)riga[2];
                    misura.NMISURA = nMisura;
                    misura.DATAMISURA = DateTime.Today;
                    misura.IDGALVANICA = IDGALVANICA;
                    string tipo = _dsServizio.Tables[tableName].Columns[j + 3].ColumnName;
                    string valore = (riga[3 + j]).ToString();
                    misura.NCOLONNA = j;
                    misura.TIPOMISURA = tipo;
                    misura.VALORE = valore;
                    Contesto.DS.CDC_MISURE.AddCDC_MISURERow(misura);
                }
            }

            bll.SalvaCDCSpessori(Contesto.DS);
        }

        private void dgvMisure_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            CalcolaValoriAggregati();
        }
    }

    public class ddlItems
    {
        public string Applicazione;
        public string Codice;

        public override string ToString()
        {
            return Codice;
        }
    }
}
