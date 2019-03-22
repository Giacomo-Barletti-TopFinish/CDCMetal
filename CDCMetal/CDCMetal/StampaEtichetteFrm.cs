using CDCMetal.BLL;
using CDCMetal.Entities;
using CDCMetal.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDCMetal
{
    public partial class StampaEtichetteFrm : Form
    {
        private BackgroundWorker bgw = new BackgroundWorker();
        private string tableName = "ETICHETTE";
        private string printer;
        public StampaEtichetteFrm(DataSet ds, string zebraPrinter)
        {
            CDCBLL bll = new CDCBLL();
            printer = zebraPrinter;
            InitializeComponent();
            bgw.DoWork += Bgw_DoWork;
            bgw.ProgressChanged += Bgw_ProgressChanged;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
            progressBar1.Maximum = 0;
            foreach (DataRow riga in ds.Tables[tableName].Rows)
            {
                string messaggioVerifica;
                List<Tuple<int, int>> SC_QTA;

                string numeroEtichette = string.Empty;
                if (riga[12] != System.DBNull.Value)
                    numeroEtichette = (string)riga[12];

                if (bll.verificaNumeroEtichette(numeroEtichette, out messaggioVerifica, out SC_QTA))
                {
                    foreach (Tuple<int, int> tupla in SC_QTA)
                    {
                        int sc = tupla.Item1;
                        int qta = tupla.Item2;
                        progressBar1.Maximum += sc * qta;
                    }
                }

            }

            bgw.RunWorkerAsync(ds);

        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            progressBar1.Value = progressBar1.Maximum;
            btnAnnulla.Text = "Termina";

            if (e.Cancelled)
            {
                txtMessage.Text += "\r\nSTAMPA ANNULLATA";
                this.Close();
                this.Dispose();
            }
            else if (e.Error != null)
            {
                txtMessage.Text += "\r\nERRORE IN STAMPA";
            }
            else
                txtMessage.Text += "\r\nSTAMPA TERMINATA";

        }

        private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage > progressBar1.Maximum ? progressBar1.Maximum : e.ProgressPercentage;
            txtMessage.Text = e.UserState as string;
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            StringBuilder sbMessaggio = new StringBuilder();
            DataSet ds = e.Argument as DataSet;

            CDCBLL bll = new CDCBLL();
            int numeroTotaleEtichette = 0;
            foreach (DataRow riga in ds.Tables[tableName].Rows)
            {
                string numeroEtichette = string.Empty;
                if (riga[12] != System.DBNull.Value)
                    numeroEtichette = (string)riga[12];


                string prefisso = (string)riga[3];
                string parte = (string)riga[4];
                string colore = (string)riga[5];
                string linea = (string)riga[9];
                string descrizione = (string)riga[10];

                string messaggioVerifica;
                List<Tuple<int, int>> SC_QTA;

                if (bll.verificaNumeroEtichette(numeroEtichette, out messaggioVerifica, out SC_QTA))
                {
                    foreach (Tuple<int, int> tupla in SC_QTA)
                    {
                        string etichetta = string.Format("{0} {1} {2} {3}   da {4} pezzi IN CORSO", prefisso, parte, colore, linea, tupla.Item2);

                        for (int i = 0; i < tupla.Item1; i++)
                        {

                            numeroTotaleEtichette++;
                            ZebraHelper.EseguiStampaEtichetta(printer, riga, tupla.Item2.ToString());
                            Thread.Sleep(150);
                            string messaggio = string.Format(@"{0}{1}", sbMessaggio.ToString(), etichetta);
                            bgw.ReportProgress(numeroTotaleEtichette, messaggio);

                            if (bgw.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                    }
                    sbMessaggio.AppendLine(string.Format("{0} {1} {2} {3}   numero etichette: {4} STAMPATE", prefisso, parte, colore, linea, numeroEtichette));
                }
                ZebraHelper.EseguiStampaEtichettaSeparatore(printer);
            }
            bgw.ReportProgress(numeroTotaleEtichette, sbMessaggio.ToString());
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            if (btnAnnulla.Text == "Termina")
            {
                this.Close();
                this.Dispose();
                return;
            }

            if (bgw.IsBusy)
            {
                bgw.CancelAsync();
            }
        }
    }
}
