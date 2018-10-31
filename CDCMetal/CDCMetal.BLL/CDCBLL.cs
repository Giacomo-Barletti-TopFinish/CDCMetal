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

    }
}
