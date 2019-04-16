using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Contracts;
using App.DataServices;
using App.Domain.Entity;

namespace App.BusinessLogic
{
    public class CustomerProvider : ICustomerProvider
    {


        private ICompanyDataService _companyDataService;
        private ICustomerCreditService _customerCreditService;

        private const string VeryImportantCompantName = "VeryImportantClient";
        private const string ImportantCompantName = "ImportantClient";


        public CustomerProvider(ICompanyDataService companyDataService, ICustomerCreditService customerCreditService)
        {
            _companyDataService = companyDataService;
            _customerCreditService = customerCreditService;


        }


        /// <summary>
        /// Create Customer POCO using company data service and credit dservice
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Customer CreateCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            //need to consider failure handling 
           //ToDo refactor company creation SRP 
            var company = _companyDataService.GetById(companyId);


            var customer = new Customer
            {
                Company = company,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstname,
                Surname = surname
            };

            if (customer.Company.Name == VeryImportantCompantName)
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (customer.Company.Name == ImportantCompantName)
            {
                ////ToDo refactor SRP - credit limit logic should be separate - Look at reducing coupling
                // Do credit check and double credit limit
                customer.HasCreditLimit = true;
                using (_customerCreditService)
                {
                    var creditLimit = _customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    customer.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Do credit check
                customer.HasCreditLimit = true;
                using (_customerCreditService)
                {
                    var creditLimit = _customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    customer.CreditLimit = creditLimit;
                }
            }

            return customer;
        }
    }
}
