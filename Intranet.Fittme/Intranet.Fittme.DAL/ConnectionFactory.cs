using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL
{
    public class ConnectionFactory
    {
        public static DbConnection site_fittme()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["fittme"].ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
