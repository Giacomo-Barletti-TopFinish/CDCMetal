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
            ddlDataCollaudo.Items.AddRange(CaricaDateCollaudo().ToArray());
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
                bll.FillCDC_COLORE(_DS, IDDETTAGLIO);
                bll.CDC_PDF(_DS, IDDETTAGLIO);
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
            dgvDettaglio.Columns[5].Frozen = true;
            dgvDettaglio.Columns[6].Frozen = true;
            dgvDettaglio.Columns[7].Frozen = true;
            dgvDettaglio.Columns[7].Width = 130;
            dgvDettaglio.Columns[8].Frozen = true;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[11]).HeaderText = "L RICH";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[12]).HeaderText = "L TOLL";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[13]).HeaderText = "L RILE";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[11]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[12]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[13]).MaxInputLength = 7;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[15]).HeaderText = "a RICH";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[16]).HeaderText = "a TOLL";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[17]).HeaderText = "a RILE";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[15]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[16]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[17]).MaxInputLength = 7;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[19]).HeaderText = "b RICH";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[20]).HeaderText = "b TOLL";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[21]).HeaderText = "b RILE";
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[19]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[20]).MaxInputLength = 7;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[21]).MaxInputLength = 7;

            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[23]).MaxInputLength = 50;

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
            dtCartelle.Columns.Add("COMMESSA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("QUANTITA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("DATAPRODUZIONE", Type.GetType("System.DateTime"));

            //10
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

                CDCDS.CDC_COLORERow coloreL = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.COLORE == CDCTipoColore.L).FirstOrDefault();
                if (coloreL != null)
                {
                    riga[9] = coloreL.DATAINSERIMENTO;
                    riga[10] = coloreL.DATACALIBRAZIONE;
                    riga[11] = coloreL.RICHIESTO;
                    riga[12] = coloreL.IsTOLLERANZANull()?string.Empty:coloreL.TOLLERANZA;
                    riga[13] = coloreL.IsRILEVATONull() ? string.Empty : coloreL.RILEVATO;
                    riga[14] = coloreL.CONFORME == "S" ? true : false;
                    riga[23] = coloreL.IsNOTANull()?string.Empty:coloreL.NOTA;
                }
                else
                {
                    riga[9] = DateTime.Today;
                    riga[10] = DateTime.Today;
                    riga[11] = CalcolaValoreRichiesto(dettaglio.COLORE, CDCTipoColore.L);
                    riga[12] = CalcolaValoreTolleranza(dettaglio.COLORE, CDCTipoColore.L);
                    riga[13] = CalcolaValoreRilevato(dettaglio.COLORE, CDCTipoColore.L);
                    riga[14] = true;
                    riga[23] = string.Empty;
                }

                CDCDS.CDC_COLORERow colorea = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.COLORE == CDCTipoColore.a).FirstOrDefault();
                if (colorea != null)
                {
                    riga[15] = colorea.RICHIESTO;
                    riga[16] = colorea.IsTOLLERANZANull() ? string.Empty : colorea.TOLLERANZA;
                    riga[17] = colorea.IsRILEVATONull() ? string.Empty : colorea.RILEVATO;
                    riga[18] = colorea.CONFORME == "S" ? true : false;
                }
                else
                {
                    riga[15] = CalcolaValoreRichiesto(dettaglio.COLORE, CDCTipoColore.a);
                    riga[16] = CalcolaValoreTolleranza(dettaglio.COLORE, CDCTipoColore.a);
                    riga[17] = CalcolaValoreRilevato(dettaglio.COLORE, CDCTipoColore.a);
                    riga[18] = true;
                }

                CDCDS.CDC_COLORERow coloreb = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.COLORE == CDCTipoColore.b).FirstOrDefault();
                if (coloreb != null)
                {
                    riga[19] = coloreb.RICHIESTO;
                    riga[20] = coloreb.IsTOLLERANZANull() ? string.Empty : coloreb.TOLLERANZA;
                    riga[21] = coloreb.IsRILEVATONull() ? string.Empty : coloreb.RILEVATO;
                    riga[22] = coloreb.CONFORME == "S" ? true : false;
                }
                else
                {
                    riga[19] = CalcolaValoreRichiesto(dettaglio.COLORE, CDCTipoColore.b);
                    riga[20] = CalcolaValoreTolleranza(dettaglio.COLORE, CDCTipoColore.b);
                    riga[21] = CalcolaValoreRilevato(dettaglio.COLORE, CDCTipoColore.b);
                    riga[22] = true;
                }
                dtCartelle.Rows.Add(riga);
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

        private void CaricaColore(CDCDS ds, decimal idDettaglio, string colore, DateTime dataInserimento, DateTime dataCalibrazione, string richiesto, string tolleranza, string rilevato, string conforme, string nota)
        {
            CDCDS.CDC_COLORERow coloreRow = _DS.CDC_COLORE.Where(x => x.IDDETTAGLIO == idDettaglio && x.COLORE == colore).FirstOrDefault();
            if (coloreRow == null)
            {
                coloreRow = _DS.CDC_COLORE.NewCDC_COLORERow();
                coloreRow.IDDETTAGLIO = idDettaglio;
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
                    string richiestL = (string)riga[11];
                    string richiesta = (string)riga[15];
                    string richiestb = (string)riga[19];

                    if (richiestL == string.Empty || richiesta == string.Empty || richiestb == string.Empty)
                        esito = false;
                }

                if(!esito)
                    MessageBox.Show("Ci sono delle righe in cui il colore richiesto non è valorizzato. Per queste righe NON verranno salvate e NON verranno creati PDF.", "INFORMAZIONE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                List<decimal> idPerPDF = new List<decimal>();
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    string richiestL = (string)riga[11];
                    string richiesta = (string)riga[15];
                    string richiestb = (string)riga[19];

                    if (richiestL == string.Empty || richiesta == string.Empty || richiestb == string.Empty)
                        continue;

                    decimal iddettaglio = (decimal)riga[0];
                    idPerPDF.Add(iddettaglio);
                    DateTime dataInserimento = (DateTime)riga[9];
                    DateTime dataCalibrazione = (DateTime)riga[10];

                    CaricaColore(_DS, iddettaglio, CDCTipoColore.L, dataInserimento, dataCalibrazione,
                        ConvertiInStringa(riga[11]), ConvertiInStringa(riga[12]), ConvertiInStringa(riga[13]), ConvertiBoolInStringa(riga[14]), ConvertiInStringa(riga[23]));

                    CaricaColore(_DS, iddettaglio, CDCTipoColore.a, dataInserimento, dataCalibrazione,
                        ConvertiInStringa(riga[15]), ConvertiInStringa(riga[16]), ConvertiInStringa(riga[17]), ConvertiBoolInStringa(riga[18]), ConvertiInStringa(riga[23]));

                    CaricaColore(_DS, iddettaglio, CDCTipoColore.b, dataInserimento, dataCalibrazione,
                        ConvertiInStringa(riga[19]), ConvertiInStringa(riga[20]), ConvertiInStringa(riga[21]), ConvertiBoolInStringa(riga[22]), ConvertiInStringa(riga[23]));

                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiColore(_DS);
                _DS.CDC_COLORE.AcceptChanges();

                Bitmap firma = Properties.Resources.loghi;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                fileCreati = bll.CreaPDFColore(idPerPDF, _DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand));
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
