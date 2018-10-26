using CDCMetal.BLL;
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

     //       CreaDsPerCartelle(dataSelezionata);

            dgvDettaglio.AutoGenerateColumns = true;
            dgvDettaglio.DataSource = _dsServizio;
            dgvDettaglio.DataMember = tableName;
            dgvDettaglio.Columns["CARTELLA"].Width = 800;
        }
    }
}
