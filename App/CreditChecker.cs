using System;

namespace App
{
    public class CreditChecker : ICustomerCreditService
    {        
        private CustomerCreditServiceClient _creditServiceClient { get; set; }
        public CreditChecker()
        {
            _creditServiceClient = new CustomerCreditServiceClient();
        }

        public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            using (_creditServiceClient)
            {
                return _creditServiceClient.GetCreditLimit(firstname, surname, dateOfBirth);
            }            
        }
    }
}
