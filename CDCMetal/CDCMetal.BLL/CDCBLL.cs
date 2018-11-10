using CDCMetal.Data;
using CDCMetal.Entities;
using CDCMetal.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.BLL
{
    public class CDCBLL
    {

        public List<DateTime> LeggiDateCollaudo()
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                return bCDCMetal.LeggiDateRiferimento();
            }
        }

        public void LeggiCollaudoDaData(CDCDS ds, DateTime dataSelezionata)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC(ds, dataSelezionata);
            }
        }

        public void FillCDC_CONFORMITA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_CONFORMITA(ds, IDDETTAGLIO);
            }
        }

        public void FillCDC_ANTIALLERGICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_ANTIALLERGICO(ds, IDDETTAGLIO);
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

        public void CDC_PDF(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.CDC_PDF(ds, IDDETTAGLIO);
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

        public void SalvaDatiConformita(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateConformita(ds);
            }
        }

        public void SalvaDatiAntiallergia(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_ANTIALLERGICO(ds);
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
                //  DateTime dt = DateTime.ParseExact(dettaglio.DATACOLLAUDO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt = DateTime.Today;
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

                CreaCDCAntiallergico(path, nichelFree, dettaglio.DATACOLLAUDO, antiallergico.DATAPRODUZIONE.ToShortDateString(), dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE, dettaglio.QUANTITA.ToString(), image);

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
                string cartella = CreaPathCartella(dt, pathCollaudo, dettaglio.ACCESSORISTA, dettaglio.PREFISSO, dettaglio.PARTE, dettaglio.COLORE, dettaglio.COMMESSAORDINE);

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
                        Rilevato = coloreRow.IsRILEVATONull()?string.Empty:coloreRow.RILEVATO,
                        Tolleranza = coloreRow.IsTOLLERANZANull() ? string.Empty : coloreRow.TOLLERANZA,
                    };
                    misure.Add(m);
                }
                string data = colore.DATAINSERIMENTO.ToShortDateString();
                string dataCalibrazione = colore.DATACALIBRAZIONE.ToShortDateString();
                string strumento = colore.STRUMENTO;
                string nota = colore.IsNOTANull() ? string.Empty : colore.NOTA;

                string operatore = "LB";
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

        private static void CreaReportColorimetrico(string filename, string dataCollaudo, string dataCalibrazione,
string prefisso, string parte, string colore, string commessa, string quantita, string operatore,
string strumentoMisura, string nota, List<MisuraColore> misure, byte[] iloghi)
        {
            PDFHelper pdfHelper = new PDFHelper();
            pdfHelper.CreaReportColorimetrico(dataCollaudo, dataCalibrazione, prefisso, parte, colore, commessa, quantita, operatore, strumentoMisura, nota, misure, iloghi);

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
                    RIferimento = dimensione.RIFERIMENTO,
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
    }
}
