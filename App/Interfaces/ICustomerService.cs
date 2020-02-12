using App.Dto;
using System;

namespace App.Interfaces
{
    public interface ICustomerService
    {
        bool AddCustomer(AddCustomerRequest newCustomer);
        bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId);
    }
}
