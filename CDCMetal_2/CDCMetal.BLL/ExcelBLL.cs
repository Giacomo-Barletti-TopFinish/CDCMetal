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
    public class ExcelBLL
    {
        public bool LeggiExcelCDC(CDCDS ds, string filePath, string utente,  out string messaggioErrore) //string brand,
        {
            messaggioErrore = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                ds.CDC_DETTAGLIO_rel.Clear();
                ds.CDC_EXCEL.Clear();

                decimal IDEXCEL=0;
                //using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness()) //JACOPO
                //{
                //    //////long ID = bCDCMetal.GetID(); //JACOPO
                //    long ID = bCDCMetal.GetSQLID(); //JACOPO
                //    IDEXCEL = (decimal)ID; //JACOPO
                //}
                
                CDCDS.CDC_EXCELRow excelRow = ds.CDC_EXCEL.NewCDC_EXCELRow();
                excelRow.IDEXCEL = IDEXCEL;
                excelRow.NOMEFILE = filePath.Length > 200 ? filePath.Substring(0, 200) : filePath;
                excelRow.DATAACQUISIZIONE = DateTime.Today;
                excelRow.UTENTE = utente.Length > 50 ? utente.Substring(0, 50) : utente;
                //excelRow.AZIENDA = brand; //?????????????????

                MemoryStream ms = new MemoryStream();
                byte[] dati = new byte[fs.Length];
                fs.Read(dati, 0, (int)fs.Length);
                excelRow.DATI = dati;


                excelRow.DATARIFERIMENTO = DateTime.Today; //JACOPO -- poi la cambio con data Collaudo
                ds.CDC_EXCEL.AddCDC_EXCELRow(excelRow); //aggiunta JACOPO


                ExcelHelper excel = new ExcelHelper();
                if (!excel.ReadCDC(fs, ds, IDEXCEL, utente, out messaggioErrore))
                {
                    ds.CDC_DETTAGLIO_rel.Clear();
                    ds.CDC_EXCEL.Clear();
                    return false;
                }

                if (ds.CDC_DETTAGLIO_rel.Count > 0)
                {
                    string data = ds.CDC_DETTAGLIO_rel.FirstOrDefault().DATACOLLAUDO;

                 //agganciare riga di EXCEL già presente //JaCOPO

                    DateTime dt = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    excelRow.DATARIFERIMENTO = dt;
                    //ds.CDC_EXCEL.AddCDC_EXCELRow(excelRow); // tolto JACOPO
                    return true;
                }
                else
                {
                    messaggioErrore = "Il file excel risulta essere vuoto";
                    return false;
                }

                fs.Close();
            }
            return false;
        }

        public List<decimal> VerificaExcelCaricato(CDCDS ds)
        {
            List<decimal> IDPRENOTAZIONE = ds.CDC_DETTAGLIO_rel.Select(x => x.IDPRENOTAZIONE).Distinct().ToList();

            CDCDS dsDB = new CDCDS();
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC_DETTAGLIO(IDPRENOTAZIONE, dsDB);
            }

            List<decimal> IDPRENOTAZIONEDUPLICATI = dsDB.CDC_DETTAGLIO.Select(x => x.IDPRENOTAZIONE).Distinct().ToList();

            foreach (CDCDS.CDC_DETTAGLIO_relRow riga in ds.CDC_DETTAGLIO_rel)
            {
                if (riga.IsENTENull() || string.IsNullOrEmpty(riga.ENTE))
                    riga.ENTE = "-";
            }

            return IDPRENOTAZIONEDUPLICATI;
        }

        public decimal[]   VerificaExcelCaricatoBrand(CDCDS ds)
        {
         
            decimal[] IDPRENOTAZIONESENZABRAND = new decimal[] { }; //= ds.CDC_DETTAGLIO.Where(x => x.IDBRAND == NULL ).Select(x => x.IDPRENOTAZIONE).Distinct().ToList();
            int index = 0;

            foreach (CDCDS.CDC_DETTAGLIO_relRow riga in ds.CDC_DETTAGLIO_rel)
            {
                if (riga.IsIDBRANDNull())
                {
                    Array.Resize(ref IDPRENOTAZIONESENZABRAND, index + 1);
                    IDPRENOTAZIONESENZABRAND.SetValue(riga.IDPRENOTAZIONE, index);
                    index = index + 1;
                }
            }

            return IDPRENOTAZIONESENZABRAND;
        }


        public void Salva(CDCDS ds)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.UpdateCDC_EXCEL(ds);


           

                //long lastid = bCDCMetal.GetLastIdentity();

                //foreach (System.Data.DataRow dr in ds.CDC_DETTAGLIO.Rows)
                //{
                //    dr["IDEXCEL"] = lastid;
                //}

                bCDCMetal.UpdateCDC_DETTAGLIO_rel(ds);
            }

        }

        public bool LeggiExcelAnalisiPiombo(CDCDS ds, string filePath, string utente, out string messaggioErrore)
        {
            messaggioErrore = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                ds.CDC_CERTIFICATIPIOMBO.Clear();


                ExcelHelper excel = new ExcelHelper();
                if (!excel.ReadAnalisiPiombo(fs, ds, utente, out messaggioErrore))
                {
                    ds.CDC_CERTIFICATIPIOMBO.Clear();
                    return false;
                }

                if (ds.CDC_CERTIFICATIPIOMBO.Count == 0)
                {
                    messaggioErrore = "Il file excel risulta essere vuoto";
                    return false;
                }

                fs.Close();
            }
            return true;
        }
    }
}
