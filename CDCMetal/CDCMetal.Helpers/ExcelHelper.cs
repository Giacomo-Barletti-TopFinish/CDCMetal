using CDCMetal.Entities;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Helpers
{
    public class ExcelHelper
    {
        public bool ReadCDC(Stream stream, CDCDS ds, decimal IDEXCEL, out string messaggioErrore)
        {
            messaggioErrore = string.Empty;
            SpreadsheetDocument document = SpreadsheetDocument.Open(stream, true);
            SharedStringTable sharedStringTable = document.WorkbookPart.SharedStringTablePart.SharedStringTable;

            WorkbookPart wbPart = document.WorkbookPart;

            WorksheetPart worksheetPart = wbPart.WorksheetParts.First();
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            CellFormats cellFormats = wbPart.WorkbookStylesPart.Stylesheet.CellFormats;
            NumberingFormats numberingFormats = wbPart.WorkbookStylesPart.Stylesheet.NumberingFormats;

            int rowCount = sheetData.Elements<Row>().Count();

            bool intestazione = true;
            foreach (Row r in sheetData.Elements<Row>())
            {
                if(intestazione)
                {
                    intestazione = false;
                    continue;
                }
                bool esito = true;
                CDCDS.CDC_DETTAGLIORow dettaglio = ds.CDC_DETTAGLIO.NewCDC_DETTAGLIORow();
                dettaglio.IDEXCEL = IDEXCEL;
                foreach (Cell cell in r.Elements<Cell>())
                {
                    string cella = EstraiValoreCella(cell, sharedStringTable, cellFormats, numberingFormats);
                    cella = cella.Trim();
                    string colonna = GetColumnReference(cell);
                    switch (colonna)
                    {
                        case "A": //idprenotazione
                            esito = EstraiValoreCellaDecimal(cella, "IDPRENOTAZIONE", dettaglio, out messaggioErrore);
                            break;
                        case "B": // id_verbale
                            {
                                int lunghezza = 10;
                                dettaglio.IDVERBALE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "C": // data_collaudo
                            if (!VerificaData(cella))
                            {
                                messaggioErrore = "La colonna DATA COLLAUDO non è una data nel formato yyyy/mm/dd";
                                esito = false;
                            }
                            else
                            {
                                int lunghezza = 10;
                                dettaglio.DATACOLLAUDO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "D": // accessorista
                            {
                                int lunghezza = 30;
                                dettaglio.ACCESSORISTA = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "E": // prefisso
                            {
                                int lunghezza = 7;
                                dettaglio.PREFISSO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "F": // parte
                            {
                                int lunghezza = 6;
                                dettaglio.PARTE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "G": // colore
                            {
                                int lunghezza = 5;
                                dettaglio.COLORE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "H": // misura
                            {
                                esito = EstraiValoreCellaDecimal(cella, "MISURA", dettaglio, out messaggioErrore);
                            }
                            break;
                        case "I": // livellodiff
                            if(!string.IsNullOrEmpty(cella))                                
                                esito = EstraiValoreCellaDecimal(cella, "LIVELLODIFF", dettaglio, out messaggioErrore);
                            break;
                        case "J": // commessaordine
                            {
                                int lunghezza = 18;
                                dettaglio.COMMESSAORDINE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "K": // ente
                            {
                                int lunghezza = 5;
                                dettaglio.ENTE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "L": // um
                            {
                                int lunghezza = 2;
                                dettaglio.UM = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "M": // qta
                            esito = EstraiValoreCellaDecimal(cella, "QUANTITA", dettaglio, out messaggioErrore);
                            break;
                        case "N": // assegn
                            {
                                int lunghezza = 2;
                                dettaglio.ASSEGN = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "O": // auto
                            {
                                int lunghezza = 5;
                                dettaglio.AUTO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "P": // coll_to
                            {
                                int lunghezza = 5;
                                dettaglio.COLLTO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "Q": // qtavalidita
                            {
                                int lunghezza = 6;
                                dettaglio.QUANTITAVALIDITA = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "R": // lotto_bloccato
                            {
                                int lunghezza = 2;
                                dettaglio.LOTTOBLOCCATO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "S": // verbalebloccato
                            {
                                int lunghezza = 2;
                                dettaglio.VERBALEBLOCCATO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "T": // deroga
                            {
                                int lunghezza = 2;
                                dettaglio.DEROGA = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "U": // controllo dimensionale
                            {
                                int lunghezza = 2;
                                dettaglio.CONTROLLODIMENSIONALE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "V": // controllo estetico
                            {
                                int lunghezza = 2;
                                dettaglio.CONTROLLOESTETICO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "W": // controllo funzionale
                            {
                                int lunghezza = 2;
                                dettaglio.CONTROLLOFUNZIONALE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "X": // esitotest chimico
                            {
                                int lunghezza = 2;
                                dettaglio.ESITOTESTCHIMICO= cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "Y": // test fisico
                            {
                                int lunghezza = 2;
                                dettaglio.TESTFISICO= cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "Z": // notecollaudo
                            {
                                int lunghezza = 100;
                                dettaglio.NOTECOLLAUDO = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;
                        case "AA": // assegnazione
                            {
                                int lunghezza = 500;
                                dettaglio.ASSEGNAZIONE = cella.Length > lunghezza ? cella.Substring(0, lunghezza) : cella;
                            }
                            break;

                    }
                    if (!esito)
                        return false;
                }
                ds.CDC_DETTAGLIO.AddCDC_DETTAGLIORow(dettaglio);
            }

            return true;
        }

        private bool EstraiValoreCellaDecimal(string cella, string colonna, CDCDS.CDC_DETTAGLIORow dettaglio, out string messaggioErrore)
        {
            messaggioErrore = string.Empty;
            decimal aux;
            if (!decimal.TryParse(cella, out aux))
            {
                messaggioErrore = string.Format("Errore lettura colonna ID {0} il valore non è un numero", colonna);
                return false;
            }
            else
            {
                dettaglio[colonna] = aux;

            }
            return true;
        }

        private bool VerificaData(string cella)
        {
            string[] str = cella.Split('/');
            if (str.Length == 3)
            {
                int anno;
                if (int.TryParse(str[2], out anno))
                {
                    if (anno < 2017 || anno > 2025)
                        return false;
                }
                else
                    return false;
                int mese;
                if (int.TryParse(str[1], out mese))
                {
                    if (mese < 1 || mese > 12)
                        return false;
                }
                else
                    return false;
                int giorno;
                if (int.TryParse(str[0], out giorno))
                {
                    if (giorno < 1 || giorno > 31)
                        return false;
                }
                else
                    return false;
            }
            else return false;

            return true; ;
        }
        private string GetColumnReference(Cell cell)
        {
            List<char> numeri = new List<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string reference = cell.CellReference;
            string colonna = string.Empty;
            foreach (char ch in reference.ToCharArray())
            {
                if (!numeri.Contains(ch))
                {
                    colonna = colonna + ch;
                }
            }
            return colonna;
        }

        private string EstraiValoreCella(Cell cell, SharedStringTable sharedStringTable, CellFormats cellFormats, NumberingFormats numberingFormats)
        {
            CellValue cellValue = cell.CellValue;
            if (cellValue != null)
            {
                if (cell.DataType != null)
                {
                    switch (cell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            return sharedStringTable.ElementAt(Int32.Parse(cell.InnerText)).InnerText;

                        case CellValues.Date:
                            double oaDateAsDouble;
                            if (double.TryParse(cell.InnerText, out oaDateAsDouble)) //this line is Culture dependent!
                            {
                                DateTime dateTime = DateTime.FromOADate(oaDateAsDouble);
                                return dateTime.ToShortDateString();
                            }
                            return string.Empty;


                        default:
                            return cell.InnerText;
                    }
                }
                else
                {
                    if (cell.StyleIndex != null)
                    {
                        var cellFormat = (CellFormat)cellFormats.ElementAt((int)cell.StyleIndex.Value);
                        if (cellFormat.NumberFormatId != null)
                        {
                            var numberFormatId = cellFormat.NumberFormatId.Value;
                            var numberingFormat = numberingFormats.Cast<NumberingFormat>()
                                .SingleOrDefault(f => f.NumberFormatId.Value == numberFormatId);

                            // Here's yer string! Example: $#,##0.00_);[Red]($#,##0.00)
                            if (numberingFormat != null && numberingFormat.FormatCode.Value.Contains("yyyy-mm-dd"))
                            {
                                string formatString = numberingFormat.FormatCode.Value;
                                double oaDateAsDouble;
                                if (double.TryParse(cell.InnerText, out oaDateAsDouble)) //this line is Culture dependent!
                                {
                                    DateTime dateTime = DateTime.FromOADate(oaDateAsDouble);
                                    return dateTime.ToShortDateString();
                                }
                                else
                                    return string.Empty;
                            }
                            else
                                return cell.InnerText;
                        }
                    }
                    else
                        return cell.InnerText;
                }

            }

            return string.Empty;

        }
    }
}
