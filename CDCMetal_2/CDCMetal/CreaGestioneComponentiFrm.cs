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
    public partial class CreaGestioneComponentiFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "DETTAGLIO";
        private string selPARTE = "";
        private string selCOLORE = "";
        private decimal selIDARTICOLO = 0;

        public CreaGestioneComponentiFrm()
        {
            InitializeComponent();
        }
        private void CreaGestioneComponentiFrm_Load(object sender, EventArgs e)
        {
            PopolaDDLDate();
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

        private void ddlDataCollaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopolaDDLBrand(ddlDataCollaudo.SelectedItem.ToString());
        }

        private void btnLeggiDati_Click(object sender, EventArgs e)
        {

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

            DataCollaudoSTR dataSelezionata = new DataCollaudoSTR(ddlBrand.SelectedItem.ToString(), ddlDataCollaudo.SelectedItem.ToString());
            CDCBLL bll = new CDCBLL();
            _DS = new Entities.CDCDS();
            bll.LeggiCollaudoDaDataSTR(_DS, dataSelezionata);
            bll.CaricaBrands(_DS);
            bll.CaricaArticoli(_DS);
            bll.CaricaArticoliDimensioni(_DS);
            bll.CaricaArticoliSpessori(_DS);

            if (_DS.CDC_DETTAGLIO.Count > 0)
            {
                //List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                //bll.FillCDC_CONFORMITA(_DS, IDDETTAGLIO);
                //bll.CDC_PDF(_DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerDettaglio();




            dgvDettaglio.ReadOnly = true;
            dgvDettaglio.AllowUserToDeleteRows = false;
            dgvDettaglio.AllowUserToAddRows = false;
            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;
            dgvDettaglio.Columns["ACCESSORISTA"].Visible = false;

            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                string parte = (string)riga.Cells["PARTE"].Value;
                string colore = (string)riga.Cells["COLORE"].Value;

                CDCDS.CDC_ARTICOLIRow arow = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).FirstOrDefault();
                if (arow == null)
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                    riga.Cells[2].Style.BackColor = Color.Yellow;
                }
                else
                {
                    List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> artdimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == arow.IDARTICOLO).OrderBy(x => x.IDDIMENSIONE).ToList();
                    List<CDCDS.CDC_ARTICOLI_SPESSORIRow> artspessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == arow.IDARTICOLO).OrderBy(x => x.IDSPESSORE).ToList();
                    if (artspessori.Count == 0 || artdimensioni.Count == 0)
                    {
                        riga.Cells[1].Style.BackColor = Color.Orange;
                        riga.Cells[2].Style.BackColor = Color.Orange;
                    }
                    else
                    {
                        riga.Cells[1].Style.BackColor = Color.White;
                        riga.Cells[2].Style.BackColor = Color.White;
                    }
                }
            }

            dgvArticoli.DataSource = null;
            dgvArticoli.DataMember = "";

            dgvDimensioni.DataSource = null;
            dgvDimensioni.DataMember = "";

            dgvSpessori.DataSource = null;
            dgvSpessori.DataMember = "";


        }

        private void CreaDsPerDettaglio()
        {

            _dsServizio = new DataSet();
            DataTable dtDettaglio = _dsServizio.Tables.Add();
            dtDettaglio.TableName = tableName;
            dtDettaglio.Columns.Add("ACCESSORISTA", Type.GetType("System.String")).ReadOnly = true;
            dtDettaglio.Columns.Add("PARTE", Type.GetType("System.String"));
            dtDettaglio.Columns.Add("COLORE", Type.GetType("System.String"));
            dtDettaglio.Columns.Add("BRAND", Type.GetType("System.String"));


            foreach (CDCDS.CDC_DETTAGLIORow dett in _DS.CDC_DETTAGLIO)
            {
                string brand = "";
                CDCDS.CDC_BRANDSRow brow = _DS.CDC_BRANDS.Where(x => x.IDBRAND == dett.IDBRAND).FirstOrDefault();
                if (brow != null)
                { brand = brow.CODICE; }

                DataRow riga = dtDettaglio.NewRow();
                riga[0] = dett.ACCESSORISTA;
                riga[1] = dett.PARTE;
                riga[2] = dett.COLORE;
                riga[3] = brand;

                dtDettaglio.Rows.Add(riga);
            }
        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataRow r = _DS.CDC_DETTAGLIO.Rows[e.RowIndex];

            selPARTE = (string)r["PARTE"];
            selCOLORE = (string)r["COLORE"];

            CaricaArticolo(selPARTE, selCOLORE);


            dgvDimensioni.DataSource = null;
            dgvDimensioni.DataMember = "";

            dgvSpessori.DataSource = null;
            dgvSpessori.DataMember = "";

        }

        private void CaricaArticolo(string PARTE, string COLORE)
        {
            _dsServizio = new DataSet();

            DataTable dtArticoli = _dsServizio.Tables.Add();
            dtArticoli.TableName = "ARTICOLI";


            dtArticoli.Columns.Add("IDARTICOLO", Type.GetType("System.Decimal"));
            dtArticoli.Columns.Add("PARTE", Type.GetType("System.String"));
            dtArticoli.Columns.Add("COLORE", Type.GetType("System.String"));
            dtArticoli.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
            dtArticoli.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            dtArticoli.Columns.Add("COLORECOMPONENTE", Type.GetType("System.String"));
            dtArticoli.Columns.Add("MISUREPERCAMPIONE", Type.GetType("System.Int32"));
            dtArticoli.Columns.Add("PESO", Type.GetType("System.Double"));
            dtArticoli.Columns.Add("SFRIDO", Type.GetType("System.Double")); //7
            dtArticoli.Columns.Add("SUPERFICIE", Type.GetType("System.Double"));

            List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == PARTE && x.COLORE == COLORE).ToList();
            if (articoli.Count > 0)
            {
                foreach (CDCDS.CDC_ARTICOLIRow ra in articoli)
                {
                    DataRow riga = dtArticoli.NewRow();
                    riga["IDARTICOLO"] = ra.IDARTICOLO;
                    riga["PARTE"] = ra.PARTE;
                    riga["COLORE"] = ra.COLORE;
                    riga["SEQUENZA"] = ra.SEQUENZA;
                    riga["DESCRIZIONE"] = ra.DESCRIZIONE;
                    riga["COLORECOMPONENTE"] = ra.COLORECOMPONENTE;
                    riga["MISUREPERCAMPIONE"] = ra.MISUREPERCAMPIONE;
                    riga["PESO"] = ra.PESO;
                    riga["SFRIDO"] = ra.SFRIDO;
                    riga["SUPERFICIE"] = ra.SUPERFICIE;

                    dtArticoli.Rows.Add(riga);
                }
            }


            dgvArticoli.ReadOnly = false;
            dgvArticoli.DataSource = _dsServizio;
            dgvArticoli.DataMember = "ARTICOLI";
            dgvArticoli.AutoGenerateColumns = true;
            dgvArticoli.AllowUserToAddRows = true;
            dgvArticoli.AllowUserToDeleteRows = true;

            dgvArticoli.Columns[0].Visible = false;
            dgvArticoli.Columns[1].Visible = false;
            dgvArticoli.Columns[2].Visible = false;

          
        }


        private void buSalva_Click(object sender, EventArgs e)
        {
            string tableName = "ARTICOLI";
            if (_dsServizio.Tables[tableName].Rows.Count == 0)
                return;



            try
            {

                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    //decimal IDARTICOLO =  (decimal)riga["IDARTICOLO"];
                    // string RIFERIMENTO = (string)riga["PARTE"];


                    //PARTE = (string)riga["PARTE"];
                    //COLORE = (string)riga["COLORE"];

                    // if (!string.IsNullOrEmpty(riga["PARTE"])
                    if (riga["IDARTICOLO"] != DBNull.Value)
                    {
                        decimal IDARTICOLO = (decimal)riga["IDARTICOLO"];

                        // && x.RIFERIMENTO.Trim() == RIFERIMENTO
                        CDCDS.CDC_ARTICOLIRow articolo = _DS.CDC_ARTICOLI.Where(x => x.IDARTICOLO == IDARTICOLO).FirstOrDefault();
                        if (articolo == null)
                        {

                            articolo = _DS.CDC_ARTICOLI.NewCDC_ARTICOLIRow();

                            articolo.PARTE = selPARTE;
                            articolo.COLORE = selCOLORE;

                            //articolo.PARTE = ((string)riga["PARTE"]).ToUpper().Trim();
                            //articolo.COLORE = ((string)riga["COLORE"]).ToUpper().Trim();
                            articolo.SEQUENZA = (Int32)riga["SEQUENZA"];
                            articolo.DESCRIZIONE = ((string)riga["DESCRIZIONE"]).ToUpper().Trim();
                            articolo.COLORECOMPONENTE = ((string)riga["COLORECOMPONENTE"]).ToUpper().Trim();
                            articolo.MISUREPERCAMPIONE = (Int32)riga["MISUREPERCAMPIONE"];
                            articolo.PESO = (double)riga["PESO"];
                            articolo.SFRIDO = (double)riga["SFRIDO"];
                            articolo.SUPERFICIE = (double)riga["SUPERFICIE"];

                            //articolo.IDDETTAGLIO = IDDETTAGLIO;

                            //articolo.MINIMO = ConvertiInStringa(riga[""]).ToUpper().Trim();

                            //articolo.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                            //articolo.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                            //articolo.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                            articolo.DATARIFERIMENTO = DateTime.Now;
                            articolo.UTENTE = Contesto.Utente.FULLNAMEUSER;
                            _DS.CDC_ARTICOLI.AddCDC_ARTICOLIRow(articolo);
                        }
                        else
                        {
                            articolo.PARTE = ((string)riga["PARTE"]).ToUpper().Trim();
                            articolo.COLORE = ((string)riga["COLORE"]).ToUpper().Trim();
                            articolo.SEQUENZA = (Int32)riga["SEQUENZA"];
                            articolo.DESCRIZIONE = ((string)riga["DESCRIZIONE"]).ToUpper().Trim();
                            articolo.COLORECOMPONENTE = ((string)riga["COLORECOMPONENTE"]).ToUpper().Trim();
                            articolo.MISUREPERCAMPIONE = (Int32)riga["MISUREPERCAMPIONE"];
                            articolo.PESO = (double)riga["PESO"];
                            articolo.SFRIDO = (double)riga["SFRIDO"];
                            articolo.SUPERFICIE = (double)riga["SUPERFICIE"];
                            articolo.DATARIFERIMENTO = DateTime.Now;
                            articolo.UTENTE = Contesto.Utente.FULLNAMEUSER;


                        }

                    }
                    else
                    {


                        CDCDS.CDC_ARTICOLIRow articolo = _DS.CDC_ARTICOLI.NewCDC_ARTICOLIRow();

                        articolo.PARTE = selPARTE;
                        articolo.COLORE = selCOLORE;
                        articolo.SEQUENZA = (Int32)riga["SEQUENZA"];
                        articolo.DESCRIZIONE = ((string)riga["DESCRIZIONE"]).ToUpper().Trim();

                        articolo.COLORECOMPONENTE = ((string)riga["COLORECOMPONENTE"]).ToUpper().Trim();
                        articolo.MISUREPERCAMPIONE = (Int32)riga["MISUREPERCAMPIONE"];
                        articolo.PESO = (double)riga["PESO"];
                        articolo.SFRIDO = (double)riga["SFRIDO"];
                        articolo.SUPERFICIE = (double)riga["SUPERFICIE"];

                        articolo.DATARIFERIMENTO = DateTime.Now;
                        articolo.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        _DS.CDC_ARTICOLI.AddCDC_ARTICOLIRow(articolo);
                    }
                }


                //cerco i cancellati e li marco 
                List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.RowState != DataRowState.Added  && x.PARTE == selPARTE && x.COLORE == selCOLORE).ToList();
                foreach (CDCDS.CDC_ARTICOLIRow articolo in articoli)
                {
                        DataRow[] art = _dsServizio.Tables["ARTICOLI"].Select("IDARTICOLO = " + articolo.IDARTICOLO.ToString());

                        if (art.GetLength(0) == 0)
                        {
                            articolo.DATARIFERIMENTO = DateTime.Now;
                            articolo.UTENTE = Contesto.Utente.FULLNAMEUSER;
                            articolo.DELETED = "S";
                        }
                }


                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiArticoli(_DS);
                _DS.CDC_ARTICOLI.AcceptChanges();

                _DS.CDC_ARTICOLI.Clear();
                bll.CaricaArticoli(_DS);
                CaricaArticolo(selPARTE, selCOLORE);


                dgvDimensioni.DataSource = null;
                dgvDimensioni.DataMember = "";

                dgvSpessori.DataSource = null;
                dgvSpessori.DataMember = "";

            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in crea Salva Articoli");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }



            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                string parte = (string)riga.Cells["PARTE"].Value;
                string colore = (string)riga.Cells["COLORE"].Value;

                CDCDS.CDC_ARTICOLIRow arow = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).FirstOrDefault();
                if (arow == null)
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                    riga.Cells[2].Style.BackColor = Color.Yellow;
                }
                else
                {
                    List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> artdimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == arow.IDARTICOLO).OrderBy(x => x.IDDIMENSIONE).ToList();
                    List<CDCDS.CDC_ARTICOLI_SPESSORIRow> artspessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == arow.IDARTICOLO).OrderBy(x => x.IDSPESSORE).ToList();
                    if (artspessori.Count == 0 || artdimensioni.Count == 0)
                    {
                        riga.Cells[1].Style.BackColor = Color.Orange;
                        riga.Cells[2].Style.BackColor = Color.Orange;
                    }
                    else
                    {
                        riga.Cells[1].Style.BackColor = Color.White;
                        riga.Cells[2].Style.BackColor = Color.White;
                    }
                }
            }

        }

        private void dgvArticoli_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selIDARTICOLO = 0;

            try
            {
                if ((e.RowIndex == -1) || (e.RowIndex > _dsServizio.Tables["ARTICOLI"].Rows.Count - 1))
                {
                    dgvDimensioni.DataSource = null;
                    dgvDimensioni.DataMember = "";
                    dgvSpessori.DataSource = null;
                    dgvSpessori.DataMember = "";
                    //selIDARTICOLO = 0;
                    //CaricaDimensioniSpessori(selIDARTICOLO);
                    return;
                }

                DataRow r = _dsServizio.Tables["ARTICOLI"].Rows[e.RowIndex];
                selIDARTICOLO = (decimal)r["IDARTICOLO"];
                CaricaDimensioniSpessori(selIDARTICOLO);
            }
            catch (Exception ex)
            { }

        }

        private void CaricaDimensioniSpessori(decimal IDARTICOLO)
        {


            DataTable dtArticoli_Dimensioni = _dsServizio.Tables["ARTICOLI_DIMENSIONI"];
            if (dtArticoli_Dimensioni != null)
            { dtArticoli_Dimensioni.Clear(); }
            else
            {
                dtArticoli_Dimensioni = _dsServizio.Tables.Add();
                dtArticoli_Dimensioni.TableName = "ARTICOLI_DIMENSIONI";

                dtArticoli_Dimensioni.Columns.Add("IDDIMENSIONE", Type.GetType("System.Decimal"));
                dtArticoli_Dimensioni.Columns.Add("IDARTICOLO", Type.GetType("System.Decimal"));
                dtArticoli_Dimensioni.Columns.Add("RIFERIMENTO", Type.GetType("System.String"));
                dtArticoli_Dimensioni.Columns.Add("GRANDEZZA", Type.GetType("System.String"));
                dtArticoli_Dimensioni.Columns.Add("RICHIESTO", Type.GetType("System.Double"));
                dtArticoli_Dimensioni.Columns.Add("TOLLERANZA", Type.GetType("System.Double"));
                dtArticoli_Dimensioni.Columns.Add("MINIMO", Type.GetType("System.Double"));
                dtArticoli_Dimensioni.Columns.Add("MASSIMO", Type.GetType("System.Double"));
                dtArticoli_Dimensioni.Columns.Add("TAMPONE", Type.GetType("System.String"));
                dtArticoli_Dimensioni.Columns.Add("CONTAMPONE", Type.GetType("System.Boolean"));
            }



            List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> articoli_dimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == IDARTICOLO).ToList();
            if (articoli_dimensioni.Count > 0)
            {
                foreach (CDCDS.CDC_ARTICOLI_DIMENSIONIRow ra in articoli_dimensioni)
                {
                    DataRow riga = dtArticoli_Dimensioni.NewRow();
                    riga["IDDIMENSIONE"] = ra.IDDIMENSIONE;
                    riga["IDARTICOLO"] = ra.IDARTICOLO;
                    riga["RIFERIMENTO"] = ra.RIFERIMENTO;
                    riga["GRANDEZZA"] = ra.GRANDEZZA;
                    riga["RICHIESTO"] = ra.RICHIESTO;
                    riga["TOLLERANZA"] = ra.TOLLERANZA;
                    riga["MINIMO"] = ra.MINIMO;
                    riga["MASSIMO"] = ra.MASSIMO;
                    riga["TAMPONE"] = ra.TAMPONE;
                    riga["CONTAMPONE"] = ra.CONTAMPONE == "S" ? true : false; ;

                    dtArticoli_Dimensioni.Rows.Add(riga);
                }
            }


            dgvDimensioni.ReadOnly = false;
            dgvDimensioni.DataSource = _dsServizio;
            dgvDimensioni.DataMember = "ARTICOLI_DIMENSIONI";
            dgvDimensioni.AutoGenerateColumns = true;
            dgvDimensioni.AllowUserToAddRows = true;
            dgvDimensioni.AllowUserToDeleteRows = true;

            dgvDimensioni.Columns[0].Visible = false;
            dgvDimensioni.Columns[1].Visible = false;


            ///spessori
            ///
            DataTable dtArticoli_Spessori = _dsServizio.Tables["ARTICOLI_SPESSORI"];
            if (dtArticoli_Spessori != null)
            { dtArticoli_Spessori.Clear(); }
            else
            {
                dtArticoli_Spessori = _dsServizio.Tables.Add();
                dtArticoli_Spessori.TableName = "ARTICOLI_SPESSORI";

                dtArticoli_Spessori.Columns.Add("IDSPESSORE", Type.GetType("System.Decimal"));
                dtArticoli_Spessori.Columns.Add("IDARTICOLO", Type.GetType("System.Decimal"));
                dtArticoli_Spessori.Columns.Add("ETICHETTA", Type.GetType("System.String"));
                dtArticoli_Spessori.Columns.Add("MINIMO", Type.GetType("System.Double"));
                dtArticoli_Spessori.Columns.Add("NOMINALE", Type.GetType("System.Double"));
                dtArticoli_Spessori.Columns.Add("MASSIMO", Type.GetType("System.Double"));

            }

            List<CDCDS.CDC_ARTICOLI_SPESSORIRow> articoli_spessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == IDARTICOLO).ToList();
            if (articoli_spessori.Count > 0)
            {
                foreach (CDCDS.CDC_ARTICOLI_SPESSORIRow ra in articoli_spessori)
                {
                    DataRow riga = dtArticoli_Spessori.NewRow();
                    riga["IDSPESSORE"] = ra.IDSPESSORE;
                    riga["IDARTICOLO"] = ra.IDARTICOLO;
                    riga["ETICHETTA"] = ra.ETICHETTA;
                    riga["MINIMO"] = ra.MINIMO;
                    riga["NOMINALE"] = ra.NOMINALE;
                    riga["MASSIMO"] = ra.MASSIMO;

                    dtArticoli_Spessori.Rows.Add(riga);
                }
            }

            dgvSpessori.ReadOnly = false;
            dgvSpessori.DataSource = _dsServizio;
            dgvSpessori.DataMember = "ARTICOLI_SPESSORI";
            dgvSpessori.AutoGenerateColumns = true;
            dgvSpessori.AllowUserToAddRows = true;
            dgvSpessori.AllowUserToDeleteRows = true;

            dgvSpessori.Columns[0].Visible = false;
            dgvSpessori.Columns[1].Visible = false;


        }



        private void buSalvaDimensioniSpessori_Click(object sender, EventArgs e)
        {
            string tableName1 = "ARTICOLI_DIMENSIONI";
            string tableName2 = "ARTICOLI_SPESSORI";
            //if (_dsServizio.Tables[tableName1].Rows.Count == 0 && _dsServizio.Tables[tableName2].Rows.Count == 0)
            //    return;



            try
            {
                //DIMENSIONI
                foreach (DataRow riga in _dsServizio.Tables[tableName1].Rows)
                {
                    if (riga["IDDIMENSIONE"] != DBNull.Value)
                    {
                        decimal IDDIMENSIONE = (decimal)riga["IDDIMENSIONE"];

                        // && x.RIFERIMENTO.Trim() == RIFERIMENTO
                        CDCDS.CDC_ARTICOLI_DIMENSIONIRow articolo_dimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDDIMENSIONE == IDDIMENSIONE).FirstOrDefault();
                        if (articolo_dimensioni == null)
                        {

                            articolo_dimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.NewCDC_ARTICOLI_DIMENSIONIRow();

                            articolo_dimensioni.IDARTICOLO = ((decimal)riga["IDARTICOLO"]);

                            articolo_dimensioni.RIFERIMENTO = ((string)riga["RIFERIMENTO"]).ToUpper().Trim();
                            articolo_dimensioni.GRANDEZZA = ((string)riga["GRANDEZZA"]).ToUpper().Trim();
                            articolo_dimensioni.RICHIESTO = (double)riga["RICHIESTO"];
                            articolo_dimensioni.TOLLERANZA = (double)riga["TOLLERANZA"];
                            articolo_dimensioni.MINIMO = (double)riga["MINIMO"];
                            articolo_dimensioni.MASSIMO = (double)riga["MASSIMO"];
                            articolo_dimensioni.TAMPONE = ConvertiInStringa(riga["TAMPONE"]).ToUpper().Trim();
                            articolo_dimensioni.CONTAMPONE = ConvertiBoolInStringa(riga["CONTAMPONE"]); // ((string)riga["CONTAMPONE"]).ToUpper().Trim();

                            articolo_dimensioni.DATAINSERIMENTO = DateTime.Now;
                            articolo_dimensioni.UTENTE = Contesto.Utente.FULLNAMEUSER;
                            _DS.CDC_ARTICOLI_DIMENSIONI.AddCDC_ARTICOLI_DIMENSIONIRow(articolo_dimensioni);
                        }
                        else
                        {
                            articolo_dimensioni.IDARTICOLO = ((decimal)riga["IDARTICOLO"]);

                            articolo_dimensioni.RIFERIMENTO = ((string)riga["RIFERIMENTO"]).ToUpper().Trim();
                            articolo_dimensioni.GRANDEZZA = ((string)riga["GRANDEZZA"]).ToUpper().Trim();
                            articolo_dimensioni.RICHIESTO = (double)riga["RICHIESTO"];
                            articolo_dimensioni.TOLLERANZA = (double)riga["TOLLERANZA"];
                            articolo_dimensioni.MINIMO = (double)riga["MINIMO"];
                            articolo_dimensioni.MASSIMO = (double)riga["MASSIMO"];
                            articolo_dimensioni.TAMPONE = ConvertiInStringa(riga["TAMPONE"]).ToUpper().Trim();
                            articolo_dimensioni.CONTAMPONE = ConvertiBoolInStringa(riga["CONTAMPONE"]); //((string)riga["CONTAMPONE"]).ToUpper().Trim();
                            articolo_dimensioni.DATAINSERIMENTO = DateTime.Now;
                            articolo_dimensioni.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        }

                    }
                    else
                    {


                        CDCDS.CDC_ARTICOLI_DIMENSIONIRow articolo_dimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.NewCDC_ARTICOLI_DIMENSIONIRow();

                        articolo_dimensioni.IDARTICOLO = selIDARTICOLO;

                        articolo_dimensioni.RIFERIMENTO = ((string)riga["RIFERIMENTO"]).ToUpper().Trim();
                        articolo_dimensioni.GRANDEZZA = ((string)riga["GRANDEZZA"]).ToUpper().Trim();
                        articolo_dimensioni.RICHIESTO = (double)riga["RICHIESTO"];
                        articolo_dimensioni.TOLLERANZA = (double)riga["TOLLERANZA"];
                        articolo_dimensioni.MINIMO = (double)riga["MINIMO"];
                        articolo_dimensioni.MASSIMO = (double)riga["MASSIMO"];
                        articolo_dimensioni.TAMPONE = ConvertiInStringa(riga["TAMPONE"]).ToUpper().Trim();
                        articolo_dimensioni.CONTAMPONE = ConvertiBoolInStringa(riga["CONTAMPONE"]); //((string)riga["CONTAMPONE"]).ToUpper().Trim();
                        articolo_dimensioni.DATAINSERIMENTO = DateTime.Now;
                        articolo_dimensioni.UTENTE = Contesto.Utente.FULLNAMEUSER;

                        _DS.CDC_ARTICOLI_DIMENSIONI.AddCDC_ARTICOLI_DIMENSIONIRow(articolo_dimensioni);
                    }
                }

                //SPESSORI
                foreach (DataRow riga in _dsServizio.Tables[tableName2].Rows)
                {
                    if (riga["IDSPESSORE"] != DBNull.Value)
                    {
                        decimal IDSPESSORE = (decimal)riga["IDSPESSORE"];

                        // && x.RIFERIMENTO.Trim() == RIFERIMENTO
                        CDCDS.CDC_ARTICOLI_SPESSORIRow articolo_spessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDSPESSORE == IDSPESSORE).FirstOrDefault();
                        if (articolo_spessori == null)
                        {

                            articolo_spessori = _DS.CDC_ARTICOLI_SPESSORI.NewCDC_ARTICOLI_SPESSORIRow();

                            articolo_spessori.IDARTICOLO = ((decimal)riga["IDARTICOLO"]);

                            articolo_spessori.ETICHETTA = ((string)riga["ETICHETTA"]).ToUpper().Trim();
                            articolo_spessori.MINIMO = (double)riga["MINIMO"];
                            articolo_spessori.MASSIMO = (double)riga["MASSIMO"];
                            articolo_spessori.NOMINALE = (double)riga["NOMINALE"];

                            articolo_spessori.DATAINSERIMENTO = DateTime.Now;
                            articolo_spessori.UTENTE = Contesto.Utente.FULLNAMEUSER;
                            _DS.CDC_ARTICOLI_SPESSORI.AddCDC_ARTICOLI_SPESSORIRow(articolo_spessori);
                        }
                        else
                        {
                            articolo_spessori.IDARTICOLO = ((decimal)riga["IDARTICOLO"]);

                            articolo_spessori.ETICHETTA = ((string)riga["ETICHETTA"]).ToUpper().Trim();
                            articolo_spessori.MINIMO = (double)riga["MINIMO"];
                            articolo_spessori.MASSIMO = (double)riga["MASSIMO"];
                            articolo_spessori.NOMINALE = (double)riga["NOMINALE"];
                            articolo_spessori.DATAINSERIMENTO = DateTime.Now;
                            articolo_spessori.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        }

                    }
                    else
                    {


                        CDCDS.CDC_ARTICOLI_SPESSORIRow articolo_spessori = _DS.CDC_ARTICOLI_SPESSORI.NewCDC_ARTICOLI_SPESSORIRow();

                        articolo_spessori.IDARTICOLO = selIDARTICOLO;

                        articolo_spessori.ETICHETTA = ((string)riga["ETICHETTA"]).ToUpper().Trim();
                        articolo_spessori.MINIMO = (double)riga["MINIMO"];
                        articolo_spessori.MASSIMO = (double)riga["MASSIMO"];
                        articolo_spessori.NOMINALE = (double)riga["NOMINALE"];
                        articolo_spessori.DATAINSERIMENTO = DateTime.Now;
                        articolo_spessori.UTENTE = Contesto.Utente.FULLNAMEUSER;

                        _DS.CDC_ARTICOLI_SPESSORI.AddCDC_ARTICOLI_SPESSORIRow(articolo_spessori);
                    }
                }


                //cerco i cancellati e li marco 
                List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> art_dims = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.RowState != DataRowState.Added && x.IDARTICOLO == selIDARTICOLO).ToList();
                foreach (CDCDS.CDC_ARTICOLI_DIMENSIONIRow art_dim in art_dims)
                {
                    DataRow[] art = _dsServizio.Tables["ARTICOLI_DIMENSIONI"].Select("IDDIMENSIONE = " + art_dim.IDDIMENSIONE.ToString());

                    if (art.GetLength(0) == 0)
                    {
                        art_dim.DATAINSERIMENTO = DateTime.Now;
                        art_dim.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        art_dim.DELETED = "S";
                    }
                }

                List<CDCDS.CDC_ARTICOLI_SPESSORIRow> art_spess = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.RowState != DataRowState.Added && x.IDARTICOLO == selIDARTICOLO).ToList();
                foreach (CDCDS.CDC_ARTICOLI_SPESSORIRow art_spes in art_spess)
                {
                    DataRow[] art = _dsServizio.Tables["ARTICOLI_SPESSORI"].Select("IDSPESSORE = " + art_spes.IDSPESSORE.ToString());

                    if (art.GetLength(0) == 0)
                    {
                        art_spes.DATAINSERIMENTO = DateTime.Now;
                        art_spes.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        art_spes.DELETED = "S";
                    }
                }



                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiArticoli_Dimensioni(_DS);
                bll.SalvaDatiArticoli_Spessori(_DS);

                _DS.CDC_ARTICOLI_DIMENSIONI.AcceptChanges();
                _DS.CDC_ARTICOLI_SPESSORI.AcceptChanges();

                _DS.CDC_ARTICOLI_DIMENSIONI.Clear();
                _DS.CDC_ARTICOLI_SPESSORI.Clear();

                bll.CaricaArticoliDimensioni(_DS);
                bll.CaricaArticoliSpessori(_DS);

                CaricaDimensioniSpessori(selIDARTICOLO);

            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in Salva Dimensioni Spessori");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                string parte = (string)riga.Cells["PARTE"].Value;
                string colore = (string)riga.Cells["COLORE"].Value;

                CDCDS.CDC_ARTICOLIRow arow = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).FirstOrDefault();
                if (arow == null)
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                    riga.Cells[2].Style.BackColor = Color.Yellow;
                }
                else
                {
                    List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> artdimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == arow.IDARTICOLO).OrderBy(x => x.IDDIMENSIONE).ToList();
                    List<CDCDS.CDC_ARTICOLI_SPESSORIRow> artspessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == arow.IDARTICOLO).OrderBy(x => x.IDSPESSORE).ToList();
                    if (artspessori.Count == 0 || artdimensioni.Count == 0)
                    {
                        riga.Cells[1].Style.BackColor = Color.Orange;
                        riga.Cells[2].Style.BackColor = Color.Orange;
                    }
                    else
                    {
                        riga.Cells[1].Style.BackColor = Color.White;
                        riga.Cells[2].Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void dgvArticoli_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            object val = dgvArticoli.SelectedRows[0].Cells["IDARTICOLO"].Value;

            if (val.ToString() == "")
                return;

            decimal idarticolo = Convert.ToDecimal(val);

            List<CDCDS.CDC_ARTICOLI_SPESSORIRow> articolo_spessori = _DS.CDC_ARTICOLI_SPESSORI.Where(x => x.IDARTICOLO == idarticolo).ToList();
            List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> articolo_dimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == idarticolo).ToList();


            if (articolo_spessori.Count > 0 || articolo_dimensioni.Count >0)
            {
                MessageBox.Show("Cancellare prima dimensioni e spessori!", "Cancella riga");
                e.Cancel = true;
                return;
            }

            DialogResult response = MessageBox.Show("Sei sicuro?", "Cancella riga", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if ((response == DialogResult.No))
            {
                e.Cancel = true;
            }



        }

        private void dgvDimensioni_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            object val = dgvDimensioni.SelectedRows[0].Cells["IDDIMENSIONE"].Value;

            if (val.ToString() == "")
                return;

            DialogResult response = MessageBox.Show("Sei sicuro?", "Cancella riga", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if ((response == DialogResult.No))
            {
                e.Cancel = true;
            }
        }

        private void dgvSpessori_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            object val = dgvSpessori.SelectedRows[0].Cells["IDSPESSORE"].Value;

            if (val.ToString() == "")
                return;

            DialogResult response = MessageBox.Show("Sei sicuro?", "Cancella riga", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if ((response == DialogResult.No))
            {
                e.Cancel = true;
            }
        }
    }

}
