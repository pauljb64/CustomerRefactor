using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Contracts;
using App.DataServices;
using App.Domain.Entity;

namespace App
{
    public class CustomerService : ICustomerService
    {

        private ICompanyDataService _companyDataService;
        private ICompanyDataProvider _companyDataProvider;
        private ICustomerDataService _customerDataService;
        private ICompanyMapper _companyMapper;
        private IDataBaseConnection _dataBaseConnection;
        private ICustomerCreditService _customerCreditService;
        private ICustomerProvider _customerProvider;
        private ICustomerValidator _customerValidator;
        




        /// <summary>
        /// Constructor some consideration of DI required perhaps 
        /// </summary>
        /// <param name="companyDataService"></param>
        /// <param name="companyDataProvider"></param>
        /// <param name="customerDataService"></param>
        /// <param name="companyMapper"></param>
        /// <param name="dataBaseConnection"></param>
        /// <param name="customerCreditService"></param>
        /// <param name="customerProvider"></param>
        /// <param name="customerValidator"></param>
        public CustomerService(ICompanyDataService companyDataService,
                               ICompanyDataProvider companyDataProvider,
                               ICustomerDataService customerDataService,
                               ICompanyMapper companyMapper,
                               IDataBaseConnection dataBaseConnection,
                               ICustomerCreditService customerCreditService,
            ICustomerProvider customerProvider,
        ICustomerValidator customerValidator)
        {
            _companyDataService = companyDataService;
            _companyDataProvider = companyDataProvider;
            _customerDataService = customerDataService;
            _companyMapper = companyMapper;
            _dataBaseConnection = dataBaseConnection;
            _customerCreditService = customerCreditService;
            _customerProvider = customerProvider;
            _customerValidator= customerValidator;
        }

        /// <summary>
        /// Orchestrate the creation of customer creation
        /// </summary>
        /// <param name="firname"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool AddCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            //need to consider failure handling 
            var customer = _customerProvider.CreateCustomer(firstname, surname,email,dateOfBirth, companyId);

            if (_customerValidator.CheckCustomer(customer))
            {
                _customerDataService.AddCustomer(customer);

                return true;
               
            }
            return false;


        }
    }
}
