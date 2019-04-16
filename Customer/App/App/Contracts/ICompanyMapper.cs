using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using App.Domain.Entity;

namespace App.Contracts
{
    public interface ICompanyMapper
    {
        //Company Map(SqlDataReader dataReader);
        Company Map(DataSet companyDataset);
    }
}
