using CDCMetal.BLL;
using CDCMetal.Entities;
using CDCMetal.Helpers;
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
    public partial class CreaEtichetteFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "ETICHETTE";
        public CreaEtichetteFrm()
        {
            InitializeComponent();
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

        private void CreaEtichetteFrm_Load(object sender, EventArgs e)
        {
            CaricaStampanti();
            PopolaDDLDate();
        }

        private void CaricaStampanti()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                ddlStampanti.Items.Add(printer);
            }
            if (ddlStampanti.Items.Count > 0)
                ddlStampanti.SelectedIndex = 0;
        }

        private void btnLeggiDati_Click(object sender, EventArgs e)
        {
            btnVerificaEtichette.Enabled = false;
            btnStampaEtichette.Enabled = false;

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

           // bll.LeggiCollaudoDaDataSTR(_DS, dataSelezionata);
            bll.LeggiCollaudoDaDataConDescrizioneSTR(_DS, dataSelezionata);
            

            if (_DS.CDC_DETTAGLIO1.Count > 0)
            {
                btnVerificaEtichette.Enabled = true;
                List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO1.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_ETICHETTE_DETTAGLIO(_DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerEtichette();

            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;

            dgvDettaglio.Columns[0].Frozen = true;
            dgvDettaglio.Columns[1].Frozen = true;
            dgvDettaglio.Columns[2].Frozen = true;
            dgvDettaglio.Columns[3].Frozen = true;
            dgvDettaglio.Columns[3].Width = 70;
            dgvDettaglio.Columns[4].Frozen = true;
            dgvDettaglio.Columns[4].Width = 70;
            dgvDettaglio.Columns[5].Frozen = true;
            dgvDettaglio.Columns[5].Width = 70;
            dgvDettaglio.Columns[6].Frozen = true;
            dgvDettaglio.Columns[6].Width = 130;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[10]).MaxInputLength = 50;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[9]).MaxInputLength = 5;
            dgvDettaglio.Columns[8].Width = 210;
            dgvDettaglio.Columns[9].Width = 70;
            dgvDettaglio.Columns[10].Width = 210;


        }

        private void CreaDsPerEtichette()
        {

            _dsServizio = new DataSet();
            DataTable dtCartelle = _dsServizio.Tables.Add();
            dtCartelle.TableName = tableName;
            dtCartelle.Columns.Add("IDPRENOTAZIONE", Type.GetType("System.Decimal")).ReadOnly = true;
            dtCartelle.Columns.Add("ACCESSORISTA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("DATACOLLAUDO", Type.GetType("System.DateTime")).ReadOnly = true;
            dtCartelle.Columns.Add("PREFISSO", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("PARTE", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("COLORE", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("COMMESSA", Type.GetType("System.String")).ReadOnly = true;
            dtCartelle.Columns.Add("QUANTITA", Type.GetType("System.String")).ReadOnly = true; //7
            dtCartelle.Columns.Add("NOTA", Type.GetType("System.String")).ReadOnly = true; //8
            dtCartelle.Columns.Add("LINEA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));//10
            dtCartelle.Columns.Add("DESTINAZIONE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("NUMERO ETICHETTE", Type.GetType("System.String"));//12


            foreach (CDCDS.CDC_DETTAGLIO1Row dettaglio in _DS.CDC_DETTAGLIO1)
            {
                DataRow riga = dtCartelle.NewRow();

                riga[0] = dettaglio.IDPRENOTAZIONE;
                riga[1] = CDCBLL.ConvertiAccessorista(dettaglio.ACCESSORISTA);
                riga[2] = dettaglio.DATACOLLAUDO;
                riga[3] = dettaglio.PREFISSO;
                riga[4] = dettaglio.PARTE;
                riga[5] = dettaglio.COLORE;
                riga[6] = dettaglio.COMMESSAORDINE;
                riga[7] = dettaglio.QUANTITA;
                riga[8] = dettaglio.IsNOTECOLLAUDONull() ? string.Empty : dettaglio.NOTECOLLAUDO;
                riga[10] = dettaglio.DESCRIZIONE;
                riga[11] = "GUCCI LOGISTICA";

                if (!dettaglio.IsNOTECOLLAUDONull())
                {
                    riga[12] = EstraiNumeroEtichette(dettaglio.NOTECOLLAUDO, dettaglio.QUANTITA.ToString());
                }

                CDCDS.CDC_ETICHETTE_DETTAGLIORow etichetta = _DS.CDC_ETICHETTE_DETTAGLIO.Where(x => x.PREFISSO == dettaglio.PREFISSO && x.PARTE == dettaglio.PARTE && x.COLORE == dettaglio.COLORE).FirstOrDefault();
                if (etichetta != null)
                {
                    riga[9] = etichetta.LINEA;
                    riga[10] = etichetta.DESCRIZIONE;
                }

                dtCartelle.Rows.Add(riga);
            }

        }
        private string EstraiNumeroEtichette(string notaCollaudo, string quantita)
        {
            StringBuilder sb = new StringBuilder();

            if (notaCollaudo.Trim().ToUpper() == "SC 1")
            {
                sb.AppendFormat("1X{0};", quantita);
            }
            else
            {
                string nota = notaCollaudo.ToUpper().Replace("SC", string.Empty);
                string[] note = nota.Split(' ');
                foreach (string n in note)
                {
                    string nscqta = n.Replace("SC", string.Empty).Replace(" ", string.Empty);
                    string[] nsc = nscqta.Split('X');
                    if (nsc.Length == 2)
                    {
                        sb.AppendFormat("{0}X{1};", nsc[0], nsc[1]);
                    }
                }
            }
            return sb.ToString();
        }
        private void btnVerificaEtichette_Click(object sender, EventArgs e)
        {

            btnStampaEtichette.Enabled = false; ;
            StringBuilder messaggioErrore = new StringBuilder();

            StringBuilder messaggioBuono = new StringBuilder();
            bool mostraMessaggioErrore = false;

            if (ddlStampanti.SelectedIndex == -1)
            {
                mostraMessaggioErrore = true;
                messaggioErrore.AppendLine(string.Format("Selezionare una stampamnte"));
            }

            CDCBLL bll = new CDCBLL();

            try
            {
                lblMessaggio.Text = string.Empty;
                if (_dsServizio.Tables[tableName].Rows.Count == 0)
                {
                    lblMessaggio.Text = "NESSUNA ETICHETTA DA STAMPARE";
                    return;
                }
                int indiceRiga = 1;
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {

                    string linea = string.Empty;
                    if (riga[9] != System.DBNull.Value)
                        linea = (string)riga[9];
                    string descrizione = string.Empty;
                    if (riga[10] != System.DBNull.Value)
                        descrizione = (string)riga[10];
                    string destinazione = string.Empty;
                    if (riga[11] != System.DBNull.Value)
                        destinazione = (string)riga[11];
                    string numeroEtichette = string.Empty;
                    if (riga[12] != System.DBNull.Value)
                        numeroEtichette = (string)riga[12];

                    descrizione = descrizione.Trim();
                    destinazione = destinazione.Trim();
                    numeroEtichette = numeroEtichette.Replace(" ", string.Empty).ToUpper();

                    string finitura = (string)riga[5];
                    finitura = finitura.ToUpper().Trim();
                    if ((!string.IsNullOrEmpty(finitura) || !(finitura == "NULL")) && string.IsNullOrEmpty(linea))
                    {
                        mostraMessaggioErrore = true;
                        messaggioErrore.AppendLine(string.Format("Riga {0}: Inserire la linea", indiceRiga));
                    }
                    if (string.IsNullOrEmpty(descrizione))
                    {
                        mostraMessaggioErrore = true;
                        messaggioErrore.AppendLine(string.Format("Riga {0}: Inserire la descrizione", indiceRiga));
                    }
                    if (string.IsNullOrEmpty(destinazione))
                    {
                        mostraMessaggioErrore = true;
                        messaggioErrore.AppendLine(string.Format("Riga {0}: Inserire la destinazione", indiceRiga));
                    }

                    if (string.IsNullOrEmpty(numeroEtichette))
                    {
                        mostraMessaggioErrore = true;
                        messaggioErrore.AppendLine(string.Format("Riga {0}: Inserire il numero di etichette", indiceRiga));
                    }
                    indiceRiga++;

                    string messaggio;
                    List<Tuple<int, int>> SC_QTA;
                    if (!bll.verificaNumeroEtichette(numeroEtichette, out messaggio, out SC_QTA))
                    {
                        mostraMessaggioErrore = true;
                        messaggioErrore.AppendLine(string.Format("Riga {0}: numero etichette errato", indiceRiga));
                    }
                    else
                    {
                        string prefisso = (string)riga[3];
                        string parte = (string)riga[4];
                        string colore = (string)riga[5];
                        string commessa = (string)riga[6];
                        messaggioBuono.AppendLine(string.Format("{0}-{1}-{2}  {3}", prefisso, parte, colore, commessa));
                        messaggioBuono.AppendLine(messaggio);
                    }

                }

                if (mostraMessaggioErrore)
                {
                    MessageBox.Show(messaggioErrore.ToString(), "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string zebraPrinter = ddlStampanti.SelectedItem.ToString();

                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    ZebraHelper.EseguiStampaEtichetta(zebraPrinter, riga, "1");
                }

                MessageBox.Show(messaggioBuono.ToString(), "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnStampaEtichette.Enabled = true;
            }

            catch (Exception ex)
            {
                btnStampaEtichette.Enabled = false;
                MostraEccezione(ex, "Errore nella verifica etichette");
            }
        }

        private void btnStampaEtichette_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessaggio.Text = string.Empty;
                if (ddlStampanti.SelectedIndex == -1)
                {
                    lblMessaggio.Text = "Selezionare una stampante";
                    return;
                }

                string zebraPrinter = ddlStampanti.SelectedItem.ToString();

                SalvaDescrizioneEtichette();

                StampaEtichetteFrm form = new StampaEtichetteFrm(_dsServizio, zebraPrinter);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                btnStampaEtichette.Enabled = false;
                MostraEccezione(ex, "Errore nella stampa etichette");
            }
        }

        private void SalvaDescrizioneEtichette()
        {
            foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
            {
                string prefisso = (string)riga[3];
                string parte = (string)riga[4];
                string colore = (string)riga[5];
                string linea = (string)riga[9];
                string descrizione = (string)riga[10];
                descrizione = descrizione.Trim().ToUpper();
                linea = linea.Trim().ToUpper();

                CDCDS.CDC_ETICHETTE_DETTAGLIORow rigaEtichetta = _DS.CDC_ETICHETTE_DETTAGLIO.Where(x => x.PREFISSO == prefisso && x.PARTE == parte && x.COLORE == colore).FirstOrDefault();
                if (rigaEtichetta == null)
                {
                    rigaEtichetta = _DS.CDC_ETICHETTE_DETTAGLIO.NewCDC_ETICHETTE_DETTAGLIORow();
                    rigaEtichetta.PREFISSO = prefisso;
                    rigaEtichetta.PARTE = parte;
                    rigaEtichetta.COLORE = colore;
                    rigaEtichetta.LINEA = linea;
                    rigaEtichetta.DESCRIZIONE = descrizione.Length > 50 ? descrizione.Substring(0, 50) : descrizione;
                    _DS.CDC_ETICHETTE_DETTAGLIO.AddCDC_ETICHETTE_DETTAGLIORow(rigaEtichetta);
                }
                else
                {
                    rigaEtichetta.LINEA = linea;
                    rigaEtichetta.DESCRIZIONE = descrizione.Length > 50 ? descrizione.Substring(0, 50) : descrizione;
                }
            }

            CDCBLL bll = new CDCBLL();
            bll.SalvaDescrizioneEtichette(_DS);

        }

        private void ddlDataCollaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopolaDDLBrand(ddlDataCollaudo.SelectedItem.ToString());
        }
    }
}
