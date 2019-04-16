using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.App.Domain.Mappers
{
    public class CompanyMapper: Contracts.ICompanyMapper
    {
        /// <summary>
        /// Map dataset to Comany 
        /// </summary>
        /// <param name="companyDataset"></param>
        /// <returns></returns>
        public Company Map(DataSet companyDataset)
        {

            //ToDO need to consider how we handle multipe results and just the last is returned
            //Classification enum needs reviewing in case it cannot be parsed
            Company company = null;
            if (companyDataset != null)
            {
                foreach (DataTable tableItem in companyDataset.Tables)
                {
                    foreach (DataRow rowItem in tableItem.Rows)
                        company = new Company
                        {
                            Id = (int)rowItem["CompanyId"],
                            Name = rowItem["Name"].ToString(),
                            //Classification = (Classification)int.Parse(rowItem["ClassificationId"].ToString())
                            Classification = (Classification)int.Parse(rowItem["ClassificationId"].ToString())
                        };
                }
            }
            return company;

        }

    }
}
