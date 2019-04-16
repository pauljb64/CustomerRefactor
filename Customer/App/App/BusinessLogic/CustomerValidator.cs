using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Contracts;
using App.DataServices;


namespace App.BusinessLogic
{



    public class CustomerValidator : ICustomerValidator
    {

        private ICustomerCreditService _customerCreditService;





        public CustomerValidator(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }




        /// <summary>
        /// Validate Custer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool CheckCustomer(Customer customer)
        {

            if (string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname))
            {
                return false;
            }
            //Conside regex perhap
            if (!customer.EmailAddress.Contains("@") && !customer.EmailAddress.Contains("."))
            {
                return false;
            }
            //leap Year processing
            var now = DateTime.Now;
            int age = now.Year - customer.DateOfBirth.Year;
            if (now.Month < customer.DateOfBirth.Month || 
                (now.Month == customer.DateOfBirth.Month && now.Day < customer.DateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
          
            // consider if we needed to change the credit limit - perhaps from configuration or service
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }


            return true;
        }
    }
}
