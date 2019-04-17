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
        public int CalculateCustomerCreditLimit(Company CustomerCompany)
        {
            throw new NotImplementedException();
        }
    }
}
