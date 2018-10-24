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
         
    }
}
