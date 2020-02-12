using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
    }
}
