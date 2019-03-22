using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Data
{
    public class CDCMetalAdapter : CDCMetalAdapterBase
    {
        public CDCMetalAdapter(System.Data.IDbConnection connection, IDbTransaction transaction) :
            base(connection, transaction)
        { }

        public void LeggiUtente(CDCDS ds, string User)
        {
            string select = @"SELECT * FROM GRUPPO.USR_USER WHERE UIDUSER= $P<UTENTE>";
            ParamSet ps = new ParamSet();

            ps.AddParam("UTENTE", DbType.String, User);
            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.USR_USER);
            }
        }

        public void FillCDC_DETTAGLIO(List<decimal> IDPRENOTAZIONE, CDCDS ds)
        {
            string selezione = ConvertToStringForInCondition(IDPRENOTAZIONE);
            string select = @"SELECT * FROM CDC_DETTAGLIO WHERE IDPRENOTAZIONE IN ({0})";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO);
            }
        }

        public void UpdateTable(string tablename, CDCDS ds)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM {0}", tablename);

            using (DbDataAdapter a = BuildDataAdapter(query))
            {
                try
                {
                    a.ContinueUpdateOnError = false;
                    DataTable dt = ds.Tables[tablename];
                    DbCommandBuilder cmd = BuildCommandBuilder(a);
                    a.UpdateCommand = cmd.GetUpdateCommand();
                    a.DeleteCommand = cmd.GetDeleteCommand();
                    a.InsertCommand = cmd.GetInsertCommand();
                    a.Update(dt);
                }
                catch (DBConcurrencyException ex)
                {

                }
                catch
                {
                    throw;
                }
            }
        }

        public List<DateTime> LeggiDateRiferimento()
        {
            List<DateTime> date = new List<DateTime>();
            string select = @"SELECT DISTINCT DATARIFERIMENTO FROM CDC_EXCEL ORDER BY DATARIFERIMENTO DESC";

            using (DbCommand cmd = BuildCommand(select))
            {
                DbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    DateTime dt = (DateTime)dataReader.GetValue(0);
                    date.Add(dt);
                }
                dataReader.Close();
            }

            return date;
        }

        public void FillCDC_DETTAGLIO(CDCDS ds, List<decimal> IDEXCEL)
        {
            string selezione = ConvertToStringForInCondition(IDEXCEL);
            string select = @"SELECT * FROM CDC_DETTAGLIO WHERE IDEXCEL IN ({0}) ORDER BY PREFISSO, PARTE, COLORE, MISURA,COMMESSAORDINE";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO);
            }
        }

        public void FillCDC_EXCEL(CDCDS ds, DateTime DATARIFERIMENTO)
        {
            string select = @"SELECT * FROM CDC_EXCEL WHERE DATARIFERIMENTO = $P<DATARIFERIMENTO>";

            ParamSet ps = new ParamSet();
            ps.AddParam("DATARIFERIMENTO", DbType.DateTime, DATARIFERIMENTO);

            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.CDC_EXCEL);
            }
        }

        public void FillCDC_CONFORMITA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_CONFORMITA WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_CONFORMITA);
            }
        }

        public void FillCDC_DETTAGLIO_CON_DESCRIZIONE(CDCDS ds, List<decimal> IDEXCEL)
        {
            string selezione = ConvertToStringForInCondition(IDEXCEL);
            string select = @"SELECT cdc.*, nvl(( select NVL(desmagazz,'') from gruppo.magazz ma where rownum = 1 and  ma.modello = ma.modellobase and ma.modello like cdc.prefisso||'-'||cdc.parte||'-'||cdc.colore||'%'),' ') as descrizione
                                FROM CDC_DETTAGLIO cdc
                                WHERE IDEXCEL IN ({0}) ORDER BY PREFISSO, PARTE, COLORE, MISURA,COMMESSAORDINE";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO1);
            }
        }

        public void CDC_PDF(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_PDF WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_PDF);
            }
        }

        public void FillCDC_CONFORMITA_DETTAGLIO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DET.* FROM CDC_CONFORMITA_DETTAGLIO DET
                                INNER JOIN CDC_DETTAGLIO CD ON CD.PREFISSO = DET.PREFISSO AND CD.PARTE = DET.PARTE AND CD.COLORE = DET.COLORE 
                                WHERE CD.IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_CONFORMITA_DETTAGLIO);
            }
        }

        public void FillCDC_ETICHETTE_DETTAGLIO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DET.* FROM CDC_ETICHETTE_DETTAGLIO DET
                                INNER JOIN CDC_DETTAGLIO CD ON CD.PREFISSO = DET.PREFISSO AND CD.PARTE = DET.PARTE AND CD.COLORE = DET.COLORE 
                                WHERE CD.IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_ETICHETTE_DETTAGLIO);
            }
        }

        public void FillCDC_DIMEMSIONI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_DIMEMSIONI WHERE IDDETTAGLIO IN ({0}) ORDER BY RIFERIMENTO";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DIMEMSIONI);
            }
        }

        public void FillCDC_DIMEMSIONI_MISURE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DISTINCT DET.* FROM CDC_DIMEMSIONI_MISURE DET
                                INNER JOIN CDC_DETTAGLIO CD ON CD.PARTE = DET.PARTE 
                                WHERE CD.IDDETTAGLIO IN ({0}) ORDER BY DET.RIFERIMENTO";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DIMEMSIONI_MISURE);
            }
        }

        public void FillCDC_ANTIALLERGICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_ANTIALLERGICO WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_ANTIALLERGICO);
            }
        }

        public void FillCDC_COLORE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_COLORE WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_COLORE);
            }
        }

        public void FillCDC_GALVANICA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_GALVANICA WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_GALVANICA);
            }
        }

        public void FillCDC_MISURE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DISTINCT DET.* FROM CDC_MISURE DET
                                INNER JOIN CDC_GALVANICA CD ON CD.IDGALVANICA = DET.IDGALVANICA 
                                WHERE CD.IDDETTAGLIO IN ({0}) ORDER BY DET.IDGALVANICA,DET.NMISURA,DET.NCOLONNA";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_MISURE);
            }
        }

        public void FillCDC_SPESSORE(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_SPESSORE";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_SPESSORE);
            }
        }


        public void FillCDC_APPLICAZIONE(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_APPLICAZIONE";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_APPLICAZIONE);
            }
        }
    }
}
