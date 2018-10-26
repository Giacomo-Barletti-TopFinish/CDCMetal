using CDCMetal.Data;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
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

        public void LeggiDateCollaudo(CDCDS ds, DateTime dataSelezionata)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC(ds, dataSelezionata);
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

        public static string CreaPathCartella(DateTime dataSelezionata,string pathCollaudo, string Accessorista, string Prefisso, string Parte, string Colore, string Commessa)
        {
            string accessorista = ConvertiAccessorista(Accessorista);
            string meseCollaudo = creaStringaCartellaMeseCollaudo(dataSelezionata);
            string giornoCollaudo = creaStringaCartellaGiornoCollaudo(dataSelezionata);

            string cartellaArticolo = string.Format("{0}-{1}-{2} {3}", Prefisso, Parte, Colore, Commessa.Replace('_', ' '));

            return string.Format(@"{0}\{1}\{2}\{3}\{4}", pathCollaudo, accessorista, meseCollaudo, giornoCollaudo, cartellaArticolo);
        }
    }
}
