using CDCMetal.BLL;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDCMetal
{
    public partial class MainForm : Form
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CDCContext Contesto = new CDCContext();

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

        private void CreaContesto()
        {
            Contesto = new CDCContext();

            Contesto.UtenteConnesso = true;
            Contesto.Utente = new Security.Utente();
            Contesto.PathSchedeTecniche = CDCMetal.Properties.Settings.Default.PathSchedeTecnic;
            Contesto.PathRefertiLaboratorio = Properties.Settings.Default.PathRefertiLaboratorio;
            Contesto.StrumentoColore = Properties.Settings.Default.StrumentoColore;
            Contesto.StrumentoSpessore = Properties.Settings.Default.StrumentoSpessore;
            Contesto.PathCollaudo = CDCMetal.Properties.Settings.Default.PathCollaudo;
            Contesto.PathAnalisiPiombo = CDCMetal.Properties.Settings.Default.PathAnalisiPiombo;
#if DEBUG
            Contesto.PathCollaudo = @"C:\Temp\CDC\CARTELLACOLLAUDO";
            Contesto.PathRefertiLaboratorio = @"C:\Temp\CDC\REFERTILABORATORIO";
            Contesto.PathAnalisiPiombo = @"C:\Temp\CDC\ANALISIPIOMBO";
#endif

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
            string fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            CreaContesto();
            stUser.Text = Contesto.Utente.DisplayName;
            //try
            //{
            ////    EseguiLogin();
            //}
            //catch (Exception ex)
            //{
            //    MostraEccezione("ERRORE IN ESEGUI LOGIN", ex);
            //}
            AbilitaMenu();

        }

        private void AbilitaMenu()
        {
            DisabilitaElementiMenu(cdcMenu.Items, true);
            loginToolStripMenuItem.Enabled = true;
            exitToolStripMenuItem.Enabled = true;
            fileToolStripMenuItem.Enabled = true;

            excelToolStripMenuItem.Enabled = Contesto.Utente.AbilitaExcel;
            cartelleToolStripMenuItem.Enabled = Contesto.Utente.AbilitaCartelle;
            conformitàToolStripMenuItem.Enabled = Contesto.Utente.AbilitaConformita;
            dimensioniToolStripMenuItem.Enabled = Contesto.Utente.AbilitaDimensioni;
            laboratorioToolStripMenuItem.Enabled = Contesto.Utente.AbilitaLaboratorio;
            pDFToolStripMenuItem.Enabled = Contesto.Utente.AbilitaPDF;
            eTICHETTEToolStripMenuItem.Enabled = Contesto.Utente.AbilitaEtichette;
            gestioneToolStripMenuItem.Enabled = Contesto.Utente.AbilitaGestione;
        }

        private void DisabilitaElementiMenu(ToolStripItemCollection elementi, bool abilita)
        {
            foreach (ToolStripItem elemento in elementi)
            {
                if (elemento is ToolStripMenuItem)
                {
                    (elemento as ToolStripMenuItem).Enabled = abilita;
                    if ((elemento as ToolStripMenuItem).DropDownItems.Count > 0)
                    {
                        DisabilitaElementiMenu((elemento as ToolStripMenuItem).DropDownItems, abilita);
                    }
                }

            }
        }

        //private void EseguiLogin()
        //{
        //    LoginDialog frm = new LoginDialog();
        //    if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        Contesto.UtenteConnesso = true;
        //        Contesto.Utente = frm.User;
        //        lblUserLoggato.Text = frm.User.FULLNAMEUSER.Trim();
        //    }
        //    else
        //    {

        //        Contesto.UtenteConnesso = false;
        //        Contesto.Utente = null;
        //        lblUserLoggato.Text = string.Empty;
        //    }
        //}

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaContesto();
                //    EseguiLogin();
                AbilitaMenu();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN ESEGUI LOGIN", ex);
            }
        }

        public void MostraEccezione(string messaggioLog, Exception ex)
        {
            LogScriviErrore(messaggioLog, ex);
            ExceptionFrm frm = new ExceptionFrm(ex);
            frm.ShowDialog();

        }

        private void creaCartelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaCartelleFrm form = new CreaCartelleFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CARTELLE", ex);
            }

        }

        private void creaCertificatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaConformitaFrm form = new CreaConformitaFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO CONFORMITA'", ex);
            }
        }

        private void creaCertificatoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // dimensionale
            try
            {
                CreaDimensionaleFrm form = new CreaDimensionaleFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO DIMENSIONALE", ex);
            }

        }

        private void antiallergicoNichelFreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaAntiallergicoFrm form = new CreaAntiallergicoFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO ANTIALLERGICO", ex);
            }

        }

        private void colorimetricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaColorimetricoFrm form = new CreaColorimetricoFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO COLORIMETRICO", ex);
            }
        }

        private void listaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ListaPDFFrm form = new ListaPDFFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN MOSTRA LISTA PDF", ex);
            }
        }

        private void spessoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaSpessoreFrm form = new CreaSpessoreFrm(TipoCertificatoSpessore.CERTIFICATOSPESSOREGENERICO);
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO SPESSORE", ex);
            }
        }

        private void creaEtichetteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaEtichetteFrm form = new CreaEtichetteFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA ETICHETTE", ex);
            }
        }

        private void verniciCoprentiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaVerniciCoprentiFrm form = new CreaVerniciCoprentiFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO VERNICI COPRENTI", ex);
            }
        }

        private void tenutaAcidoNitricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaTenutaAcidoNitricoFrm form = new CreaTenutaAcidoNitricoFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO TENUTA ACIDO NITRICO", ex);
            }
        }

        private void singoloPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AnalisiPiomboFrm form = new AnalisiPiomboFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO ANALISI PIOMBO", ex);
            }
        }

        private void daExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AnalisiPiomboDaExcelFrm form = new AnalisiPiomboDaExcelFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATO ANALISI PIOMBO DA EXCEL", ex);
            }
        }

        private void associaCertificatoPiomboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AssociaCertificatiPiomboFrm form = new AssociaCertificatiPiomboFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA ASSOCIA CERTIFICATO PIOMBO ", ex);
            }
        }

        private void spessoriNichelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaSpessoreFrm form = new CreaSpessoreFrm(TipoCertificatoSpessore.CERTIFICATOSPESSORENICHEL);
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN CREA CERTIFICATI SPESSORI PIOMBO", ex);
            }

        }

        private void toolStripMenuGestioneBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CreaGestioneBrandFrm form = new CreaGestioneBrandFrm();// (TipoCertificatoSpessore.CERTIFICATOSPESSORENICHEL);
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN GESTIONE BRAND", ex);
            }       
        }

          private void toolStripMenuGestioneColori_Click(object sender, EventArgs e)
        {
            try
            {

                CreaGestioneColoriFrm form = new CreaGestioneColoriFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN GESTIONE COLORI", ex);
            }
        }


        private void toolStripMenuGestioneComponenti_Click(object sender, EventArgs e)
        {
            try
            {

                CreaGestioneComponentiFrm form = new CreaGestioneComponentiFrm();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MostraEccezione("ERRORE IN GESTIONE COMPONENTI", ex);
            }
        }
    }
}
