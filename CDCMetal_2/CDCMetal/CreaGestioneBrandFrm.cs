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
    public partial class CreaGestioneBrandFrm : BaseChildForm
    {
       // private DataSet _dsServizio = new DataSet();
        private string tableNameB = "BRANDS";
        private string tableNameE = "ENTI";

        public CreaGestioneBrandFrm()
        {
            InitializeComponent();


            CDCBLL bll = new CDCBLL();
            bll.CaricaBrands(_DS);
            bll.CaricaEnti(_DS);//JACOPO

            DataTable listBrands = _DS.CDC_BRANDS.Clone();
            listBrands = _DS.CDC_BRANDS.Copy();


            dgvBrands.ReadOnly = false;
            dgvBrands.AutoGenerateColumns = true;
            dgvBrands.DataSource = _DS;
            dgvBrands.DataMember = _DS.CDC_BRANDS.TableName;

            dgvBrands.Columns["IDBRAND"].Visible = true;
            dgvBrands.Columns["UTENTE"].Visible = false;
            dgvBrands.Columns["DATARIFERIMENTO"].Visible = false;

            //dgvExcelCaricato.Columns["IDDETTAGLIO"].Visible = false;
            //dgvExcelCaricato.Columns["IDEXCEL"].Visible = false;
            //dgvExcelCaricato.Columns["IDBRAND"].Visible = false;

            dgvEnti.ReadOnly = false;
            dgvEnti.AutoGenerateColumns = true;
            dgvEnti.DataSource = _DS;
            dgvEnti.DataMember = _DS.CDC_ENTI.TableName;
            dgvEnti.Columns["IDENTE"].Visible = false;
            dgvEnti.Columns["UTENTE"].Visible = false;
            dgvEnti.Columns["DATARIFERIMENTO"].Visible = false;
            dgvEnti.Columns["IDBRAND"].Visible = false;

            var comboCol = new DataGridViewComboBoxColumn();
            comboCol.HeaderText = "BRAND";
            comboCol.Name = "BRAND";
            comboCol.DataPropertyName = "IDBRAND";
            comboCol.AutoComplete = true;
            comboCol.Width = 100;
            comboCol.DropDownWidth = 100;
            comboCol.DataSource = listBrands; // _DS.CDC_BRANDS.Where(x => x.IDBRAND >0);
            comboCol.DisplayMember = "CODICE";
            comboCol.ValueMember = "IDBRAND";
            comboCol.DisplayIndex = 5;
            dgvEnti.Columns.Add(comboCol);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (CDCDS.CDC_BRANDSRow riga in _DS.CDC_BRANDS.Rows)
            {
               if (riga.RowState == DataRowState.Added || riga.RowState == DataRowState.Modified)
                {
                    riga.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    riga.DATARIFERIMENTO = DateTime.Now;
                }
            }

            CDCBLL bll = new CDCBLL();
            bll.SalvaDatiBrands(_DS);
            _DS.AcceptChanges();

            _DS.CDC_BRANDS.Clear();
            bll.CaricaBrands(_DS);

            DataTable listBrands = _DS.CDC_BRANDS.Clone();
            listBrands = _DS.CDC_BRANDS.Copy();

            DataGridViewComboBoxColumn comboCol = (DataGridViewComboBoxColumn)dgvEnti.Columns["BRAND"];
            comboCol.DataSource = listBrands;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (CDCDS.CDC_ENTIRow riga in _DS.CDC_ENTI.Rows)
            {
                if (riga.RowState == DataRowState.Added || riga.RowState == DataRowState.Modified)
                {
                    riga.UTENTE = Contesto.Utente.FULLNAMEUSER;
                    riga.DATARIFERIMENTO = DateTime.Now;
                }
            }


            CDCBLL bll = new CDCBLL();
            bll.SalvaDatiEnti(_DS);
            _DS.AcceptChanges();

            _DS.CDC_ENTI.Clear();
            bll.CaricaEnti(_DS);


        }
    }
}
