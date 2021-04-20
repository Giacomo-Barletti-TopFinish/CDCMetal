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
        private string tipoCertificato;
        private DataSet _dsServizio = new DataSet();
        private string tblMisure = "MISURE";
        private string tblAggregati = "AGGREGATI";
        private string tblSpessori = "SPESSORI";

        private Random _random = new Random(DateTime.Now.Millisecond);

        private enum COLONNEAGGREGATI { Media = 0, StdDev, Pct, Range, Minimo, Massimo };

        private CDCDS.CDC_DETTAGLIORow _dettaglio;
        public CreaSpessoreFrm(string tipoCertificatoSpessore)
        {
            tipoCertificato = tipoCertificatoSpessore;
            InitializeComponent();
        }

        private void CreaSpessoreFrm_Load(object sender, EventArgs e)
        {
            PopolaDDLDate();
            CaricaTabelleSpessori();
        }

        private void PopolaDDLDate()
        {
            ddlDataCollaudo.Items.AddRange(CaricaDateCollaudoSingle().ToArray());
        }
        private void PopolaDDLBrand(string datacollaudo)
        {
            ddlBrand.Items.Clear();
            ddlBrand.Items.Add("");
            ddlBrand.Items.AddRange(CaricaBrandCollaudoSingle(datacollaudo).ToArray());
        }

        private void CaricaTabelleSpessori()
        {
            CDCBLL bll = new CDCBLL();
            bll.LeggiTabelleSpessori(_DS, tipoCertificato);

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
            if (ddlBrand.SelectedIndex == -1 || ddlBrand.SelectedItem.ToString() == "")
            {
                lblMessaggio.Text = "Selezionare un brand";
                return;
            }


            dgvSpessore.DataSource = null;
            dgvSpessore.DataMember = "";

            dgvMisure.DataSource = null;
            dgvMisure.DataMember = "";

            dgvAggregati.DataSource = null;
            dgvAggregati.DataMember = "";


            //try
            //{
            // DataCollaudo dataSelezionata = (DataCollaudo)ddlDataCollaudo.SelectedItem;
            DataCollaudoSTR dataSelezionata = new DataCollaudoSTR(ddlBrand.SelectedItem.ToString(), ddlDataCollaudo.SelectedItem.ToString());


                CDCBLL bll = new CDCBLL();

                _DS.CDC_EXCEL.Clear();
                _DS.CDC_DETTAGLIO.Clear();
                _DS.CDC_ARTICOLI.Clear();
                _DS.CDC_ARTICOLI_DIMENSIONI.Clear();
                _DS.CDC_ARTICOLI_SPESSORI.Clear();
                _DS.CDC_NUMEROCAMPIONI.Clear();
                _DS.CDC_SPESSORE.Clear();
                _DS.CDC_GALVANICA.Clear();
                _DS.CDC_MISURE.Clear();

                _dettaglio = null;

                bll.LeggiCollaudoDaDataSTR(_DS, dataSelezionata);



                if (_DS.CDC_DETTAGLIO.Count > 0)
                {
                    btnCreaPDF.Enabled = true;
                    List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                    bll.FillCDC_GALVANICA(_DS, IDDETTAGLIO, tipoCertificato);
                    bll.CDC_PDF(_DS, IDDETTAGLIO);

                    bll.CaricaArticoli(_DS);
                    bll.CaricaArticoliSpessori(_DS);
                    bll.CaricaNumeroCampioni(_DS);
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
                dgvDettaglio.Columns[29].Visible = false;

                foreach (DataGridViewColumn column in dgvDettaglio.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                ///////

                //foreach (DataGridViewRow riga in dgvDettaglio.Rows)
                //{

                //    string parte = (string)riga.Cells["PARTE"].Value;
                //    string colore = (string)riga.Cells["COLORE"].Value;

                //    List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).OrderBy(x => x.SEQUENZA).ToList();
                //    if (articoli.Count == 0)
                //    {
                //        riga.Cells[7].Style.BackColor = Color.Yellow;
                //        riga.Cells[8].Style.BackColor = Color.Yellow;
                //    }
                //    else
                //    {
                //        foreach (CDCDS.CDC_ARTICOLIRow articolo in articoli)
                //        {
                //            List<CDCDS.CDC_ARTICOLI_SPESSORIRow> artspessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == articolo.IDARTICOLO).OrderBy(x => x.IDSPESSORE).ToList();
                //            if (artspessori.Count == 0)
                //            {
                //                riga.Cells[7].Style.BackColor = Color.Orange;
                //                riga.Cells[8].Style.BackColor = Color.Orange;
                //            }
                //        }
                //    }
                //}


                /////////

                evidenziaPDFFatti();
            //}
            //catch (Exception ex)
            //{
            //    MostraEccezione(ex, "Errore in leggi dati");
            //}

        }

        private void evidenziaPDFFatti()
        {
            List<decimal> iddettaglioConPdf;
            if (tipoCertificato == TipoCertificatoSpessore.CERTIFICATOSPESSOREGENERICO)
                iddettaglioConPdf = _DS.CDC_PDF.Where(x => x.TIPO == CDCTipoPDF.CERTIFICATOSPESSORE).Select(x => x.IDDETTAGLIO).Distinct().ToList();
            else
                iddettaglioConPdf = _DS.CDC_PDF.Where(x => x.TIPO == CDCTipoPDF.CERTIFICATOSPESSORENICHEL).Select(x => x.IDDETTAGLIO).Distinct().ToList();

            //foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            //{
            //    decimal IDDETTAGLIO = (decimal)riga.Cells["IDDETTAGLIO"].Value;
            //    if (iddettaglioConPdf.Contains(IDDETTAGLIO))
            //    {
            //        riga.Cells[1].Style.BackColor = Color.Yellow;
            //    }
            //}

            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                decimal IDDETTAGLIO = (decimal)riga.Cells["IDDETTAGLIO"].Value;
                if (iddettaglioConPdf.Contains(IDDETTAGLIO))
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                }


                //-----------

                string parte = (string)riga.Cells["PARTE"].Value;
                string colore = (string)riga.Cells["COLORE"].Value;

                List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).OrderBy(x => x.SEQUENZA).ToList();
                if (articoli.Count == 0)
                {
                    riga.Cells[7].Style.BackColor = Color.Yellow;
                    riga.Cells[8].Style.BackColor = Color.Yellow;
                }
                else
                {
                    foreach (CDCDS.CDC_ARTICOLIRow articolo in articoli)
                    {
                        List<CDCDS.CDC_ARTICOLI_SPESSORIRow> artspessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == articolo.IDARTICOLO).OrderBy(x => x.IDSPESSORE).ToList();
                        if (artspessori.Count == 0)
                        {
                            riga.Cells[7].Style.BackColor = Color.Orange;
                            riga.Cells[8].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }


        }

        private void ImpostaApplicazione(string colore, string parte)
        {
            CDCDS.CDC_APPLICAZIONERow applicazione = _DS.CDC_APPLICAZIONE.Where(x => x.COLORE == colore && x.PARTE == parte).FirstOrDefault();
            if (applicazione != null)
            {
                txtApplicazione.Text = applicazione.IsAPPLICAZIONENull() ? string.Empty : applicazione.APPLICAZIONE;
                // nMisurePerCampione.Value = applicazione.NUMEROCAMPIONI;
                txtSpessoreRichiesto.Text = applicazione.IsSPESSORENull() ? string.Empty : applicazione.SPESSORE;
            }
            else
            {
                txtApplicazione.Text = string.Empty;
                txtSpessoreRichiesto.Text =  string.Empty;
                // nMisurePerCampione.Value = 0;
            }
        }

        private void ImpostaSpessore(Decimal iddettaglio, Decimal quantita, string parte, string colore)
        {



            DataTable dtSpessori;
            if (_dsServizio.Tables[tblSpessori] == null)
            {
                dtSpessori = _dsServizio.Tables.Add();
                dtSpessori.TableName = tblSpessori;

                dtSpessori.Columns.Add("IDSPESSORE", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("SEQUENZA", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
                dtSpessori.Columns.Add("MISUREPERCAMPIONE", Type.GetType("System.Int32"));
                dtSpessori.Columns.Add("NUMEROCAMPIONI", Type.GetType("System.Int32"));
                dtSpessori.Columns.Add("Materiale", Type.GetType("System.String"));
                dtSpessori.Columns.Add("Massimo", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("Nominale", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("Minimo", Type.GetType("System.Decimal"));
                dtSpessori.Columns.Add("Denominatore", Type.GetType("System.Decimal"));
            }
            else
            {
                dtSpessori = _dsServizio.Tables[tblSpessori];
                dtSpessori.Clear();
            }


            List<CDCDS.CDC_SPESSORERow> spessori = _DS.CDC_SPESSORE.Where(x => x.IDDETTAGLIO == iddettaglio).OrderBy(x => x.SEQUENZA).ToList();
            if (spessori.Count > 0)
            {

                foreach (CDCDS.CDC_SPESSORERow spessore in spessori)
                {

                    DataRow riga = dtSpessori.NewRow();

                    riga[0] = spessore.IDSPESSORE;
                    riga[1] = spessore.IDDETTAGLIO ;
                    riga[2] = spessore.SEQUENZA;
                    riga[3] = spessore.DESCRIZIONE;
                    riga[4] = spessore.MISUREPERCAMPIONE;
                    riga[5] = spessore.NUMEROCAMPIONI;
                    riga[6] = spessore.ETICHETTA;
                    riga[7] = spessore.MASSIMO;
                    riga[8] = spessore.NOMINALE;
                    riga[9] = spessore.MINIMO;
                    riga[10] = spessore.DENOMINATORE;

                    dtSpessori.Rows.Add(riga);
                }
            }
            else
            {
                List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).OrderBy(x => x.SEQUENZA).ToList();
                foreach (CDCDS.CDC_ARTICOLIRow articolo in articoli)
                {
                    List<CDCDS.CDC_ARTICOLI_SPESSORIRow> artspessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == articolo.IDARTICOLO).OrderBy(x => x.IDSPESSORE).ToList();
                    foreach (CDCDS.CDC_ARTICOLI_SPESSORIRow artspessore in artspessori)
                    {

                        DataRow riga = dtSpessori.NewRow();

                        //CDCDS.CDC_SPESSORERow vSpessore = _DS.CDC_SPESSORE.Where(x => x.IDDETTAGLIO == iddettaglio && x.SEQUENZA == articolo.SEQUENZA && x.ETICHETTA == artspessore.ETICHETTA).FirstOrDefault();
                        //if (vSpessore != null)
                        //{
                        //    riga[0] = vSpessore.IDSPESSORE;
                        //    //riga[13] = vCoprente.DATATEST;
                        //    //riga[14] = vCoprente.NUMEROCAMPIONI;
                        //    //riga[15] = vCoprente.FORNITORE;
                        //}
                        //else
                        //{ riga[0] = -1; }

                        riga[0] = -1;
                        riga[1] = iddettaglio;
                        riga[2] = articolo.SEQUENZA;
                        riga[3] = articolo.DESCRIZIONE;
                        riga[4] = articolo.MISUREPERCAMPIONE;
                        riga[5] = NumeroCampioni(ddlBrand.SelectedItem.ToString(), quantita, articolo.MISUREPERCAMPIONE);
                        riga[6] = artspessore.ETICHETTA;
                        riga[7] = artspessore.MASSIMO;
                        riga[8] = artspessore.NOMINALE;
                        riga[9] = artspessore.MINIMO;
                        riga[10] = Denominatore(artspessore.MINIMO, artspessore.MASSIMO, artspessore.NOMINALE); // spessore.DENOMINATORE;

                        dtSpessori.Rows.Add(riga);

                        //if (articoli.Count == 0)
                        //{
                        //    DataRow riga = dtCartelle.NewRow();


                        //SCORRERE PER SEQUENZA...
                        // SE TROVATO 
                        //SENNO PRENDERE DA ARTICOLI_SPESSORI


                        //foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.COLORE == colore && x.PARTE == parte).OrderBy(x => x.SEQUENZA))
                        //foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.IDDETTAGLIO == iddettaglio).OrderBy(x => x.SEQUENZA))
                        //{
                        //    DataRow riga = dtSpessori.NewRow();

                        //    riga[0] = spessore.IDSPESSORE;
                        //    riga[1] = spessore.IDDETTAGLIO;
                        //    riga[2] = spessore.SEQUENZA;
                        //    riga[3] = spessore.DESCRIZIONE;
                        //    riga[4] = spessore.ETICHETTA;
                        //    riga[5] = spessore.MASSIMO;
                        //    riga[6] = spessore.MINIMO;
                        //    riga[7] = spessore.DENOMINATORE;

                        //    dtSpessori.Rows.Add(riga);
                        //}
                    }
                }
            }

            dgvSpessore.AutoGenerateColumns = true;
            dgvSpessore.DataSource = _dsServizio;
            dgvSpessore.DataMember = tblSpessori;
            dgvSpessore.Columns[0].Visible = false;
            dgvSpessore.Columns[1].Visible = false;

            ((DataGridViewTextBoxColumn)dgvSpessore.Columns[6]).MaxInputLength = 2; //0

            foreach (DataGridViewTextBoxColumn dgc in dgvSpessore.Columns)
                dgc.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblMessaggio.Text = string.Empty;

              


                //DataGridViewRow dr = dgvDettaglio.Rows[e.RowIndex];
                //if (dr.Cells[7].Style.BackColor == Color.Orange || dr.Cells[7].Style.BackColor == Color.Yellow)
                //{
                    dgvSpessore.DataSource = null;
                    dgvSpessore.DataMember = "";

                    dgvMisure.DataSource = null;
                    dgvMisure.DataMember = "";

                    dgvAggregati.DataSource = null;
                    dgvAggregati.DataMember = "";
                //    return;
                //}
                if (e.RowIndex == -1) return;


                DataRow r = _DS.CDC_DETTAGLIO.Rows[e.RowIndex];
                decimal IDDETTAGLIO = (decimal)r[0];
                _dettaglio = _DS.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();

                ImpostaApplicazione(_dettaglio.COLORE, _dettaglio.PARTE);
                _dsServizio = new DataSet();

                //CalcolaNumeroCampioni();
                ImpostaSpessore(_dettaglio.IDDETTAGLIO, _dettaglio.QUANTITA, _dettaglio.PARTE, _dettaglio.COLORE);

                CDCDS.CDC_GALVANICARow galvanica = _DS.CDC_GALVANICA.Where(x => x.IDDETTAGLIO == IDDETTAGLIO && x.CERTIFICATO == tipoCertificato).FirstOrDefault();
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

                    //if (_dsServizio.Tables[tblAggregati] != null)
                    //{
                    //    _dsServizio.Tables.Remove(tblAggregati);
                    //}

                    //DataTable dtAggregati = _dsServizio.Tables.Add();
                    //dtAggregati.TableName = tblAggregati;
                    ////dtAggregati.Columns.Add("DATO", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
                    //dtAggregati.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Materiale", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Media", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Std Dev", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Pct(%) Dev", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Massimo", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Minimo", Type.GetType("System.String"));
                    //dtAggregati.Columns.Add("Range", Type.GetType("System.String"));

                    //mostradgvAggregati();
                    //mostradgvMisure();

                }
                //ImpostaSpessore(_dettaglio.IDDETTAGLIO, _dettaglio.QUANTITA, _dettaglio.PARTE, _dettaglio.COLORE);


            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in dgvDettaglio_CellClick");
            }
        }

        private void CaricaCampioniMisuraPrecedente(CDCDS.CDC_GALVANICARow galvanica)
        {

           

            List<CDCDS.CDC_MISURERow> misurePrecedenti = _DS.CDC_MISURE.Where(x => x.RowState != DataRowState.Deleted && x.IDGALVANICA == galvanica.IDGALVANICA).OrderBy(x => x.SEQUENZA).ThenBy(x => x.NCOLONNA).ThenBy(x => x.NMISURA).ToList();

            int numerocampioni = (int)misurePrecedenti.Max(x => x.NMISURA); // + 1;


            //CREO LA TABELLA
            if (_dsServizio.Tables[tblMisure] != null)
            {
                _dsServizio.Tables.Remove(tblMisure);
            }

            DataTable dtMisure = _dsServizio.Tables.Add();
            dtMisure.TableName = tblMisure;
            dtMisure.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtMisure.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
            dtMisure.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
            dtMisure.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            dtMisure.Columns.Add("Materiale", Type.GetType("System.String"));

            for (int i = 1; i <= numerocampioni; i++)
            {
                dtMisure.Columns.Add("DATO" + i.ToString(), Type.GetType("System.Decimal"));
            }

            foreach (DataRow dr in _dsServizio.Tables[tblSpessori].Rows)
            {
                decimal seq = (decimal)dr[2];
                string tipomat = dr[6].ToString();
                //materiali
                DataRow riga = dtMisure.NewRow();
                riga["IDPRENOTAZIONE"] = _dettaglio.IDPRENOTAZIONE;
                riga["IDDETTAGLIO"] = _dettaglio.IDDETTAGLIO;
                riga["SEQUENZA"] = seq;
                riga["DESCRIZIONE"] = dr[3].ToString();
                riga["MATERIALE"] = tipomat;

              
                List<CDCDS.CDC_MISURERow> misureCorrenti =   misurePrecedenti.Where(x => x.SEQUENZA == seq && x.TIPOMISURA == tipomat).OrderBy(x => x.NMISURA).ToList();

                foreach (CDCDS.CDC_MISURERow mis in misureCorrenti)
                {
                    int nMis = (int)mis.NMISURA;
                    riga["DATO" + nMis.ToString()] = Convert.ToDecimal(mis.VALORE);
                }

                dtMisure.Rows.Add(riga);
            }

            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.ReadOnly = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tblMisure;


            dgvMisure.Columns[0].Visible = false;
            dgvMisure.Columns[1].Visible = false;

            for (int i = 1; i <= numerocampioni; i++)
            {
                dgvMisure.Columns[4 + i].Width = 50;
            }

            //dgvMisure.Update();
            //dgvMisure.Refresh();


            CalcolaValoriAggregati_new();

            mostradgvAggregati();
            mostradgvMisure();

            return;

            //    if (((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand == CDCBrands.YSL
            //|| ((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand == CDCBrands.Balenciaga
            //|| ((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand == CDCBrands.McQueen)
            //    {
            //        if (galvanica.MISURECAMPIONE > 0)
            //        {
            //            int aux = numeroCampioni / (int)galvanica.MISURECAMPIONE;
            //        }
            //    }
            //    int numeroColonne = misurePrecedenti.Where(x => x.NMISURA == 1).Count();



            int numeroColonne = 0;
            int numeroCampioni = 0;

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
            //dgvMisure.AutoGenerateColumns = true;
            //dgvMisure.DataSource = _dsServizio;
            //dgvMisure.DataMember = tblMisure;

            //if (dgvMisure.Columns.Count > 3)
            //{
            //    dgvMisure.Columns[0].Visible = false;
            //    dgvMisure.Columns[1].Visible = false;
            //    dgvMisure.Columns[2].ReadOnly = true;
            //}
            //dgvMisure.Update();
        }


        private bool VerificaSpessori(out string messaggio)
        {
            messaggio = string.Empty;

            if (dgvSpessore.Rows.Count == 0)
            {
                messaggio = "Non ci sono valori nella tabella degli spessori";
                return false;
            }
            return true;


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

        //private void popolaCDC_SPESSORE()
        //{
        //    int sequenza = 0;
        //    List<CDCDS.CDC_SPESSORERow> elementi = _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).ToList();
        //    foreach (CDCDS.CDC_SPESSORERow elemento in elementi)
        //        elemento.Delete();

        //    foreach (DataGridViewRow dr in dgvSpessore.Rows)
        //    {
        //        if (dr.IsNewRow) continue;
        //        string etichetta = dr.Cells[0].Value.ToString();
        //        decimal Massimo = (decimal)dr.Cells[1].Value;
        //        decimal Minimo = (decimal)dr.Cells[2].Value;
        //        decimal Denominatore = (decimal)dr.Cells[3].Value;
        //        CDCDS.CDC_SPESSORERow spessore = _DS.CDC_SPESSORE.NewCDC_SPESSORERow();
        //        spessore.COLORE = _dettaglio.COLORE;
        //        spessore.DENOMINATORE = Denominatore;
        //        spessore.ETICHETTA = etichetta;
        //        spessore.MASSIMO = Massimo;
        //        spessore.MINIMO = Minimo;
        //        spessore.PARTE = _dettaglio.PARTE;
        //        spessore.SEQUENZA = sequenza;
        //        spessore.CERTIFICATO = tipoCertificato;
        //        _DS.CDC_SPESSORE.AddCDC_SPESSORERow(spessore);

        //        sequenza++;
        //    }
        //}

        private void btnCreaCampioni_Click(object sender, EventArgs e)
        {
            lblMessaggio.Text = string.Empty;
            if (_dettaglio == null)
            {
                lblMessaggio.Text = "Selezionare una riga dalla tabella Schede Collaudo";
                return;
            }

            //if (nMisurePerCampione.Value == 0)
            //{
            //    lblMessaggio.Text = "Indicare il numero di misure per campione";
            //    return;
            //}

            string messaggio;
            if (!VerificaSpessori(out messaggio))
            {
                lblMessaggio.Text = messaggio;
                return;
            }

            if (_dsServizio.Tables[tblMisure] != null)
            {
                _dsServizio.Tables.Remove(tblMisure);
            }

            DataTable dtMisure = _dsServizio.Tables.Add();
            dtMisure.TableName = tblMisure;
            dtMisure.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtMisure.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
            //dtMisure.Columns.Add("IDSPESSORE", Type.GetType("System.Decimal"));
            dtMisure.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
            dtMisure.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            dtMisure.Columns.Add("Materiale", Type.GetType("System.String"));



            Int32 numerocampioni = 0;
            foreach (DataRow dr in _dsServizio.Tables[tblSpessori].Rows)
            {
                if ((int)dr[5] > numerocampioni)
                    numerocampioni = (int)dr[5];
            }
            for (int i = 1; i <= numerocampioni; i++)
            {
                dtMisure.Columns.Add("DATO" + i.ToString(), Type.GetType("System.Decimal"));
            }


            //crea Misure
            foreach (DataRow dr in _dsServizio.Tables[tblSpessori].Rows)
            {
                //materiali
                DataRow riga = dtMisure.NewRow();
                riga["IDPRENOTAZIONE"] = _dettaglio.IDPRENOTAZIONE;
                riga["IDDETTAGLIO"] = _dettaglio.IDDETTAGLIO;
                riga["SEQUENZA"] = (decimal)dr[2];
                riga["DESCRIZIONE"] = dr[3].ToString();
                riga["MATERIALE"] = dr[6].ToString();

                Int32 Row_numerocampioni = (int)dr[5];
                for (int i = 1; i <= Row_numerocampioni; i++)
                {
                    decimal massimoD = Convert.ToDecimal(dr[7]);
                    decimal minimoD = Convert.ToDecimal(dr[9]);
                    decimal denominatore = (decimal)dr[10];

                    if (denominatore == 0) denominatore = 1;

                    int massimo = Convert.ToInt32(massimoD * denominatore);
                    int minimo = Convert.ToInt32(minimoD * denominatore);
                    //int massimo = (int)dr[7].ToString(); // (int)dr[7];
                    //int minimo = (int)dr[9].ToString(); // (int)dr[9];



                    int numero = _random.Next(minimo, massimo);
                    decimal valore = (decimal)numero;
                    valore = valore / denominatore;
                    //riga[3 + j] = valore;
                    //j++;

                    riga["DATO" + i.ToString()] = valore;
                }

                //dtDimensioni.Columns.Add(spessore.ETICHETTA, Type.GetType("System.Decimal"));
                //dtAggregati.Columns.Add(spessore.ETICHETTA, Type.GetType("System.String"));

                dtMisure.Rows.Add(riga);
            }





            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.ReadOnly = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tblMisure;

            //if (dgvMisure.Columns.Count > 3)
            //{
            dgvMisure.Columns[0].Visible = false;
            dgvMisure.Columns[1].Visible = false;
            //    dgvMisure.Columns[2].ReadOnly = true;
            //}

            for (int i = 1; i <= numerocampioni; i++)
            {
                dgvMisure.Columns[4 + i].Width = 50;
            }

            dgvMisure.Update();
            dgvMisure.Refresh();


            CalcolaValoriAggregati_new();

            //mostradgvAggregati();
            //mostradgvMisure();

            return;

            //       int numeroCampioni = int.Parse(txtNumeroCampioni.Text); // caso GUCCI
            //if (((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand == CDCBrands.YSL
            //    || ((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand == CDCBrands.Balenciaga
            //    || ((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand == CDCBrands.McQueen)

            //    if (((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand == CDCBrands.YSL
            //|| ((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand == CDCBrands.Balenciaga
            //|| ((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand == CDCBrands.McQueen)
            //    {
            //        int numeroCampioniPerPezzo = int.Parse(txtNumeroCampioni.Text);
            //        numeroCampioni = numeroCampioniPerPezzo * (int)nMisurePerCampione.Value;
            //    }

            try
            {
                /////////popolaCDC_SPESSORE();

                _dsServizio = new DataSet();

                DataTable dtDimensioni = _dsServizio.Tables.Add();
                dtDimensioni.TableName = tblMisure;
                dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
                dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));
                dtDimensioni.Columns.Add("MISURA", Type.GetType("System.Int32"));

                DataTable dtAggregati = _dsServizio.Tables.Add();
                dtAggregati.TableName = tblAggregati;
                //dtAggregati.Columns.Add("DATO", Type.GetType("System.String"));



                // foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).OrderBy(x => x.SEQUENZA))
                foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.IDDETTAGLIO == _dettaglio.IDDETTAGLIO).OrderBy(x => x.SEQUENZA))
                {
                    dtDimensioni.Columns.Add(spessore.ETICHETTA, Type.GetType("System.Decimal"));
                    dtAggregati.Columns.Add(spessore.ETICHETTA, Type.GetType("System.String"));
                }

                for (int i = 0; i < numerocampioni; i++)
                {
                    DataRow riga = dtDimensioni.NewRow();
                    riga[0] = _dettaglio.IDPRENOTAZIONE;
                    riga[1] = _dettaglio.IDDETTAGLIO;
                    riga[2] = i;

                    int j = 0;
                    foreach (CDCDS.CDC_SPESSORERow spessore in _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.IDDETTAGLIO == _dettaglio.IDDETTAGLIO).OrderBy(x => x.SEQUENZA))
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

                //CalcolaValoriAggregati();
                CalcolaValoriAggregati_new();
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in Crea Campioni Click");
            }

        }

        //private void nMisurePerCampione_ValueChanged(object sender, EventArgs e)
        //{
        //    CalcolaNumeroCampioni();
        //}

        private Int32 NumeroCampioni(string Brand, decimal quantita, decimal misurepercampione)
        {

            //Brand = "GUCCI";
            //tipoCertificato = "SPESSORE";


            List<CDCDS.CDC_NUMEROCAMPIONIRow> numcampioni = _DS.CDC_NUMEROCAMPIONI.Where(x => x.BRAND.Trim() == Brand && x.TIPO == tipoCertificato).OrderBy(x => x.QUANTITA).ToList();
            foreach (CDCDS.CDC_NUMEROCAMPIONIRow numcamp in numcampioni)
            {

                if (quantita <= numcamp.QUANTITA)
                {
                    if (numcamp.MOLTIPLICAREPERNUMEROMISURE == "S")
                    { return (Int32)misurepercampione * numcamp.NUMEROCAMPIONI; }
                    else
                    { return numcamp.NUMEROCAMPIONI; }
                }
            }

            return 0;
        }

        private double Denominatore(double minimo, double massimo, double nominale)
        {
            int lengthMin = 1;
            int lengthMax = 1;
            int lengthNom = 1;
            int lenghReturn = 1;

            string valstr = minimo.ToString();
            if (valstr.Contains(','))
            {
                lengthMin = valstr.Substring(valstr.IndexOf(",") + 1).Length;
                lenghReturn = lengthMin;
            }

            valstr = massimo.ToString();
            if (valstr.Contains(','))
            {
                lengthMax = valstr.Substring(valstr.IndexOf(",") + 1).Length;
                if (lengthMax > lenghReturn)
                    lenghReturn = lengthMax;
            }

            valstr = nominale.ToString();
            if (valstr.Contains(','))
            {
                lengthNom = valstr.Substring(valstr.IndexOf(",") + 1).Length;
                if (lengthNom > lenghReturn)
                    lenghReturn = lengthNom;
            }

            if (lenghReturn == 0)
            { return 1; }
            else
            { return System.Math.Pow(10, lenghReturn); }


        }

        //private void CalcolaNumeroCampioni()
        //{
        //    if (ddlBrand.SelectedIndex == -1) return;

        //    BrandCollaudoSingle bs = (BrandCollaudoSingle)ddlBrand.SelectedItem;

        //    switch (bs.Brand) //dataCollaudo.Brand)
        //    {
        //        case CDCBrands.Gucci:
        //            int numeroCampioni = 0;
        //            if (_dettaglio == null) return;

        //            if (_dettaglio.QUANTITA < 25)
        //                numeroCampioni = 3 * (int)nMisurePerCampione.Value;

        //            if (_dettaglio.QUANTITA >= 25 && _dettaglio.QUANTITA <= 150)
        //                numeroCampioni = 3 * (int)nMisurePerCampione.Value;

        //            if (_dettaglio.QUANTITA >= 151 && _dettaglio.QUANTITA <= 1200)
        //                numeroCampioni = 5 * (int)nMisurePerCampione.Value;

        //            if (_dettaglio.QUANTITA >= 1201 && _dettaglio.QUANTITA <= 10000)
        //                numeroCampioni = 8 * (int)nMisurePerCampione.Value;

        //            if (_dettaglio.QUANTITA > 10001)
        //                numeroCampioni = 8 * (int)nMisurePerCampione.Value;

        //            txtNumeroCampioni.Text = numeroCampioni.ToString();
        //            txtNumeroCampioni.ReadOnly = true;
        //            break;
        //        case CDCBrands.Balenciaga:
        //        case CDCBrands.McQueen:
        //        case CDCBrands.YSL:
        //            txtNumeroCampioni.ReadOnly = false;
        //            txtNumeroCampioni.Text = "3";
        //            break;

        //    }

        //}

        private void CalcolaValoriAggregati_new()
        {



            DataTable tblCampioni = _dsServizio.Tables[tblMisure];

            //DataTable tbl = _dsServizio.Tables[tblAggregati];

            if (_dsServizio.Tables[tblAggregati] != null)
            {
                _dsServizio.Tables.Remove(tblAggregati);
            }

            DataTable tbl = _dsServizio.Tables.Add();
            tbl.TableName = tblAggregati;

            tbl.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
            tbl.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            tbl.Columns.Add("Materiale", Type.GetType("System.String"));
            tbl.Columns.Add("Media", Type.GetType("System.String"));
            tbl.Columns.Add("Std Dev", Type.GetType("System.String"));
            tbl.Columns.Add("Pct(%) Dev", Type.GetType("System.String"));
            tbl.Columns.Add("Massimo", Type.GetType("System.String"));
            tbl.Columns.Add("Minimo", Type.GetType("System.String"));
            tbl.Columns.Add("Range", Type.GetType("System.String"));



          //  tbl.Clear();

            foreach (DataRow dr in tblCampioni.Rows)
            {
                DataRow rigaAg = tbl.NewRow();
                rigaAg[0] = dr[2];
                rigaAg[1] = dr[3];
                rigaAg[2] = dr[4];

                //vedere quanti campioni
                int numerocampioni = 0;
                for (int i = 1; i < 1000; i++)
                {

                    if (tblCampioni.Columns["DATO" + i.ToString()] != null)
                    {
                        if (dr["DATO" + i.ToString()].ToString() != "")
                        { numerocampioni = i; }
                        else
                        { break; }
                    }
                    else
                    { break; }
                }


                List<decimal> array = GetList_new(dr, numerocampioni);

                decimal media = array.Average();
                rigaAg[3] = Math.Round(media, 2).ToString();

                double media2 = (double)array.Average();
                double stdDev = Math.Sqrt(array.Sum(x => Math.Pow((double)x - media2, 2)) / array.Count);
                rigaAg[4] = Math.Round(stdDev, 3).ToString();

                double pct = 0;
                //double media = (double)Double.Parse((string)tbl.Rows[0][i + 1]);
                //double stddev = (double)Double.Parse((string)tbl.Rows[1][i + 1]);
                if (media2 != 0) pct = stdDev / media2;
                rigaAg[5] = Math.Round(pct * 100, 1).ToString();

                double massimo = (double)array.Max();
                rigaAg[6] = Math.Round(massimo, 3).ToString();

                double minimo = (double)array.Min();
                rigaAg[7] = Math.Round(minimo, 3).ToString();

                rigaAg[8] = Math.Round(massimo - minimo,3).ToString();


                tbl.Rows.Add(rigaAg);
            }


            dgvAggregati.AutoGenerateColumns = true;
            dgvAggregati.DataSource = _dsServizio;
            dgvAggregati.DataMember = tblAggregati;
            dgvAggregati.Update();
            dgvAggregati.Refresh();

        }

        private void CalcolaValoriAggregati()
        {
            DataTable tbl = _dsServizio.Tables[tblAggregati];
            DataTable tblCampioni = _dsServizio.Tables[tblMisure];

            tbl.Clear();



            int numeroColonne = tblCampioni.Columns.Count - 3;
            int numeroCampioni = Int32.Parse("1"); // txtNumeroCampioni.Text);  ??????????

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

        private List<decimal> GetList_new(DataRow dr, int columns)
        {
            decimal[] array = new decimal[columns];

            for (int i = 0; i < columns; i++)
                //array[i] = Convert.ToDecimal(dr["DATO" + (i+1)].ToString().Replace(",","."));
                array[i] = (decimal)dr["DATO" + (i + 1)];
            return array.ToList();
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
            //dgvAggregati.AutoGenerateColumns = true;
            //dgvAggregati.DataSource = _dsServizio;
            //dgvAggregati.DataMember = tblAggregati;

            //dgvAggregati.Refresh();
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

            // string brand = ((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand;
            string brand = ((BrandCollaudoSingle)ddlBrand.SelectedItem).Brand;
            //try
            //{
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


                //CDCDS.CDC_SPESSORERow spessore = _DS.CDC_SPESSORE.Where(x => x.RowState != DataRowState.Deleted && x.IDDETTAGLIO == IDDETTAGLIO && x.SEQUENZA == 0).FirstOrDefault();

                //if (spessore == null)
                //{
                //    lblMessaggio.Text = "Impossibile individuare lo spessore richiesto. Impossibile procedere";
                //    return;
                //}

                //if (spessore.DENOMINATORE == 0)
                //{
                //    lblMessaggio.Text = "Denominatore a zero. Impossibile procedere";
                //    return;
                //}

                //if (nMisurePerCampione.Value == 0)
                //{
                //    lblMessaggio.Text = "Numero misure per campioni non può essere zero";
                //    return;
                //}

                CDCBLL bll = new CDCBLL();
                //int misurePerCampione = (int)nMisurePerCampione.Value;

                CDCDS.CDC_APPLICAZIONERow applicazioneRow = _DS.CDC_APPLICAZIONE.Where(x => x.PARTE == _dettaglio.PARTE && x.COLORE == _dettaglio.COLORE).FirstOrDefault();
                if (applicazioneRow == null)
                {
                    applicazioneRow = _DS.CDC_APPLICAZIONE.NewCDC_APPLICAZIONERow();
                    applicazioneRow.PARTE = _dettaglio.PARTE;
                    applicazioneRow.COLORE = _dettaglio.COLORE;
                    applicazioneRow.APPLICAZIONE = txtApplicazione.Text;
                    applicazioneRow.NUMEROCAMPIONI = 0; // nMisurePerCampione.Value;
                    applicazioneRow.SPESSORE = txtSpessoreRichiesto.Text;
                    applicazioneRow.CERTIFICATO = tipoCertificato;
                    _DS.CDC_APPLICAZIONE.AddCDC_APPLICAZIONERow(applicazioneRow);
                }
                else
                {
                    applicazioneRow.APPLICAZIONE = txtApplicazione.Text;
                    applicazioneRow.NUMEROCAMPIONI = 0; // nMisurePerCampione.Value;
                    applicazioneRow.SPESSORE = txtSpessoreRichiesto.Text;
                }

                int misurePerCampione = 0;
                decimal IDGALVANICA = bll.InserisciCDCGalvanica(_DS, txtSpessoreRichiesto.Text, IDDETTAGLIO, txtApplicazione.Text, Contesto.StrumentoSpessore, misurePerCampione, Contesto.Utente.FULLNAMEUSER, tipoCertificato);
                //jacopo


                //caricare gli spessori
                foreach (DataRow riga in _dsServizio.Tables[tblSpessori].Rows)
                {
                    decimal IDSPESSORE = (decimal)riga[0];
                    if (IDSPESSORE == -1) { IDSPESSORE = -100; } //
                    decimal iddettaglio = (decimal)riga[1];



                    //decimal IDSPESSORE = Convert.ToDecimal(riga[0].ToString());
                    CDCDS.CDC_SPESSORERow vSpessore = _DS.CDC_SPESSORE.Where(x => x.IDSPESSORE == IDSPESSORE).FirstOrDefault();
                    if (vSpessore == null)
                    {
                        vSpessore = _DS.CDC_SPESSORE.NewCDC_SPESSORERow();

                        vSpessore.IDDETTAGLIO = iddettaglio;
                        vSpessore.SEQUENZA = Convert.ToDecimal(riga[2].ToString());
                        vSpessore.DESCRIZIONE = riga["DESCRIZIONE"].ToString();

                        vSpessore.MISUREPERCAMPIONE = (int)riga[4];
                        vSpessore.NUMEROCAMPIONI = (int)riga[5];
                        vSpessore.ETICHETTA = riga[6].ToString();
                        vSpessore.MASSIMO = (decimal)riga[7];
                        vSpessore.NOMINALE = (decimal)riga[8];
                        vSpessore.MINIMO = (decimal)riga[9];
                        vSpessore.DENOMINATORE = (decimal)riga[10];
                        vSpessore.CERTIFICATO = tipoCertificato;
                        //     vCoprenteRow.FORNITORE = fornitore;
                        //    vCoprenteRow.TURBULA = ConvertiBoolInStringa(riga[16]);
                        //   vCoprenteRow.QUADRETTATURA = ConvertiBoolInStringa(riga[17]);

                        vSpessore.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        vSpessore.DATAINSERIMENTO = DateTime.Now;

                        _DS.CDC_SPESSORE.AddCDC_SPESSORERow(vSpessore);
                    }
                    else
                    {
                        vSpessore.IDDETTAGLIO = iddettaglio;
                        vSpessore.SEQUENZA = (decimal)riga[2];
                        vSpessore.DESCRIZIONE = riga["DESCRIZIONE"].ToString();

                        vSpessore.MISUREPERCAMPIONE = (int)riga[4];
                        vSpessore.NUMEROCAMPIONI = (int)riga[5];
                        vSpessore.ETICHETTA = riga[6].ToString();
                        vSpessore.MASSIMO = (decimal)riga[7];
                        vSpessore.NOMINALE = (decimal)riga[8];
                        vSpessore.MINIMO = (decimal)riga[9];
                        vSpessore.DENOMINATORE = (decimal)riga[10];
                        vSpessore.CERTIFICATO = tipoCertificato;
                        vSpessore.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        vSpessore.DATAINSERIMENTO = DateTime.Now;
                    }



                }



                List<CDCDS.CDC_MISURERow> idMisuraDaCancellare = _DS.CDC_MISURE.Where(x => x.RowState != DataRowState.Deleted && x.IDGALVANICA == IDGALVANICA).ToList();
                foreach (CDCDS.CDC_MISURERow misura in idMisuraDaCancellare)
                {
                    misura.Delete();
                }


                //capire quante colonne dati di Misure
                int numeromisure = 0;
                for (int i = 1; i < 1000; i++)
                {
                    if (_dsServizio.Tables[tblMisure].Columns["DATO" + i.ToString()] != null)
                    { numeromisure = i; }
                    else
                    { break; }
                }

                decimal sequenza = 1000;
                decimal ncolonna = 0;
                foreach (DataRow riga in _dsServizio.Tables[tblMisure].Rows)
                {
                    if (sequenza == Convert.ToDecimal(riga[2].ToString()))
                    { ncolonna = ncolonna = +1; }
                    else
                    { ncolonna = 0; }


                    sequenza = Convert.ToDecimal(riga[2].ToString());
                    string tipo = riga[4].ToString();

                    for (int j = 1; j <= numeromisure; j++)
                    {
                        if (riga["DATO" + j.ToString()].ToString() != "")
                        {
                            CDCDS.CDC_MISURERow misura = _DS.CDC_MISURE.NewCDC_MISURERow();
                            // int nMisura = (int)riga[2];
                            misura.SEQUENZA = sequenza;
                            misura.DESCRIZIONE = riga[3].ToString();
                            misura.NMISURA = j;
                            misura.DATAMISURA = DateTime.Today;
                            misura.IDGALVANICA = IDGALVANICA;
                            
                            string valore = riga["DATO" + j.ToString()].ToString();
                            misura.NCOLONNA = ncolonna;  //a rottura di kopè
                            misura.TIPOMISURA = tipo;
                            misura.VALORE = valore;
                            _DS.CDC_MISURE.AddCDC_MISURERow(misura);
                        }
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

                //List<string> medie = new List<string>();
                //List<string> Std = new List<string>();
                //List<string> Pct = new List<string>();
                //List<string> range = new List<string>();
                //List<string> minimo = new List<string>();
                //List<string> massimo = new List<string>();

                //DataRow rigaMedie = _dsServizio.Tables[tblAggregati].Rows[0];
                //DataRow rigaStd = _dsServizio.Tables[tblAggregati].Rows[1];
                //DataRow rigaPct = _dsServizio.Tables[tblAggregati].Rows[2];
                //DataRow rigarange = _dsServizio.Tables[tblAggregati].Rows[3];
                //DataRow rigaMinimo = _dsServizio.Tables[tblAggregati].Rows[4];
                //DataRow rigaMassimo = _dsServizio.Tables[tblAggregati].Rows[5];

                //for (int ncol = 0; ncol < _dsServizio.Tables[tblAggregati].Columns.Count; ncol++)
                //{
                //    medie.Add(rigaMedie[ncol].ToString());
                //    Std.Add(rigaStd[ncol].ToString());
                //    Pct.Add(rigaPct[ncol].ToString());
                //    range.Add(rigarange[ncol].ToString());
                //    minimo.Add(rigaMinimo[ncol].ToString());
                //    massimo.Add(rigaMassimo[ncol].ToString());
                //}
                //filename = bll.CreaPDFSpessore(IDDETTAGLIO, tipoCertificato, _DS, Contesto.PathCollaudo, iLogo, iBowman, chkCopiaReferto.Checked, Contesto.GetPathRefertiLaboratorio(brand),
                //    medie, Std, Pct, range, minimo, massimo, brand, ""); //txtNumeroCampioni.Text);


                DataTable dtAggregati = _dsServizio.Tables[tblAggregati];
                filename = bll.CreaPDFSpessore_new(IDDETTAGLIO, tipoCertificato, _DS, Contesto.PathCollaudo, iLogo, iBowman, chkCopiaReferto.Checked, 
                    Contesto.GetPathRefertiLaboratorio(brand),  dtAggregati, brand, ""); //txtNumeroCampioni.Text);



                if (chkApriPDF.Checked)
                {
                    Process.Start(filename);
                }
                evidenziaPDFFatti();
            //}
            //catch (Exception ex)
            //{
            //    MostraEccezione(ex, "Errore in crea PDF");
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //}
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
            //try
            //{
            //    lblMessaggio.Text = "";
            //    dgvSpessore.Rows[e.RowIndex].ErrorText = "";

            //    decimal newInteger;

            //    if (dgvSpessore.Rows[e.RowIndex].IsNewRow) { return; }

            //    if (e.ColumnIndex == 0) { return; }

            //    if (!decimal.TryParse(e.FormattedValue.ToString(),
            //        out newInteger) || newInteger < 0)
            //    {
            //        e.Cancel = true;
            //        lblMessaggio.Text = "La cella deve contenere un valore numerico non negativo";
            //        dgvSpessore.Rows[e.RowIndex].ErrorText = "La cella deve contenere un valore numerico non negativo";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MostraEccezione(ex, "Errore nella validazione della cella dvgMisure");
            //}

        }

        private void ddlDataCollaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopolaDDLBrand(ddlDataCollaudo.SelectedItem.ToString());

            dgvDettaglio.DataSource = null;
            dgvDettaglio.DataMember = "";

            dgvSpessore.DataSource = null;
            dgvSpessore.DataMember = "";

            dgvMisure.DataSource = null;
            dgvMisure.DataMember = "";

            dgvAggregati.DataSource = null;
            dgvAggregati.DataMember = "";

        }

        private void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDettaglio.DataSource = null;
            dgvDettaglio.DataMember = "";

            dgvSpessore.DataSource = null;
            dgvSpessore.DataMember = "";

            dgvMisure.DataSource = null;
            dgvMisure.DataMember = "";

            dgvAggregati.DataSource = null;
            dgvAggregati.DataMember = "";
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
