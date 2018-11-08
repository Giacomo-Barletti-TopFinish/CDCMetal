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
    public partial class CreaAntiallergicoFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "ANTIALLERGICO";
        public CreaAntiallergicoFrm()
        {
            InitializeComponent();
        }

        private void CreaAntiallergicoFrm_Load(object sender, EventArgs e)
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
                return;
            }

            DateTime dataSelezionata = (DateTime)ddlDataCollaudo.SelectedItem;

            CDCBLL bll = new CDCBLL();

            Contesto.DS = new Entities.CDCDS();

            bll.LeggiCollaudoDaData(Contesto.DS, dataSelezionata);


            if (Contesto.DS.CDC_DETTAGLIO.Count > 0)
            {
                btnCreaPDF.Enabled = true;
                List<decimal> IDDETTAGLIO = Contesto.DS.CDC_DETTAGLIO.Select(x => x.IDDETTAGLIO).Distinct().ToList();
                bll.FillCDC_ANTIALLERGICO(Contesto.DS, IDDETTAGLIO);
                bll.CDC_PDF(Contesto.DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerAntiallergico(dataSelezionata);

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
            dgvDettaglio.Columns[9].Width = 130;

        }

        private void CreaDsPerAntiallergico(DateTime dataSelezionata)
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
            dtCartelle.Columns.Add("ANTIALLERGICO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("NICHELFREE", Type.GetType("System.Boolean"));


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

                CDCDS.CDC_ANTIALLERGICORow antiallergico = Contesto.DS.CDC_ANTIALLERGICO.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO).FirstOrDefault();
                if (antiallergico != null)
                {
                    riga[9] = antiallergico.DATAPRODUZIONE;
                    riga[10] = antiallergico.NICHELFREE == "S" ? false : true;
                    riga[11] = antiallergico.NICHELFREE == "S" ? true : false;
                }
                else
                {
                    riga[9] = DateTime.Today;
                    riga[10] = false;
                    riga[11] = false;
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
                    bool aux1 = (bool)riga[10];
                    bool aux2 = (bool)riga[11];
                    if (aux1 == aux2)
                        esito = false;
                }

                if (!esito)
                {
                    lblMessaggio.Text = "Impossibile creare i file. La condizione Antiallergico e Nichel Free sono mutuamente esclusive.";
                    return;
                }
                List<decimal> idPerPDF = new List<decimal>();
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    decimal iddettaglio = (decimal)riga[0];
                    idPerPDF.Add(iddettaglio);
                    CDCDS.CDC_ANTIALLERGICORow antiallergicoRow = Contesto.DS.CDC_ANTIALLERGICO.Where(x => x.IDDETTAGLIO == iddettaglio).FirstOrDefault();
                    if (antiallergicoRow == null)
                    {
                        antiallergicoRow = Contesto.DS.CDC_ANTIALLERGICO.NewCDC_ANTIALLERGICORow();
                        antiallergicoRow.IDDETTAGLIO = iddettaglio;
                        antiallergicoRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        antiallergicoRow.DATAINSERIMENTO = DateTime.Now;
                        antiallergicoRow.DATAPRODUZIONE = (DateTime)riga[9];

                        antiallergicoRow.NICHELFREE = ConvertiBoolInStringa(riga[11]);
                        Contesto.DS.CDC_ANTIALLERGICO.AddCDC_ANTIALLERGICORow(antiallergicoRow);
                    }
                    else
                    {
                        antiallergicoRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        antiallergicoRow.DATAINSERIMENTO = DateTime.Now;
                        antiallergicoRow.DATAPRODUZIONE = (DateTime)riga[9];

                        antiallergicoRow.NICHELFREE = ConvertiBoolInStringa(riga[11]);
                    }

                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiAntiallergia(Contesto.DS);
                Contesto.DS.CDC_ANTIALLERGICO.AcceptChanges();

                Bitmap firma = Properties.Resources.logo_tf_autodichiarazione;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                fileCreati = bll.CreaPDFAntiallergico(idPerPDF, Contesto.DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.PathRefertiLaboratorio);
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

        private void dgvDettaglio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == 10)
                dgvDettaglio.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = (bool)dgvDettaglio.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (e.ColumnIndex == 11)
                dgvDettaglio.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = (bool)dgvDettaglio.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }


    }
}
