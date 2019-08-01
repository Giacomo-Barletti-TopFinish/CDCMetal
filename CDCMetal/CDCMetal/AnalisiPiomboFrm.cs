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
    public partial class AnalisiPiomboFrm : BaseChildForm
    {
        public AnalisiPiomboFrm()
        {
            InitializeComponent();
        }
        private const string barraTonda = "Barra tonda";
        private const string piatto = "Piatto";
        private void AnalisiPiomboFrm_Load(object sender, EventArgs e)
        {
            CaricaDDLMateriale();
            CaricaDDLElemento();
            dtDataCertificato.Value = DateTime.Today;
        }

        private void CaricaDDLMateriale()
        {
            ddlMateriale.Items.Add(new Materiale("OTTONE CON PIOMBO", 8.40m));
            ddlMateriale.Items.Add(new Materiale("OTTONE SENZA PIOMBO", 8.36m));
        }

        private void CaricaDDLElemento()
        {
            ddlElemento.Items.Add(barraTonda);
            ddlElemento.Items.Add(piatto);
        }
        private void ddlElemento_SelectedIndexChanged(object sender, EventArgs e)
        {
            nLarghezza.Value = 0;
            nLarghezza.Value = 0;
            nSpessore.Value = 0;

            if (ddlElemento.SelectedItem as string == barraTonda)
            {
                lblLarghezza.Text = "Diametro (mm)";
                lblSpessore.Visible = false;
                nSpessore.Visible = false;
            }
            else
            {
                lblLarghezza.Text = "Larghezza (mm)";
                lblSpessore.Visible = true;
                nSpessore.Visible = true;
            }
            calcolaVolumeEPeso();
        }

        private void calcolaVolumeEPeso()
        {
            decimal volume = 0;
            if (ddlElemento.SelectedItem as string == barraTonda)
            {
                decimal raggio = nLarghezza.Value / 2;
                volume = (decimal)Math.PI * raggio * raggio * nLunghezza.Value;
            }
            else
            {
                volume = nLunghezza.Value * nLarghezza.Value * nSpessore.Value;
            }
            volume = volume / 1000;
            txtVolume.Text = volume.ToString();
            if (ddlMateriale.SelectedIndex != -1)
            {
                Materiale m = (Materiale)ddlMateriale.SelectedItem;
                decimal peso = volume * m.PesoSpecifico;
                txtPeso.Text = peso.ToString();
            }

        }

        private void nLunghezza_ValueChanged(object sender, EventArgs e)
        {
            calcolaVolumeEPeso();
        }

        private void ddlMateriale_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcolaVolumeEPeso();
        }

        private void CalcolaEsito()
        {
            Color colore;
            CDCBLL bll = new CDCBLL();
            txtEsito.Text = bll.CalcolaEsitoAnalisiPiombo(nPd.Value, out colore);
            txtEsito.BackColor = colore;
        }

        private void CalcolaPiombo()
        {
            if (nPesoCampione.Value == 0) return;
            decimal Piombo = (nConcentrazione.Value * nMatracciolo.Value) / nPesoCampione.Value;
            if (Piombo > nPd.Maximum)
            {
                string messaggio = string.Format("Valore del piombo (PPM) superiore a {0}. Nella finestra verrà visualizzato il valore massimo consentito di {0}", nPd.Maximum);
                MessageBox.Show(messaggio, "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Piombo = nPd.Maximum;
            }
            nPd.Value = Piombo;
        }

        private void nM_ValueChanged(object sender, EventArgs e)
        {
            CalcolaPiombo();
            CalcolaEsito();
        }

        private void btnCreaPDF_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            bool esito = true;
            if (ddlElemento.SelectedIndex == -1)
            {
                esito = false;
                sb.AppendLine("Indicare l'elemento da analizzare");
            }

            if (string.IsNullOrEmpty(txtCodice.Text))
            {
                esito = false;
                sb.AppendLine("Indicare il codice campione");
            }

            if (ddlMateriale.SelectedIndex == -1)
            {
                esito = false;
                sb.AppendLine("Indicare il materiale");
            }

            if (string.IsNullOrEmpty(txtLotto.Text))
            {
                esito = false;
                sb.AppendLine("Indicare il lotto");
            }

            if (nLunghezza.Value == 0)
            {
                esito = false;
                sb.AppendLine("Indicare la lunghezza");
            }

            if (ddlElemento.SelectedItem as string == barraTonda && nLarghezza.Value == 0)
            {
                esito = false;
                sb.AppendLine("Indicare il diametro");
            }

            if (ddlElemento.SelectedItem as string != barraTonda && nLarghezza.Value == 0)
            {
                esito = false;
                sb.AppendLine("Indicare la larghezza");
            }

            if (ddlElemento.SelectedItem as string != barraTonda && nSpessore.Value == 0)
            {
                esito = false;
                sb.AppendLine("Indicare lo spessore");
            }
            try
            {
                SalvaCertificatoPiombo();

                Bitmap firma = Properties.Resources.logo_tf_autodichiarazione;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                CDCBLL bll = new CDCBLL();
                string path = bll.CreaPDFCertificatoPiombo(ddlElemento.SelectedItem as string, nLunghezza.Value.ToString(), nLarghezza.Value.ToString(), nSpessore.Value.ToString(), txtCodice.Text, txtLotto.Text,
                     txtEsito.Text, txtEsito.BackColor, txtMetodo.Text, dtDataCertificato.Value, nPd.Value, nCd.Value, Contesto.PathAnalisiPiombo, image);

                string messaggio = string.Format("Il file {0} è stato creato", path);
                MessageBox.Show(messaggio, "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MainForm.LogScriviErrore("ERRORE IN CREA PDF", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }

        }

        private void SalvaCertificatoPiombo()
        {
            CDCDS ds = new CDCDS();
            CDCDS.CDC_CERTIFICATIPIOMBORow cPiombo = ds.CDC_CERTIFICATIPIOMBO.NewCDC_CERTIFICATIPIOMBORow();
            cPiombo.ELEMENTO = ddlElemento.SelectedItem as string;
            cPiombo.CODICE = txtCodice.Text;
            Materiale materiale = ddlMateriale.SelectedItem as Materiale;
            cPiombo.MATERIALE = materiale.Nome;
            cPiombo.LOTTO = txtLotto.Text;
            cPiombo.LUNGHEZZA = nLunghezza.Value;
            cPiombo.LARGHEZZA = nLarghezza.Value;
            cPiombo.SPESSORE = nSpessore.Value;
            cPiombo.METODO = txtMetodo.Text;
            cPiombo.PESOCAMPIONE = nPesoCampione.Value;
            cPiombo.MATRACCIOLO = nMatracciolo.Value;
            cPiombo.CONCENTRAZIONE = nConcentrazione.Value;
            cPiombo.PBPPM = nPd.Value;
            cPiombo.CDPPM = nCd.Value;
            cPiombo.ESITO = txtEsito.Text;
            cPiombo.DATACERTIFICATO = dtDataCertificato.Value;
            cPiombo.UTENTE = Contesto.Utente.FULLNAMEUSER;
            cPiombo.DATAINSERIMENTO = DateTime.Now;
            ds.CDC_CERTIFICATIPIOMBO.AddCDC_CERTIFICATIPIOMBORow(cPiombo);

            CDCBLL bll = new CDCBLL();
            bll.SalvaCertificatiPiombo(ds);

        }
    }

    public class Materiale
    {
        public string Nome;
        public decimal PesoSpecifico;

        public Materiale(string Nome, decimal PesoSpecifico)
        {
            this.PesoSpecifico = PesoSpecifico;
            this.Nome = Nome;
        }

        public override string ToString()
        {
            return Nome;
        }
    }

}
