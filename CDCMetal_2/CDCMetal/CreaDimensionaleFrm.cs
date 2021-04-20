using CDCMetal.BLL;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDCMetal
{
    public partial class CreaDimensionaleFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "DIMENSIONALE";
        public CreaDimensionaleFrm()
        {
            InitializeComponent();
        }

        private void CreaDimensionaleFrm_Load(object sender, EventArgs e)
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

            // DataCollaudo dataSelezionata = (DataCollaudo)ddlDataCollaudo.SelectedItem;
            DataCollaudoSTR dataSelezionata = new DataCollaudoSTR(ddlBrand.SelectedItem.ToString(), ddlDataCollaudo.SelectedItem.ToString());
            CDCBLL bll = new CDCBLL();
            _DS = new Entities.CDCDS();
            bll.LeggiCollaudoDaDataSTR(_DS, dataSelezionata);


            if (_DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_DIMEMSIONI(_DS, IDDETTAGLIO);
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

            //dgvDettaglio.Columns[8].Visible = false;
            dgvDettaglio.Columns[9].Visible = false;
            dgvDettaglio.Columns[10].Visible = false;
            //dgvDettaglio.Columns[12].Visible = false;
            dgvDettaglio.Columns[13].Visible = false;
            dgvDettaglio.Columns[14].Visible = false;
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

            bll.CaricaArticoli(_DS);
            bll.CaricaArticoliDimensioni(_DS);

      


            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                string ColoreRiga = "BIANCO";

                string parte = (string)riga.Cells["PARTE"].Value;
                string colore = (string)riga.Cells["COLORE"].Value;

                CDCDS.CDC_ARTICOLIRow arow = _DS.CDC_ARTICOLI.Where(x => x.PARTE == parte && x.COLORE == colore).FirstOrDefault();
                if (arow == null)
                {
                    ColoreRiga = "GIALLO";
                }
                else
                {
                    //cerco
                    decimal IDARTICOLO = (decimal)arow["IDARTICOLO"];
                    CDCDS.CDC_ARTICOLI_DIMENSIONIRow drow = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == IDARTICOLO).FirstOrDefault();

                    if (drow == null)
                    { ColoreRiga = "GIALLO"; }
                }

                if (ColoreRiga == "GIALLO")
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                    riga.Cells[2].Style.BackColor = Color.Yellow;
                }
                else
                {
                    riga.Cells[1].Style.BackColor = Color.White;
                    riga.Cells[2].Style.BackColor = Color.White;
                }

            }


        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataRow r = _DS.CDC_DETTAGLIO.Rows[e.RowIndex];
            decimal IDDETTAGLIO = (decimal)r[0];
            decimal IDPRENOTAZIONE = (decimal)r[1];
            string PARTE = (string)r[7];
            string COLORE = (string)r[8];

            _dsServizio = new DataSet();

            DataTable dtDimensioni = _dsServizio.Tables.Add();
            dtDimensioni.TableName = tableName;
            //dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("IDDIMENSIONALE", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));

            dtDimensioni.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
            dtDimensioni.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));

            dtDimensioni.Columns.Add("RIFERIMENTO", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("GRANDEZZA", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("RICHIESTO", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("TOLLERANZA", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("MINIMO", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("MASSIMO", Type.GetType("System.String")); //7
            dtDimensioni.Columns.Add("TAMPONE", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("CONTAMPONE", Type.GetType("System.Boolean"));
            dtDimensioni.Columns.Add("CONFORME", Type.GetType("System.Boolean"));

            dtDimensioni.Columns.Add("PARTE", Type.GetType("System.String"));
            dtDimensioni.Columns.Add("COLORE", Type.GetType("System.String"));

            List<CDCDS.CDC_DIMEMSIONIRow> dimensioni = _DS.CDC_DIMEMSIONI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).OrderBy(x => x.SEQUENZA).ToList();
            
            // List<CDCDS.CDC_DIMEMSIONI_MISURERow> misure = _DS.CDC_DIMEMSIONI_MISURE.Where(x => x.PARTE == PARTE).ToList();
            List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == PARTE && x.COLORE == COLORE).OrderBy(x => x.SEQUENZA).ToList();


            //for (int i = 0; i < 20; i++)
            //{

            if (dimensioni.Count > 0)
            {
                foreach (CDCDS.CDC_DIMEMSIONIRow dimensione in dimensioni)
                {
                    DataRow riga = dtDimensioni.NewRow();
                    riga[0] = dimensione.IDDIMENSIONALE; // IDPRENOTAZIONE;
                    riga[1] = dimensione.IDDETTAGLIO;

                    riga[13] = PARTE;
                    riga[14] = COLORE;

                    riga[2] = dimensione.SEQUENZA;
                    riga[3] = dimensione.DESCRIZIONE;
                    riga[4] = dimensione.RIFERIMENTO;
                    riga[5] = dimensione.GRANDEZZA;
                    riga[6] = dimensione.RICHIESTO;
                    if (dimensione.IsTOLLERANZANull())
                        riga[7] = string.Empty;
                    else
                        riga[7] = dimensione.TOLLERANZA;

                    if (dimensione.IsMINIMONull())
                        riga[8] = string.Empty;
                    else
                        riga[8] = dimensione.MINIMO;

                    if (dimensione.IsMASSIMONull())
                        riga[9] = string.Empty;
                    else
                        riga[9] = dimensione.MASSIMO;

                    if (dimensione.IsTAMPONENull())
                        riga[10] = string.Empty;
                    else
                        riga[10] = dimensione.TAMPONE;

                    riga[11] = dimensione.CONTAMPONE == "S" ? true : false;
                    riga[12] = dimensione.CONFORME == "S" ? true : false;

                    dtDimensioni.Rows.Add(riga);
                }
            }
            else
            {
                foreach (CDCDS.CDC_ARTICOLIRow art in articoli)
                {
                    decimal IDARTICOLO = (decimal)art["IDARTICOLO"];
                    List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> articoli_dimensioni = _DS.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == IDARTICOLO).ToList();

                    foreach (CDCDS.CDC_ARTICOLI_DIMENSIONIRow art_dim in articoli_dimensioni)
                    {
                        DataRow riga = dtDimensioni.NewRow();
                        riga[0] = -1; // IDDIMENSIONALE;
                        riga[1] = IDDETTAGLIO;

                        riga[13] = PARTE;
                        riga[14] = COLORE;

                        riga[2] = art.SEQUENZA;
                        riga[3] = art.DESCRIZIONE;

                        riga[4] = art_dim.RIFERIMENTO;
                        riga[5] = art_dim.GRANDEZZA;
                        riga[6] = art_dim.RICHIESTO;
                        if (art_dim.IsTOLLERANZANull())
                            riga[7] = string.Empty;
                        else
                            riga[7] = art_dim.TOLLERANZA;

                        if (art_dim.IsMINIMONull())
                            riga[8] = string.Empty;
                        else
                            riga[8] = art_dim.MINIMO;

                        if (art_dim.IsMASSIMONull())
                            riga[9] = string.Empty;
                        else
                            riga[9] = art_dim.MASSIMO;

                        if (art_dim.IsTAMPONENull())
                            riga[10] = string.Empty;
                        else
                            riga[10] = art_dim.TAMPONE;

                        riga[11] = art_dim.CONTAMPONE == "S" ? true : false;
                        riga[12] = false;

                        dtDimensioni.Rows.Add(riga);
                    }
                }

            }

            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tableName;

            dgvMisure.Columns[0].Visible = false;
            dgvMisure.Columns[1].Visible = false;

            dgvMisure.Columns[13].Visible = false;
            dgvMisure.Columns[14].Visible = false;
            dgvMisure.Columns[3].Width = 120;
            dgvMisure.Columns[5].Width = 120;
            dgvMisure.Columns[10].Width = 220;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[4]).MaxInputLength = 2;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[5]).MaxInputLength = 30;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[6]).MaxInputLength = 5;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[7]).MaxInputLength = 5;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[8]).MaxInputLength = 5;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[9]).MaxInputLength = 5;
            //((DataGridViewTextBoxColumn)dgvMisure.Columns[10]).MaxInputLength = 50;

            dgvMisure.Columns[2].ReadOnly = true;
            dgvMisure.Columns[3].ReadOnly = true;
            dgvMisure.Columns[4].ReadOnly = true;
            dgvMisure.Columns[5].ReadOnly = true;
            dgvMisure.Columns[6].ReadOnly = true;
            dgvMisure.Columns[7].ReadOnly = true;
            dgvMisure.Columns[8].ReadOnly = true;
            dgvMisure.Columns[9].ReadOnly = true;
            dgvMisure.Columns[10].ReadOnly = true;
            dgvMisure.Columns[11].ReadOnly = true;


        }

        private void btnCreaPDF_Click(object sender, EventArgs e)
        {
            if (_dsServizio.Tables[tableName].Rows.Count == 0)
                return;

            lblMessaggio.Text = string.Empty;
            decimal IDDETTAGLIO = -1;

            foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
            {
                decimal IDDIMENSIONALE = (decimal)riga[0];
                if (IDDIMENSIONALE == -1) { IDDIMENSIONALE = -100; } //
                IDDETTAGLIO = (decimal)riga[1];
                string RIFERIMENTO = ConvertiInStringa(riga[4]);
                RIFERIMENTO = RIFERIMENTO.ToUpper().Trim();
                string PARTE = (string)riga[13];
                string COLORE = (string)riga[14];
                //if (IDDIMENSIONALE != -1)
                //{
                    //CDCDS.CDC_DIMEMSIONIRow dimensione = _DS.CDC_DIMEMSIONI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO && x.RIFERIMENTO.Trim() == RIFERIMENTO).FirstOrDefault();
                    CDCDS.CDC_DIMEMSIONIRow dimensione = _DS.CDC_DIMEMSIONI.Where(x => x.IDDIMENSIONALE == IDDIMENSIONALE).FirstOrDefault();
                    if (dimensione == null)
                    {
                        dimensione = _DS.CDC_DIMEMSIONI.NewCDC_DIMEMSIONIRow();

                        dimensione.IDDETTAGLIO = (decimal)riga[1];
                        dimensione.SEQUENZA = (Int32)riga[2];
                        dimensione.CONFORME = ConvertiBoolInStringa(riga[12]);
                        dimensione.CONTAMPONE = ConvertiBoolInStringa(riga[11]);
                        dimensione.DESCRIZIONE = ((string)riga[3]).ToUpper().Trim();
                        dimensione.GRANDEZZA = ((string)riga[5]).ToUpper().Trim();                        
                        dimensione.MASSIMO = ConvertiInStringa(riga[9]).ToUpper().Trim();
                        dimensione.MINIMO = ConvertiInStringa(riga[8]).ToUpper().Trim();
                        dimensione.RICHIESTO = ((string)riga[6]).ToUpper().Trim();
                        dimensione.RIFERIMENTO = ((string)riga[4]).ToUpper().Trim();
                        dimensione.TAMPONE = ConvertiInStringa(riga[10]).ToUpper().Trim();
                        dimensione.TOLLERANZA = ConvertiInStringa(riga[7]).ToUpper().Trim();

                        dimensione.DATAINSERIMENTO = DateTime.Now;
                        dimensione.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        _DS.CDC_DIMEMSIONI.AddCDC_DIMEMSIONIRow(dimensione);
                    }
                    else
                    {
                        
                        dimensione.IDDETTAGLIO = (decimal)riga[1];
                        dimensione.SEQUENZA = (Int32)riga[2];
                        dimensione.CONFORME = ConvertiBoolInStringa(riga[12]);
                        dimensione.CONTAMPONE = ConvertiBoolInStringa(riga[11]);
                        dimensione.DESCRIZIONE = ((string)riga[3]).ToUpper().Trim();
                        dimensione.GRANDEZZA = ((string)riga[5]).ToUpper().Trim();
                        dimensione.MASSIMO = ConvertiInStringa(riga[9]).ToUpper().Trim();
                        dimensione.MINIMO = ConvertiInStringa(riga[8]).ToUpper().Trim();
                        dimensione.RICHIESTO = ((string)riga[6]).ToUpper().Trim();
                        dimensione.RIFERIMENTO = ((string)riga[4]).ToUpper().Trim();
                        dimensione.TAMPONE = ConvertiInStringa(riga[10]).ToUpper().Trim();
                        dimensione.TOLLERANZA = ConvertiInStringa(riga[7]).ToUpper().Trim();

                        dimensione.DATAINSERIMENTO = DateTime.Now;
                        dimensione.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    }

                    //CDCDS.CDC_DIMEMSIONI_MISURERow misura = _DS.CDC_DIMEMSIONI_MISURE.Where(x => x.PARTE == PARTE && x.RIFERIMENTO.Trim() == RIFERIMENTO).FirstOrDefault();
                    //if (misura == null)
                    //{
                    //    misura = _DS.CDC_DIMEMSIONI_MISURE.NewCDC_DIMEMSIONI_MISURERow();
                    //    misura.PARTE = PARTE;
                    //    misura.CONTAMPONE = ConvertiBoolInStringa(riga[9]);
                    //    misura.GRANDEZZA = ((string)riga[3]).ToUpper().Trim();
                    //    misura.MASSIMO = ConvertiInStringa(riga[7]).ToUpper().Trim();
                    //    misura.MINIMO = ConvertiInStringa(riga[6]).ToUpper().Trim();
                    //    misura.RICHIESTO = ((string)riga[4]).ToUpper().Trim();
                    //    misura.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                    //    misura.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                    //    misura.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                    //    misura.DATAINSERIMENTO = DateTime.Now;
                    //    misura.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    //    _DS.CDC_DIMEMSIONI_MISURE.AddCDC_DIMEMSIONI_MISURERow(misura);
                    //}
                    //else
                    //{
                    //    misura.CONTAMPONE = ConvertiBoolInStringa(riga[9]);
                    //    misura.GRANDEZZA = ((string)riga[3]).ToUpper().Trim();
                    //    misura.MASSIMO = ConvertiInStringa(riga[7]).ToUpper().Trim();
                    //    misura.MINIMO = ConvertiInStringa(riga[6]).ToUpper().Trim();
                    //    misura.RICHIESTO = ((string)riga[4]).ToUpper().Trim();
                    //    misura.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                    //    misura.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                    //    misura.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                    //    misura.DATAINSERIMENTO = DateTime.Now;
                    //    misura.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    //}
                //}
            }

            CDCBLL bll = new CDCBLL();
            bll.SalvaDatiDimensioni(_DS);
            _DS.CDC_DIMEMSIONI.AcceptChanges();
           // _DS.CDC_DIMEMSIONI_MISURE.AcceptChanges();

            Bitmap firma = Properties.Resources.firma_vittoria;
            if (Contesto.Utente.IDUSER == "0000000122")
                firma = Properties.Resources.firma_celeste;

            ImageConverter converter = new ImageConverter();
            byte[] iFirma = (byte[])converter.ConvertTo(firma, typeof(byte[]));

            Bitmap loghi = Properties.Resources.loghi;

            byte[] iLoghi = (byte[])converter.ConvertTo(loghi, typeof(byte[]));

            bll.CreaPDFDimensionale(IDDETTAGLIO, _DS, Contesto.Utente.FULLNAMEUSER, Contesto.PathCollaudo, iFirma, iLoghi, Contesto.Utente.FULLNAMEUSER);

            if (chkCopiaSchedaTecnica.Checked)
            {
                DataRow riga = _dsServizio.Tables[tableName].Rows[0];
                string PARTE = (string)riga[13];
                string COLORE = (string)riga[14];

                string filename = string.Format(@"{0}\{1}-{2}.pdf", Contesto.PathSchedeTecniche, PARTE, COLORE);

                if (!File.Exists(filename))
                {
                    string messaggio = string.Format("Impossibile trovare il file per PARTE:{0} e COLORE:{1}. File: {2}", PARTE, COLORE, filename);
                    MessageBox.Show(messaggio);
                    return;
                }

                CDCDS.CDC_DETTAGLIORow dettaglio = _DS.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string cartellaDestinazione = CDCBLL.CreaPathCartella(dt, Contesto.PathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);

                string destinazione = string.Format(@"{0}\{1}-{2}.pdf", cartellaDestinazione, PARTE, COLORE);
                if (File.Exists(destinazione))
                    File.Delete(destinazione);

                File.Copy(filename, destinazione, true);
            }

        }

        private void ddlDataCollaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopolaDDLBrand(ddlDataCollaudo.SelectedItem.ToString());
        }


        //private void CaricaDimensionale(CDCDS ds)
        //{


        //    //CDCBLL bll = new CDCBLL();
        //    //_DS = new Entities.CDCDS();

        //    if (ds.CDC_DETTAGLIO.Count == 0)
        //        return;


        //    _dsServizio = new DataSet();
        //    DataTable dtDimensioni = _dsServizio.Tables.Add();
        //    dtDimensioni.TableName = tableName;
        //    //dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
        //    dtDimensioni.Columns.Add("IDDIMENSIONALE", Type.GetType("System.Decimal"));
        //    dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));

        //    dtDimensioni.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
        //    dtDimensioni.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));

        //    dtDimensioni.Columns.Add("RIFERIMENTO", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("GRANDEZZA", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("RICHIESTO", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("TOLLERANZA", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("MINIMO", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("MASSIMO", Type.GetType("System.String")); //7
        //    dtDimensioni.Columns.Add("TAMPONE", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("CONTAMPONE", Type.GetType("System.Boolean"));
        //    dtDimensioni.Columns.Add("CONFORME", Type.GetType("System.Boolean"));

        //    dtDimensioni.Columns.Add("PARTE", Type.GetType("System.String"));
        //    dtDimensioni.Columns.Add("COLORE", Type.GetType("System.String"));

        //    foreach (CDCDS.CDC_DETTAGLIORow dr in ds.CDC_DETTAGLIO.Rows)
        //    {
        //        decimal IDDETTAGLIO = (decimal)dr["IDDETTAGLIO"];
        //        //decimal IDPRENOTAZIONE = (decimal)dr["IDPRENOTAZIONE"];
        //        string PARTE = (string)dr["PARTE"];
        //        string COLORE = (string)dr["COLORE"];

        //        List<CDCDS.CDC_DIMEMSIONIRow> dimens = ds.CDC_DIMEMSIONI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).ToList();
        //        if (dimens.Count > 0)
        //        {
        //            foreach (CDCDS.CDC_DIMEMSIONIRow dimrow in dimens)
        //            {
        //                DataRow riga = dtDimensioni.NewRow();
        //                riga["PARTE"] = PARTE;
        //                riga["COLORE"] = COLORE;
        //                riga["IDDIMENSIONALE"] = dimrow.IDDIMENSIONALE;
        //                riga["IDDETTAGLIO"] = dimrow.IDDETTAGLIO;
        //                riga["SEQUENZA"] = dimrow.SEQUENZA;
        //                riga["DESCRIZIONE"] = dimrow.DESCRIZIONE;
        //                riga["RIFERIMENTO"] = dimrow.RIFERIMENTO;
        //                riga["GRANDEZZA"] = dimrow.GRANDEZZA;
        //                riga["RICHIESTO"] = dimrow.RICHIESTO;
        //                riga["TOLLERANZA"] = dimrow.TOLLERANZA;
        //                riga["MINIMO"] = dimrow.MINIMO;
        //                riga["MASSIMO"] = dimrow.MASSIMO;
        //                riga["TAMPONE"] = dimrow.TAMPONE;
        //                riga["CONTAMPONE"] = dimrow.CONTAMPONE == "S" ? true : false; 
        //                riga["CONFORME"] = dimrow.CONFORME == "S" ? true : false;

        //                dtDimensioni.Rows.Add(riga);
        //            }
        //        }
        //        else
        //        {
        //            List<CDCDS.CDC_ARTICOLIRow> articoli = ds.CDC_ARTICOLI.Where(x => x.PARTE == PARTE && x.COLORE == COLORE).ToList();

        //            foreach (CDCDS.CDC_ARTICOLIRow art in articoli)
        //            {
        //                decimal IDARTICOLO = (decimal)art["IDARTICOLO"];
        //                List<CDCDS.CDC_ARTICOLI_DIMENSIONIRow> articoli_dimensioni = ds.CDC_ARTICOLI_DIMENSIONI.Where(x => x.IDARTICOLO == IDARTICOLO).ToList();

        //                foreach (CDCDS.CDC_ARTICOLI_DIMENSIONIRow art_dim in articoli_dimensioni)
        //                {
        //                    DataRow riga = dtDimensioni.NewRow();

        //                    riga["PARTE"] = PARTE;
        //                    riga["COLORE"] = COLORE;
        //                    riga["IDDETTAGLIO"] = IDDETTAGLIO;

        //                    riga["SEQUENZA"] = art.SEQUENZA;
        //                    riga["DESCRIZIONE"] = art.DESCRIZIONE;
        //                    riga["RIFERIMENTO"] = art_dim.RIFERIMENTO;
        //                    riga["GRANDEZZA"] = art_dim.GRANDEZZA;
        //                    riga["RICHIESTO"] = art_dim.RICHIESTO;
        //                    riga["TOLLERANZA"] = art_dim.TOLLERANZA;
        //                    riga["MINIMO"] = art_dim.MINIMO;
        //                    riga["MASSIMO"] = art_dim.MASSIMO;
        //                    riga["TAMPONE"] = art_dim.TAMPONE;
        //                    riga["CONTAMPONE"] = art_dim.CONTAMPONE == "S" ? true : false; 
        //                    riga["CONFORME"] = false;

        //                    dtDimensioni.Rows.Add(riga);
        //                }
        //            }
        //        }

        //    }

        //}



    }
}

