using App.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataProvider
{
    public class AppDatabaseConnection: IDataBaseConnection
    {
        public IDbConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
