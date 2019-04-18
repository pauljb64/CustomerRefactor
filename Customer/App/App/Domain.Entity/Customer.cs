using System;

namespace App.Domain.Entity
{
    public class Customer
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Company Company { get; set; }

        public Customer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            DateOfBirth = dateOfBirth;
            EmailAddress = email;
            Firstname = firstname;
            Surname = surname;
            CreditLimit = 0;
            Company = null;
            HasCreditLimit = false;

        }
        public Customer()
        {
        }

        //These are changes


    }
}