using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

       public void LeggiUtente(CDCDS ds , string User)
        {
            string select = @"SELECT * FROM GRUPPO.USR_USER WHERE UIDUSER= $P<UTENTE>";
            ParamSet ps = new ParamSet();

            ps.AddParam("UTENTE", DbType.String, User);
            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.USR_USER);
            }
        }


    }
}
