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
    public partial class MainForm : Form
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private CDCDS.USR_USERRow _user;
        private bool _utenteRiconosciuto = false;

        public static void LogScriviErrore(string Messaggio, Exception ex)
        {
            _log.Error(Messaggio, ex);
        }

        public static void LogScrivi(string Messaggio)
        {
            _log.Info(Messaggio);
        }

        public MainForm()
        {
            InitializeComponent();
            LogScrivi("Applicazione CDCMetal avviata");
            lb
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void caricaNuovoDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelCaricaNuovoDocumentoFrm form = new ExcelCaricaNuovoDocumentoFrm();
            form.MdiParent = this;
            form.Show();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogScrivi("Applicazione CDCMetal fermata");
            if (this.MdiChildren.Count() > 0)
            {
                foreach (Form f in MdiChildren)
                    f.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                EseguiLogin();
            }
            catch (Exception ex)
            {
                LogScriviErrore("ERRORE IN ESEGUI LOGIN", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
        }

        private void EseguiLogin()
        {
            LoginDialog frm = new LoginDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _utenteRiconosciuto = true;
                _user = frm.User;
                lblUserLoggato.Text = _user.FULLNAMEUSER.Trim();
            }
            else
            {
                _utenteRiconosciuto = false; ;
                _user = null;
                lblUserLoggato.Text = string.Empty;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EseguiLogin();
            }
            catch(Exception ex)
            {
                LogScriviErrore("ERRORE IN ESEGUI LOGIN", ex);
                ExceptionFrm frm = new ExceptionFrm(ex);
                frm.ShowDialog();
            }
        }
    }
}
