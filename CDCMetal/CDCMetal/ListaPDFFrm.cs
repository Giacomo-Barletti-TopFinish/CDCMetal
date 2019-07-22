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
    public partial class ListaPDFFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "LISTAPDF";

        public ListaPDFFrm()
        {
            InitializeComponent();
        }

        private void ListaPDFFrm_Load(object sender, EventArgs e)
        {
            PopolaDDLDate();
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

            DataCollaudo dataSelezionata = (DataCollaudo)ddlDataCollaudo.SelectedItem;

            CDCBLL bll = new CDCBLL();

            Contesto.DS = new Entities.CDCDS();

            bll.LeggiCollaudoDaData(Contesto.DS, dataSelezionata);


            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                List<decimal> IDDETTAGLIO = Contesto.DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.CDC_PDF(Contesto.DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerListaPDF();

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

            ((DataGridViewCheckBoxColumn)dgvDettaglio.Columns[9]).HeaderText = "CDC";
            ((DataGridViewCheckBoxColumn)dgvDettaglio.Columns[10]).HeaderText = "DIMENSIONE";
            ((DataGridViewCheckBoxColumn)dgvDettaglio.Columns[11]).HeaderText = "ALLERGICO";
            ((DataGridViewCheckBoxColumn)dgvDettaglio.Columns[12]).HeaderText = "COLORE";
            ((DataGridViewCheckBoxColumn)dgvDettaglio.Columns[13]).HeaderText = "MISURA";
        }

        private void CreaDsPerListaPDF( )
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

            //9
            dtCartelle.Columns.Add("CDC", Type.GetType("System.Boolean")).ReadOnly = true;
            dtCartelle.Columns.Add("DIM", Type.GetType("System.Boolean")).ReadOnly = true;
            dtCartelle.Columns.Add("ANT", Type.GetType("System.Boolean")).ReadOnly = true;
            dtCartelle.Columns.Add("COL", Type.GetType("System.Boolean")).ReadOnly = true;
            dtCartelle.Columns.Add("MIS", Type.GetType("System.Boolean")).ReadOnly = true;


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in Contesto.DS.CDC_DETTAGLIO)
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

                CDCDS.CDC_PDFRow PDF = Contesto.DS.CDC_PDF.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOCONFORMITA).FirstOrDefault();
                riga[9] = PDF == null ? false : true;

                PDF = Contesto.DS.CDC_PDF.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATODIMENSIONALE).FirstOrDefault();
                riga[10] = PDF == null ? false : true;

                PDF = Contesto.DS.CDC_PDF.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOANTIALLERGICO).FirstOrDefault();
                riga[11] = PDF == null ? false : true;

                PDF = Contesto.DS.CDC_PDF.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOCOLORIMETRICO).FirstOrDefault();
                riga[12] = PDF == null ? false : true;

                PDF = Contesto.DS.CDC_PDF.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOSPESSORE).FirstOrDefault();
                riga[13] = PDF == null ? false : true;

                dtCartelle.Rows.Add(riga);
            }

        }
    }
}
