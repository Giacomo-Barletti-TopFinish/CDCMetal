using CDCMetal.BLL;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private string tblMisure = "MISURE";
        private string tblAggregati = "AGGREGATI";
        private string tblSpessori = "SPESSORI";

        private Random _random = new Random(DateTime.Now.Millisecond);

        private enum COLONNEAGGREGATI { Media = 0, StdDev, Pct, Range, Minimo, Massimo };

        private CDCDS.CDC_DETTAGLIORow _dettaglio;
        public CreaSpessoreFrm()
        {
            InitializeComponent();
        }

        private void CreaSpessoreFrm_Load(object sender, EventArgs e)
        {
            PopolaDDLDate();
            CaricaTabelleSpessori();
        }

        private void PopolaDDLDate()
        {
            ddlDataCollaudo.Items.AddRange(CaricaDateCollaudo().ToArray());
        }

        private void CaricaTabelleSpessori()
        {
            CDCBLL bll = new CDCBLL();
            bll.LeggiTabelleSpessori(_DS);

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
            try
            {
                DataCollaudo dataSelezionata = (DataCollaudo)ddlDataCollaudo.SelectedItem;

                CDCBLL bll = new CDCBLL();

                _DS.CDC_EXCEL.Clear();
                _DS.CDC_DETTAGLIO.Clear();
                _dettaglio = null;

                bll.LeggiCollaudoDaData(_DS, dataSelezionata);


                if (_DS.CDC_DETTAGLIO.Count > 0)
                {
                    btnCreaPDF.Enabled = true;
                    List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                    bll.FillCDC_GALVANICA(_DS, IDDETTAGLIO);
                    bll.CDC_PDF(_DS, IDDETTAGLIO);
                }
                else
                {
                    lblMessaggio.Text = "Nessuna riga trovata per questa data";
                }


                dgvDettaglio.AutoGenerateColumns = true;
                dgvDettaglio.DataSource = _DS;
                dgvDettaglio.DataMember = _DS.CDC_DETTAGLIO.TableName;

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

                evidenziaPDFFatti();
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in leggi dati");
            }

        }

        private void evidenziaPDFFatti()
        {
            List<decimal> iddettaglioConPdf = _DS.CDC_PDF.Where(x => x.TIPO == CDCTipoPDF.CERTIFICATOSPESSORE).Select(x => x.IDDETTAGLIO).Distinct().ToList();
            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                decimal IDDETTAGLIO = (decimal)riga.Cells["IDDETTAGLIO"].Value;
                if (iddettaglioConPdf.Contains(IDDETTAGLIO))
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                }
            }

        }

        private void ImpostaApplicazione(string colore, string parte)
        {
            CDCDS.CDC_APPLICAZIONERow applicazione = _DS.CDC_APPLICAZIONE.Where(x => x.COLORE == colore && x.PARTE == parte).FirstOrDefault();
            if (applicazione != null)
            {
                txtApplicazione.Text = applicazione.IsAPPLICAZIONENull() ? string.Empty : applicazione.APPLICAZIONE;
                nMisurePerCampione.Value = applicazione.NUMEROCAMPIONI;
                txtSpessoreRichiesto.Text = applicazione.IsSPESSORENull() ? string.Empty : applicazione.SPESSORE;
            }
            else
            {
                txtApplicazione.Text = string.Empty;
                nMisurePerCampione.Value = 0;
            }
        }

        private void ImpostaSpessore(string parte, string colore)
        {
            DataTable dtSpessori;
            if (_dsServizio.Tables[tblSpessori] == null)
            {
                dtSpessori = _dsServizio.Tables.Add();
                dtSpessori.TableName = tblSpessori;
                dtSpessori.Columns.Add("Materiale", Type.GetType("System.String"));
                dtSpessori.Columns.Add("Massimo", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("Minimo", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("Denominatore", Type.GetType("System.Decimal"));
            }
            else
            {
                dtSpessori = _dsServizio.Tables[tblSpessori];
                dtSpessori.Clear();
            }

            foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.COLORE == colore && x.PARTE == parte).OrderBy(x => x.SEQUENZA))
            {
                DataRow riga = dtSpessori.NewRow();
                riga[0] = spessore.ETICHETTA;
                riga[1] = spessore.MASSIMO;
                riga[2] = spessore.MINIMO;
                riga[3] = spessore.DENOMINATORE;

                dtSpessori.Rows.Add(riga);
            }

            dgvSpessore.AutoGenerateColumns = true;
            dgvSpessore.DataSource = _dsServizio;
            dgvSpessore.DataMember = tblSpessori;

            ((DataGridViewTextBoxColumn)dgvSpessore.Columns[0]).MaxInputLength = 2;

            foreach (DataGridViewTextBoxColumn dgc in dgvSpessore.Columns)
                dgc.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblMessaggio.Text = string.Empty;

                if (e.RowIndex == -1) return;
                DataRow r = _DS.CDC_DETTAGLIO.Rows[e.RowIndex];
                decimal IDDETTAGLIO = (decimal)r[0];
                _dettaglio = _DS.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();

                ImpostaApplicazione(_dettaglio.COLORE, _dettaglio.PARTE);
                _dsServizio = new DataSet();

                CalcolaNumeroCampioni();

                CDCDS.CDC_GALVANICARow galvanica = _DS.CDC_GALVANICA.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (galvanica != null)
                {
                    CaricaCampioniMisuraPrecedente(galvanica);
                }
                else
                {
                    if (_dsServizio.Tables[tblMisure] != null)
                    {
                        _dsServizio.Tables.Remove(tblMisure);
                    }

                    DataTable dtDimensioni = _dsServizio.Tables.Add();
                    dtDimensioni.TableName = tblMisure;
                    dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
                    dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
                    dtDimensioni.Columns.Add("MISURA", Type.GetType("System.Int32"));

                    if (_dsServizio.Tables[tblAggregati] != null)
                    {
                        _dsServizio.Tables.Remove(tblAggregati);
                    }
                    DataTable dtAggregati = _dsServizio.Tables.Add();
                    dtAggregati.TableName = tblAggregati;
                    dtAggregati.Columns.Add("DATO", Type.GetType("System.String"));

                    mostradgvAggregati();
                    mostradgvMisure();

                }
                ImpostaSpessore(_dettaglio.PARTE, _dettaglio.COLORE);


            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in dgvDettaglio_CellClick");
            }
        }

        private void CaricaCampioniMisuraPrecedente(CDCDS.CDC_GALVANICARow galvanica)
        {

            List<CDCDS.CDC_MISURERow> misurePrecedenti = _DS.CDC_MISURE.Where(x => x.IDGALVANICA == galvanica.IDGALVANICA).OrderBy(x => x.NMISURA).ThenBy(x => x.NCOLONNA).ToList();

            int numeroCampioni = (int)misurePrecedenti.Max(x => x.NMISURA) + 1;
            txtNumeroCampioni.Text = numeroCampioni.ToString();
            if (((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand == CDCBrands.YSL)
            {
                if(galvanica.MISURECAMPIONE>0)
                {
                    int aux = numeroCampioni / (int)galvanica.MISURECAMPIONE;
                    txtNumeroCampioni.Text = aux.ToString();
                }
            }
            int numeroColonne = misurePrecedenti.Where(x => x.NMISURA == 1).Count();

            if (_dsServizio.Tables[tblMisure] != null)
            {
                _dsServizio.Tables.Remove(tblMisure);
            }

            DataTable dtDimensioni = _dsServizio.Tables.Add();
            dtDimensioni.TableName = tblMisure;
            dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("MISURA", Type.GetType("System.Int32"));

            if (_dsServizio.Tables[tblAggregati] != null)
            {
                _dsServizio.Tables.Remove(tblAggregati);
            }
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

            mostradgvMisure();

            CalcolaValoriAggregati();
        }

        private void mostradgvMisure()
        {
            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tblMisure;

            if (dgvMisure.Columns.Count > 3)
            {
                dgvMisure.Columns[0].Visible = false;
                dgvMisure.Columns[1].Visible = false;
                dgvMisure.Columns[2].ReadOnly = true;
            }
            dgvMisure.Update();
        }

        private bool VerificaSpessori(out string messaggio)
        {
            messaggio = string.Empty;

            if (dgvSpessore.Rows.Count == 0)
            {
                messaggio = "Non ci sono valori nella tabella degli spessori";
                return false;
            }

            if (dgvSpessore.Rows.Count == 1 && dgvSpessore.Rows[0].IsNewRow)
            {
                messaggio = "Non ci sono valori nella tabella degli spessori";
                return false;
            }

            foreach (DataGridViewRow dr in dgvSpessore.Rows)
            {
                if (dr.IsNewRow) continue;
                for (int col = 0; col < dgvSpessore.Columns.Count; col++)
                {
                    if (string.IsNullOrEmpty(dr.Cells[col].Value.ToString()))
                    {
                        messaggio = "Ci sono celle vuote nella tabella spessori";
                        return false;
                    }
                }

                decimal Massimo;
                if (!decimal.TryParse(dr.Cells[1].Value.ToString(), out Massimo))
                {
                    messaggio = "Nella tabella spessori la colonna massimo deve contenere solo numeri";
                    return false;
                }
                decimal Minimo;
                if (!decimal.TryParse(dr.Cells[2].Value.ToString(), out Minimo))
                {
                    messaggio = "Nella tabella spessori la colonna minimo deve contenere solo numeri";
                    return false;
                }
                decimal Denominatore;
                if (!decimal.TryParse(dr.Cells[3].Value.ToString(), out Denominatore))
                {
                    messaggio = "Nella tabella spessori la colonna denominatore deve contenere solo numeri";
                    return false;
                }

                if (Massimo <= Minimo)
                {
                    messaggio = "Nella tabella spessori il valore massimo deve essere superiore al minimo";
                    return false;
                }

                if (Denominatore == 0)
                {
                    messaggio = "Nella tabella spessori la colonna denominatore non può essere zero";
                    return false;
                }
            }
            return true;
        }

        private void popolaCDC_SPESSORE()
        {
            int sequenza = 0;
            List<CDCDS.CDC_SPESSORERow> elementi = _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).ToList();
            foreach (CDCDS.CDC_SPESSORERow elemento in elementi)
                elemento.Delete();

            foreach (DataGridViewRow dr in dgvSpessore.Rows)
            {
                if (dr.IsNewRow) continue;
                string etichetta = dr.Cells[0].Value.ToString();
                decimal Massimo = (decimal)dr.Cells[1].Value;
                decimal Minimo = (decimal)dr.Cells[2].Value;
                decimal Denominatore = (decimal)dr.Cells[3].Value;
                CDCDS.CDC_SPESSORERow spessore = _DS.CDC_SPESSORE.NewCDC_SPESSORERow();
                spessore.COLORE = _dettaglio.COLORE;
                spessore.DENOMINATORE = Denominatore;
                spessore.ETICHETTA = etichetta;
                spessore.MASSIMO = Massimo;
                spessore.MINIMO = Minimo;
                spessore.PARTE = _dettaglio.PARTE;
                spessore.SEQUENZA = sequenza;
                _DS.CDC_SPESSORE.AddCDC_SPESSORERow(spessore);

                sequenza++;
            }
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

            string messaggio;
            if (!VerificaSpessori(out messaggio))
            {
                lblMessaggio.Text = messaggio;
                return;
            }

            int numeroCampioni = int.Parse(txtNumeroCampioni.Text); // caso GUCCI
            if (((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand == CDCBrands.YSL)
            {
                int numeroCampioniPerPezzo = int.Parse(txtNumeroCampioni.Text);
                numeroCampioni = numeroCampioniPerPezzo * (int)nMisurePerCampione.Value;
            }

            try
            {
                popolaCDC_SPESSORE();

                _dsServizio = new DataSet();

                DataTable dtDimensioni = _dsServizio.Tables.Add();
                dtDimensioni.TableName = tblMisure;
                dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
                dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
                dtDimensioni.Columns.Add("MISURA", Type.GetType("System.Int32"));

                DataTable dtAggregati = _dsServizio.Tables.Add();
                dtAggregati.TableName = tblAggregati;
                dtAggregati.Columns.Add("DATO", Type.GetType("System.String"));

                foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).OrderBy(x => x.SEQUENZA))
                {
                    dtDimensioni.Columns.Add(spessore.ETICHETTA, Type.GetType("System.Decimal"));
                    dtAggregati.Columns.Add(spessore.ETICHETTA, Type.GetType("System.String"));
                }

                for (int i = 0; i < numeroCampioni; i++)
                {
                    DataRow riga = dtDimensioni.NewRow();
                    riga[0] = _dettaglio.IDPRENOTAZIONE;
                    riga[1] = _dettaglio.IDDETTAGLIO;
                    riga[2] = i;

                    int j = 0;
                    foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).OrderBy(x => x.SEQUENZA))
                    {
                        int massimo = (int)spessore.MASSIMO;
                        int minimo = (int)spessore.MINIMO;
                        decimal denominatore = spessore.DENOMINATORE;

                        if (denominatore == 0) denominatore = 1;

                        int numero = _random.Next(minimo, massimo);
                        decimal valore = (decimal)numero;
                        valore = valore / denominatore;
                        riga[3 + j] = valore;
                        j++;
                    }
                    dtDimensioni.Rows.Add(riga);
                }

                mostradgvMisure();

                CalcolaValoriAggregati();
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in Crea Campioni Click");
            }

        }

        private void nMisurePerCampione_ValueChanged(object sender, EventArgs e)
        {
            CalcolaNumeroCampioni();
        }

        private void CalcolaNumeroCampioni()
        {
            if (ddlDataCollaudo.SelectedIndex == -1) return;

            DataCollaudo dataCollaudo = (DataCollaudo)ddlDataCollaudo.SelectedItem;

            switch (dataCollaudo.Brand)
            {
                case CDCBrands.Gucci:
                    int numeroCampioni = 0;
                    if (_dettaglio == null) return;

                    if (_dettaglio.QUANTITA < 25)
                        numeroCampioni = 3 * (int)nMisurePerCampione.Value;

                    if (_dettaglio.QUANTITA >= 25 && _dettaglio.QUANTITA <= 150)
                        numeroCampioni = 3 * (int)nMisurePerCampione.Value;

                    if (_dettaglio.QUANTITA >= 151 && _dettaglio.QUANTITA <= 1200)
                        numeroCampioni = 5 * (int)nMisurePerCampione.Value;

                    if (_dettaglio.QUANTITA >= 1201 && _dettaglio.QUANTITA <= 10000)
                        numeroCampioni = 8 * (int)nMisurePerCampione.Value;

                    if (_dettaglio.QUANTITA > 10001)
                        numeroCampioni = 8 * (int)nMisurePerCampione.Value;

                    txtNumeroCampioni.Text = numeroCampioni.ToString();
                    txtNumeroCampioni.ReadOnly = true;
                    break;

                case CDCBrands.YSL:
                    txtNumeroCampioni.ReadOnly = false;
                    txtNumeroCampioni.Text = "1";
                    break;

            }

        }

        private void CalcolaValoriAggregati()
        {
            DataTable tbl = _dsServizio.Tables[tblAggregati];
            DataTable tblCampioni = _dsServizio.Tables[tblMisure];

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

            mostradgvAggregati();
        }

        private List<decimal> GetList(DataTable dt, int column)
        {
            decimal[] array = new decimal[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
                array[i] = (decimal)dt.Rows[i][column];
            return array.ToList();
        }

        private void mostradgvAggregati()
        {
            dgvAggregati.AutoGenerateColumns = true;
            dgvAggregati.DataSource = _dsServizio;
            dgvAggregati.DataMember = tblAggregati;

            dgvAggregati.Refresh();
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
            string filename = string.Empty;

            string brand = ((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblMessaggio.Text = string.Empty;

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

                if (string.IsNullOrEmpty(txtApplicazione.Text) && CDCBrands.Gucci == brand)
                {
                    lblMessaggio.Text = "Il campo applicazione è vuoto impossibile procedere";
                    return;
                }

                if (string.IsNullOrEmpty(txtSpessoreRichiesto.Text) && CDCBrands.Gucci == brand)
                {
                    lblMessaggio.Text = "Il campo spessore richiesto è vuoto impossibile procedere";
                    return;
                }
                decimal IDDETTAGLIO = _dettaglio.IDDETTAGLIO;

                CDCDS.CDC_SPESSORERow spessore = _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE && x.SEQUENZA == 0).FirstOrDefault();
                if (spessore == null)
                {
                    lblMessaggio.Text = "Impossibile individuare lo spessore richiesto. Impossibile procedere";
                    return;
                }

                if (spessore.DENOMINATORE == 0)
                {
                    lblMessaggio.Text = "Denominatore a zero. Impossibile procedere";
                    return;
                }

                if (nMisurePerCampione.Value == 0)
                {
                    lblMessaggio.Text = "Numero misure per campioni non può essere zero";
                    return;
                }

                CDCBLL bll = new CDCBLL();
                int misurePerCampione = (int)nMisurePerCampione.Value;

                CDCDS.CDC_APPLICAZIONERow applicazioneRow = _DS.CDC_APPLICAZIONE.Where(x => x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).FirstOrDefault();
                if (applicazioneRow == null)
                {
                    applicazioneRow = _DS.CDC_APPLICAZIONE.NewCDC_APPLICAZIONERow();
                    applicazioneRow.PARTE = _dettaglio.PARTE;
                    applicazioneRow.COLORE = _dettaglio.COLORE;
                    applicazioneRow.APPLICAZIONE = txtApplicazione.Text;
                    applicazioneRow.NUMEROCAMPIONI = nMisurePerCampione.Value;
                    applicazioneRow.SPESSORE = txtSpessoreRichiesto.Text;
                    _DS.CDC_APPLICAZIONE.AddCDC_APPLICAZIONERow(applicazioneRow);
                }
                else
                {
                    applicazioneRow.APPLICAZIONE = txtApplicazione.Text;
                    applicazioneRow.NUMEROCAMPIONI = nMisurePerCampione.Value;
                    applicazioneRow.SPESSORE = txtSpessoreRichiesto.Text;
                }


                decimal IDGALVANICA = bll.InserisciCDCGalvanica(_DS, txtSpessoreRichiesto.Text, IDDETTAGLIO, txtApplicazione.Text, Contesto.StrumentoSpessore, misurePerCampione, Contesto.Utente.FULLNAMEUSER);

                List<CDCDS.CDC_MISURERow> idMisuraDaCancellare = _DS.CDC_MISURE.Where(x => x.IDGALVANICA == IDGALVANICA).ToList();
                foreach (CDCDS.CDC_MISURERow misura in idMisuraDaCancellare)
                {
                    misura.Delete();
                }

                foreach (DataRow riga in _dsServizio.Tables[tblMisure].Rows)
                {

                    for (int j = 0; j < _dsServizio.Tables[tblMisure].Columns.Count - 3; j++)
                    {
                        CDCDS.CDC_MISURERow misura = _DS.CDC_MISURE.NewCDC_MISURERow();
                        int nMisura = (int)riga[2];
                        misura.NMISURA = nMisura;
                        misura.DATAMISURA = DateTime.Today;
                        misura.IDGALVANICA = IDGALVANICA;
                        string tipo = _dsServizio.Tables[tblMisure].Columns[j + 3].ColumnName;
                        string valore = (riga[3 + j]).ToString();
                        misura.NCOLONNA = j;
                        misura.TIPOMISURA = tipo;
                        misura.VALORE = valore;
                        _DS.CDC_MISURE.AddCDC_MISURERow(misura);
                    }
                }

                bll.SalvaTabelleSpessori(_DS);
                _DS.AcceptChanges();

                Bitmap logoSpessori = Properties.Resources.logo_spessori_v2;
                ImageConverter converter = new ImageConverter();
                byte[] iLogo = (byte[])converter.ConvertTo(logoSpessori, typeof(byte[]));

                Bitmap bowman = Properties.Resources.Bowman;
                converter = new ImageConverter();
                byte[] iBowman = (byte[])converter.ConvertTo(bowman, typeof(byte[]));

                List<string> medie = new List<string>();
                List<string> Std = new List<string>();
                List<string> Pct = new List<string>();
                List<string> range = new List<string>();
                List<string> minimo = new List<string>();
                List<string> massimo = new List<string>();

                DataRow rigaMedie = _dsServizio.Tables[tblAggregati].Rows[0];
                DataRow rigaStd = _dsServizio.Tables[tblAggregati].Rows[1];
                DataRow rigaPct = _dsServizio.Tables[tblAggregati].Rows[2];
                DataRow rigarange = _dsServizio.Tables[tblAggregati].Rows[3];
                DataRow rigaMinimo = _dsServizio.Tables[tblAggregati].Rows[4];
                DataRow rigaMassimo = _dsServizio.Tables[tblAggregati].Rows[5];

                for (int ncol = 0; ncol < _dsServizio.Tables[tblAggregati].Columns.Count; ncol++)
                {
                    medie.Add(rigaMedie[ncol].ToString());
                    Std.Add(rigaStd[ncol].ToString());
                    Pct.Add(rigaPct[ncol].ToString());
                    range.Add(rigarange[ncol].ToString());
                    minimo.Add(rigaMinimo[ncol].ToString());
                    massimo.Add(rigaMassimo[ncol].ToString());
                }
                filename = bll.CreaPDFSpessore(IDDETTAGLIO, _DS, Contesto.PathCollaudo, iLogo, iBowman, chkCopiaReferto.Checked, Contesto.GetPathRefertiLaboratorio(brand),
                    medie, Std, Pct, range, minimo, massimo, brand, txtNumeroCampioni.Text);

                if (chkApriPDF.Checked)
                {
                    Process.Start(filename);
                }
                evidenziaPDFFatti();
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in crea PDF");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dgvMisure_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                CalcolaValoriAggregati();
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore nella selezione della cella dvgMisure");
            }
        }

        private void dgvSpessore_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                lblMessaggio.Text = "";
                dgvSpessore.Rows[e.RowIndex].ErrorText = "";

                decimal newInteger;

                if (dgvSpessore.Rows[e.RowIndex].IsNewRow) { return; }

                if (e.ColumnIndex == 0) { return; }

                if (!decimal.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    lblMessaggio.Text = "La cella deve contenere un valore numerico non negativo";
                    dgvSpessore.Rows[e.RowIndex].ErrorText = "La cella deve contenere un valore numerico non negativo";
                }
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore nella validazione della cella dvgMisure");
            }

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
