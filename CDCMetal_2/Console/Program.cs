using CDCMetal.BLL;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathTemporaneo = @"c:\temp\piombo\";
            string filename = @"C:\VSProgetti\CDCMetal\CDCMetal\CDCMetal\Resources\logo_tf_autodichiarazione.png";
            Bitmap firma = (Bitmap)Bitmap.FromFile(filename);
       //     Bitmap firma = Properties.Resources.logo_tf_autodichiarazione;
            ImageConverter converter = new ImageConverter();
            byte[] image = (byte[])converter.ConvertTo(firma, typeof(byte[]));

            CDCBLL bll = new CDCBLL();
            CDCDS ds = new CDCDS();
            bll.FillCDC_CERTIFICATIPIOMBO(ds);

            StringBuilder files = new StringBuilder();

            foreach (CDCDS.CDC_CERTIFICATIPIOMBORow riga in ds.CDC_CERTIFICATIPIOMBO)
            {
                decimal nPd = riga.PBPPM;
                Color colore;
                bll.CalcolaEsitoAnalisiPiombo(nPd, out colore);
                string spessore = string.Empty;
                string lunghezza = string.Empty;
                string larghezza = string.Empty;
                string elemento = string.Empty;
                if (!riga.IsELEMENTONull())
                    elemento = riga.ELEMENTO.ToString();

                if (!riga.IsLUNGHEZZANull())
                    lunghezza = riga.LUNGHEZZA.ToString();
                if (!riga.IsLARGHEZZANull())
                    larghezza = riga.LARGHEZZA.ToString();

                if (!riga.IsSPESSORENull())
                    spessore = riga.SPESSORE.ToString();
                string path = bll.CreaPDFCertificatoPiombo(elemento, lunghezza, larghezza, spessore.ToString(), riga.CODICE, riga.LOTTO,
                riga.ESITO, colore, riga.METODO, riga.DATACERTIFICATO, riga.PBPPM, riga.CDPPM, pathTemporaneo, image);
                files.AppendLine(path);

            }

        }
    }
}

