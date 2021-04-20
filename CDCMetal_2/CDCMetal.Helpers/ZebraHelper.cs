using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Helpers
{
    public class ZebraHelper
    {
        public static void EseguiStampaEtichetta(string zebraPrinter, DataRow riga, string quantita)
        {
//#if DEBUG
//            return;
//#endif

            string linea = string.Empty;
            if (riga[9] != System.DBNull.Value)
                linea = (string)riga[9];
            string descrizione = string.Empty;
            if (riga[10] != System.DBNull.Value)
                descrizione = (string)riga[10];
            string destinazione = string.Empty;
            if (riga[11] != System.DBNull.Value)
                destinazione = (string)riga[11];
            string numeroEtichette = string.Empty;
            if (riga[12] != System.DBNull.Value)
                numeroEtichette = (string)riga[12];

            string accessorista = (string)riga[1];
            if (accessorista == "TopFinish")
                accessorista = "METAL-PLUS";
            else
                accessorista = "METAL-PLUS";

            string prefisso = (string)riga[3];
            string parte = (string)riga[4];
            string colore = (string)riga[5];

            string commessa = (string)riga[6];
            decimal idcommessa = (decimal)riga[0];

            EtichettaCDC(zebraPrinter, accessorista, descrizione, quantita, destinazione, commessa, idcommessa.ToString(), prefisso, parte, colore, linea);

        }

        public static void EseguiStampaEtichettaSeparatore(string zebraPrinter)
        {
//#if DEBUG
//            return;
//#endif
            string asterischi = "******************";
            EtichettaCDC(zebraPrinter, asterischi, string.Empty, asterischi, string.Empty, asterischi, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        }

        public static void EtichettaCDC(string zebraPrinter, string accessorista, string descrizione, string quantita, string destinazione, string commessa, string collaudo, string prefisso, string parte, string colore, string linea)
        {
            string articolo = string.Format("{0}-{1}-{2}-{3}", prefisso, parte, colore, linea);
            if (string.IsNullOrEmpty(colore)||colore.ToUpper().Trim()=="NULL")
                articolo = string.Format("{0}-{1}", prefisso, parte);

            //            zebraPrinter = @"\\k-conf-01\ZDesigner GK420t";
            StringBuilder sb = new StringBuilder();
            string fontGrande = "TN,9,9";
            string fontNormale = "QN,20,12";

            int posizioneX = 30;

            sb.Append("^XA");
            sb.Append(InserisciTesto(posizioneX, 20, fontGrande, accessorista.Trim().ToUpper()));

            sb.Append(InserisciTesto(posizioneX, 60, fontNormale, destinazione.Trim().ToUpper()));

            sb.Append(InserisciTesto(posizioneX, 90, fontGrande, articolo.Trim().ToUpper()));

            sb.Append(InserisciTesto(posizioneX, 135, fontNormale, descrizione.Trim().ToUpper()));
            sb.Append(InserisciTesto(posizioneX, 170, fontGrande, commessa.Trim().ToUpper()));

            string qta = string.Format("Quantita' n.     {0}", quantita);
            sb.Append(InserisciTesto(posizioneX, 220, fontGrande, qta));

            string str = string.Format("Id Collaudo     {0}", collaudo);
            sb.Append(InserisciTesto(posizioneX, 270, fontGrande, str));

            sb.Append("^XZ");

            RawPrinterHelper.SendStringToPrinter(zebraPrinter, sb.ToString());
        }
        private static string InserisciTesto(int x, int y, string font, string testo)
        {
            return string.Format("^FO{0},{1}^A{2}^FD{3}^FS", x, y, font, testo);
        }
    }
}
