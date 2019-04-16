using App.Domain.Entity;

namespace App.Contracts
{
    public interface ICustomerDataService
    {
        void AddCustomer(Customer customer);
    }
}