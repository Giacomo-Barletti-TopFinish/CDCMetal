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

            Contesto.DS = new Entities.CDCDS();

            bll.LeggiCollaudoDaData(Contesto.DS, dataSelezionata);


            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = Contesto.DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_DIMEMSIONI(Contesto.DS, IDDETTAGLIO);
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
            dgvDettaglio.Columns[12].Visible = false;
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

            foreach (DataGridViewColumn column in dgvDettaglio.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataRow r = Contesto.DS.CDC_DETTAGLIO.Rows[e.RowIndex];
            decimal IDDETTAGLIO = (decimal)r[0];
            decimal IDPRENOTAZIONE = (decimal)r[1];
            string PARTE = (string)r[7];
            string COLORE = (string)r[8];

            _dsServizio = new DataSet();

            DataTable dtDimensioni = _dsServizio.Tables.Add();
            dtDimensioni.TableName = tableName;
            dtDimensioni.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal"));
            dtDimensioni.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal"));

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

            List<CDCDS.CDC_DIMEMSIONIRow> dimensioni = Contesto.DS.CDC_DIMEMSIONI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).ToList();
            List<CDCDS.CDC_DIMEMSIONI_MISURERow> misure = Contesto.DS.CDC_DIMEMSIONI_MISURE.Where(x => x.PARTE == PARTE).ToList();

            for (int i = 0; i < 20; i++)
            {
                DataRow riga = dtDimensioni.NewRow();
                riga[0] = IDPRENOTAZIONE;
                riga[1] = IDDETTAGLIO;
                riga[11] = PARTE;
                riga[12] = COLORE;
                if (dimensioni.Count > i)
                {
                    CDCDS.CDC_DIMEMSIONIRow dimensione = dimensioni[i];
                    riga[2] = dimensione.RIFERIMENTO;
                    riga[3] = dimensione.GRANDEZZA;
                    riga[4] = dimensione.RICHIESTO;
                    if (dimensione.IsTOLLERANZANull())
                        riga[5] = string.Empty;
                    else
                        riga[5] = dimensione.TOLLERANZA;

                    if (dimensione.IsMINIMONull())
                        riga[6] = string.Empty;
                    else
                        riga[6] = dimensione.MINIMO;

                    if (dimensione.IsMASSIMONull())
                        riga[7] = string.Empty;
                    else
                        riga[7] = dimensione.MASSIMO;

                    if (dimensione.IsTAMPONENull())
                        riga[8] = string.Empty;
                    else
                        riga[8] = dimensione.TAMPONE;

                    riga[9] = dimensione.CONTAMPONE == "S" ? true : false;
                    riga[10] = dimensione.CONFORME == "S" ? true : false;
                }
                else
                {
                    if (misure.Count > i)
                    {
                        CDCDS.CDC_DIMEMSIONI_MISURERow misura = misure[i];
                        riga[2] = misura.RIFERIMENTO;
                        riga[3] = misura.GRANDEZZA;
                        riga[4] = misura.RICHIESTO;
                        if (misura.IsTOLLERANZANull())
                            riga[5] = string.Empty;
                        else
                            riga[5] = misura.TOLLERANZA;

                        if (misura.IsMINIMONull())
                            riga[6] = string.Empty;
                        else
                            riga[6] = misura.MINIMO;

                        if (misura.IsMASSIMONull())
                            riga[7] = string.Empty;
                        else
                            riga[7] = misura.MASSIMO;

                        if (misura.IsTAMPONENull())
                            riga[8] = string.Empty;
                        else
                            riga[8] = misura.TAMPONE;
                        riga[9] = misura.CONTAMPONE == "S" ? true : false;
                    }
                }

                dtDimensioni.Rows.Add(riga);
            }

            dgvMisure.AutoGenerateColumns = true;
            dgvMisure.DataSource = _dsServizio;
            dgvMisure.DataMember = tableName;

            dgvMisure.Columns[0].Visible = false;
            dgvMisure.Columns[1].Visible = false;
            dgvMisure.Columns[11].Visible = false;
            dgvMisure.Columns[12].Visible = false;
            dgvMisure.Columns[3].Width = 220;
            dgvMisure.Columns[8].Width = 320;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[2]).MaxInputLength = 2;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[3]).MaxInputLength = 30;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[4]).MaxInputLength = 5;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[5]).MaxInputLength = 5;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[6]).MaxInputLength = 5;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[7]).MaxInputLength = 5;
            ((DataGridViewTextBoxColumn)dgvMisure.Columns[8]).MaxInputLength = 50;

        }

        private void btnCreaPDF_Click(object sender, EventArgs e)
        {
            if (_dsServizio.Tables[tableName].Rows.Count == 0)
                return;

            lblMessaggio.Text = string.Empty;
            decimal IDDETTAGLIO = -1;

            foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
            {
                IDDETTAGLIO = (decimal)riga[1];
                string RIFERIMENTO = ConvertiInStringa(riga[2]);
                RIFERIMENTO = RIFERIMENTO.ToUpper().Trim();
                string PARTE = (string)riga[11];
                string COLORE = (string)riga[12];
                if (!string.IsNullOrEmpty(RIFERIMENTO))
                {
                    CDCDS.CDC_DIMEMSIONIRow dimensione = Contesto.DS.CDC_DIMEMSIONI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO && x.RIFERIMENTO.Trim() == RIFERIMENTO).FirstOrDefault();
                    if (dimensione == null)
                    {
                        dimensione = Contesto.DS.CDC_DIMEMSIONI.NewCDC_DIMEMSIONIRow();
                        dimensione.CONFORME = ConvertiBoolInStringa(riga[10]);
                        dimensione.CONTAMPONE = ConvertiBoolInStringa(riga[9]);
                        dimensione.DATAINSERIMENTO = DateTime.Now;
                        dimensione.GRANDEZZA = ((string)riga[3]).ToUpper().Trim();
                        dimensione.IDDETTAGLIO = IDDETTAGLIO;
                        dimensione.MASSIMO = ConvertiInStringa(riga[7]).ToUpper().Trim();
                        dimensione.MINIMO = ConvertiInStringa(riga[6]).ToUpper().Trim();
                        dimensione.RICHIESTO = ((string)riga[4]).ToUpper().Trim();
                        dimensione.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                        dimensione.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                        dimensione.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                        dimensione.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        Contesto.DS.CDC_DIMEMSIONI.AddCDC_DIMEMSIONIRow(dimensione);
                    }
                    else
                    {
                        dimensione.CONFORME = ConvertiBoolInStringa(riga[10]);
                        dimensione.CONTAMPONE = ConvertiBoolInStringa(riga[9]);
                        dimensione.DATAINSERIMENTO = DateTime.Now;
                        dimensione.GRANDEZZA = ((string)riga[3]).ToUpper().Trim();
                        dimensione.IDDETTAGLIO = IDDETTAGLIO;
                        dimensione.MASSIMO = ConvertiInStringa(riga[7]).ToUpper().Trim();
                        dimensione.MINIMO = ConvertiInStringa(riga[6]).ToUpper().Trim();
                        dimensione.RICHIESTO = ((string)riga[4]).ToUpper().Trim();
                        dimensione.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                        dimensione.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                        dimensione.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                        dimensione.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    }

                    CDCDS.CDC_DIMEMSIONI_MISURERow misura = Contesto.DS.CDC_DIMEMSIONI_MISURE.Where(x => x.PARTE == PARTE && x.RIFERIMENTO.Trim() == RIFERIMENTO).FirstOrDefault();
                    if (misura == null)
                    {
                        misura = Contesto.DS.CDC_DIMEMSIONI_MISURE.NewCDC_DIMEMSIONI_MISURERow();
                        misura.PARTE = PARTE;
                        misura.CONTAMPONE = ConvertiBoolInStringa(riga[9]);
                        misura.GRANDEZZA = ((string)riga[3]).ToUpper().Trim();
                        misura.MASSIMO = ConvertiInStringa(riga[7]).ToUpper().Trim();
                        misura.MINIMO = ConvertiInStringa(riga[6]).ToUpper().Trim();
                        misura.RICHIESTO = ((string)riga[4]).ToUpper().Trim();
                        misura.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                        misura.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                        misura.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                        Contesto.DS.CDC_DIMEMSIONI_MISURE.AddCDC_DIMEMSIONI_MISURERow(misura);
                    }
                    else
                    {
                        misura.CONTAMPONE = ConvertiBoolInStringa(riga[9]);
                        misura.GRANDEZZA = ((string)riga[3]).ToUpper().Trim();
                        misura.MASSIMO = ConvertiInStringa(riga[7]).ToUpper().Trim();
                        misura.MINIMO = ConvertiInStringa(riga[6]).ToUpper().Trim();
                        misura.RICHIESTO = ((string)riga[4]).ToUpper().Trim();
                        misura.RIFERIMENTO = ((string)riga[2]).ToUpper().Trim();
                        misura.TAMPONE = ConvertiInStringa(riga[8]).ToUpper().Trim();
                        misura.TOLLERANZA = ConvertiInStringa(riga[5]).ToUpper().Trim();
                    }
                }
            }

            CDCBLL bll = new CDCBLL();
            bll.SalvaDatiDimensioni(Contesto.DS);
            Contesto.DS.CDC_DIMEMSIONI.AcceptChanges();
            Contesto.DS.CDC_DIMEMSIONI_MISURE.AcceptChanges();

            Bitmap firma = Properties.Resources.firma_vittoria;
            if (Contesto.Utente.IDUSER == "0000000122")
                firma = Properties.Resources.firma_celeste;

            ImageConverter converter = new ImageConverter();
            byte[] iFirma = (byte[])converter.ConvertTo(firma, typeof(byte[]));

            Bitmap loghi = Properties.Resources.loghi;

            byte[] iLoghi = (byte[])converter.ConvertTo(loghi, typeof(byte[]));

            bll.CreaPDFDimensionale(IDDETTAGLIO, Contesto.DS, Contesto.Utente.FULLNAMEUSER, Contesto.PathCollaudo, iFirma, iLoghi);

            if (chkCopiaSchedaTecnica.Checked)
            {
                DataRow riga = _dsServizio.Tables[tableName].Rows[0];
                string PARTE = (string)riga[11];
                string COLORE = (string)riga[12];

                string filename = string.Format(@"{0}\{1}-{2}.pdf", Contesto.PathSchedeTecniche, PARTE, COLORE);

                if (!File.Exists(filename))
                {
                    string messaggio = string.Format("Impossibile trovare il file per PARTE:{0} e COLORE:{1}. File: {2}", PARTE, COLORE, filename);
                    MessageBox.Show(messaggio);
                    return;
                }

                CDCDS.CDC_DETTAGLIORow dettaglio = Contesto.DS.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string cartellaDestinazione = CDCBLL.CreaPathCartella(dt, Contesto.PathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);

                string destinazione = string.Format(@"{0}\{1}-{2}.pdf", cartellaDestinazione, PARTE, COLORE);
                if (File.Exists(destinazione))
                    File.Delete(destinazione);

                File.Copy(filename, destinazione, true);
            }
          
        }
    }
}

