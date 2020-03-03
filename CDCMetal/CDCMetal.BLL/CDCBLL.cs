using CDCMetal.Data;
using CDCMetal.Entities;
using CDCMetal.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.BLL
{
    public class CDCBLL
    {

        public void CaricaBrands(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_BRANDS(ds);
            }
        }

        public void FillCDC_CERTIFICATIPIOMBO(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_CERTIFICATIPIOMBO(ds);
            }
        }

        public List<DataCollaudo> LeggiDateCollaudo()
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                return bCDCMetal.LeggiDateRiferimento();
            }
        }

        public void LeggiTabelleSpessori(CDCDS ds)
        {
            ds.CDC_SPESSORE.Clear();
            ds.CDC_APPLICAZIONE.Clear();
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_SPESSORE(ds);
                bCDCMetal.FillCDC_APPLICAZIONE(ds);
            }
        }

        public void LeggiCollaudoDaData(CDCDS ds, DataCollaudo dataSelezionata)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC(ds, dataSelezionata);
            }
        }

        public void LeggiMateriaPrimaArticoli(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_MATERIAPRIMA(ds);
            }
        }
        public void LeggiCollaudoDaDataConDescrizione(CDCDS ds, DataCollaudo dataSelezionata)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_CON_DESCRIZIONE(ds, dataSelezionata.Data, dataSelezionata.Brand);
            }
        }

        public void FillCDC_CONFORMITA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_CONFORMITA(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_ETICHETTE_DETTAGLIO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_ETICHETTE_DETTAGLIO(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_ANTIALLERGICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_ANTIALLERGICO(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_VERNICICOPRENTI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_VERNICICOPRENTI(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_TENUTACIDONITRICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_TENUTACIDONITRICO(ds, IDDETTAGLIO);
            }
        }
        public void FillCDC_COLORE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_COLORE(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_DIMEMSIONI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_DIMEMSIONI(ds, IDDETTAGLIO);
                bCDCMetal.FillCDC_DIMEMSIONI_MISURE(ds, IDDETTAGLIO);
            }
        }

        public void SalvaDescrizioneEtichette(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_ETICHETTE_DETTAGLIO(ds);
            }
        }

        public string CalcolaEsitoAnalisiPiombo(decimal nPd, out System.Drawing.Color colore)
        {
            string esito = string.Empty;
            colore = SystemColors.Control;

            if (nPd > 0 && nPd < 70)
            {
                esito = "PASS";
                colore = Color.LightGreen;
            }

            if (nPd >= 70 && nPd < 80)
            {
                esito = "PASS";
                colore = Color.Yellow;
            }

            if (nPd >= 80 && nPd < 90)
            {
                esito = "FAIL";
                colore = Color.Red;
            }
            if (nPd >= 90)
            {
                esito = "FAIL";
                colore = Color.Red;
            }
            return esito;
        }
        public bool verificaNumeroEtichette(string numeroEtichette, out string messaggio, out List<Tuple<int, int>> SC_QTA)
        {
            StringBuilder sb = new StringBuilder();
            SC_QTA = new List<Tuple<int, int>>();

            bool esito = true;
            string[] nscQta = numeroEtichette.ToUpper().Split(';');
            foreach (string ns in nscQta)
            {
                string[] n = ns.Split('X');
                if (n.Length == 2)
                {
                    int sc;
                    int qta;
                    if (!int.TryParse(n[0], out sc))
                        esito = false;
                    if (!int.TryParse(n[1], out qta))
                        esito = false;

                    if (esito)
                    {
                        sb.AppendLine(string.Format("                        {0} scatole da {1} pezzi", sc, qta));
                        SC_QTA.Add(new Tuple<int, int>(sc, qta));
                    }
                }
            }

            messaggio = sb.ToString();
            return esito;
        }
        public void FillCDC_GALVANICA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                ds.CDC_GALVANICA.Clear();
                ds.CDC_MISURE.Clear();
                bCDCMetal.FillCDC_GALVANICA(ds, IDDETTAGLIO);
                bCDCMetal.FillCDC_MISURE(ds, IDDETTAGLIO);
            }
        }

        public void CDC_PDF(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                ds.CDC_PDF.Clear();
                bCDCMetal.CDC_PDF(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_ASSOCIAZIONEPIOMBO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                ds.CDC_ASSOCIAZIONEPIOMBO.Clear();
                bCDCMetal.FillCDC_ASSOCIAZIONEPIOMBO(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_CERTIFICATIPIOMBO_NonAssegnati(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_CERTIFICATIPIOMBO_NonAssegnati(ds);
            }
        }

        public static string ConvertiAccessorista(string Accessorista)
        {
            if (Accessorista.Trim() == "Metalplus s.r.l.")
                return "Metalplus";
            else
                return "TopFinish";
        }

        private static string creaStringaCartellaGiornoCollaudo(DateTime dataSelezionata)
        {
            return string.Format("collaudo {0}-{1}-{2}", dataSelezionata.Day, dataSelezionata.Month, dataSelezionata.Year % 100);
        }
        private static string creaStringaCartellaMeseCollaudo(DateTime dataSelezionata)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
            string mese = dataSelezionata.ToString("MMMM", culture);
            return string.Format("COLLAUDI {0} {1}", mese.ToUpper(), dataSelezionata.Year);
        }

        public static string CreaPathCartella(DateTime dataSelezionata, string pathCollaudo, string Accessorista, string Prefisso, string Parte, string Colore, string Commessa)
        {
            string accessorista = ConvertiAccessorista(Accessorista);
            string meseCollaudo = creaStringaCartellaMeseCollaudo(dataSelezionata);
            string giornoCollaudo = creaStringaCartellaGiornoCollaudo(dataSelezionata);

            string cartellaArticolo = string.Format("{0}-{1}-{2} {3}", Prefisso, Parte, Colore, Commessa.Replace('_', ' '));

            return string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCollaudo, accessorista, meseCollaudo, giornoCollaudo, cartellaArticolo);
        }

        public static string CreaPathCartellaAnalisiPiombo(DateTime dataSelezionata, string pathCollaudo)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");

            string mese = dataSelezionata.ToString("MMMM", culture);
            string giornoCollaudo = string.Format("{0}.{1}", dataSelezionata.Day.ToString().PadLeft(2, '0'), dataSelezionata.Month.ToString().PadLeft(2, '0'));


            return string.Format(@"{0}\{1}\{2}\{3}", pathCollaudo, dataSelezionata.Year.ToString(), mese, giornoCollaudo);
        }
        public void SalvaDatiConformita(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateConformita(ds);
            }
        }

        public void SalvaMateriaPrima(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateMateriaprima(ds);
            }
        }

        public void SalvaDatiAntiallergia(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_ANTIALLERGICO(ds);
            }
        }

        public void SalvaDatiVerniciaturaCoprente(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_VERNICICOPRENTI(ds);
            }
        }

        public void SalvaDatiTenutaAcidoNitrico(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_TENUTACIDONITRICO(ds);
            }
        }
        public void SalvaDatiAssociazionePiombo(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_ASSOCIAZIONEPIOMBO(ds);
            }
        }

        public void SalvaDatiColore(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_COLORE(ds);
            }
        }

        public void SalvaDatiDimensioni(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateDimensioni(ds);
            }
        }
        public string CreaPDFConformita(List<decimal> idPerPDF, CDCDS ds, string pathCollaudo, byte[] image)
        {
            StringBuilder fileCreati = new StringBuilder();

            foreach (decimal IDDETTAGLIO in idPerPDF)
            {
                CDCDS.CDC_CONFORMITARow conformita = ds.CDC_CONFORMITA.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (conformita == null)
                    throw new Exception("CDC_CONFORMITA riga non trovat per IDDETTAGLIO" + IDDETTAGLIO.ToString());
                CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == conformita.IDDETTAGLIO).FirstOrDefault();
                if (dettaglio == null)
                {
                    throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC CONFORMITA'");
                }
                DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string cartella = CreaPathCartella(dt, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);
                string fileName = string.Format("CDC {0}.pdf", dettaglio.PARTE.Trim());
                string path = string.Format(@"{0}\{1}", cartella, fileName);

                if (!Directory.Exists(cartella))
                    Directory.CreateDirectory(cartella);

                if (File.Exists(path))
                    File.Delete(path);

                bool fisico = conformita.FISICOCHIMICO == "S" ? true : false;
                bool funzionale = conformita.FUNZIONALE == "S" ? true : false;
                bool dimensionale = conformita.DIMENSIONALE == "S" ? true : false;
                bool estetico = conformita.ESTETICO == "S" ? true : false;
                bool acconto = conformita.ACCONTO == "S" ? true : false;
                bool saldo = conformita.SALDO == "S" ? true : false;
                string altro = conformita.IsALTRONull() ? string.Empty : conformita.ALTRO;
                string certificato = conformita.IsCERTIFICATINull() ? string.Empty : conformita.CERTIFICATI;

                CreaCDC(path, dettaglio.ACCESSORISTA, dettaglio.IDPRENOTAZIONE.ToString(), DateTime.Today.ToShortDateString(),
                    dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.QUANTITA.ToString(), conformita.DESCRIZIONE, dettaglio.COMMESSAORDINE, fisico, funzionale,
                    dimensionale, estetico, acconto, saldo, altro, certificato, image);
                fileCreati.AppendLine(path);

                CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == conformita.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOCONFORMITA).FirstOrDefault();
                if (pdf == null)
                {
                    pdf = ds.CDC_PDF.NewCDC_PDFRow();
                    pdf.TIPO = CDCTipoPDF.CERTIFICATOCONFORMITA;
                    pdf.NOMEFILE = path;
                    pdf.IDDETTAGLIO = conformita.IDDETTAGLIO;
                    ds.CDC_PDF.AddCDC_PDFRow(pdf);

                    using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                        bCDCMetalBusiness.UpdateCDC_PDF(ds);
                    ds.CDC_PDF.AcceptChanges();
                }
            }

            return fileCreati.ToString();
        }

        public string CreaPDFAntiallergico(List<decimal> idPerPDF, CDCDS ds, string pathCollaudo, byte[] image, bool CopiaReferto, string pathCartellaReferto)
        {
            StringBuilder fileCreati = new StringBuilder();

            foreach (decimal IDDETTAGLIO in idPerPDF)
            {
                CDCDS.CDC_ANTIALLERGICORow antiallergico = ds.CDC_ANTIALLERGICO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (antiallergico == null)
                    throw new Exception("CDC_ANTIALLERGICO riga non trovat per IDDETTAGLIO" + IDDETTAGLIO.ToString());
                CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == antiallergico.IDDETTAGLIO).FirstOrDefault();
                if (dettaglio == null)
                {
                    throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC ANTIALLERGICO'");
                }
                DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime dt = DateTime.Today;
                string cartella = CreaPathCartella(dt, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);
                bool nichelFree = antiallergico.NICHELFREE == "S" ? true : false;

                string articolo = string.Format("{0} {1} {2}", dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE.Replace('_', ' '));
                string certificazione = nichelFree ? "Nichel Free" : "Antiallergico";

                string fileName = string.Format("Certificazione {0} {1}.pdf", certificazione, articolo);
                string path = string.Format(@"{0}\{1}", cartella, fileName);

                if (!Directory.Exists(cartella))
                    Directory.CreateDirectory(cartella);

                if (File.Exists(path))
                    File.Delete(path);

                CreaCDCAntiallergico(path, nichelFree, antiallergico.DATAPRODUZIONE.ToShortDateString(), antiallergico.DATAPRODUZIONE.ToShortDateString(), dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE, dettaglio.QUANTITA.ToString(), image);

                if (CopiaReferto)
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
                    string acce = "Top";
                    if (dettaglio.ACCESSORISTA.Trim() == "Metalplus s.r.l.")
                        acce = "M+";
                    string meseCollaudo = dt.ToString("MMMM", culture);
                    string giorno = string.Format("{0}.{1}", dt.Day.ToString("00"), dt.Month.ToString("00"));
                    string pathCartella = string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCartellaReferto, dt.Year.ToString(), meseCollaudo, giorno, acce);
                    string pathReferto = string.Format(@"{0}\{1}", pathCartella, fileName);

                    if (!Directory.Exists(pathCartella))
                        Directory.CreateDirectory(pathCartella);

                    if (File.Exists(pathReferto))
                        File.Delete(pathReferto);
                    File.Copy(path, pathReferto, true);
                }

                fileCreati.AppendLine(path);

                CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == antiallergico.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOANTIALLERGICO).FirstOrDefault();
                if (pdf == null)
                {
                    pdf = ds.CDC_PDF.NewCDC_PDFRow();
                    pdf.TIPO = CDCTipoPDF.CERTIFICATOANTIALLERGICO;
                    pdf.NOMEFILE = path;
                    pdf.IDDETTAGLIO = antiallergico.IDDETTAGLIO;
                    ds.CDC_PDF.AddCDC_PDFRow(pdf);

                    using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                        bCDCMetalBusiness.UpdateCDC_PDF(ds);
                    ds.CDC_PDF.AcceptChanges();
                }
            }

            return fileCreati.ToString();
        }

        public string CreaPDFVerniceCoprente(List<decimal> idPerPDF, CDCDS ds, string pathCollaudo, byte[] image, bool CopiaReferto, string pathCartellaReferto)
        {
            StringBuilder fileCreati = new StringBuilder();

            foreach (decimal IDDETTAGLIO in idPerPDF)
            {
                CDCDS.CDC_VERNICICOPRENTIRow vCoprente = ds.CDC_VERNICICOPRENTI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (vCoprente == null)
                    throw new Exception("CDC_VERNICICOPRENTI riga non trovat per IDDETTAGLIO" + IDDETTAGLIO.ToString());
                CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == vCoprente.IDDETTAGLIO).FirstOrDefault();
                if (dettaglio == null)
                {
                    throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC VERNICI COPRENTI'");
                }
                DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime dt = DateTime.Today;
                string cartella = CreaPathCartella(dt, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);
                bool turbula = vCoprente.TURBULA == "S" ? true : false;
                bool quadrettatura = vCoprente.QUADRETTATURA == "S" ? true : false;

                string articolo = dettaglio.PARTE;
                string finitura = dettaglio.COLORE;

                string fileName = string.Format("TEST VERNICI COPRENTI {0} {1}.pdf", articolo, finitura);
                string path = string.Format(@"{0}\{1}", cartella, fileName);

                if (!Directory.Exists(cartella))
                    Directory.CreateDirectory(cartella);

                if (File.Exists(path))
                    File.Delete(path);

                CreaReportVerniciCoprenti(path, turbula, quadrettatura, vCoprente.DATATEST.ToShortDateString(), dettaglio.PARTE, dettaglio.COLORE, vCoprente.FORNITORE, vCoprente.NUMEROCAMPIONI.ToString(), image);

                if (CopiaReferto)
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
                    string acce = "Top";
                    if (dettaglio.ACCESSORISTA.Trim() == "Metalplus s.r.l.")
                        acce = "M+";
                    string meseCollaudo = dt.ToString("MMMM", culture);
                    string giorno = string.Format("{0}.{1}", dt.Day.ToString("00"), dt.Month.ToString("00"));
                    string pathCartella = string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCartellaReferto, dt.Year.ToString(), meseCollaudo, giorno, acce);
                    string pathReferto = string.Format(@"{0}\{1}", pathCartella, fileName);

                    if (!Directory.Exists(pathCartella))
                        Directory.CreateDirectory(pathCartella);

                    if (File.Exists(pathReferto))
                        File.Delete(pathReferto);
                    File.Copy(path, pathReferto, true);
                }

                fileCreati.AppendLine(path);

                CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == vCoprente.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOVERNICECOPRENTE).FirstOrDefault();
                if (pdf == null)
                {
                    pdf = ds.CDC_PDF.NewCDC_PDFRow();
                    pdf.TIPO = CDCTipoPDF.CERTIFICATOVERNICECOPRENTE;
                    pdf.NOMEFILE = path;
                    pdf.IDDETTAGLIO = vCoprente.IDDETTAGLIO;
                    ds.CDC_PDF.AddCDC_PDFRow(pdf);

                    using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                        bCDCMetalBusiness.UpdateCDC_PDF(ds);
                    ds.CDC_PDF.AcceptChanges();
                }
            }

            return fileCreati.ToString();
        }
        private const string barraTonda = "Barra tonda";
        private const string piatto = "Piatto";

        public string CreaNomefileCertificatiAnalisiPiombo(string elemento, string lunghezza, string larghezza, string spessore, string codiceCampione, DateTime dataAnalisi, string pathLaboratorio, out string cartella, out string nomeCampione)
        {
            cartella = CreaPathCartellaAnalisiPiombo(dataAnalisi, pathLaboratorio);

            nomeCampione = string.Empty;
            string fileName = "report.pdf";
            //if (elemento == barraTonda)
            //{
            //    fileName = string.Format("BARRA DIAMETRO {0} {1}_{2}_{3}.pdf", larghezza, codiceCampione, dataAnalisi.Day.ToString().PadLeft(2, '0'), dataAnalisi.Month.ToString().PadLeft(2, '0'));
            //    nomeCampione = string.Format("{0} {1}x{2} {3}", elemento, lunghezza, larghezza, codiceCampione);
            //}
            //else if (elemento == piatto)
            //{
            //    fileName = string.Format("PIATTO {0}x{1}x{2} {3}_{4}_{5}.pdf", lunghezza, larghezza, spessore, codiceCampione, dataAnalisi.Day.ToString().PadLeft(2, '0'), dataAnalisi.Month.ToString().PadLeft(2, '0'));
            //    nomeCampione = string.Format("{0} {1}x{2}x{3} {4}", elemento, lunghezza, larghezza, spessore, codiceCampione);
            //}
            //else
            {
                fileName = string.Format("{0}_{1}_{2}.pdf", codiceCampione, dataAnalisi.Day.ToString().PadLeft(2, '0'), dataAnalisi.Month.ToString().PadLeft(2, '0'));
                nomeCampione = string.Format("{0}", codiceCampione);
            }
            fileName = fileName.Replace("\\", string.Empty).Replace("/", string.Empty);
            return string.Format(@"{0}\{1}", cartella, fileName);

        }
        public string CreaPDFCertificatoPiombo(string elemento, string lunghezza, string larghezza, string spessore, string codiceCampione, string lotto, string esito, System.Drawing.Color colore, string metodo, DateTime dataAnalisi, decimal PdPPM, decimal CdPPM, string pathLaboratorio, byte[] image)
        {
            string cartella;
            string nomeCampione;
            string path = CreaNomefileCertificatiAnalisiPiombo(elemento, lunghezza, larghezza, spessore, codiceCampione, dataAnalisi, pathLaboratorio, out cartella, out nomeCampione);

            path = PathHelper.VerificaPathPerScritturaOracle(path);

            if (!Directory.Exists(cartella))
                Directory.CreateDirectory(cartella);

            if (File.Exists(path))
                File.Delete(path);

            CreaReportCertificatoPiombo(path, elemento, nomeCampione, lotto, esito, colore, metodo, dataAnalisi, PdPPM, CdPPM, pathLaboratorio, image);


            return path;
        }

        public string CreaPDFTenutaAcidoNitrico(List<decimal> idPerPDF, CDCDS ds, string pathCollaudo, byte[] image, bool CopiaReferto, string pathCartellaReferto)
        {
            StringBuilder fileCreati = new StringBuilder();

            foreach (decimal IDDETTAGLIO in idPerPDF)
            {
                CDCDS.CDC_TENUTACIDONITRICORow tenuta = ds.CDC_TENUTACIDONITRICO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (tenuta == null)
                    throw new Exception("CDC_TENUTACIDONITRICO riga non trovat per IDDETTAGLIO" + IDDETTAGLIO.ToString());
                CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == tenuta.IDDETTAGLIO).FirstOrDefault();
                if (dettaglio == null)
                {
                    throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC_TENUTACIDONITRICO'");
                }
                DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime dt = DateTime.Today;
                string cartella = CreaPathCartella(dt, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);

                string articolo = dettaglio.PARTE;
                string finitura = dettaglio.COLORE;

                string fileName = string.Format("CONTROLLO TENUTA ACIDO NITRICO {0} {1}.pdf", articolo, finitura);
                string path = string.Format(@"{0}\{1}", cartella, fileName);

                if (!Directory.Exists(cartella))
                    Directory.CreateDirectory(cartella);

                if (File.Exists(path))
                    File.Delete(path);

                bool esito = tenuta.ESITO == "S" ? true : false;

                string bolla = tenuta.IsBOLLANull() ? string.Empty : tenuta.BOLLA;
                string dataDDT = tenuta.IsDATADDTNull() ? string.Empty : tenuta.DATADDT.ToShortDateString();

                CreaReportTenutaAcidoNitrico(path, esito, tenuta.DATATEST.ToShortDateString(), dettaglio.PARTE, dettaglio.COLORE, bolla, dataDDT, tenuta.NUMEROCAMPIONI.ToString(), image);

                if (CopiaReferto)
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
                    string acce = "Top";
                    if (dettaglio.ACCESSORISTA.Trim() == "Metalplus s.r.l.")
                        acce = "M+";
                    string meseCollaudo = dt.ToString("MMMM", culture);
                    string giorno = string.Format("{0}.{1}", dt.Day.ToString("00"), dt.Month.ToString("00"));
                    string pathCartella = string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCartellaReferto, dt.Year.ToString(), meseCollaudo, giorno, acce);
                    string pathReferto = string.Format(@"{0}\{1}", pathCartella, fileName);

                    if (!Directory.Exists(pathCartella))
                        Directory.CreateDirectory(pathCartella);

                    if (File.Exists(pathReferto))
                        File.Delete(pathReferto);
                    File.Copy(path, pathReferto, true);
                }

                fileCreati.AppendLine(path);

                CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == tenuta.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOTENUTAACIDONITRICO).FirstOrDefault();
                if (pdf == null)
                {
                    pdf = ds.CDC_PDF.NewCDC_PDFRow();
                    pdf.TIPO = CDCTipoPDF.CERTIFICATOTENUTAACIDONITRICO;
                    pdf.NOMEFILE = path;
                    pdf.IDDETTAGLIO = tenuta.IDDETTAGLIO;
                    ds.CDC_PDF.AddCDC_PDFRow(pdf);

                    using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                        bCDCMetalBusiness.UpdateCDC_PDF(ds);
                    ds.CDC_PDF.AcceptChanges();
                }
            }

            return fileCreati.ToString();
        }

        public string CreaPDFColore(List<decimal> idPerPDF, CDCDS ds, string pathCollaudo, byte[] image, bool CopiaReferto, string pathCartellaReferto)
        {
            StringBuilder fileCreati = new StringBuilder();

            foreach (decimal IDDETTAGLIO in idPerPDF)
            {
                CDCDS.CDC_COLORERow colore = ds.CDC_COLORE.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (colore == null)
                    throw new Exception("CDC_COLORE riga non trovat per IDDETTAGLIO" + IDDETTAGLIO.ToString());
                CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == colore.IDDETTAGLIO).FirstOrDefault();
                if (dettaglio == null)
                {
                    throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC COLORE'");
                }
                //  DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt = DateTime.Today;
                DateTime dtCollaudo = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string cartella = CreaPathCartella(dtCollaudo, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);

                string articolo = string.Format("{0} {1} {2}", dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE.Replace('_', ' '));

                string fileName = string.Format("M-Q01.02-REV00_Colore {0}.pdf", articolo);//M-Q01.02-REV00_Colore 4155F 8053 EACO 2018 189 K
                string path = string.Format(@"{0}\{1}", cartella, fileName);

                List<MisuraColore> misure = new List<MisuraColore>();
                foreach (CDCDS.CDC_COLORERow coloreRow in ds.CDC_COLORE.Where(x => x.IDDETTAGLIO == IDDETTAGLIO))
                {
                    MisuraColore m = new MisuraColore()
                    {
                        Conforme = coloreRow.CONFORME == "S" ? "OK" : "KO",
                        ControlloColore = coloreRow.COLORE + "'",
                        Richiesto = coloreRow.RICHIESTO,
                        Rilevato = coloreRow.IsRILEVATONull() ? string.Empty : coloreRow.RILEVATO,
                        Tolleranza = coloreRow.IsTOLLERANZANull() ? string.Empty : coloreRow.TOLLERANZA,
                    };
                    misure.Add(m);
                }
                string data = colore.DATAINSERIMENTO.ToShortDateString();
                string dataCalibrazione = colore.DATACALIBRAZIONE.ToShortDateString();
                string strumento = colore.STRUMENTO;
                string nota = colore.IsNOTANull() ? string.Empty : colore.NOTA;

                string operatore = "MM";
                if (!Directory.Exists(cartella))
                    Directory.CreateDirectory(cartella);

                if (File.Exists(path))
                    File.Delete(path);

                CreaReportColorimetrico(path, data, dataCalibrazione, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE, dettaglio.QUANTITA.ToString(), operatore, strumento, nota, misure, image);

                if (CopiaReferto)
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
                    string acce = "Top";
                    if (dettaglio.ACCESSORISTA.Trim() == "Metalplus s.r.l.")
                        acce = "M+";
                    string meseCollaudo = dtCollaudo.ToString("MMMM", culture);
                    string giorno = string.Format("{0}.{1}", dtCollaudo.Day.ToString("00"), dtCollaudo.Month.ToString("00"));
                    string pathCartella = string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCartellaReferto, dtCollaudo.Year.ToString(), meseCollaudo, giorno, acce);
                    string pathReferto = string.Format(@"{0}\{1}", pathCartella, fileName);

                    if (!Directory.Exists(pathCartella))
                        Directory.CreateDirectory(pathCartella);

                    if (File.Exists(pathReferto))
                        File.Delete(pathReferto);
                    File.Copy(path, pathReferto, true);
                }

                fileCreati.AppendLine(path);

                CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == colore.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOCOLORIMETRICO).FirstOrDefault();
                if (pdf == null)
                {
                    pdf = ds.CDC_PDF.NewCDC_PDFRow();
                    pdf.TIPO = CDCTipoPDF.CERTIFICATOCOLORIMETRICO;
                    pdf.NOMEFILE = path;
                    pdf.IDDETTAGLIO = colore.IDDETTAGLIO;
                    ds.CDC_PDF.AddCDC_PDFRow(pdf);

                    using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                        bCDCMetalBusiness.UpdateCDC_PDF(ds);
                    ds.CDC_PDF.AcceptChanges();
                }
            }

            return fileCreati.ToString();
        }

        public string CreaPDFSpessore(decimal IDDETTAGLIO, CDCDS ds, string pathCollaudo, byte[] iLoghi, byte[] iBowman, bool CopiaReferto, string pathCartellaReferto,
            List<string> medie, List<string> Std, List<String> Pct, List<string> range, List<string> minimo, List<string> massimo, string Brand, string numeroCampioni)
        {
            string fileCreato = string.Empty;

            CDCDS.CDC_GALVANICARow galvanica = ds.CDC_GALVANICA.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
            if (galvanica == null)
                throw new Exception("CDC_GALVANICA riga non trovata per IDDETTAGLIO" + IDDETTAGLIO.ToString());
            CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == galvanica.IDDETTAGLIO).FirstOrDefault();
            if (dettaglio == null)
            {
                throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC_GALVANICA'");
            }

            List<CDCDS.CDC_MISURERow> misureRow = ds.CDC_MISURE.Where(x => x.IDGALVANICA == galvanica.IDGALVANICA).OrderBy(x => x.NMISURA).ThenBy(x => x.NCOLONNA).ToList();
            int numeroMisure = misureRow.Max(x => (int)x.NMISURA) + 1;

            List<string> etichette = misureRow.Where(x => x.NMISURA == 0).OrderBy(x => x.NCOLONNA).Select(x => string.Format("{0} µm", x.TIPOMISURA)).ToList();

            DateTime dt = DateTime.Today;
            DateTime dtCollaudo = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string cartella = CreaPathCartella(dtCollaudo, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);

            string commessa = dettaglio.COMMESSAORDINE.Replace('_', ' ');
            string commessaPerStampa = string.Format("{0}-{1}-{2}-{3}-PZ{4}", dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE, dettaglio.QUANTITA);
            string fileName = string.Format("{0} {1} {2}.pdf", dettaglio.PARTE, dettaglio.COLORE, commessa);//A3174 0933 EACP 2018 1916 E.pdf
            string path = string.Format(@"{0}\{1}", cartella, fileName);

            List<List<string>> misure = new List<List<string>>();
            for (int nMisura = 0; nMisura < numeroMisure; nMisura++)
            {
                List<string> misura = misureRow.Where(x => x.NMISURA == nMisura).OrderBy(x => x.NCOLONNA).Select(x => string.Format("{0}", x.VALORE)).ToList();
                misure.Add(misura);
            }
            List<string> elementoRiga = new List<string>();

            string operatore = "MM";
            if (!Directory.Exists(cartella))
                Directory.CreateDirectory(cartella);

            if (File.Exists(path))
                File.Delete(path);
            switch (Brand)
            {
                case CDCBrands.Gucci:
                    CreaReportSpessoriGucci(path, dt, commessaPerStampa, operatore, galvanica.SPESSORE, galvanica.APPLICAZIONE, galvanica.STRUMENTO, numeroMisure, etichette, medie, Std, Pct, range, minimo, massimo, iLoghi, iBowman, misure);
                    break;

                case CDCBrands.Balenciaga:
                case CDCBrands.McQueen:
                case CDCBrands.YSL:
                    CreaReportSpessoriYSL(path, dt, numeroCampioni, dettaglio.PARTE, dettaglio.COLORE, numeroMisure, etichette, medie, iLoghi, misure);
                    break;
            }

            if (CopiaReferto)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("it-IT");
                string acce = "Top";
                if (dettaglio.ACCESSORISTA.Trim() == "Metalplus s.r.l.")
                    acce = "M+";
                string meseCollaudo = dtCollaudo.ToString("MMMM", culture);
                string giorno = string.Format("{0}.{1}", dtCollaudo.Day.ToString("00"), dtCollaudo.Month.ToString("00"));
                string pathCartella = string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCartellaReferto, dtCollaudo.Year.ToString(), meseCollaudo, giorno, acce);
                string pathReferto = string.Format(@"{0}\{1}", pathCartella, fileName);

                if (!Directory.Exists(pathCartella))
                    Directory.CreateDirectory(pathCartella);

                if (File.Exists(pathReferto))
                    File.Delete(pathReferto);
                File.Copy(path, pathReferto, true);
            }

            fileCreato = path;

            CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == galvanica.IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATOSPESSORE).FirstOrDefault();
            if (pdf == null)
            {
                pdf = ds.CDC_PDF.NewCDC_PDFRow();
                pdf.TIPO = CDCTipoPDF.CERTIFICATOSPESSORE;
                pdf.NOMEFILE = path;
                pdf.IDDETTAGLIO = galvanica.IDDETTAGLIO;
                ds.CDC_PDF.AddCDC_PDFRow(pdf);

                using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                    bCDCMetalBusiness.UpdateCDC_PDF(ds);
                ds.CDC_PDF.AcceptChanges();
            }


            return fileCreato;
        }


        private static void CreaCDC(string filename, string ragioneSociale, string idCollaudo, string data,
          string prefisso, string parte, string colore, string quantita,
          string descrizione, string commessa, bool controlloFisico, bool controlloFunzionale, bool controlloDimensionale,
          bool controlloEstetico, bool acconto, bool saldo, string altro, string certificati, byte[] image)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaCDC(ragioneSociale, idCollaudo, data,
             prefisso, parte, colore, quantita,
             descrizione, commessa, controlloFisico, controlloFunzionale, controlloDimensionale,
             controlloEstetico, acconto, saldo, altro, certificati, image);

            pdfHelper.SalvaPdf(filename);
        }

        private static void CreaCDCDimensionale(string filename, string data,
       string prefisso, string parte, string colore, string operatore,
        string commessa, byte[] iloghi, byte[] firma, List<MisuraDimensionale> misure)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportDimensionale(data, prefisso, parte, colore, operatore, commessa, iloghi, firma, misure);

            pdfHelper.SalvaPdf(filename);
        }

        private static void CreaCDCAntiallergico(string filename, bool nichelFree, string data, string dataProduzione, string prefisso, string parte, string colore, string commessa, string quantita, byte[] iloghi)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportAntiallergico(nichelFree, data, dataProduzione, prefisso, parte, colore, commessa, quantita, iloghi);

            pdfHelper.SalvaPdf(filename);
        }

        private static void CreaReportVerniciCoprenti(string filename, bool turbula, bool quadrettatura, string data, string parte, string colore, string fornitore, string numeroCampioni, byte[] iloghi)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportVerniciCoprenti(turbula, quadrettatura, data, parte, colore, fornitore, numeroCampioni, iloghi);


            pdfHelper.SalvaPdf(filename);
        }

        private static void CreaReportCertificatoPiombo(string filename, string elemento, string nomecampione, string lotto, string esito, System.Drawing.Color colore, string metodo, DateTime dataAnalisi, decimal PdPPM, decimal CdPPM, string pathLaboratorio, byte[] image)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportCertificatoPiombo(elemento, nomecampione, lotto, esito, colore, metodo, dataAnalisi, PdPPM, CdPPM, pathLaboratorio, image);


            pdfHelper.SalvaPdf(filename);
        }
        private static void CreaReportTenutaAcidoNitrico(string filename, bool esito, string data, string parte, string colore, string bolla, string dataDDT, string numeroCampioni, byte[] iloghi)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportTenutaAcidoNitrico(esito, data, parte, colore, bolla, dataDDT, numeroCampioni, iloghi);


            pdfHelper.SalvaPdf(filename);
        }
        private static void CreaReportColorimetrico(string filename, string dataCollaudo, string dataCalibrazione,
string prefisso, string parte, string colore, string commessa, string quantita, string operatore,
string strumentoMisura, string nota, List<MisuraColore> misure, byte[] iloghi)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportColorimetrico(dataCollaudo, dataCalibrazione, prefisso, parte, colore, commessa, quantita, operatore, strumentoMisura, nota, misure, iloghi);

            pdfHelper.SalvaPdf(filename);
        }

        private static void CreaReportSpessoriGucci(string filename, DateTime data, string commessa, string operatore, string spessoreRichiesto, string applicazione, string strumentoMisura, int numeroMisure,
            List<string> etichette, List<string> medie, List<string> Std, List<string> Pct, List<string> range, List<string> minimo, List<string> massimo, byte[] iloghi,
            byte[] iBowman, List<List<string>> misure)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportSpessoriGucci(data, commessa, operatore, spessoreRichiesto, applicazione, strumentoMisura, numeroMisure, etichette, medie, Std, Pct, range, minimo, massimo, iloghi, iBowman, misure);

            pdfHelper.SalvaPdf(filename);
        }

        private static void CreaReportSpessoriYSL(string filename, DateTime data, string numeroCampioni, string parte, string colore, int numeroMisure,
          List<string> etichette, List<string> medie, byte[] iloghi, List<List<string>> misure)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportSpessoriYSL(data, numeroCampioni, parte, colore, numeroMisure, etichette, medie, iloghi, misure);

            pdfHelper.SalvaPdf(filename);
        }
        public string CreaPDFDimensionale(decimal IDDETTAGLIO, CDCDS ds, string operatore, string pathCollaudo, byte[] iFirma, byte[] iLoghi)
        {

            CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
            List<CDCDS.CDC_DIMEMSIONIRow> dimensioni = ds.CDC_DIMEMSIONI.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).ToList();

            if (dimensioni.Count == 0)
                throw new Exception("CDC_DETTAGLIO non trovat per IDDETTAGLIO" + IDDETTAGLIO.ToString());

            if (dettaglio == null)
            {
                throw new Exception("IMPOSSIBILE TROVARE CDC DETTAGLIO DA CDC CONFORMITA'");
            }

            DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string cartella = CreaPathCartella(dt, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);
            string fileName = string.Format("{0}-{1}-DIM.pdf", dettaglio.PARTE.Trim(), dettaglio.COLORE.Trim());//4155F-8053-DIM
            string path = string.Format(@"{0}\{1}", cartella, fileName);

            if (!Directory.Exists(cartella))
                Directory.CreateDirectory(cartella);

            if (File.Exists(path))
                File.Delete(path);

            List<MisuraDimensionale> misure = new List<MisuraDimensionale>();
            foreach (CDCDS.CDC_DIMEMSIONIRow dimensione in dimensioni)
            {
                MisuraDimensionale misura = new MisuraDimensionale()
                {
                    campoTampone = dimensione.CONTAMPONE == "S" ? true : false,
                    Conforme = dimensione.CONFORME == "S" ? "OK" : "KO",
                    Grandezza = dimensione.GRANDEZZA,
                    Massimo = dimensione.MASSIMO,
                    Minimo = dimensione.MINIMO,
                    Richieste = dimensione.RICHIESTO,
                    Riferimento = dimensione.RIFERIMENTO,
                    Tampone = dimensione.TAMPONE,
                    Tolleranza = dimensione.TOLLERANZA
                };
                misure.Add(misura);
            }

            CreaCDCDimensionale(path, dettaglio.DATACOLLAUDO, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, operatore, dettaglio.COMMESSAORDINE, iLoghi, iFirma, misure);

            CDCDS.CDC_PDFRow pdf = ds.CDC_PDF.Where(x => x.IDDETTAGLIO == IDDETTAGLIO && x.TIPO == CDCTipoPDF.CERTIFICATODIMENSIONALE).FirstOrDefault();
            if (pdf == null)
            {
                pdf = ds.CDC_PDF.NewCDC_PDFRow();
                pdf.TIPO = CDCTipoPDF.CERTIFICATODIMENSIONALE;
                pdf.NOMEFILE = path;
                pdf.IDDETTAGLIO = IDDETTAGLIO;
                ds.CDC_PDF.AddCDC_PDFRow(pdf);

                using (CDCMetalBusiness bCDCMetalBusiness = new CDCMetalBusiness())
                    bCDCMetalBusiness.UpdateCDC_PDF(ds);
                ds.CDC_PDF.AcceptChanges();
            }


            return path;
        }

        public decimal InserisciCDCGalvanica(CDCDS ds, string spessore, decimal IDDETTAGLIO, string applicazione, string strumentoMisura, int misurePreCampione, string utente)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                CDCDS.CDC_GALVANICARow galvanica = ds.CDC_GALVANICA.Where(x => x.IDDETTAGLIO == IDDETTAGLIO).FirstOrDefault();
                if (galvanica == null)
                {
                    galvanica = ds.CDC_GALVANICA.NewCDC_GALVANICARow();
                    galvanica.APPLICAZIONE = applicazione;
                    galvanica.DATAREFERTO = DateTime.Today;
                    galvanica.IDDETTAGLIO = IDDETTAGLIO;
                    galvanica.IDGALVANICA = (decimal)bCDCMetal.GetID();
                    galvanica.SPESSORE = spessore.ToString();
                    galvanica.UTENTE = utente;
                    galvanica.STRUMENTO = strumentoMisura;
                    galvanica.MISURECAMPIONE = misurePreCampione;
                    ds.CDC_GALVANICA.AddCDC_GALVANICARow(galvanica);
                }
                else
                {
                    galvanica.APPLICAZIONE = applicazione;
                    galvanica.DATAREFERTO = DateTime.Today;
                    galvanica.SPESSORE = spessore.ToString();
                    galvanica.UTENTE = utente;
                    galvanica.MISURECAMPIONE = misurePreCampione;
                    galvanica.STRUMENTO = strumentoMisura;
                }

                return galvanica.IDGALVANICA;
            }
        }

        public void SalvaTabelleSpessori(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                try
                {
                    bCDCMetal.UpdateTabelleSpessore(ds);
                    ds.AcceptChanges();
                }
                catch
                {
                    bCDCMetal.Rollback();
                    throw;
                }
            }
        }

        public void SalvaCertificatiPiombo(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {

                foreach (CDCDS.CDC_CERTIFICATIPIOMBORow riga in ds.CDC_CERTIFICATIPIOMBO)
                {
                    decimal? lunghezza = riga.IsLUNGHEZZANull() ? (decimal?)null : riga.LUNGHEZZA;
                    decimal? larghezza = riga.IsLARGHEZZANull() ? (decimal?)null : riga.LARGHEZZA;
                    decimal? spessore = riga.IsSPESSORENull() ? (decimal?)null : riga.SPESSORE;
                    string elemento = riga.IsELEMENTONull() ? string.Empty : riga.ELEMENTO;

                    bCDCMetal.InsertCDC_CERTIFICATOPIOMBO(elemento, riga.CODICE, riga.MATERIALE, riga.LOTTO, lunghezza, larghezza, spessore,
                        riga.METODO, riga.PESOCAMPIONE, riga.MATRACCIOLO, riga.CONCENTRAZIONE,
                        riga.PBPPM, riga.CDPPM, riga.ESITO, riga.DATACERTIFICATO, riga.UTENTE, riga.DATAINSERIMENTO, riga.PATHFILE);
                }


                //bCDCMetal.UpdateCertificatiPiombo(ds);
                //ds.AcceptChanges();
            }
        }
    }
}
