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
    public partial class CreaCartelleFrm : BaseChildForm
    {
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
    }
}
