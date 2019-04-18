using App.Contracts;
using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic
{
    public class CreditScore : ICreditScore
    {
        private DataServices.ICustomerCreditService _customerCreditService;

        public CreditScore(DataServices.ICustomerCreditService CustomerCreditService)
        {
            _customerCreditService = CustomerCreditService;

        }
        public int CalculateCustomerCreditLimit(Customer applicant)
        {
            int creditLimit = 0;
            using (_customerCreditService)
            {
                creditLimit = _customerCreditService.GetCreditLimit(applicant.Firstname, applicant.Surname, applicant.DateOfBirth);
                creditLimit = creditLimit * 2;

            }
            return creditLimit;
        }
    }
}
