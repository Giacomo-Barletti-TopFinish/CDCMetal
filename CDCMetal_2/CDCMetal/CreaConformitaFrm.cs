﻿using CDCMetal.BLL;
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
                bll.FillCDC_CONFORMITA(_DS, IDDETTAGLIO);
                bll.CDC_PDF(_DS, IDDETTAGLIO);
            }
            else
            {
                lblMessaggio.Text = "Nessuna riga trovata per questa data";
            }

            CreaDsPerCartificati();

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
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[8]).MaxInputLength = 50;
            dgvDettaglio.Columns[8].Width = 170;
            dgvDettaglio.Columns[17].Visible = false; ;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[15]).MaxInputLength = 50;
            ((DataGridViewTextBoxColumn)dgvDettaglio.Columns[16]).MaxInputLength = 50;

        }

        private void CreaDsPerCartificati()
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
            dtCartelle.Columns.Add("DESCRIZIONE", Type.GetType("System.String"));
            dtCartelle.Columns.Add("FISICO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("FUNZIONALE", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("DIMENSIONALE", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("ESTETICO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("ACCONTO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("SALDO", Type.GetType("System.Boolean"));
            dtCartelle.Columns.Add("ALTRO", Type.GetType("System.String"));
            dtCartelle.Columns.Add("CERTIFICATI", Type.GetType("System.String"));

            dtCartelle.Columns.Add("IDDETTAGLIO", Type.GetType("System.Decimal")).ReadOnly = true;


            foreach (CDCDS.CDC_DETTAGLIORow dettaglio in _DS.CDC_DETTAGLIO)
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
                riga[17] = dettaglio.IDDETTAGLIO;
                CDCDS.CDC_CONFORMITARow conformita = _DS.CDC_CONFORMITA.Where(x => x.IDDETTAGLIO == dettaglio.IDDETTAGLIO).FirstOrDefault();
                if (conformita != null)
                {
                    riga[8] = conformita.DESCRIZIONE;
                    riga[9] = conformita.FISICOCHIMICO == "S" ? true : false;
                    riga[10] = conformita.FUNZIONALE == "S" ? true : false;
                    riga[11] = conformita.DIMENSIONALE == "S" ? true : false;
                    riga[12] = conformita.ESTETICO == "S" ? true : false;
                    riga[13] = conformita.ACCONTO == "S" ? true : false;
                    riga[14] = conformita.SALDO == "S" ? true : false;
                    riga[15] = conformita.IsALTRONull() ? string.Empty : conformita.ALTRO;
                    riga[16] = conformita.IsCERTIFICATINull() ? string.Empty : conformita.CERTIFICATI;
                }
                else
                {
                    riga[9] = true;
                    riga[11] = true;
                    riga[12] = true;

                    CDCDS.CDC_CONFORMITA_DETTAGLIORow descrizione = _DS.CDC_CONFORMITA_DETTAGLIO.
                        Where(x => x.PARTE == dettaglio.PARTE && x.PREFISSO == dettaglio.PREFISSO && x.COLORE == dettaglio.COLORE).FirstOrDefault();
                    if (descrizione != null)
                        riga[8] = descrizione.DESCRIZIONE;
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
                    if (riga[8] == DBNull.Value || string.IsNullOrEmpty((string)riga[7]))
                        esito = false;
                }

                if (!esito)
                {
                    lblMessaggio.Text = "Impossibile creare i file. Ci sono delle descrizioni vuote";
                    return;
                }
                List<decimal> idPerPDF = new List<decimal>();
                foreach (DataRow riga in _dsServizio.Tables[tableName].Rows)
                {
                    decimal iddettaglio = (decimal)riga[17];
                    idPerPDF.Add(iddettaglio);
                    CDCDS.CDC_CONFORMITARow conformitaRow = _DS.CDC_CONFORMITA.Where(x => x.IDDETTAGLIO == iddettaglio).FirstOrDefault();
                    if (conformitaRow == null)
                    {
                        conformitaRow = _DS.CDC_CONFORMITA.NewCDC_CONFORMITARow();
                        conformitaRow.IDCONFORMITA = 0;               
                        conformitaRow.IDDETTAGLIO = iddettaglio;
                        conformitaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        conformitaRow.DATAINSERIMENTO = DateTime.Now;
                        conformitaRow.FISICOCHIMICO = ConvertiBoolInStringa(riga[9]);
                        conformitaRow.FUNZIONALE = ConvertiBoolInStringa(riga[10]);
                        conformitaRow.DIMENSIONALE = ConvertiBoolInStringa(riga[11]);
                        conformitaRow.ESTETICO = ConvertiBoolInStringa(riga[12]);
                        conformitaRow.ACCONTO = ConvertiBoolInStringa(riga[13]);
                        conformitaRow.SALDO = ConvertiBoolInStringa(riga[14]);
                        conformitaRow.DESCRIZIONE = ((string)riga[8]).ToUpper().Trim();
                        if (riga[15] == DBNull.Value)
                            conformitaRow.SetALTRONull();
                        else
                        {
                            string aux = (string)riga[15];
                            if (string.IsNullOrEmpty(aux))
                                conformitaRow.SetALTRONull();
                            else
                                conformitaRow.ALTRO = aux;
                        }

                        if (riga[16] == DBNull.Value)
                            conformitaRow.SetALTRONull();
                        else
                        {
                            string aux = (string)riga[16];
                            if (string.IsNullOrEmpty(aux))
                                conformitaRow.SetCERTIFICATINull();
                            else
                                conformitaRow.CERTIFICATI = aux;
                        }

                        _DS.CDC_CONFORMITA.AddCDC_CONFORMITARow(conformitaRow);
                    }
                    else
                    {
                        conformitaRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        conformitaRow.DATAINSERIMENTO = DateTime.Now;
                        conformitaRow.FISICOCHIMICO = ConvertiBoolInStringa(riga[9]);
                        conformitaRow.FUNZIONALE = ConvertiBoolInStringa(riga[10]);
                        conformitaRow.DIMENSIONALE = ConvertiBoolInStringa(riga[11]);
                        conformitaRow.ESTETICO = ConvertiBoolInStringa(riga[12]);
                        conformitaRow.ACCONTO = ConvertiBoolInStringa(riga[13]);
                        conformitaRow.SALDO = ConvertiBoolInStringa(riga[14]);
                        conformitaRow.DESCRIZIONE = ((string)riga[8]).ToUpper().Trim();
                        if (riga[15] == DBNull.Value)
                            conformitaRow.SetALTRONull();
                        else
                        {
                            string aux = (string)riga[15];
                            if (string.IsNullOrEmpty(aux))
                                conformitaRow.SetALTRONull();
                            else
                                conformitaRow.ALTRO = aux;
                        }

                        if (riga[16] == DBNull.Value)
                            conformitaRow.SetCERTIFICATINull();
                        else
                        {
                            string aux = (string)riga[16];
                            if (string.IsNullOrEmpty(aux))
                                conformitaRow.SetCERTIFICATINull();
                            else
                                conformitaRow.CERTIFICATI = aux;
                        }
                    }

                    string parte = (string)riga[4];
                    string prefisso = (string)riga[3];
                    string colore = (string)riga[5];

                    CDCDS.CDC_CONFORMITA_DETTAGLIORow dettaglioRow = _DS.CDC_CONFORMITA_DETTAGLIO.Where(x => x.PARTE == parte && x.PREFISSO == prefisso && x.COLORE == colore).FirstOrDefault();
                    if (dettaglioRow == null)
                    {
                        dettaglioRow = _DS.CDC_CONFORMITA_DETTAGLIO.NewCDC_CONFORMITA_DETTAGLIORow();
                        dettaglioRow.PREFISSO = prefisso;
                        dettaglioRow.PARTE = parte;
                        dettaglioRow.COLORE = colore;
                        dettaglioRow.DESCRIZIONE = ((string)riga[8]).ToUpper().Trim();
                        dettaglioRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        dettaglioRow.DATAINSERIMENTO = DateTime.Now;
                        _DS.CDC_CONFORMITA_DETTAGLIO.AddCDC_CONFORMITA_DETTAGLIORow(dettaglioRow);
                    }
                    else
                    {   dettaglioRow.DESCRIZIONE = ((string)riga[8]).ToUpper().Trim();
                        dettaglioRow.UTENTE = Contesto.Utente.FULLNAMEUSER;
                        dettaglioRow.DATAINSERIMENTO = DateTime.Now;
                    }
                }

                CDCBLL bll = new CDCBLL();
                bll.SalvaDatiConformita(_DS);
                _DS.AcceptChanges();
                Bitmap firma = Properties.Resources.FirmaCDC;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

                fileCreati = bll.CreaPDFConformita(idPerPDF, _DS, Contesto.PathCollaudo, image, Contesto.Utente.FULLNAMEUSER);
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
