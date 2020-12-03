using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDCMetal.Data.Core;

namespace CDCMetal.Data
{

    public class CDCMetalBusinessBase : BusinessBase
    {
        protected static string ConnectionName
        {
            get
            {
                return "CDC";
            }
        }

        protected static string ConnectionString
        {
            get
            {

                ConnectionStringSettings c = ConfigurationManager.ConnectionStrings[ConnectionName];
                return c.ConnectionString;
            }
        }
        protected string _connectionString;
        public CDCMetalBusinessBase()
        {
            _connectionString = ConnectionString;
        }

        protected override IDbConnection OpenConnection(string contextName)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            return con;
        }

        public void Rollback()
        {
            SetAbort();
        }

    }
}
