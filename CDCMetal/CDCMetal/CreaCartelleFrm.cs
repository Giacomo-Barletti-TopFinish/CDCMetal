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
using System.IO;

namespace CDCMetal
{
    public partial class CreaCartelleFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "CARTELLE";
        private int idColonnaCartella = 6;
        public CreaCartelleFrm()
        {
            InitializeComponent();
        }

        private void CreaCartelleFrm_Load(object sender, EventArgs e)
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
            btnCreaCartelle.Enabled = false;

            lblMessaggio.Text = string.Empty;
            if (ddlDataCollaudo.SelectedIndex == -1)
            {
                lblMessaggio.Text = "Selezionare una data";
            }

            DateTime dataSelezionata = (DateTime)ddlDataCollaudo.SelectedItem;

            CDCBLL bll = new CDCBLL();

            Contesto.DS = new Entities.CDCDS();

            bll.LeggiDateCollaudo(Contesto.DS, dataSelezionata);

            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaCartelle.Enabled = true;
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerCartelle(dataSelezionata);

            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;
            dgvDettaglio.Columns["CARTELLA"].Width = 800;


        }

        private void btnCreaCartelle_Click(object sender, EventArgs e)
        {
            StringBuilder cartelleNonCreate = new StringBuilder();
            StringBuilder cartelleCreate = new StringBuilder();

            lblMessaggio.Text = string.Empty;
            if (_dsServizio.Tables[tableName].Rows.Count == 0)
            {
                lblMessaggio.Text = "NESSUNA CARTELLA DA CREARE";
                return;
            }

            foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
            {

                string cartella = (string)riga[idColonnaCartella];

                if (!Directory.Exists(cartella))
                {
                    Directory.CreateDirectory(cartella);
                    cartelleCreate.AppendLine(cartella);
                }
                else
                    cartelleNonCreate.AppendLine(cartella);
            }

            StringBuilder messaggio = new StringBuilder();
            messaggio.AppendLine("Sono state create le seguenti cartelle");
            messaggio.Append(cartelleCreate.ToString());
            messaggio.AppendLine(string.Empty);
            messaggio.AppendLine("Le seguenti cartelle esistevano già");
            messaggio.Append(cartelleNonCreate.ToString());

            MessageBox.Show(messaggio.ToString(), "INFO CREAZIONE CARTELLE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnCreaCartelle.Enabled = false;
        }

        private void CreaDsPerCartelle(DateTime dataSelezionata)
        {

            _dsServizio = new DataSet();
            DataTable dtCartelle = _dsServizio.Tables.Add();
            dtCartelle.TableName = tableName;
            dtCartelle.Columns.Add("ACCESSORISTA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("DATACOLLAUDO", Type.GetType("System.DateTime"));
            dtCartelle.Columns.Add("PREFISSO", Type.GetType("System.String"));
            dtCartelle.Columns.Add("PARTE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("COLORE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("COMMESSA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("CARTELLA", Type.GetType("System.String"));


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in Contesto.DS.CDC_DETTAGLIO)
            {
                DataRow riga = dtCartelle.NewRow();

                if (dettaglio.ACCESSORISTA.Trim() == "Metalplus s.r.l.")
                    riga[0] = "Metalplus";
                else
                    riga[0] = "TopFinish";

                riga[1] = dettaglio.DATACOLLAUDO;
                riga[2] = dettaglio.PREFISSO;
                riga[3] = dettaglio.PARTE;
                riga[4] = dettaglio.COLORE;
                riga[5] = dettaglio.COMMESSAORDINE;

                string meseCollaudo = creaStringaCartellaMeseCollaudo(dataSelezionata);
                string giornoCollaudo = creaStringaCartellaGiornoCollaudo(dataSelezionata);

                string cartellaArticolo = string.Format("{0}-{1}-{2} {3}", dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE.Replace('_', ' '));

                string cartella = string.Format(@"{0}\{1}\{2}\{3}\{4}", Contesto.PathCollaudo, riga[0], meseCollaudo, giornoCollaudo, cartellaArticolo);
                riga[idColonnaCartella] = cartella;

                dtCartelle.Rows.Add(riga);
            }

        }

        private string creaStringaCartellaGiornoCollaudo(DateTime dataSelezionata)
        {
            return string.Format("collaudo {0}-{1}-{2}", dataSelezionata.Day, dataSelezionata.Month, dataSelezionata.Year % 100);
        }
        private string creaStringaCartellaMeseCollaudo(DateTime dataSelezionata)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
            string mese = dataSelezionata.ToString("MMMM", culture);
            return string.Format("COLLAUDI {0} {1}", mese.ToUpper(), dataSelezionata.Year);
        }
    }
}
