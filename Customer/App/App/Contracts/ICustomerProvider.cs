using System;
using App.Domain.Entity;

namespace App.Contracts
{
    public interface ICustomerProvider
    {
        Customer CreateCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId);
    }
}