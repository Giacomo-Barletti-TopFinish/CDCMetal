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
    public partial class CreaGestioneColoriFrm : BaseChildForm
    {
        private DataSet _dsServizio = new DataSet();
        private string tableName = "COLORI";

        public CreaGestioneColoriFrm()
        {
            InitializeComponent();
            PopolaDDLColore();
        }
        private void PopolaDDLColore()
        {
            ddlColore.Items.Add("");
            ddlColore.Items.AddRange(CaricaColoriSingle().ToArray());
        }

        private void ddlColore_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RIEMPI GRID  
            CDCBLL bll = new CDCBLL();
            _DS.CDC_COLORI.Clear();

            lblMessaggio.Text = string.Empty;
            if (ddlColore.SelectedIndex == -1)
            {
                //lblMessaggio.Text = "Selezionare un colore";
                return;
            }
            if (ddlColore.SelectedItem.ToString() == "")
            {
                //lblMessaggio.Text = "Selezionare un colore";
                return;
            }

       
            bll.CaricaColori(_DS, ddlColore.SelectedItem.ToString());


            CDCDS.CDC_COLORIRow colrowL = _DS.CDC_COLORI.Where(x => x.CODICE == CDCTipoColore.L).FirstOrDefault();
            if (colrowL == null)
            {
                colrowL = _DS.CDC_COLORI.NewCDC_COLORIRow();
                colrowL[1] = ddlColore.SelectedItem.ToString();
                colrowL[2] = CDCTipoColore.L;
                _DS.CDC_COLORI.AddCDC_COLORIRow(colrowL);
            }
            CDCDS.CDC_COLORIRow colrowa = _DS.CDC_COLORI.Where(x => x.CODICE == CDCTipoColore.a).FirstOrDefault();
            if (colrowa == null)
            {
                colrowa = _DS.CDC_COLORI.NewCDC_COLORIRow();
                colrowa[1] = ddlColore.SelectedItem.ToString();
                colrowa[2] = CDCTipoColore.a;
                _DS.CDC_COLORI.AddCDC_COLORIRow(colrowa);
            }
            CDCDS.CDC_COLORIRow colrowb = _DS.CDC_COLORI.Where(x => x.CODICE == CDCTipoColore.b).FirstOrDefault();
            if (colrowb == null)
            {
                colrowb = _DS.CDC_COLORI.NewCDC_COLORIRow();
                colrowb[1] = ddlColore.SelectedItem.ToString();
                colrowb[2] = CDCTipoColore.b;
                _DS.CDC_COLORI.AddCDC_COLORIRow(colrowb);
            }


            //CreaDsPerColori();


            dgvColori.ReadOnly = false;
            dgvColori.AllowUserToDeleteRows = false;
            dgvColori.AllowUserToAddRows = false;
            dgvColori.DataSource = _DS.CDC_COLORI;
            //dgvColori.DataSource = _dsServizio;
            //dgvColori.DataMember = tableName;
            dgvColori.Columns["IDCOLORE"].Visible = false;
            dgvColori.Columns["COLORECOMPONENTE"].ReadOnly = true;
            dgvColori.Columns["CODICE"].ReadOnly = true;
            dgvColori.Columns["UTENTE"].Visible = false;
            dgvColori.Columns["DATARIFERIMENTO"].Visible = false;
         

        }
        //private void CreaDsPerColori()
        //{

        //    _dsServizio = new DataSet();
        //    DataTable dtColori = _dsServizio.Tables.Add();
        //    dtColori.TableName = tableName;
        //    dtColori.Columns.Add("IDCOLORE", Type.GetType("System.Decimal")).ReadOnly = true;
        //    dtColori.Columns.Add("COLORECOMPONENTE", Type.GetType("System.String"));
        //    dtColori.Columns.Add("CODICE", Type.GetType("System.String"));
        //    dtColori.Columns.Add("RICHIESTO", Type.GetType("System.String"));
        //    dtColori.Columns.Add("TOLLERANZA", Type.GetType("System.String"));
        //    dtColori.Columns.Add("UNITAMISURA", Type.GetType("System.String"));
        //    dtColori.Columns.Add("UTENTE", Type.GetType("System.String")).ReadOnly = true;
        //    dtColori.Columns.Add("DATARIFERIMENTO", Type.GetType("System.DateTime")).ReadOnly = true;

        //    foreach (CDCDS.CDC_COLORIRow color in _DS.CDC_COLORI)
        //    {
        //        DataRow riga = dtColori.NewRow();

        //        riga[0] = color.IDCOLORE;
        //        riga[1] = color.COLORECOMPONENTE;
        //        riga[2] = color.CODICE;
        //        riga[3] = color.RICHIESTO;
        //        riga[4] = color.TOLLERANZA;
        //        riga[5] = color.UNITAMISURA;
        //        riga[6] = color.UTENTE;
        //        riga[7] = color.DATARIFERIMENTO;

        //        dtColori.Rows.Add(riga);
        //    }

        //    //cerco ed aggiungo riga L se manca
        //    CDCDS.CDC_COLORIRow coloreL = _DS.CDC_COLORI.Where(x => x.CODICE == CDCTipoColore.L).FirstOrDefault();
        //    if (coloreL == null)
        //    {
        //        DataRow riga = dtColori.NewRow();

        //        //riga[0] = color.IDCOLORE;
        //        riga[1] = ddlColore.SelectedItem.ToString();
        //        riga[2] = CDCTipoColore.L;
        //        //riga[3] = color.RICHIESTO;
        //        //riga[4] = color.TOLLERANZA;
        //        //riga[5] = color.UNITAMISURA;
        //        //riga[6] = color.UTENTE;
        //        //riga[7] = color.DATARIFERIMENTO;

        //        dtColori.Rows.Add(riga);
        //    }

     
     


        //}
        private void button1_Click(object sender, EventArgs e)
        {

            bool esito = true;

            foreach (CDCDS.CDC_COLORIRow riga in _DS.CDC_COLORI.Rows)
            {
                if (riga.RowState == DataRowState.Added || riga.RowState == DataRowState.Modified)
                {
                    riga.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    riga.DATARIFERIMENTO = DateTime.Now;
                }
                if (riga.RowState == DataRowState.Added)
                {
                    riga.COLORECOMPONENTE = ddlColore.SelectedItem.ToString();
                }

                if (riga["RICHIESTO"] == DBNull.Value || string.IsNullOrEmpty((string)riga["RICHIESTO"]))
                    esito = false;

                if (riga["TOLLERANZA"] == DBNull.Value || string.IsNullOrEmpty((string)riga["TOLLERANZA"]))
                    esito = false;

                if (riga["UNITAMISURA"] == DBNull.Value || string.IsNullOrEmpty((string)riga["UNITAMISURA"]))
                    esito = false;
            }

            if (!esito)
            {
                lblMessaggio.Text = "Impossibile salvare. Ci sono dei valori mancanti";
                return;
            }




            CDCBLL bll = new CDCBLL();
            bll.SalvaDatiColori(_DS);
            _DS.AcceptChanges();

        }
    }
}
