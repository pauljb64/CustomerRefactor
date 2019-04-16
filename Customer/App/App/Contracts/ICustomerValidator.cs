using App.Domain.Entity;

namespace App.Contracts
{
    public interface ICustomerValidator
    {
        bool CheckCustomer(Customer customer);
    }
}