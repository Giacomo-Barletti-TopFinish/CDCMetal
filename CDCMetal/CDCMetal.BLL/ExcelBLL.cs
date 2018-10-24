using CDCMetal.Data;
using CDCMetal.Entities;
using CDCMetal.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.BLL
{
    public class ExcelBLL
    {
        public bool LeggiExcelCDC(CDCDS ds, string filePath, string utente, out string messaggioErrore)
        {
            messaggioErrore = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                ds.CDC_DETTAGLIO.Clear();
                ds.CDC_EXCEL.Clear();

                decimal IDEXCEL;
                using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
                {
                    long ID = bCDCMetal.GetID();
                    IDEXCEL = (decimal)ID;
                }

                CDCDS.CDC_EXCELRow excelRow = ds.CDC_EXCEL.NewCDC_EXCELRow();
                excelRow.IDEXCEL = IDEXCEL;
                excelRow.NOMEFILE = filePath.Length > 200 ? filePath.Substring(0, 200) : filePath;
                excelRow.DATAACQUISIZIONE = DateTime.Today;
                excelRow.UTENTE = utente.Length > 50 ? utente.Substring(0, 50) : utente;

                MemoryStream ms = new MemoryStream();
                byte[] dati = new byte[fs.Length];
                fs.Read(dati, 0, (int)fs.Length);
                excelRow.DATI = dati;

                ExcelHelper excel = new ExcelHelper();
                if(!excel.ReadCDC(fs, ds, IDEXCEL, out messaggioErrore))
                {
                    ds.CDC_DETTAGLIO.Clear();
                    ds.CDC_EXCEL.Clear();
                    return false;
                }

                if (ds.CDC_DETTAGLIO.Count > 0)
                {
                    excelRow.DATARIFERIMENTO = ds.CDC_DETTAGLIO.FirstOrDefault().DATACOLLAUDO;
                    ds.CDC_EXCEL.AddCDC_EXCELRow(excelRow);
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
    }
}
