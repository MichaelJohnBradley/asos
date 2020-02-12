using App.Dto;
using App.Interfaces;
using App.Repositories;
using System;

namespace App
{
    public class CustomerService : ICustomerService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerCreditService _creditChecker;

        public CustomerService(ICompanyRepository companyRepository, ICustomerRepository customerRepository, ICustomerCreditService creditChecker)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _creditChecker = creditChecker ?? throw new ArgumentNullException(nameof(creditChecker));
        }
        public CustomerService()
        {
            //provide a concrete type for the backwards compatiblity harness
            _companyRepository = new CompanyRepository();
            _customerRepository = new CustomerRepository();
            _creditChecker = new CreditChecker();
        }
        public bool AddCustomer(AddCustomerRequest newCustomer)
        {
            Guard.IsNullOrEmpty(newCustomer?.FirstName);
            Guard.IsNullOrEmpty(newCustomer?.Surname);            
            Guard.IsEmailValid(newCustomer?.Email);
           
            if (!IsValidAge(newCustomer.DateOfBirth, 21))
                return false;
            
            var company = _companyRepository.GetById(newCustomer.CompanyId);
            
            var customer = new Customer
            {
                Company = company,
                DateOfBirth = newCustomer.DateOfBirth,
                EmailAddress = newCustomer.Email,
                Firstname = newCustomer.FirstName,
                Surname = newCustomer.Surname
            };

            customer.HasCreditLimit = CompanyHasCreditLimit(company.Name);

            var creditLimit = DoCreditCheck(company, customer);
            customer.CreditLimit = creditLimit;

            var overLimit = IsOverLimit(customer, 500);

            if (overLimit) return false;

            _customerRepository.AddCustomer(customer);

            return true;
        }

        //i might have refactored this method into a age validator class 
        // and then i would have added unit test as method would not be private
        private bool IsValidAge(DateTime dateOfBirth, int ageLimit)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < ageLimit)
            {
                return false;
            }
            return true;
        }

        //i would have moved this method too either into another class or as part of the customer class
        private bool IsOverLimit(Customer customer, decimal limit)
        {
            if (customer.HasCreditLimit && customer.CreditLimit < limit)
            {
                return false;
            }
            return true;
        }

        //with some more time i could
        //refactor company to have sub classes eg VeryImportantClient : Company and ImportantClient: Company 
        //can then select the company type and use the override methods
        //would then be able to have the credit limit multiplier to be moved there instead of the method below        
        

        private int DoCreditCheck(Company company, Customer customer)
        {                      
            var creditLimit = _creditChecker.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);

            CalculateCreditLimit(creditLimit, company);
            
            return creditLimit;
        }

        private bool CompanyHasCreditLimit(string companyName)
        {
            if (companyName == "VeryImportantClient")
            {
                return false;
            }
            return true;
        }

        private int CalculateCreditLimit(int creditLimit, Company company)
        {
            if(company.Name == "ImportantClient")
            {
                return creditLimit * 2;
            }
            return creditLimit;
        }

        [Obsolete]
        public bool AddCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            try
            {
                Guard.IsNullOrEmpty(firstname);
                Guard.IsNullOrEmpty(surname);
                Guard.IsEmailValid(email);

                var newCustomer = new AddCustomerRequest
                {
                    FirstName = firstname,
                    Surname = surname,
                    Email = email,
                    DateOfBirth = dateOfBirth,
                    CompanyId = companyId
                };
                return AddCustomer(newCustomer);
            }
            catch
            {
                return false;
            }            
        }
    }
}
