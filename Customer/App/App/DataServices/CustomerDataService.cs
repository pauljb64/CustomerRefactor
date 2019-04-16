using App.Contracts;
using App.DataProvider;
using App.Domain.Entity;

namespace App.DataServices
{
    //Used to wrap the static class mainly to assist testing
    public class CustomerDataService: ICustomerDataService
    {
        public void AddCustomer(Customer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}
