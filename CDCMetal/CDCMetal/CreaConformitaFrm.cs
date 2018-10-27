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
    public partial class CreaConformitaFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "CONFORMITA";
        public CreaConformitaFrm()
        {
            InitializeComponent();
        }

        private void CreaConformitaFrm_Load(object sender, EventArgs e)
        {
            CaricaDateCollaudo();
        }
        private void CaricaDateCollaudo()
        {
            CDCBLL bll = new CDCBLL();
            List<DateTime> date = bll.LeggiDateCollaudo();
            foreach (DateTime dt in date)
                ddlDataCollaudo.Items.Add(dt);

        }

        private void btnLeggiDati_Click(object sender, EventArgs e)
        {
            btnCreaPDF.Enabled = false;

            lblMessaggio.Text = string.Empty;
            if (ddlDataCollaudo.SelectedIndex == -1)
            {
                lblMessaggio.Text = "Selezionare una data";
            }

            DateTime dataSelezionata = (DateTime)ddlDataCollaudo.SelectedItem;

            CDCBLL bll = new CDCBLL();

            Contesto.DS = new Entities.CDCDS();

            bll.LeggiCollaudoDaData(Contesto.DS, dataSelezionata);


            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = Contesto.DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_CONFORMITA(Contesto.DS, IDDETTAGLIO);
                bll.CDC_PDF(Contesto.DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerCartificati(dataSelezionata);

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
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[7]).MaxInputLength = 50;
            dgvDettaglio.Columns[7].Width = 170;
            dgvDettaglio.Columns[14].Visible = false; ;

        }

        private void CreaDsPerCartificati(DateTime dataSelezionata)
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
            dtCartelle.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("FISICO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("FUNZIONALE", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("DIMENSIONALE", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("ESTETICO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("ACCONTO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("SALDO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal")).ReadOnly = true;


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in Contesto.DS.CDC_DETTAGLIO)
            {
                DataRow riga = dtCartelle.NewRow();

                riga[0] = dettaglio.IDPRENOTAZIONE;
                riga[1] = CDCBLL.ConvertiAccessorista(dettaglio.ACCESSORISTA);
                riga[2] = dettaglio.DATACOLLAUDO;
                riga[3] = dettaglio.PREFISSO;
                riga[4] = dettaglio.PARTE;
                riga[5] = dettaglio.COLORE;
                riga[6] = dettaglio.COMMESSAORDINE;
                riga[14] = dettaglio.IDDETTAGLIO;
                CDCDS.CDC_CONFORMITARow conformita = Contesto.DS.CDC_CONFORMITA.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO).FirstOrDefault();
                if (conformita != null)
                {
                    riga[7] = conformita.DESCRIZIONE;
                    riga[8] = conformita.FISICOCHIMICO == "S" ? true : false;
                    riga[9] = conformita.FUNZIONALE == "S" ? true : false;
                    riga[10] = conformita.DIMENSIONALE == "S" ? true : false;
                    riga[11] = conformita.ESTETICO == "S" ? true : false;
                    riga[12] = conformita.ACCONTO == "S" ? true : false;
                    riga[13] = conformita.SALDO == "S" ? true : false;
                }
                else
                {
                    CDCDS.CDC_CONFORMITA_DETTAGLIORow descrizione = Contesto.DS.CDC_CONFORMITA_DETTAGLIO.
                        Where(x => x.PARTE == dettaglio.PARTE && x.PREFISSO == dettaglio.PREFISSO && x.COLORE == dettaglio.COLORE).FirstOrDefault();
                    if (descrizione != null)
                        riga[7] = descrizione.DESCRIZIONE;
                }

                dtCartelle.Rows.Add(riga);
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
                    if (riga[7] == DBNull.Value || string.IsNullOrEmpty((string)riga[7]))
                        esito = false;
                }

                if (!esito)
                {
                    lblMessaggio.Text = "Impossibile creare i file. Ci sono delle descrizioni vuote";
                    return;
                }

                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    decimal iddettaglio = (decimal)riga[14];
                    CDCDS.CDC_CONFORMITARow conformitaRow = Contesto.DS.CDC_CONFORMITA.Where(x => x.IDDETTAGLIO == iddettaglio).FirstOrDefault();
                    if (conformitaRow == null)
                    {
                        conformitaRow = Contesto.DS.CDC_CONFORMITA.NewCDC_CONFORMITARow();
                        conformitaRow.IDDETTAGLIO = iddettaglio;
                        conformitaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        conformitaRow.DATAINSERIMENTO = DateTime.Now;
                        conformitaRow.FISICOCHIMICO = ConvertiBoolInStringa(riga[8]);
                        conformitaRow.FUNZIONALE = ConvertiBoolInStringa(riga[9]);
                        conformitaRow.DIMENSIONALE = ConvertiBoolInStringa(riga[10]);
                        conformitaRow.ESTETICO = ConvertiBoolInStringa(riga[11]);
                        conformitaRow.ACCONTO = ConvertiBoolInStringa(riga[12]);
                        conformitaRow.SALDO = ConvertiBoolInStringa(riga[13]);
                        conformitaRow.DESCRIZIONE = ((string)riga[7]).ToUpper().Trim();
                        Contesto.DS.CDC_CONFORMITA.AddCDC_CONFORMITARow(conformitaRow);
                    }
                    else
                    {
                        conformitaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        conformitaRow.DATAINSERIMENTO = DateTime.Now;
                        conformitaRow.FISICOCHIMICO = ConvertiBoolInStringa(riga[8]);
                        conformitaRow.FUNZIONALE = ConvertiBoolInStringa(riga[9]);
                        conformitaRow.DIMENSIONALE = ConvertiBoolInStringa(riga[10]);
                        conformitaRow.ESTETICO = ConvertiBoolInStringa(riga[11]);
                        conformitaRow.ACCONTO = ConvertiBoolInStringa(riga[12]);
                        conformitaRow.SALDO = ConvertiBoolInStringa(riga[13]);
                        conformitaRow.DESCRIZIONE = ((string)riga[7]).ToUpper().Trim();
                    }

                    string parte = (string)riga[4];
                    string prefisso = (string)riga[3];
                    string colore = (string)riga[5];

                    CDCDS.CDC_CONFORMITA_DETTAGLIORow dettaglioRow = Contesto.DS.CDC_CONFORMITA_DETTAGLIO.Where(x => x.PARTE == parte && x.PREFISSO == prefisso && x.COLORE == colore).FirstOrDefault();
                    if (dettaglioRow == null)
                    {
                        dettaglioRow = Contesto.DS.CDC_CONFORMITA_DETTAGLIO.NewCDC_CONFORMITA_DETTAGLIORow();
                        dettaglioRow.PREFISSO = prefisso;
                        dettaglioRow.PARTE = parte;
                        dettaglioRow.COLORE = colore;
                        dettaglioRow.DESCRIZIONE = ((string)riga[7]).ToUpper().Trim();
                        Contesto.DS.CDC_CONFORMITA_DETTAGLIO.AddCDC_CONFORMITA_DETTAGLIORow(dettaglioRow);
                    }
                    else
                        dettaglioRow.DESCRIZIONE = ((string)riga[7]).ToUpper().Trim();
                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiConformita(Contesto.DS);
                Contesto.DS.AcceptChanges();
                Bitmap firma = Properties.Resources.FirmaCDC;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                fileCreati = bll.CreaPDFConformita(Contesto.DS, Contesto.PathCollaudo, image);
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

        private string ConvertiBoolInStringa(object o)
        {
            if (o == DBNull.Value)
                return "N";
            if (o is Boolean)
            {
                bool aux = (bool)o;
                return aux ? "S" : "N";
            }
            return "N";
        }
    }
}
