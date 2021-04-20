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
    public partial class CreaTenutaAcidoNitricoFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "TENUTAACIDONITRICO";

        public CreaTenutaAcidoNitricoFrm()
        {
            InitializeComponent();
        }

        private void CreaTenutaAcidoNitricoFrm_Load(object sender, EventArgs e)
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
                bll.FillCDC_TENUTACIDONITRICO(_DS, IDDETTAGLIO);
                bll.CDC_PDF(_DS, IDDETTAGLIO);

                bll.CaricaArticoli(_DS);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerTenutaAcidoNitrico();

            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;

            dgvDettaglio.Columns[0].Frozen = true;
            dgvDettaglio.Columns[0].Visible = false;
            dgvDettaglio.Columns[1].Frozen = true;
            dgvDettaglio.Columns[1].Visible = false;

            dgvDettaglio.Columns[2].Frozen = true;
            dgvDettaglio.Columns[3].Frozen = true;
            dgvDettaglio.Columns[4].Frozen = true;

            dgvDettaglio.Columns[5].Frozen = true;
            dgvDettaglio.Columns[5].Width = 80;
            dgvDettaglio.Columns[6].Frozen = true;
            dgvDettaglio.Columns[6].Width = 80;
            dgvDettaglio.Columns[7].Frozen = true;
            dgvDettaglio.Columns[7].Width = 80;
            dgvDettaglio.Columns[8].Frozen = true;
            dgvDettaglio.Columns[8].Width = 80;

            dgvDettaglio.Columns[9].Frozen = true;
            dgvDettaglio.Columns[10].Frozen = true;

            dgvDettaglio.Columns[11].Frozen = true; //7
            dgvDettaglio.Columns[11].Width = 130; //7
            dgvDettaglio.Columns[12].Frozen = true;
            dgvDettaglio.Columns[12].Width = 80;

            dgvDettaglio.Columns[13].Width = 90; //9
            dgvDettaglio.Columns[16].Width = 130; //10
            dgvDettaglio.Columns[17].Width = 130;//13


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

        private void CreaDsPerTenutaAcidoNitrico()
        {

            _dsServizio = new DataSet();
            DataTable dtCartelle = _dsServizio.Tables.Add();
            dtCartelle.TableName = tableName;

            dtCartelle.Columns.Add("IDVERNICICOPRENTI", Type.GetType("System.Decimal"));
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
            dtCartelle.Columns.Add("DATATEST", Type.GetType("System.DateTime"));
            dtCartelle.Columns.Add("NUMEROCAMPIONI", Type.GetType("System.Decimal"));
            dtCartelle.Columns.Add("BOLLA", Type.GetType("System.String"));
            dtCartelle.Columns.Add("DATABOLLA", Type.GetType("System.DateTime"));
            dtCartelle.Columns.Add("ESITO", Type.GetType("System.Boolean"));


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in _DS.CDC_DETTAGLIO)
            {

                string PARTE = dettaglio.PARTE;
                string COLORE = dettaglio.COLORE;
                List<CDCDS.CDC_ARTICOLIRow> articoli = _DS.CDC_ARTICOLI.Where(x => x.PARTE == PARTE && x.COLORE == COLORE).OrderBy(x => x.SEQUENZA).ToList();


                if (articoli.Count == 0)
                {
                    DataRow riga = dtCartelle.NewRow();

                    riga[1] = dettaglio.IDDETTAGLIO;
                    riga[2] = dettaglio.IDPRENOTAZIONE;
                    riga[3] = dettaglio.ACCESSORISTA;
                    riga[4] = dettaglio.DATACOLLAUDO;
                    riga[5] = dettaglio.PREFISSO;
                    riga[6] = dettaglio.PARTE;
                    riga[7] = dettaglio.COLORE;

                    riga[11] = dettaglio.COMMESSAORDINE;
                    riga[12] = dettaglio.QUANTITA;

                    dtCartelle.Rows.Add(riga);


                }
                else
                {
                    foreach (CDCDS.CDC_ARTICOLIRow articolo in articoli)
                    {


                        DataRow riga = dtCartelle.NewRow();


                        riga[1] = dettaglio.IDDETTAGLIO;
                        riga[2] = dettaglio.IDPRENOTAZIONE;
                        riga[3] = dettaglio.ACCESSORISTA;
                        riga[4] = dettaglio.DATACOLLAUDO;
                        riga[5] = dettaglio.PREFISSO;
                        riga[6] = dettaglio.PARTE;
                        riga[7] = dettaglio.COLORE;

                        riga[8] = articolo.SEQUENZA;
                        riga[9] = articolo.DESCRIZIONE;
                        riga[10] = articolo.COLORECOMPONENTE;

                        riga[11] = dettaglio.COMMESSAORDINE;
                        riga[12] = dettaglio.QUANTITA;

                        CDCDS.CDC_TENUTACIDONITRICORow tenuta = _DS.CDC_TENUTACIDONITRICO.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO && x.SEQUENZA == articolo.SEQUENZA).FirstOrDefault();
                        if (tenuta != null)
                        {
                            riga[0] = tenuta.IDTENUTA;
                            riga[13] = tenuta.DATATEST;
                            riga[14] = tenuta.NUMEROCAMPIONI;
                            riga[15] = tenuta.IsBOLLANull() ? string.Empty : tenuta.BOLLA;
                            if (!tenuta.IsDATADDTNull())
                                riga[16] = tenuta.DATADDT;
                            riga[17] = tenuta.ESITO == "S" ? true : false;
                        }
                        else
                        {
                            riga[0] = -1;
                            riga[13] = DateTime.Today;
                            riga[14] = 1;
                            riga[15] = string.Empty;
                            //     riga[16] = string.Empty;
                            riga[17] = true;
                        }

                        dtCartelle.Rows.Add(riga);
                    }
                }

                //DataRow riga = dtCartelle.NewRow();

                //riga[0] = dettaglio.IDDETTAGLIO;
                //riga[1] = dettaglio.IDPRENOTAZIONE;
                //riga[2] = dettaglio.ACCESSORISTA;
                //riga[3] = dettaglio.DATACOLLAUDO;
                //riga[4] = dettaglio.PREFISSO;
                //riga[5] = dettaglio.PARTE;
                //riga[6] = dettaglio.COLORE;
                //riga[7] = dettaglio.COMMESSAORDINE;
                //riga[8] = dettaglio.QUANTITA;

                //CDCDS.CDC_TENUTACIDONITRICORow tenuta = _DS.CDC_TENUTACIDONITRICO.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO).FirstOrDefault();
                //if (tenuta != null)
                //{
                //    riga[9] = tenuta.DATATEST;
                //    riga[10] = tenuta.NUMEROCAMPIONI;
                //    riga[11] = tenuta.IsBOLLANull()?string.Empty:tenuta.BOLLA;
                //    if(!tenuta.IsDATADDTNull())
                //        riga[12] = tenuta.DATADDT;
                //    riga[13] = tenuta.ESITO == "S" ? true : false;
                //}
                //else
                //{
                //    riga[9] = DateTime.Today;
                //    riga[10] = 1;
                //    riga[11] = string.Empty;
                //    //     riga[12] = string.Empty;
                //    riga[13] = true;
                //}

                //dtCartelle.Rows.Add(riga);
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

                List<decimal> idPerPDF = new List<decimal>();
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {

                    if (riga["SEQUENZA"].ToString() != "")
                    {

                        decimal idtenuta = (decimal)riga[0];
                        if (idtenuta == -1) { idtenuta = -100; } //
                        decimal iddettaglio = (decimal)riga[1];
                        int sequenza = (int)riga["SEQUENZA"];


                        if (!idPerPDF.Contains(iddettaglio))
                        { idPerPDF.Add(iddettaglio); }


                        string stringa = ConvertiInStringa(riga[15]);
                        stringa = stringa.Length > 25 ? stringa.Substring(0, 25) : stringa;


                        CDCDS.CDC_TENUTACIDONITRICORow tenutaRow = _DS.CDC_TENUTACIDONITRICO.Where(x => x.IDTENUTA == idtenuta).FirstOrDefault();
                        if (tenutaRow == null)
                        {
                            tenutaRow = _DS.CDC_TENUTACIDONITRICO.NewCDC_TENUTACIDONITRICORow();
                            tenutaRow.IDDETTAGLIO = iddettaglio;

                            tenutaRow.SEQUENZA = sequenza;
                            tenutaRow.DESCRIZIONE = riga["DESCRIZIONE"].ToString();
                            tenutaRow.COLORECOMPONENTE = riga["COLORECOMPONENTE"].ToString();

                            tenutaRow.DATATEST = (DateTime)riga[13];
                            tenutaRow.NUMEROCAMPIONI = (Decimal)riga[14];
                            tenutaRow.BOLLA = stringa;
                            if (riga[16] != DBNull.Value)
                                tenutaRow.DATADDT = (DateTime)riga[16];
                            tenutaRow.ESITO = ConvertiBoolInStringa(riga[17]);


                            tenutaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                            tenutaRow.DATAINSERIMENTO = DateTime.Now;
                            _DS.CDC_TENUTACIDONITRICO.AddCDC_TENUTACIDONITRICORow(tenutaRow);
                        }
                        else
                        {
                            tenutaRow.IDDETTAGLIO = iddettaglio;

                            tenutaRow.SEQUENZA = sequenza;
                            tenutaRow.DESCRIZIONE = riga["DESCRIZIONE"].ToString();
                            tenutaRow.COLORECOMPONENTE = riga["COLORECOMPONENTE"].ToString();

                            tenutaRow.DATATEST = (DateTime)riga[13];
                            tenutaRow.NUMEROCAMPIONI = (Decimal)riga[14];
                            tenutaRow.BOLLA = stringa;
                            if (riga[16] != DBNull.Value)
                                tenutaRow.DATADDT = (DateTime)riga[16];
                            tenutaRow.ESITO = ConvertiBoolInStringa(riga[17]);


                            tenutaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                            tenutaRow.DATAINSERIMENTO = DateTime.Now;
                        }

                    }
                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiTenutaAcidoNitrico(_DS);
                _DS.CDC_TENUTACIDONITRICO.AcceptChanges();

                Bitmap firma = Properties.Resources.logo_spessori_v2;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                //fileCreati = bll.CreaPDFTenutaAcidoNitrico(idPerPDF, _DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(((DataCollaudo)ddlDataCollaudo.SelectedItem).Brand));
                fileCreati = bll.CreaPDFTenutaAcidoNitrico(idPerPDF, _DS, Contesto.PathCollaudo, image, chkCopiaFileReferti.Checked, Contesto.GetPathRefertiLaboratorio(ddlBrand.SelectedItem.ToString()));

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
