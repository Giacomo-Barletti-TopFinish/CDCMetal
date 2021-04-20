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
    public partial class CreaColorimetricoFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "COLORIMETRICO";
        private Random _rnd = new Random(DateTime.Now.Millisecond);
        public CreaColorimetricoFrm()
        {
            InitializeComponent();
        }

        private void CreaColorimetricoFrm_Load(object sender, EventArgs e)
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
                bll.FillCDC_COLORE(_DS, IDDETTAGLIO);
                bll.CDC_PDF(_DS, IDDETTAGLIO);

                bll.CaricaArticoli(_DS);
                bll.CaricaColori(_DS, "");
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerColorimetrico();

            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;

            dgvDettaglio.Columns[0].Frozen = true;
            dgvDettaglio.Columns[0].Visible = false;
            dgvDettaglio.Columns[1].Frozen = true;
            dgvDettaglio.Columns[2].Frozen = true;
            dgvDettaglio.Columns[3].Frozen = true;
            dgvDettaglio.Columns[4].Frozen = true;
            dgvDettaglio.Columns[4].Width = 80;
            dgvDettaglio.Columns[5].Frozen = true;
            dgvDettaglio.Columns[5].Width = 80;
            dgvDettaglio.Columns[6].Frozen = true;
            dgvDettaglio.Columns[6].Width = 80;
            dgvDettaglio.Columns[7].Frozen = true;
            dgvDettaglio.Columns[7].Width = 80;
            dgvDettaglio.Columns[8].Frozen = true;
            dgvDettaglio.Columns[9].Frozen = true;
            dgvDettaglio.Columns[9].Width = 80;

            dgvDettaglio.Columns[10].Frozen = true;
            dgvDettaglio.Columns[10].Width = 130;
            dgvDettaglio.Columns[11].Frozen = true;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[14]).HeaderText = "L RICH";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[15]).HeaderText = "L TOLL";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[16]).HeaderText = "L RILE";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[14]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[15]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[16]).MaxInputLength = 7;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[18]).HeaderText = "a RICH";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[19]).HeaderText = "a TOLL";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[20]).HeaderText = "a RILE";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[18]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[19]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[20]).MaxInputLength = 7;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[22]).HeaderText = "b RICH";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[23]).HeaderText = "b TOLL";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[24]).HeaderText = "b RILE";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[22]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[23]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[24]).MaxInputLength = 7;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[26]).MaxInputLength = 50;

            string parte = "";
            string colore = "";

            string colore1 = "GRIGIO";
            string colore2 = "BIANCO";
            string lastCOLOR = colore1;
            string nextCOLOR = colore2;

            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                string ColoreRiga = "BIANCO";

                if (riga.Cells["SEQUENZA"].Value.ToString() == "")
                {
                    ColoreRiga = "GIALLO";
                }
                else
                {
                    if (parte == (string)riga.Cells["PARTE"].Value && colore == (string)riga.Cells["COLORE"].Value)
                    { }
                    else
                    {
                        if (lastCOLOR == colore1)
                        {
                            lastCOLOR = colore2;
                            nextCOLOR = colore1;
                        }
                        else
                        {
                            lastCOLOR = colore1;
                            nextCOLOR = colore2;
                        }

                    }


                    parte = (string)riga.Cells["PARTE"].Value;
                    colore = (string)riga.Cells["COLORE"].Value;
                }

                if (ColoreRiga == "GIALLO")
                {
                    riga.Cells[7].Style.BackColor = Color.Yellow;
                    riga.Cells[8].Style.BackColor = Color.Yellow;
                }
                else
                {
                    if (lastCOLOR == "GRIGIO")
                    {
                        foreach (DataGridViewCell cel in riga.Cells)
                        { cel.Style.BackColor = Color.LightGray; }

                    }
                }

            }

        }

        private void CreaDsPerColorimetrico()
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

            dtCartelle.Columns.Add("SEQUENZA", Type.GetType("System.Int32"));
            dtCartelle.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("COLORECOMPONENTE", Type.GetType("System.String"));

            dtCartelle.Columns.Add("COMMESSA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("QUANTITA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("DATAPRODUZIONE", Type.GetType("System.DateTime"));

            dtCartelle.Columns.Add("DATACALIBRAZIONE", Type.GetType("System.DateTime"));

            dtCartelle.Columns.Add("RICHIESTOL", Type.GetType("System.String"));
            dtCartelle.Columns.Add("TOLLERANZAL", Type.GetType("System.String"));
            dtCartelle.Columns.Add("RILEVATOL", Type.GetType("System.String"));
            dtCartelle.Columns.Add("CONFORMEL", Type.GetType("System.Boolean"));

            dtCartelle.Columns.Add("RICHIESTOA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("TOLLERANZAA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("RILEVATOA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("CONFORMEA", Type.GetType("System.Boolean"));

            dtCartelle.Columns.Add("RICHIESTOB", Type.GetType("System.String"));
            dtCartelle.Columns.Add("TOLLERANZAB", Type.GetType("System.String"));
            dtCartelle.Columns.Add("RILEVATOB", Type.GetType("System.String"));
            dtCartelle.Columns.Add("CONFORMEB", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("NOTA", Type.GetType("System.String"));

            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in _DS.CDC_DETTAGLIO)
            {

                string PARTE = dettaglio.PARTE;
                string COLORE = dettaglio.COLORE;
                List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == PARTE && x.COLORE == COLORE).OrderBy(x => x.SEQUENZA).ToList();


                if (articoli.Count == 0)
                {
                    DataRow riga = dtCartelle.NewRow();

                    riga[0] = dettaglio.IDDETTAGLIO;
                    riga[1] = dettaglio.IDPRENOTAZIONE;
                    riga[2] = dettaglio.ACCESSORISTA;
                    riga[3] = dettaglio.DATACOLLAUDO;
                    riga[4] = dettaglio.PREFISSO;
                    riga[5] = dettaglio.PARTE;
                    riga[6] = dettaglio.COLORE;
                    riga[10] = dettaglio.COMMESSAORDINE;
                    riga[11] = dettaglio.QUANTITA;

                    dtCartelle.Rows.Add(riga);

                }
                else
                {
                    foreach (CDCDS.CDC_ARTICOLIRow articolo in articoli)
                    {
                        DataRow riga = dtCartelle.NewRow();

                        riga[0] = dettaglio.IDDETTAGLIO;
                        riga[1] = dettaglio.IDPRENOTAZIONE;
                        riga[2] = dettaglio.ACCESSORISTA;
                        riga[3] = dettaglio.DATACOLLAUDO;
                        riga[4] = dettaglio.PREFISSO;
                        riga[5] = dettaglio.PARTE;
                        riga[6] = dettaglio.COLORE;
                        riga[7] = articolo.SEQUENZA;
                        riga[8] = articolo.DESCRIZIONE;
                        riga[9] = articolo.COLORECOMPONENTE;
                        riga[10] = dettaglio.COMMESSAORDINE;
                        riga[11] = dettaglio.QUANTITA;

                        CDCDS.CDC_COLORERow coloreL = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.COLORECOMPONENTE == articolo.COLORECOMPONENTE && x.COLORE == CDCTipoColore.L).FirstOrDefault();
                        if (coloreL != null)
                        {
                            riga[12] = coloreL.DATAINSERIMENTO;
                            riga[13] = coloreL.DATACALIBRAZIONE;
                            riga[14] = coloreL.RICHIESTO;
                            riga[15] = coloreL.IsTOLLERANZANull() ? string.Empty : coloreL.TOLLERANZA;
                            riga[16] = coloreL.IsRILEVATONull() ? string.Empty : coloreL.RILEVATO;
                            riga[17] = coloreL.CONFORME == "S" ? true : false;
                            riga[26] = coloreL.IsNOTANull() ? string.Empty : coloreL.NOTA;
                        }
                        else
                        {
                            CDCDS.CDC_COLORIRow ConfigcoloreL = _DS.CDC_COLORI.Where(x => x.COLORECOMPONENTE == articolo.COLORECOMPONENTE && x.CODICE == CDCTipoColore.L).FirstOrDefault();

                            riga[12] = DateTime.Today;
                            riga[13] = DateTime.Today;

                            if (ConfigcoloreL == null)
                            {
                                riga[14] = CalcolaValoreRichiesto(articolo.COLORECOMPONENTE, CDCTipoColore.L);
                                riga[15] = CalcolaValoreTolleranza(articolo.COLORECOMPONENTE, CDCTipoColore.L);
                            }
                            else
                            {
                                riga[14] = ConfigcoloreL.RICHIESTO;
                                riga[15] = ConfigcoloreL.IsTOLLERANZANull() ? string.Empty : ConfigcoloreL.TOLLERANZA;
                            }
                            riga[16] = CalcolaValoreRilevato(articolo.COLORECOMPONENTE, CDCTipoColore.L);
                            riga[17] = true;
                            riga[26] = string.Empty;
                        }

                        CDCDS.CDC_COLORERow colorea = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.COLORECOMPONENTE == articolo.COLORECOMPONENTE && x.COLORE == CDCTipoColore.a).FirstOrDefault();
                        if (colorea != null)
                        {

                            riga[18] = colorea.RICHIESTO;
                            riga[19] = colorea.IsTOLLERANZANull() ? string.Empty : colorea.TOLLERANZA;
                            riga[20] = colorea.IsRILEVATONull() ? string.Empty : colorea.RILEVATO;
                            riga[21] = colorea.CONFORME == "S" ? true : false;
                        }
                        else
                        {
                            CDCDS.CDC_COLORIRow Configcolorea = _DS.CDC_COLORI.Where(x => x.COLORECOMPONENTE == articolo.COLORECOMPONENTE && x.CODICE == CDCTipoColore.a).FirstOrDefault();

                            if (Configcolorea == null)
                            {
                                riga[18] = CalcolaValoreRichiesto(articolo.COLORECOMPONENTE, CDCTipoColore.a);
                                riga[19] = CalcolaValoreTolleranza(articolo.COLORECOMPONENTE, CDCTipoColore.a);
                            }
                            else
                            {
                                riga[18] = Configcolorea.RICHIESTO;
                                riga[19] = Configcolorea.IsTOLLERANZANull() ? string.Empty : Configcolorea.TOLLERANZA;

                            }
                            riga[20] = CalcolaValoreRilevato(articolo.COLORECOMPONENTE, CDCTipoColore.a);
                            riga[21] = true;
                        }

                        CDCDS.CDC_COLORERow coloreb = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.COLORECOMPONENTE == articolo.COLORECOMPONENTE && x.COLORE == CDCTipoColore.b).FirstOrDefault();
                        if (coloreb != null)
                        {
                            riga[22] = coloreb.RICHIESTO;
                            riga[23] = coloreb.IsTOLLERANZANull() ? string.Empty : coloreb.TOLLERANZA;
                            riga[24] = coloreb.IsRILEVATONull() ? string.Empty : coloreb.RILEVATO;
                            riga[25] = coloreb.CONFORME == "S" ? true : false;
                        }
                        else
                        {

                            CDCDS.CDC_COLORIRow Configcoloreb = _DS.CDC_COLORI.Where(x => x.COLORECOMPONENTE == articolo.COLORECOMPONENTE && x.CODICE == CDCTipoColore.b).FirstOrDefault();

                            if (Configcoloreb == null)
                            {
                                riga[22] = CalcolaValoreRichiesto(articolo.COLORECOMPONENTE, CDCTipoColore.b);
                                riga[23] = CalcolaValoreTolleranza(articolo.COLORECOMPONENTE, CDCTipoColore.b);
                            }
                            else
                            {
                                riga[22] = Configcoloreb.RICHIESTO;
                                riga[23] = Configcoloreb.IsTOLLERANZANull() ? string.Empty : Configcoloreb.TOLLERANZA;

                            }
                            riga[24] = CalcolaValoreRilevato(articolo.COLORECOMPONENTE, CDCTipoColore.b);
                            riga[25] = true;
                        }
                        dtCartelle.Rows.Add(riga);
                    }
                }
            }

        }


        private string CalcolaValoreRichiesto(string colore, string CDCColore)
        {
            if (CDCColore == CDCTipoColore.L)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        return "84";
                    case "8900":
                    case "8000":
                    case "0901":
                    case "8687":
                        return "85";
                    case "0812":
                    case "8991":
                    case "0912":
                        return "84";
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        return "83";
                    case "8130":
                        return "83";
                }

            if (CDCColore == CDCTipoColore.a)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        return "2";
                    case "8000":
                    case "8900":
                    case "0901":
                        return "7";
                    case "0812":
                    case "8991":
                    case "0912":
                        return "5";
                    case "8687":
                        return "6";
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        return "0";
                    case "8130":
                        return "0";
                }

            if (CDCColore == CDCTipoColore.b)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        return "14,1";
                    case "8000":
                    case "8900":
                    case "0901":
                        return "32";
                    case "0812":
                    case "8991":
                    case "0912":
                        return "25,5";
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        return "";
                    case "8687":
                        return "30";
                    case "8130":
                        return "0";
                }
            return string.Empty;
        }

        private string CalcolaValoreTolleranza(string colore, string CDCColore)
        {
            if (CDCColore == CDCTipoColore.L)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "8900":
                    case "8687":
                    case "0953":
                        return "2";
                    case "8000":
                    case "0901":
                        return "2";
                    case "0812":
                    case "8991":
                    case "0912":
                        return "2";
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        return "2";
                    case "8130":
                        return "2";
                }

            if (CDCColore == CDCTipoColore.a)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        return "0,7";
                    case "8000":
                    case "8900":
                    case "0901":
                    case "8687":
                    case "0812":
                    case "8991":
                    case "0912":
                        return "1";
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                    case "8130":
                        return "0";
                }

            if (CDCColore == CDCTipoColore.b)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        return "1,5";
                    case "8000":
                    case "8900":
                    case "0901":
                        return "1";
                    case "0812":
                    case "8991":
                    case "8687":
                    case "0912":
                        return "2";
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        return "0";
                    case "8130":
                        return "0";
                }
            return string.Empty;
        }

        private string CalcolaValoreRilevato(string colore, string CDCColore)
        {

            decimal constante = 1.01M;
            if (CDCColore == CDCTipoColore.L)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        {
                            //19  =CASUALE.TRA(8300;8500)/100*T18                       
                            int i = _rnd.Next(8300, 8500);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8000":
                    case "8900":
                    case "0901":
                        //16    =CASUALE.TRA(8350;8400)/100*T18
                        {
                            int i = _rnd.Next(8350, 8400);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "0812":
                    case "8991":
                    case "0912":
                        //20  =CASUALE.TRA(8300;8500)/100*T18
                        {
                            int i = _rnd.Next(8300, 8500);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8687":
                        //20  =CASUALE.TRA(8300;8500)/100*T18
                        {
                            int i = _rnd.Next(8350, 8650);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        //18  =CASUALE.TRA(8350;8400)/100*T18
                        {
                            int i = _rnd.Next(8350, 8400);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8130":
                        //17 =CASUALE.TRA(6450;6500)/100*T18
                        {
                            int i = _rnd.Next(6450, 6500);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                }

            if (CDCColore == CDCTipoColore.a)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        {
                            //19  =CASUALE.TRA(2200;2600)/1000*T18                     
                            int i = _rnd.Next(2200, 2600);
                            decimal aux = i * constante;
                            aux = aux / 1000;
                            return aux.ToString("F2");
                        }
                    case "8900":
                    case "8000":
                    case "0901":
                        {
                            //16 =CASUALE.TRA(680;750)/100*T18                     
                            int i = _rnd.Next(680, 750);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8687":
                        {
                            int i = _rnd.Next(520, 690);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "0812":
                    case "8991":
                    case "0912":
                        {
                            //20  =CASUALE.TRA(450;550)/100*T18                       
                            int i = _rnd.Next(450, 550);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        {
                            //18  ""              
                            return string.Empty;
                        }
                    case "8130":
                        {
                            //17  =0                       
                            return "0";
                        }
                }

            if (CDCColore == CDCTipoColore.b)
                switch (colore)
                {
                    case "8053":
                    case "8953":
                    case "0953":
                        {
                            //19  =CASUALE.TRA(1400;1500)/100*T18                    
                            int i = _rnd.Next(1400, 1500);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8900":
                    case "8000":
                    case "0901":
                        {
                            //16=CASUALE.TRA(3150;3250)/100*T18                    
                            int i = _rnd.Next(3150, 3250);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8687":
                        {
                            //16=CASUALE.TRA(3150;3250)/100*T18                    
                            int i = _rnd.Next(2850, 3150);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "0812":
                    case "8991":
                    case "0912":
                        {
                            //20 =CASUALE.TRA(2450;2650)/100*T18                      
                            int i = _rnd.Next(2450, 2650);
                            decimal aux = i * constante;
                            aux = aux / 100;
                            return aux.ToString("F2");
                        }
                    case "8126":
                    case "8926":
                    case "0926":
                    case "8931":
                        {
                            //18  ""              
                            return string.Empty;
                        }
                    case "8130":
                        {
                            //17  =0                       
                            return "0";
                        }
                }
            return string.Empty;
        }


        private void CaricaColore(CDCDS ds, decimal idDettaglio, int sequenza, string descrizione, string colorecomponente, string colore, DateTime dataInserimento, DateTime dataCalibrazione, string richiesto, string tolleranza, string rilevato, string conforme, string nota)
        {
            CDCDS.CDC_COLORERow coloreRow = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == idDettaglio && x.SEQUENZA == sequenza && x.COLORE == colore).FirstOrDefault();
            if (coloreRow == null)
            {
                coloreRow = _DS.CDC_COLORE.NewCDC_COLORERow();
                coloreRow.IDDETTAGLIO = idDettaglio;
                coloreRow.SEQUENZA = sequenza;
                coloreRow.DESCRIZIONE = descrizione;
                coloreRow.COLORECOMPONENTE = colorecomponente;
                coloreRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                coloreRow.DATAINSERIMENTO = dataInserimento;
                coloreRow.DATACALIBRAZIONE = dataCalibrazione;
                coloreRow.STRUMENTO = Contesto.StrumentoColore;
                coloreRow.COLORE = colore;
                coloreRow.RICHIESTO = richiesto;
                coloreRow.TOLLERANZA = tolleranza;
                coloreRow.RILEVATO = rilevato;
                coloreRow.CONFORME = conforme;
                coloreRow.NOTA = nota;
                _DS.CDC_COLORE.AddCDC_COLORERow(coloreRow);
            }
            else
            {
                coloreRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                coloreRow.DATAINSERIMENTO = dataInserimento;

                coloreRow.DESCRIZIONE = descrizione;
                coloreRow.COLORECOMPONENTE = colorecomponente;

                coloreRow.DATACALIBRAZIONE = dataCalibrazione;
                coloreRow.STRUMENTO = Contesto.StrumentoColore;
                coloreRow.RICHIESTO = richiesto;
                coloreRow.TOLLERANZA = tolleranza;
                coloreRow.RILEVATO = rilevato;
                coloreRow.CONFORME = conforme;
                coloreRow.NOTA = nota;
            }
        }

        private void btnCreaPDF_Click(object sender, EventArgs e)
        {
            string fileCreati = string.Empty;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                bool esito = true;
                lblMessaggio.Text = "";

                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                  

                    if (riga["SEQUENZA"].ToString() != "")
                    {
                        string richiestL = (string)riga[14];
                        string richiesta = (string)riga[18];
                        string richiestb = (string)riga[22];

                        if (richiestL == string.Empty || richiesta == string.Empty || richiestb == string.Empty)
                            esito = false;
                    }
                }

                if (!esito)
                    MessageBox.Show("Ci sono delle righe in cui il colore richiesto non è valorizzato. Per queste righe NON verranno salvate e NON verranno creati PDF.", "INFORMAZIONE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                List<decimal> idPerPDF = new List<decimal>();
         
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    if (riga["SEQUENZA"].ToString() != "")
                    {
                        string richiestL = (string)riga[14];
                        string richiesta = (string)riga[18];
                        string richiestb = (string)riga[22];

                        if (richiestL == string.Empty || richiesta == string.Empty || richiestb == string.Empty)
                            continue;

                        decimal iddettaglio = (decimal)riga[0];
                        int sequenza = (int)riga[7];
                        string descrizione = riga[8].ToString();
                        string colorecomponente = riga[9].ToString();


                        if (!idPerPDF.Contains(iddettaglio))
                        { idPerPDF.Add(iddettaglio); }


                        DateTime dataInserimento = (DateTime)riga[12];
                        DateTime dataCalibrazione = (DateTime)riga[13];

                        CaricaColore(_DS, iddettaglio, sequenza, descrizione, colorecomponente, CDCTipoColore.L, dataInserimento, dataCalibrazione,
                            ConvertiInStringa(riga[14]), ConvertiInStringa(riga[15]), ConvertiInStringa(riga[16]), ConvertiBoolInStringa(riga[17]), ConvertiInStringa(riga[26]));

                        CaricaColore(_DS, iddettaglio, sequenza, descrizione, colorecomponente, CDCTipoColore.a, dataInserimento, dataCalibrazione,
                            ConvertiInStringa(riga[18]), ConvertiInStringa(riga[19]), ConvertiInStringa(riga[20]), ConvertiBoolInStringa(riga[21]), ConvertiInStringa(riga[26]));

                        CaricaColore(_DS, iddettaglio, sequenza, descrizione, colorecomponente, CDCTipoColore.b, dataInserimento, dataCalibrazione,
                            ConvertiInStringa(riga[22]), ConvertiInStringa(riga[23]), ConvertiInStringa(riga[24]), ConvertiBoolInStringa(riga[25]), ConvertiInStringa(riga[26]));

                    }
                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiColore(_DS);
                _DS.CDC_COLORE.AcceptChanges();

                Bitmap firma = Properties.Resources.loghi;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                //fileCreati = bll.CreaPDFColore(idPerPDF, _DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand));
                fileCreati = bll.CreaPDFColore(idPerPDF, _DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(ddlBrand.SelectedItem.ToString()));
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

        private void ddlDataCollaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopolaDDLBrand(ddlDataCollaudo.SelectedItem.ToString());
        }
    }
}
