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
    public partial class AssociaCertificatiPiomboFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private CDCDS.CDC_DETTAGLIORow _dettaglio;

        public AssociaCertificatiPiomboFrm()
        {
            InitializeComponent();
        }

        private void AssociaCertificatiPiomboFrm_Load(object sender, EventArgs e)
        {
            CDCBLL bll = new CDCBLL();
            PopolaDDLDate();
            bll.LeggiMateriaPrimaArticoli(_DS);

            CaricaCertificatiPiombo();
        }

        private const string barraTonda = "Barra tonda";
        private const string piatto = "Piatto";

        private void CaricaCertificatiPiombo()
        {
            _DS.CDC_CERTIFICATIPIOMBO.Clear();
            CDCBLL bll = new CDCBLL();
            bll.FillCDC_CERTIFICATIPIOMBO(_DS);

            foreach (CDCDS.CDC_CERTIFICATIPIOMBORow certificato in _DS.CDC_CERTIFICATIPIOMBO)
            {
                decimal volume = 0;
                decimal pesoSpecifico = 8.40m;

                if (certificato.MATERIALE == "OTTONE SENZA PIOMBO") pesoSpecifico = 8.36m;
                decimal peso = volume * pesoSpecifico;

                CertificatoPiombo cp = new CertificatoPiombo()
                {
                    Descrizione = certificato.CODICE,
                    IDCERTIFICATIPIOMBO = certificato.IDCERTIFICATIPIOMBO,
                    Peso = peso,
                    Volume = volume,
                    DataCertificato = certificato.DATACERTIFICATO,
                    path = certificato.PATHFILE
                };
                lstCertificatiDaAssociare.Items.Add(cp);
            }
        }

        private void PopolaDDLDate()
        {
            ddlDataCollaudo.Items.AddRange(CaricaDateCollaudo().ToArray());
        }

        private void btnLeggiDati_Click(object sender, EventArgs e)
        {

            lblMessaggio.Text = string.Empty;
            if (ddlDataCollaudo.SelectedIndex == -1)
            {
                lblMessaggio.Text = "Selezionare una data";
                return;
            }
            try
            {
                DataCollaudo dataSelezionata = (DataCollaudo)ddlDataCollaudo.SelectedItem;

                CDCBLL bll = new CDCBLL();

                _DS.CDC_EXCEL.Clear();
                _DS.CDC_DETTAGLIO.Clear();
                _dettaglio = null;

                bll.LeggiCollaudoDaData(_DS, dataSelezionata);


                if (_DS.CDC_DETTAGLIO.Count > 0)
                {
                    List<decimal> IDDETTAGLIO = _DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                    bll.FillCDC_ASSOCIAZIONEPIOMBO(_DS, IDDETTAGLIO);
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

                foreach (DataGridViewColumn column in dgvDettaglio.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                evidenziaDettagliAssociati();
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in leggi dati");
            }

        }

        private void evidenziaDettagliAssociati()
        {
            List<decimal> iddettaglioConPdf = _DS.CDC_ASSOCIAZIONEPIOMBO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
            foreach (DataGridViewRow riga in dgvDettaglio.Rows)
            {
                decimal IDDETTAGLIO = (decimal)riga.Cells["IDDETTAGLIO"].Value;
                if (iddettaglioConPdf.Contains(IDDETTAGLIO))
                {
                    riga.Cells[1].Style.BackColor = Color.Yellow;
                }
            }

        }

        private void dgvDettaglio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblMessaggio.Text = string.Empty;
                txtMateriaPrima.Text = string.Empty;

                if (e.RowIndex == -1) return;
                DataRow r = _DS.CDC_DETTAGLIO.Rows[e.RowIndex];
                decimal IDDETTAGLIO = (decimal)r[0];
                _dettaglio = _DS.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();

                lstCertificatiAssociati.Items.Clear();
                lstCertificatiDaAssociare.Items.Clear();
                CaricaCertificatiPiombo();
                CDCDS.CDC_MATERIAPRIMARow materiaPrima = _DS.CDC_MATERIAPRIMA.Where(x => x.PARTE == _dettaglio.PARTE).FirstOrDefault();
                if (materiaPrima != null)
                    txtMateriaPrima.Text = materiaPrima.MATERIAPRIMA;
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore in dgvDettaglio_CellClick");
            }
        }


        private void btnAssocia_Click(object sender, EventArgs e)
        {
            if (lstCertificatiDaAssociare.SelectedIndex != -1)
            {
                CertificatoPiombo cp = (CertificatoPiombo)lstCertificatiDaAssociare.SelectedItem;
                lstCertificatiDaAssociare.Items.Remove(cp);
                lstCertificatiAssociati.Items.Add(cp);
            }
        }

        private void btnRimuovi_Click(object sender, EventArgs e)
        {
            if (lstCertificatiAssociati.SelectedIndex != -1)
            {
                CertificatoPiombo cp = (CertificatoPiombo)lstCertificatiAssociati.SelectedItem;
                lstCertificatiAssociati.Items.Remove(cp);
                lstCertificatiDaAssociare.Items.Add(cp);
            }
        }

        private void btnCopiaCertificati_Click(object sender, EventArgs e)
        {
            try
            {
                CDCDS.CDC_MATERIAPRIMARow materiaPrima = _DS.CDC_MATERIAPRIMA.Where(x => x.PARTE == _dettaglio.PARTE).FirstOrDefault();
                if (materiaPrima != null)
                    materiaPrima.MATERIAPRIMA = txtMateriaPrima.Text;
                else
                {
                    materiaPrima = _DS.CDC_MATERIAPRIMA.NewCDC_MATERIAPRIMARow();
                    materiaPrima.PARTE = _dettaglio.PARTE;
                    materiaPrima.MATERIAPRIMA = txtMateriaPrima.Text;
                    _DS.CDC_MATERIAPRIMA.AddCDC_MATERIAPRIMARow(materiaPrima);
                }

                if (lstCertificatiAssociati.Items.Count == 0)
                {
                    MessageBox.Show("Associare almeno un certificato di analisi del piombo", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_dettaglio == null)
                {
                    MessageBox.Show("Selezionare una riga dalla griglia", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                StringBuilder sb = new StringBuilder();
                StringBuilder sbNonTrovati = new StringBuilder();

                CDCBLL bll = new CDCBLL();
                foreach (CertificatoPiombo cp in lstCertificatiAssociati.Items)
                {

                    //CDCDS.CDC_CERTIFICATIPIOMBORow certificato = _DS.CDC_CERTIFICATIPIOMBO.Where(x => x.IDCERTIFICATIPIOMBO == cp.IDCERTIFICATIPIOMBO).FirstOrDefault();
                    //if (certificato == null)
                    //{
                    //    MessageBox.Show("Errore nel recuperare il certificato IDCERTIFICATIPIOMBO:" + cp.IDCERTIFICATIPIOMBO.ToString(), "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //string cartella, nomeCampione;
                    //string spessore = certificato.IsSPESSORENull() ? string.Empty : certificato.SPESSORE.ToString();

                    //                    string fileDaCopiare = bll.CreaNomefileCertificatiAnalisiPiombo(certificato.ELEMENTO, certificato.LUNGHEZZA.ToString(), certificato.LARGHEZZA.ToString(), spessore, certificato.CODICE, certificato.DATACERTIFICATO, Contesto.PathAnalisiPiombo, out cartella, out nomeCampione);

                    string fileDaCopiare = cp.path;
                    DateTime dt = DateTime.Today;
                    DateTime dtCollaudo = DateTime.ParseExact(_dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string pathDestinazione = CDCBLL.CreaPathCartella(dtCollaudo, Contesto.PathCollaudo, _dettaglio.ACCESSORISTA, _dettaglio.PREFISSO, _dettaglio.PARTE, _dettaglio.COLORE, _dettaglio.COMMESSAORDINE);
                    if (File.Exists(fileDaCopiare))
                    {
                        FileInfo fi = new FileInfo(fileDaCopiare);
                        string fileDestinazione = Path.Combine(pathDestinazione, fi.Name);
                        sb.AppendLine(fileDestinazione);
                        if (!Directory.Exists(pathDestinazione))
                            Directory.CreateDirectory(pathDestinazione);
                        File.Copy(fileDaCopiare, fileDestinazione, true);
                    }
                    else
                        sbNonTrovati.AppendLine(fileDaCopiare);
                }
                bll.SalvaMateriaPrima(_DS);

                string messaggio = string.Format("I seguenti file sono stati copiati. {0}", sb.ToString());
                MessageBox.Show(messaggio, "INFORMAZIONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (sbNonTrovati.Length > 0)
                {
                    messaggio = string.Format("I seguenti file NON sono stati trovati e quindi NON SONO STATI COPIATI I CERTIFICATI E NON E' STATO ASSOCIATO IL CERTIFICATO. {0}", sb.ToString());
                    MessageBox.Show(messaggio, "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MostraEccezione(ex, "Errore salva associazione ");

            }
        }

    }

    public class CertificatoPiombo
    {
        public decimal IDCERTIFICATIPIOMBO;
        public string Descrizione;
        public decimal Volume;
        public decimal Peso;
        public DateTime DataCertificato;
        public string path;
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Descrizione, DataCertificato.ToShortDateString(), Peso / 1000);
        }
    }
}
