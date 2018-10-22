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
    public partial class LoginDialog : Form
    {
        private CDCDS.USR_USERRow _user;

        public CDCDS.USR_USERRow User
        {
            get { return _user; }
        }
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void LoginDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _user = null;
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string messaggio = string.Empty;
            bool esito = true;
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                esito = false;
                messaggio = "- Specificare l'utente";
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                esito = false;
                messaggio += Environment.NewLine + "- Inserire la password";
            }

            if (!esito)
            {
                lblErrore.Text = messaggio;
                return;
            }

            AccountBLL bll = new AccountBLL();
            if (bll.VerificaPassword(txtUser.Text.Trim().ToUpper(), txtPassword.Text.Trim().ToUpper(), out _user))
            {
                DialogResult = DialogResult.OK;
                return;
            }

            lblErrore.Text = "Password errata";
        }
    }
}
