using App.Contracts;
using App.App.Domain.Mappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using App.Domain.Entity;

namespace App.test.Mapper.Test
{
    [TestFixture]
    public class CompanyMapperTest
    {
        [Test]
        public void NullDataReaderTest()
        {
            CompanyMapper companyMapper = new CompanyMapper();
            DataSet testDataset = null;
            var result = companyMapper.Map(testDataset);
            Assert.AreEqual(null, result);
        }


        [Test]
        public void SingleRowDataReaderTest()
        {
            ICompanyMapper companyMapper = new CompanyMapper();
            DataTable dataTable = new DataTable("Company");
            dataTable.Columns.Add(new DataColumn("CompanyId", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ClassificationId", typeof(string)));
            DataRow companyRow = dataTable.NewRow();
            companyRow[0] = 1;
            companyRow[1] = "Test One";
            companyRow[2] = "2";
            dataTable.Rows.Add(companyRow);
            var testReader = new DataSet();
            testReader.Tables.Add(dataTable);
            var result = companyMapper.Map(testReader);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Test One", result.Name);
            Assert.AreEqual(Classification.Silver, result.Classification);
        }

    }
}
