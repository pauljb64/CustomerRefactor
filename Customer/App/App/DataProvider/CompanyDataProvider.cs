using App.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataProvider
{
    public class CompanyDataProvider: ICompanyDataProvider
    {
        private IDbConnection _DbConnection;

        public CompanyDataProvider(IDbConnection connection)
        {
                    _DbConnection = connection;
        }

        /// <summary>
        /// Get Company Details
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public DataSet GetCompanyById(int identity)
        {
            DataSet companyDataSet = null;
            using (_DbConnection)
            {
                var command = new System.Data.SqlClient.SqlCommand
                {
                    Connection = (SqlConnection) _DbConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetCompanyById"
                };

                var parameter = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = identity };
                command.Parameters.Add(parameter);

                _DbConnection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(companyDataSet);
               
            }
            return companyDataSet;
        }
    }
}
