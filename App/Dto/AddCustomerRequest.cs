using System;

namespace App.Dto
{
    public class AddCustomerRequest
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
    }
}
