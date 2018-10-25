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
                a.ContinueUpdateOnError = false;
                DataTable dt = ds.Tables[tablename];
                DbCommandBuilder cmd = BuildCommandBuilder(a);
                a.UpdateCommand = cmd.GetUpdateCommand();
                a.DeleteCommand = cmd.GetDeleteCommand();
                a.InsertCommand = cmd.GetInsertCommand();
                a.Update(dt);
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
    }
}
