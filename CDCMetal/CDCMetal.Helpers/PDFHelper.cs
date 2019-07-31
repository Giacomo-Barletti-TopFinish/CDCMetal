using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace CDCMetal.Helpers
{
    public class PDFHelper
    {
        private Document document = new Document();

        public void CreaCDC(string ragioneSociale, string idCollaudo, string data,
            string prefisso, string parte, string colore, string quantita,
            string descrizione, string commessa, bool controlloFisico, bool controlloFunzionale, bool controlloDimensionale,
            bool controlloEstetico, bool acconto, bool saldo, string altro, string certificati, byte[] image)
        {

            document.Info.Title = "CDC";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();
            CreaTabellaCDC(ragioneSociale, idCollaudo, data,
             prefisso, parte, colore, quantita,
             descrizione, commessa, controlloFisico, controlloFunzionale, controlloDimensionale,
             controlloEstetico, acconto, saldo, altro, certificati, image);
        }

        public void SalvaPdf(string filename)
        {
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();
            renderer.PdfDocument.Save(filename);
        }
        private void DefineBasicStyles()
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Calibri";
            style.Font.Size = 12;
            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            style = document.Styles["Heading1"];
            style.Font.Size = 17;
            style.Font.Color = Colors.Black;

            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called TextBox based on style Normal
            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = document.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;
        }

        private void CreaTabellaCDC(string ragioneSociale, string idCollaudo, string data,
            string prefisso, string parte, string colore, string quantita,
            string descrizione, string commessa, bool controlloFisico, bool controlloFunzionale, bool controlloDimensionale,
            bool controlloEstetico, bool acconto, bool saldo, string altro, string certificati, byte[] image)
        {
            document.LastSection.AddParagraph();

            // RIGA 1
            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromCentimeter(13.5));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(3.9));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(2.1);

            Cell cell = row.Cells[0];
            cell.AddParagraph("CERTIFICATO DI CONFORMITA'");
            cell.Style = "Heading1";
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell = row.Cells[1];
            cell.AddParagraph("MH-MO-007");
            cell.AddParagraph("Rev. 00 del");
            cell.AddParagraph("03/03/2017");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            document.LastSection.Add(table);

            // RIGA 2
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(7.7));
            column = table.AddColumn(Unit.FromCentimeter(5.8));
            table.AddColumn(Unit.FromCentimeter(3.9));

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("RAGIONE SOCIALE FORNITORE:");
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell = row.Cells[1];
            cell.AddParagraph("N° ID Collaudo:");
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell = row.Cells[2];
            cell.AddParagraph("DATA:");
            cell.Format.Alignment = ParagraphAlignment.Left;
            table.SetEdge(0, 0, 3, 1, Edge.Box, BorderStyle.None, 0);
            table.SetEdge(0, 0, 3, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 3, 1, Edge.Right, BorderStyle.Single, 0.75);


            document.LastSection.Add(table);

            // RIGA 3
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(7.7));
            column = table.AddColumn(Unit.FromCentimeter(5.8));
            table.AddColumn(Unit.FromCentimeter(3.9));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.6);

            cell = row.Cells[0];
            cell.AddParagraph(ragioneSociale);
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell = row.Cells[1];
            cell.AddParagraph(idCollaudo);
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell = row.Cells[2];
            cell.AddParagraph(data);
            cell.Format.Alignment = ParagraphAlignment.Center;

            table.SetEdge(0, 0, 3, 1, Edge.Top, BorderStyle.None, 0);

            document.LastSection.Add(table);

            // RIGA 4
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(5.8));
            column = table.AddColumn(Unit.FromCentimeter(5.8));
            table.AddColumn(Unit.FromCentimeter(5.8));

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("PREFISSO-PARTE-COLORE:");
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell = row.Cells[1];
            cell.AddParagraph("QUANTITA':");
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell = row.Cells[2];
            cell.AddParagraph("Descrizione:");
            cell.Format.Alignment = ParagraphAlignment.Left;
            table.SetEdge(0, 0, 3, 1, Edge.Box, BorderStyle.None, 0);
            table.SetEdge(0, 0, 3, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 3, 1, Edge.Right, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);

            // RIGA 5
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(5.8));
            column = table.AddColumn(Unit.FromCentimeter(5.8));
            table.AddColumn(Unit.FromCentimeter(5.8));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.6);

            cell = row.Cells[0];
            string testo = string.Format("{0}-{1}-{2}", prefisso, parte, colore);
            cell.AddParagraph(testo);
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell = row.Cells[1];
            cell.AddParagraph(quantita);
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell = row.Cells[2];
            cell.AddParagraph(descrizione);
            cell.Format.Alignment = ParagraphAlignment.Center;

            table.SetEdge(0, 0, 3, 1, Edge.Box, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);
            // RIGA 6
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(5.8));
            table.AddColumn(Unit.FromCentimeter(11.6));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);

            cell = row.Cells[0];
            cell.AddParagraph("ORDINE/COMMESSA:");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell = row.Cells[1];
            cell.AddParagraph(commessa);
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            table.SetEdge(0, 0, 2, 1, Edge.Top, BorderStyle.None, 0);
            table.SetEdge(0, 0, 2, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 1, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 1, Edge.Bottom, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);
            // RIGA 7
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(17.4));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);

            cell = row.Cells[0];
            cell.AddParagraph("NOTE");
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            table.SetEdge(0, 0, 1, 1, Edge.Top, BorderStyle.None, 0);
            table.SetEdge(0, 0, 1, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 1, 1, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 1, 1, Edge.Bottom, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);

            // RIGA 8.1
            table = new Table();
            // table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(5.4));
            column = table.AddColumn(Unit.FromCentimeter(12));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);

            cell = row.Cells[0];
            cell.VerticalAlignment = VerticalAlignment.Center;
            Paragraph p = cell.AddParagraph("Controllo Fisico e Chimico");
            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(string.Empty);
            p.AddFormattedText(controlloFisico ? "\u00fe" : "\u00A8", new Font("Wingdings"));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Controllo Funzionale");
            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(string.Empty);
            p.AddFormattedText(controlloFunzionale ? "\u00fe" : "\u00A8", new Font("Wingdings"));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Controllo Dimensionale");
            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(string.Empty);
            p.AddFormattedText(controlloDimensionale ? "\u00fe" : "\u00A8", new Font("Wingdings"));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Controllo Estetico");
            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(string.Empty);
            p.AddFormattedText(controlloEstetico ? "\u00fe" : "\u00A8", new Font("Wingdings"));

            table.SetEdge(0, 0, 2, 4, Edge.Top, BorderStyle.None, 0);
            table.SetEdge(0, 0, 2, 4, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 4, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 4, Edge.Bottom, BorderStyle.None, 0);

            document.LastSection.Add(table);
            // RIGA 8.2
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(17.4));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(3);

            cell = row.Cells[0];
            p = cell.AddParagraph(string.Empty);
            if (string.IsNullOrEmpty(certificati))
                p = cell.AddParagraph("Certificati: ..........................................................................................................................................");
            else
                p = cell.AddParagraph(string.Format("Certificati: {0}", certificati));
            p = cell.AddParagraph(string.Empty);
            if (string.IsNullOrEmpty(altro))
                p = cell.AddParagraph("Altro: ..................................................................................................................................................");
            else
                p = cell.AddParagraph(string.Format("Altro: {0}", altro));

            table.SetEdge(0, 0, 1, 1, Edge.Top, BorderStyle.None, 0);
            table.SetEdge(0, 0, 1, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 1, 1, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 1, 1, Edge.Bottom, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);
            // RIGA 9
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(17.4));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(2.1);

            cell = row.Cells[0];
            cell.AddParagraph("SI CERTIFICA CHE LA FORNITURA E' CONFORME IN OGNI PARTE ALL'ORDINE CUI SI RIFERISCE, AD ECCEZIONE DELLE DEROGHE/CONCESSIONI ANNOTATE, E CHE LE FORNITURE SONO STATE VERIFICATE E PROVATE IN CONFORMITA' AI REQUISITI DELL'ORDINE/COMMESSA.");

            table.SetEdge(0, 0, 1, 1, Edge.Top, BorderStyle.None, 0);
            table.SetEdge(0, 0, 1, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 1, 1, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 1, 1, Edge.Bottom, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);

            // RIGA 9
            table = new Table();

            column = table.AddColumn(Unit.FromCentimeter(4.4));
            column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(9.7));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            cell = row.Cells[0];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("ACCONTO FORNITURA");
            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(string.Empty);
            p.AddFormattedText(acconto ? "\u00fe" : "\u00A8", new Font("Wingdings"));

            cell = row.Cells[2];
            cell.MergeDown = 1;
            p = cell.AddParagraph("Timbro e Firma Responsabile della Qualità");

            string fileImage = MigraDocFilenameFromByteArray(image);

            p.AddImage(fileImage);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            cell = row.Cells[0];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("SALDO");
            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(string.Empty);
            p.AddFormattedText(saldo ? "\u00fe" : "\u00A8", new Font("Wingdings"));

            table.SetEdge(0, 0, 3, 2, Edge.Top, BorderStyle.None, 0);
            table.SetEdge(0, 0, 3, 2, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 3, 2, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 3, 2, Edge.Bottom, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 2, Edge.Right, BorderStyle.Single, 0.75);
            document.LastSection.Add(table);
        }

        private string MigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" + Convert.ToBase64String(image);
        }

        public void CreaReportDimensionale(string data,
       string prefisso, string parte, string colore, string operatore,
        string commessa, byte[] iloghi, byte[] firma, List<MisuraDimensionale> misure
        )
        {

            document.Info.Title = "Dimensionale";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();
            CreaTabellaDimensionale(data, prefisso, parte, colore, operatore,
            commessa, iloghi, firma, misure);

        }

        private void CreaTabellaDimensionale(string data,
          string prefisso, string parte, string colore, string operatore,
           string commessa, byte[] iloghi, byte[] firma, List<MisuraDimensionale> misure)
        {
            document.LastSection.AddParagraph();

            // RIGA 1
            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromCentimeter(3.4));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(10));
            table.AddColumn(Unit.FromCentimeter(3.4));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            Cell cell = row.Cells[0];
            cell.MergeDown = 2;
            Paragraph p = cell.AddParagraph();
            string iLogoMP = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(iLogoMP);

            Cell cellA = row.Cells[1];
            cellA.AddParagraph("REPORT DIMENSIONALE");
            cellA.Format.Font.Bold = true;
            cellA.Style = "Heading1";
            cellA.Format.Alignment = ParagraphAlignment.Center;
            cellA.VerticalAlignment = VerticalAlignment.Center;
            cellA.Borders.Bottom.Width = 0;

            cell = row.Cells[2];
            cell.AddParagraph("M-Q01.01-REV00");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);
            cell = row.Cells[0];
            cell = row.Cells[1];
            cell.Borders.Top.Width = 0;

            cell = row.Cells[2];
            cell.MergeDown = 1;
            cell.AddParagraph("Pagina 1/1");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            row = table.AddRow();
            //row.Height = Unit.FromCentimeter(0.5);
            cell = row.Cells[1];
            cell.AddParagraph("MODULO CON MISURE");
            cell.Format.Font.Size = 8;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;


            document.LastSection.Add(table);

            // RIGA 2
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(3.4));
            column = table.AddColumn(Unit.FromCentimeter(6.7));
            column = table.AddColumn(Unit.FromCentimeter(3.3));
            table.AddColumn(Unit.FromCentimeter(3.4));

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("ARTICOLO");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Bold = true;
            cell = row.Cells[1];
            string articolo = string.Format("{0}-{1}-{2}", prefisso, parte, colore);
            cell.AddParagraph(articolo);
            cell.Format.Font.Size = 8;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;

            cell = row.Cells[2];
            cell.AddParagraph("Controllore");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 8;
            cell.Format.Font.Bold = true;
            cell = row.Cells[3];
            cell.AddParagraph(operatore);
            cell.Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("IDX/COMMESSA");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Bold = true;
            cell = row.Cells[1];
            string commessaStr = commessa.Replace('_', ' ');
            cell.AddParagraph(commessaStr);
            cell.Format.Font.Size = 8;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;

            cell = row.Cells[2];
            cell.AddParagraph("Data");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 8;
            cell.Format.Font.Bold = true;
            cell = row.Cells[3];
            cell.AddParagraph(data);
            cell.Format.Font.Size = 8;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;

            table.SetEdge(0, 0, 4, 2, Edge.Box, BorderStyle.None, 0);
            table.SetEdge(0, 0, 4, 2, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 4, 2, Edge.Right, BorderStyle.Single, 0.75);


            document.LastSection.Add(table);

            // RIGA 3
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(3.4));
            column = table.AddColumn(Unit.FromCentimeter(5));
            column = table.AddColumn(Unit.FromCentimeter(1.7));
            column = table.AddColumn(Unit.FromCentimeter(1.7));
            column = table.AddColumn(Unit.FromCentimeter(1.6));
            column = table.AddColumn(Unit.FromCentimeter(1.7));
            table.AddColumn(Unit.FromCentimeter(1.7));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.4);

            cell = row.Cells[0];
            cell.AddParagraph("Riferimento Disegno");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 10;
            cell = row.Cells[1];
            cell.AddParagraph("Grandezza Misurata");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 10;
            cell = row.Cells[2];
            cell.AddParagraph("Valore");
            cell.AddParagraph("Richie");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 10;
            cell = row.Cells[3];
            cell.AddParagraph("TOLLERANZA");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 8;
            cell = row.Cells[4];
            cell.AddParagraph("Valore");
            cell.AddParagraph("Minimo");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 10;
            cell = row.Cells[5];
            cell.AddParagraph("Valore");
            cell.AddParagraph("Massimo");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 10;
            cell = row.Cells[6];
            cell.AddParagraph("Conforme");
            Paragraph p1 = cell.AddParagraph("OK-KO");
            p1.Format.Font.Bold = true;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 10;

            table.SetEdge(0, 0, 7, 1, Edge.Top, BorderStyle.Single, 0.75);


            //misure
            for (int i = 0; i < 20; i++)
            {
                row = table.AddRow();
                row.Height = Unit.FromCentimeter(0.5);

                MisuraDimensionale m = null;
                if (misure.Count - 1 >= i)
                    m = misure[i];

                cell = row.Cells[0];
                if (m == null)
                    cell.AddParagraph(string.Empty);
                else
                    cell.AddParagraph(m.Riferimento);
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell = row.Cells[1];
                if (m == null)
                    cell.AddParagraph(string.Empty);
                else
                    cell.AddParagraph(m.Grandezza);
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell = row.Cells[2];
                if (m == null)
                    cell.AddParagraph(string.Empty);
                else
                    cell.AddParagraph(m.Richieste);
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;



                if (m == null)
                {
                    cell = row.Cells[3];
                    cell.AddParagraph(string.Empty);
                    cell.Format.Font.Size = 8;
                    cell = row.Cells[4];
                    cell.AddParagraph(string.Empty);
                    cell.Format.Font.Size = 10;
                    cell = row.Cells[5];
                    cell.AddParagraph(string.Empty);
                    cell.Format.Font.Size = 10;
                }
                else
                {
                    if (m.campoTampone)
                    {
                        cell = row.Cells[3];
                        cell.MergeRight = 2;
                        cell.AddParagraph(m.Tampone);
                        cell.Format.Alignment = ParagraphAlignment.Center;
                        cell.VerticalAlignment = VerticalAlignment.Center; cell.Format.Font.Size = 10;

                    }
                    else
                    {
                        cell = row.Cells[3];
                        cell.AddParagraph(m.Tolleranza);
                        cell.Format.Alignment = ParagraphAlignment.Center;
                        cell.VerticalAlignment = VerticalAlignment.Center; cell.Format.Font.Size = 10;
                        cell = row.Cells[4];
                        cell.AddParagraph(m.Minimo);
                        cell.Format.Alignment = ParagraphAlignment.Center;
                        cell.VerticalAlignment = VerticalAlignment.Center; cell.Format.Font.Size = 10;
                        cell = row.Cells[5];
                        cell.AddParagraph(m.Massimo);
                        cell.Format.Alignment = ParagraphAlignment.Center;
                        cell.VerticalAlignment = VerticalAlignment.Center; cell.Format.Font.Size = 10;
                    }
                }


                cell = row.Cells[6];
                if (m == null)
                    cell.AddParagraph(string.Empty);
                else
                    cell.AddParagraph(m.Conforme);
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.Format.Font.Size = 10;
            }

            document.LastSection.Add(table);

            // RIGA 4
            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(11.8));
            column = table.AddColumn(Unit.FromCentimeter(5));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.4);

            cell = row.Cells[0];
            cell.AddParagraph(string.Empty);

            cell = row.Cells[1];
            cell.AddParagraph("Firma");
            cell.Format.Font.Size = 10;
            Paragraph pFirma = cell.AddParagraph();
            string iFirma = MigraDocFilenameFromByteArray(firma);
            pFirma.AddImage(iFirma);


            table.SetEdge(0, 0, 2, 1, Edge.Box, BorderStyle.None, 0);
            table.SetEdge(0, 0, 2, 1, Edge.Left, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 1, Edge.Right, BorderStyle.Single, 0.75);
            table.SetEdge(0, 0, 2, 1, Edge.Bottom, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);

        }

        public void CreaReportAntiallergico(bool nichelFree, string data, string dataProduzione, string prefisso, string parte, string colore, string commessa, string quantita, byte[] iloghi)
        {

            document.Info.Title = "Autodichiarazione";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();

            Paragraph p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            string iLogoMP = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(iLogoMP);
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            Font fontPiccolo = new Font("Times New Roman");
            fontPiccolo.Size = 8;
            p.AddFormattedText("Viale Kennedy, 103 - 50038 SCARPERIA (FI) - P.Iva 04949200481", fontPiccolo);
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddFormattedText("Tel 055 843611 - Fax 055 8436130 - e-mail: info@topfinish.it", fontPiccolo);

            Font fontGrande = new Font("Times New Roman");
            fontGrande.Size = 22;
            fontGrande.Bold = true;
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            p.AddLineBreak();
            p.AddLineBreak();
            if (nichelFree)
                p.AddFormattedText("Autodichiarazione per articoli Nichel free", fontGrande);
            else
                p.AddFormattedText("Autodichiarazione per articoli Antiallergico", fontGrande);
            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            Table table = new Table();
            table.Borders.Width = 1.5;

            Column column = table.AddColumn(Unit.FromCentimeter(4.5));
            column = table.AddColumn(Unit.FromCentimeter(12.5));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph("DATA");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph(data);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.AddParagraph("DATA PRODUZIONE");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(dataProduzione);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.Format.Font.Size = 14;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("ARTICOLO");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(string.Format("{0}-{1}", prefisso, parte));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("FINITURA");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(colore);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("COMMESSA");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(commessa);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("N° PEZZI PRODOTTI");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(quantita);

            document.LastSection.Add(table);


            Font fontNormale = new Font("Times New Roman");
            fontNormale.Size = 14;

            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font = fontNormale;
            p.AddLineBreak();
            p.AddLineBreak();
            if (nichelFree)
                p.AddFormattedText("L’azienda Top Finish dichiara in fede che nella realizzazione del riporto galvanico del lotto in oggetto è stato seguito il ciclo Nichel free previsto sulla scheda del Capitolato Gucci, con la giusta sequenza dei bagni evitando tassativamente di utilizzare un qualunque bagno o trattamento che possa comportare un qualsivoglia deposito di nichel su pezzi. ", fontNormale);
            else
                p.AddFormattedText("L’azienda Top Finish dichiara in fede che nella realizzazione del riporto galvanico del lotto in oggetto è stato seguito il ciclo antiallergico previsto sulla scheda del Capitolato Gucci, con la giusta sequenza dei bagni. ", fontNormale);
            p.AddLineBreak();
            p.AddLineBreak();

            fontNormale = new Font("Times New Roman");
            fontNormale.Size = 14;

            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Right;
            p.Format.Font = fontNormale;
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddText("In fede");
            p.AddLineBreak();
            p.AddText("Crolli Fabio");
        }

        public void CreaReportVerniciCoprenti(bool turbula, bool quadrettatura, string data, string parte, string colore, string fornitore, string numeroCampioni, byte[] iloghi)
        {
            parte = parte.PadLeft(5, '0');

            document.Info.Title = "Autodichiarazione";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();

            Paragraph p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            string iLogoMP = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(iLogoMP);
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            Font fontPiccolo = new Font("Times New Roman");
            fontPiccolo.Size = 8;
            fontPiccolo.Bold = true;

            p.AddFormattedText("Top Finish 2002 S.p.A.", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Viale Kennedy, 103 - 50038 SCARPERIA (FI) - P.Iva 04949200481", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Tel 055 843611 - Fax 055 8436130 - e-mail: info@topfinish.it", fontPiccolo);

            Font fontNormale = new Font("Times New Roman");
            fontNormale.Size = 12;

            Font fontGrande = new Font("Times New Roman");
            fontGrande.Size = 18;
            fontGrande.Bold = true;
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            p.AddLineBreak();
            p.AddLineBreak();
            string testo = string.Format("Scarperia, {0}", data);
            p.AddFormattedText(testo, fontGrande);
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddFormattedText("TEST VERNICI COPRENTI", fontGrande);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            Table table = new Table();
            table.Borders.Width = 1.5;
            double larghezzaColonna1 = 10.0;
            double larghezzaColonna2 = 7.0;
            Column column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna1));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna2));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph("Articolo");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph(parte);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.AddParagraph("Finitura");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(colore);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.Format.Font.Size = 14;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("N°campioni analizzati");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(numeroCampioni);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("Fornitore");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(fornitore);

            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();

            table = new Table();
            table.Borders.Width = 1.5;

            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna1));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna2));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph("Test turbula");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph(turbula ? "ok" : "ko");

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.AddParagraph("Test quadrettatura");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(quadrettatura ? "ok" : "ko");

            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Left;

            p.AddFormattedText("Test effettuati presso il Laboratorio Top Finish", fontNormale);
            p.AddLineBreak();
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Right;
            fontNormale.Bold = true;
            p.AddFormattedText("Distinti Saluti", fontNormale);
            p.AddLineBreak();
            p.AddFormattedText("Laboratorio Qualità, R&D", fontNormale);
            p.AddLineBreak();

        }

        public void CreaReportCertificatoPiombo(string elemento, string nomecampione, string lotto, string esito, System.Drawing.Color colore, string metodo, DateTime dataAnalisi, decimal PdPPM, decimal CdPPM, string pathLaboratorio, byte[] image)
        {
            document.Info.Title = "Autodichiarazione";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();

            Paragraph p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            string iLogoMP = MigraDocFilenameFromByteArray(image);
            p.AddImage(iLogoMP);
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            Font fontPiccolo = new Font("Times New Roman");
            fontPiccolo.Size = 8;
            fontPiccolo.Bold = true;

            p.AddFormattedText("Top Finish 2002 S.p.A.", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Viale Kennedy, 103 - 50038 SCARPERIA (FI) - P.Iva 04949200481", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Tel 055 843611 - Fax 055 8436130 - e-mail: info@topfinish.it", fontPiccolo);

            Font fontNormale = new Font("Times New Roman");
            fontNormale.Size = 12;

            Font fontGrande = new Font("Times New Roman");
            fontGrande.Size = 18;
            fontGrande.Bold = true;
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddFormattedText("Report analisi Pd e Cd", fontGrande);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            Table table = new Table();
            table.Borders.Width = 1.5;
            double larghezzaColonna1 = 7.5;
            double larghezzaColonna2 = 1.5;
            double larghezzaColonna3 = 6.5;
            Column column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna1));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna2));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna3));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.8);
            int tablefontsize = 10;

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            string str = string.Format("Metodo utilizzato: {0}", metodo);
            cell.AddParagraph(str);

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph("DATA:");

            cell = row.Cells[2];
            cell.AddParagraph(dataAnalisi.ToShortDateString());
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();

            table = new Table();
            table.Borders.Width = 1.5;

            column = table.AddColumn(Unit.FromCentimeter(4.5));
            column = table.AddColumn(Unit.FromCentimeter(3.0));
            column = table.AddColumn(Unit.FromCentimeter(2.0));
            column = table.AddColumn(Unit.FromCentimeter(3.0));
            column = table.AddColumn(Unit.FromCentimeter(3.0));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.8);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph("Nome campione");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph("Lotto/DDT");

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph("Esito");

            cell = row.Cells[3];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph("Pd (PPM)");

            cell = row.Cells[4];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph("Cd (PPM)");

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.8);
            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph(nomecampione);

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph(lotto);

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = tablefontsize;
            cell.AddParagraph(esito);

            cell = row.Cells[3];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = tablefontsize;

            cell.Shading.Color = Colors.Green;
            if (colore == System.Drawing.Color.Yellow)
                cell.Shading.Color = Colors.Yellow;
            else if (colore == System.Drawing.Color.Red)
                cell.Shading.Color = Colors.Red;

            cell.AddParagraph(PdPPM.ToString());
            cell = row.Cells[4];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = tablefontsize;

            if (CdPPM < 40)
                cell.AddParagraph("<40");
            else
                cell.AddParagraph("≥40");
            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();

            if(CdPPM>40)
            {
                p = document.LastSection.AddParagraph();
                p.Format.Alignment = ParagraphAlignment.Left;
                p.AddFormattedText("Nota: [Cd]/ppm<"+CdPPM.ToString(), fontNormale);

            }
            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Right;
            fontNormale.Bold = true;
            p.AddFormattedText("Lab. Qualità e R&D", fontNormale);
            p.AddLineBreak();

        }

        public void CreaReportTenutaAcidoNitrico(bool esito, string data, string parte, string colore, string bolla, string dataDDT, string numeroCampioni, byte[] iloghi)
        {
            parte = parte.PadLeft(5, '0');

            document.Info.Title = "Autodichiarazione";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();

            Paragraph p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            string iLogoMP = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(iLogoMP);
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            Font fontPiccolo = new Font("Times New Roman");
            fontPiccolo.Size = 8;
            fontPiccolo.Bold = true;

            p.AddFormattedText("Top Finish 2002 S.p.A.", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Viale Kennedy, 103 - 50038 SCARPERIA (FI) - P.Iva 04949200481", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Tel 055 843611 - Fax 055 8436130 - e-mail: info@topfinish.it", fontPiccolo);

            Font fontNormale = new Font("Times New Roman");
            fontNormale.Size = 12;

            Font fontGrande = new Font("Times New Roman");
            fontGrande.Size = 18;
            fontGrande.Bold = true;
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Left;
            p.AddLineBreak();
            p.AddLineBreak();
            string testo = string.Format("Scarperia, {0}", data);
            p.AddFormattedText(testo, fontGrande);
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddFormattedText("Controllo di tenuta all'acido nitrico", fontGrande);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            Table table = new Table();
            table.Borders.Width = 1.5;
            double larghezzaColonna1 = 10.0;
            double larghezzaColonna2 = 7.0;
            Column column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna1));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna2));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph("Articolo");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph(parte);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.AddParagraph("Finitura");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(colore);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.Format.Font.Size = 14;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("N°campioni analizzati");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(numeroCampioni);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("N°bolla");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(bolla);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("Data DDT");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(dataDDT);

            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();

            table = new Table();
            table.Borders.Width = 1.5;

            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna1));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna2));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph("Esito test (30 sec) turbula");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph(esito ? "ok" : "ko");


            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Right;
            fontNormale.Bold = true;
            p.AddFormattedText("Distinti Saluti", fontNormale);
            p.AddLineBreak();
            p.AddFormattedText("Laboratorio Qualità, R&D", fontNormale);
            p.AddLineBreak();

        }
        public void CreaReportColorimetrico(string dataCollaudo, string dataCalibrazione,
string prefisso, string parte, string colore, string commessa, string quantita, string operatore,
string strumentoMisura, string nota, List<MisuraColore> misure, byte[] iloghi)
        {

            document.Info.Title = "Colorimetrico";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();



            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(9.9));
            column = table.AddColumn(Unit.FromCentimeter(3.3));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            Paragraph p = cell.AddParagraph();
            cell.MergeDown = 1;
            string fileImage = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(fileImage);

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 18;
            cell.MergeDown = 1;
            cell.AddParagraph("REPORT COLORIMETRICO");

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("M-Q01.02-REV00");

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(2.2);
            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("Pagina 1/1");

            document.LastSection.Add(table);

            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(6.6));
            column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(3.3));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph("ARTICOLO");


            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph(string.Format("{0}-{1}-{2}", prefisso, parte, colore));

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph("Controllore");


            cell = row.Cells[3];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 8;
            cell.AddParagraph(operatore);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph("IDX/COMMESSA");


            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            string commessaStr = string.Format("{0}-PZ{1}", commessa, quantita);
            cell.AddParagraph(commessaStr);

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph("Data");


            cell = row.Cells[3];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph(dataCollaudo);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph("STRUMENTO DI MISURA");


            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph(strumentoMisura);

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph("DATA CALIBRAZIONE");


            cell = row.Cells[3];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 8;
            cell.AddParagraph(dataCalibrazione);

            table.SetEdge(0, 0, 4, 3, Edge.Top, BorderStyle.None, 0);

            document.LastSection.Add(table);


            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(4.9));
            column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(3.3));
            column = table.AddColumn(Unit.FromCentimeter(1.7));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.4);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 11;
            cell.AddParagraph("Controllo Colore");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 11;
            cell.AddParagraph("Valore Richiesto");

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 11;
            cell.AddParagraph("Tolleranza");

            cell = row.Cells[3];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 11;
            cell.AddParagraph("Rilevato");

            cell = row.Cells[4];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 11;
            cell.AddParagraph("Conforme");
            cell.AddParagraph("OK-KO");

            foreach (MisuraColore misura in misure)
            {
                row = table.AddRow();
                row.Height = Unit.FromCentimeter(0.5);

                cell = row.Cells[0];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell.AddParagraph(misura.ControlloColore);

                cell = row.Cells[1];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell.AddParagraph(misura.Richiesto);

                cell = row.Cells[2];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell.AddParagraph(misura.Tolleranza);

                cell = row.Cells[3];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell.AddParagraph(misura.Rilevato);

                cell = row.Cells[4];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Size = 10;
                cell.AddParagraph(misura.Conforme);
            }
            table.SetEdge(0, 0, 5, 1, Edge.Top, BorderStyle.None, 0);

            document.LastSection.Add(table);

            table = new Table();
            table.Borders.Width = 0;
            table.AddColumn(Unit.FromCentimeter(4.9));
            row = table.AddRow();
            row.Height = Unit.FromCentimeter(16);
            document.LastSection.Add(table);

            table = new Table();
            table.Borders.Width = 0.75;
            table.AddColumn(Unit.FromCentimeter(11.5));
            table.AddColumn(Unit.FromCentimeter(5));
            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.4);

            cell = row.Cells[0];
            cell.Format.Font.Size = 10;
            cell.AddParagraph("Note");
            cell.AddParagraph(nota);

            cell = row.Cells[1];
            cell.Format.Font.Size = 10;
            cell.AddParagraph("Firma");

            document.LastSection.Add(table);

        }

        public void CreaReportSpessoriGucci(DateTime data, string commessa, string operatore, string spessoreRichiesto, string applicazione, string strumentoMisura, int numeroMisure,
          List<string> etichette, List<string> medie, List<string> Std, List<string> Pct, List<string> range, List<string> minimo, List<string> massimo, byte[] iloghi,
          byte[] iBowman, List<List<string>> misure)
        {

            document.Info.Title = "Spessori";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();

            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.5);
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.5);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.3);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.3);

            Color grigio = Color.FromRgb(191, 191, 191);

            Table table = new Table();
            table.Borders.Width = 0;

            Column column = table.AddColumn(Unit.FromCentimeter(11));
            column = table.AddColumn(Unit.FromCentimeter(3.5));
            column = table.AddColumn(Unit.FromCentimeter(5.5));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(4);

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            Paragraph p = cell.AddParagraph("Top Finish 2002 S.p.A.");
            p.Format.Font.Size = 22;
            p.Format.Font.Bold = true;
            p = cell.AddParagraph("Viale Kennedy nr. 103 int F -50038 - Scarperia e San Piero(FI)");
            p.Format.Font.Bold = true;
            p = cell.AddParagraph("Tel. 39 055 843611 - Fax 39 055 8436130");
            p.Format.Font.Bold = true;
            p = cell.AddParagraph("www.topfinish.it - info@topfinish.it");
            p.Format.Font.Bold = true;
            p = cell.AddParagraph("P.IVA e C.F: 04949200481");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 18;
            p = cell.AddParagraph();
            string fileImage = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(fileImage);

            cell = row.Cells[2];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph();
            fileImage = MigraDocFilenameFromByteArray(iBowman);
            p.AddImage(fileImage);

            table.SetEdge(0, 0, 3, 1, Edge.Bottom, BorderStyle.Single, 0.75);

            document.LastSection.Add(table);
            document.LastSection.AddParagraph();

            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(3.9));
            column = table.AddColumn(Unit.FromCentimeter(10.1));
            column.Shading.Color = grigio;

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Strumento:");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(strumentoMisura);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Operatore:");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(operatore);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Commessa:");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(commessa);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Spessore richiesto:");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(spessoreRichiesto);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Applicazione:");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(applicazione);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Data:");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Left;
            cell.VerticalAlignment = VerticalAlignment.Center;
            string str = data.ToLongDateString();
            p = cell.AddParagraph(str);
            document.LastSection.Add(table);

            document.LastSection.AddParagraph();

            // TABELLA AGGREGATI

            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(4));
            column.Shading.Color = grigio;
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Statistiche");
            for (int i = 0; i < 8; i++)
            {
                cell = row.Cells[i + 1];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < etichette.Count)
                {
                    p = cell.AddParagraph(etichette[i]);
                    p.Format.Font.Bold = true;
                }
            }

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int i = 0; i < 9; i++)
            {
                cell = row.Cells[i];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < medie.Count)
                {
                    p = cell.AddParagraph(medie[i]);
                }
            }

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int i = 0; i < 9; i++)
            {
                cell = row.Cells[i];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < Std.Count)
                {
                    p = cell.AddParagraph(Std[i]);
                }
            }

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int i = 0; i < 9; i++)
            {
                cell = row.Cells[i];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < Pct.Count)
                {
                    p = cell.AddParagraph(Pct[i]);
                }
            }

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int i = 0; i < 9; i++)
            {
                cell = row.Cells[i];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < range.Count)
                {
                    p = cell.AddParagraph(range[i]);
                }
            }

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int i = 0; i < 9; i++)
            {
                cell = row.Cells[i];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < minimo.Count)
                {
                    p = cell.AddParagraph(minimo[i]);
                }
            }

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int i = 0; i < 9; i++)
            {
                cell = row.Cells[i];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < massimo.Count)
                {
                    p = cell.AddParagraph(massimo[i]);
                }
            }

            document.LastSection.Add(table);

            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(4));
            column.Shading.Color = grigio;
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Misure totali");
            p.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph(numeroMisure.ToString());

            table.SetEdge(0, 0, 2, 1, Edge.Top, BorderStyle.None, 0);

            document.LastSection.Add(table);

            document.LastSection.AddParagraph();

            // TABELLA MISURE

            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(4));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));
            column = table.AddColumn(Unit.FromCentimeter(1.8));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);
            row.Shading.Color = grigio;

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Data");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            p = cell.AddParagraph("Misura");

            for (int i = 0; i < 8; i++)
            {
                cell = row.Cells[i + 2];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                if (i < etichette.Count)
                {
                    p = cell.AddParagraph(etichette[i]);
                }
            }

            //   for (int riga = 0; riga < 24; riga++)
            for (int riga = 0; riga < misure.Count; riga++)
            {
                row = table.AddRow();
                row.Height = Unit.FromCentimeter(0.5);

                if (riga < misure.Count)
                {
                    List<string> misura = misure[riga];
                    cell = row.Cells[0];
                    cell.Format.Alignment = ParagraphAlignment.Center;
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    p = cell.AddParagraph(data.ToShortDateString());

                    cell = row.Cells[1];
                    cell.Format.Alignment = ParagraphAlignment.Center;
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    p = cell.AddParagraph((riga + 1).ToString());

                    for (int colonna = 0; colonna < 9; colonna++)
                    {
                        if (colonna < misura.Count)
                        {
                            cell = row.Cells[colonna + 2];
                            cell.Format.Alignment = ParagraphAlignment.Center;
                            cell.VerticalAlignment = VerticalAlignment.Center;

                            p = cell.AddParagraph(misura[colonna]);
                        }
                    }

                }
            }

            document.LastSection.Add(table);

        }


        public void CreaReportSpessoriYSL(DateTime data, string numeroCampioni, string parte, string colore, int numeroMisure,
          List<string> etichette, List<string> medie, byte[] iloghi, List<List<string>> misure)
        {
            parte = parte.PadLeft(5, '0');
            int nCampioni = int.Parse(numeroCampioni);
            int nMisurePerPezzo = 0;
            if (nCampioni > 0) nMisurePerPezzo = numeroMisure / nCampioni;

            document.Info.Title = "Spessori";
            document.Info.Subject = String.Empty;
            document.Info.Author = string.Empty;

            Section section = document.AddSection();

            DefineBasicStyles();

            Paragraph p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            string iLogoMP = MigraDocFilenameFromByteArray(iloghi);
            p.AddImage(iLogoMP);
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Center;
            Font fontPiccolo = new Font("Times New Roman");
            fontPiccolo.Size = 8;
            fontPiccolo.Bold = true;

            p.AddFormattedText("Top Finish 2002 S.p.A.", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Viale Kennedy, 103 - 50038 SCARPERIA (FI) - P.Iva 04949200481", fontPiccolo);
            p.AddLineBreak();
            p.AddFormattedText("Tel 055 843611 - Fax 055 8436130 - e-mail: info@topfinish.it", fontPiccolo);

            Font fontNormale = new Font("Times New Roman");
            fontNormale.Size = 12;

            Font fontGrande = new Font("Times New Roman");
            fontGrande.Size = 18;
            fontGrande.Bold = true;
            p = document.LastSection.AddParagraph();
            p.Format.Alignment = ParagraphAlignment.Left;
            p.AddLineBreak();
            p.AddLineBreak();
            string testo = string.Format("Scarperia, {0}", data.ToShortDateString());
            p.AddFormattedText(testo, fontGrande);
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddFormattedText("Verifica spessori", fontGrande);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();

            Table table = new Table();
            table.Borders.Width = 1.5;
            double larghezzaColonna1 = 10.0;
            double larghezzaColonna2 = 7.0;
            Column column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna1));
            column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna2));

            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);

            Cell cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph("Articolo");

            cell = row.Cells[1];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.AddParagraph(parte);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.AddParagraph("Finitura");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;

            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(colore);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.3);
            cell = row.Cells[0];
            cell.Format.Font.Size = 14;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("N°campioni analizzati");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(numeroCampioni.ToString());

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(1.1);
            cell = row.Cells[0];
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph("N° misure per pezzo");
            cell = row.Cells[1];
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 14;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(nMisurePerPezzo.ToString());

            document.LastSection.Add(table);

            p = document.LastSection.AddParagraph();
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddLineBreak();

            // TABELLA MISURE

            table = new Table();
            table.Borders.Width = 0.75;

            column = table.AddColumn(Unit.FromCentimeter(2.0));
            double larghezzaColonna = 2.0;
            if (etichette.Count > 0)
                larghezzaColonna = 15.0 / etichette.Count;
            for (int i = 0; i < etichette.Count; i++)
                column = table.AddColumn(Unit.FromCentimeter(larghezzaColonna));

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            cell = row.Cells[0];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Bold = true;
            cell.Format.Font.Size = 14;
            p = cell.AddParagraph("Misura");

            for (int i = 0; i < etichette.Count; i++)
            {
                cell = row.Cells[i + 1];
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.Format.Font.Bold = true;
                cell.Format.Font.Size = 14;
                p = cell.AddParagraph(etichette[i]);
            }

            int indiceMisura = 1;

            for (int riga = 0; riga < misure.Count; riga++)
            {
                row = table.AddRow();
                row.Height = Unit.FromCentimeter(0.5);

                if (riga < misure.Count)
                {
                    List<string> misura = misure[riga];

                    cell = row.Cells[0];
                    cell.Format.Alignment = ParagraphAlignment.Center;
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    cell.Format.Font.Bold = true;
                    cell.Format.Font.Size = 14;
                    p = cell.AddParagraph((indiceMisura).ToString());

                    indiceMisura++;
                    if (indiceMisura > nMisurePerPezzo) indiceMisura = 1;

                    for (int colonna = 0; colonna < etichette.Count; colonna++)
                    {
                        if (colonna < misura.Count)
                        {
                            cell = row.Cells[colonna + 1];
                            cell.Format.Alignment = ParagraphAlignment.Center;
                            cell.VerticalAlignment = VerticalAlignment.Center;
                            cell.Format.Font.Bold = true;
                            cell.Format.Font.Size = 14;

                            p = cell.AddParagraph(misura[colonna]);
                        }
                    }

                }
            }

            // riga dei valori medii
            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            for (int colonna = 0; colonna < medie.Count; colonna++)
            {
                if (colonna < medie.Count)
                {
                    cell = row.Cells[colonna];
                    cell.Format.Alignment = ParagraphAlignment.Center;
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    cell.Format.Font.Bold = true;
                    cell.Format.Font.Size = 14;
                    p = cell.AddParagraph(medie[colonna]);
                }
            }

            document.LastSection.Add(table);

        }
    }

    public class MisuraDimensionale
    {
        public string Riferimento { get; set; }
        public string Grandezza { get; set; }
        public string Richieste { get; set; }
        public string Tolleranza { get; set; }
        public string Minimo { get; set; }
        public string Massimo { get; set; }
        public string Conforme { get; set; }
        public bool campoTampone { get; set; }
        public string Tampone { get; set; }
    }

    public class MisuraColore
    {
        public string ControlloColore { get; set; }
        public string Richiesto { get; set; }
        public string Tolleranza { get; set; }
        public string Rilevato { get; set; }
        public string Conforme { get; set; }
    }
}
