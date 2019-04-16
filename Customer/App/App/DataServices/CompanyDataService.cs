using App.Contracts;
using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataServices
{
    public class CompanyDataService : ICompanyDataService
    {

   
        private ICompanyDataProvider _companyDataProvider;
        private ICompanyMapper _companyMapper;



        public CompanyDataService(ICompanyMapper companyMapper, ICompanyDataProvider companyDataProvider )
        {
            _companyDataProvider = companyDataProvider;
            _companyMapper = companyMapper;
        }

        public Company GetById(int id)
        {
            return _companyMapper.Map(_companyDataProvider.GetCompanyById(id));
        }
    }
}
