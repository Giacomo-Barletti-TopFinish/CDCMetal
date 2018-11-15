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
