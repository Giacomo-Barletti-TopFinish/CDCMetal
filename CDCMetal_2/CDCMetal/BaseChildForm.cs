using CDCMetal.Entities;
using CDCMetal.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDCMetal
{
    public class BaseChildForm : Form
    {
        protected CDCDS _DS = new CDCDS();
        protected CDCContext Contesto
        {
            get
            {
                return (this.ParentForm as MainForm).Contesto;
            }
            set
            {
                (this.ParentForm as MainForm).Contesto = value;
            }
        }


        //*** OLD
        protected List<DataCollaudo> CaricaDateCollaudo()
        {
            CDCBLL bll = new CDCBLL();
            return bll.LeggiDateCollaudo();
        }

        protected List<DataCollaudoSingle> CaricaDateCollaudoSingle()
        {
            CDCBLL bll = new CDCBLL();
            return bll.LeggiDateCollaudoSingle();
        }
        protected List<BrandCollaudoSingle> CaricaBrandCollaudoSingle(string datacollaudo)
        {
            CDCBLL bll = new CDCBLL();
            return bll.LeggiBrandCollaudoSingle(datacollaudo);
        }
        protected List<ColoreSingle> CaricaColoriSingle()
        {
            CDCBLL bll = new CDCBLL();
            return bll.LeggiColoreSingle();
        }

        protected string ConvertiBoolInStringa(object o)
        {
            if (o == DBNull.Value)
                return "N";
            if (o is Boolean)
            {
                bool aux = (bool)o;
                return aux ? "S" : "N";
            }
            return "N";
        }

        protected string ConvertiInStringa(object o)
        {
            if (o == DBNull.Value)
                return string.Empty;
            if (o is String)
            {
                string aux = (string)o;
                return aux;
            }
            return string.Empty;
        }

        protected void MostraEccezione(Exception ex, string messaggioLog)
        {
            (this.ParentForm as MainForm).MostraEccezione(messaggioLog, ex);
        }
    }
}
