using App.Interfaces;

namespace App.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {        
        public CustomerRepository()
        {
            
        }

        public void AddCustomer(Customer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}
