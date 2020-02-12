using App;
using App.Dto;
using App.Interfaces;
using Moq;
using System;
using Xunit;

namespace Test.UnitTests
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ICustomerCreditService> _mockCreditChecker;
        private readonly ICustomerService _sut;
        public CustomerServiceTest()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockCreditChecker = new Mock<ICustomerCreditService>();
            _sut = new CustomerService(_mockCompanyRepository.Object, _mockCustomerRepository.Object, _mockCreditChecker.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddCustomer_WhenFirstNameIsNullOrEmpty_ThrowsError(string firstName)
        {
            //arrange
            var newCustomer = new AddCustomerRequest { FirstName = firstName, Surname = "surname", CompanyId = 1, DateOfBirth = new DateTime(1989, 4, 10) };
            
            //act + assert
            Assert.Throws<ArgumentException>(() => _sut.AddCustomer(newCustomer));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddCustomer_WhenSurnameIsNullOrEmpty_ThrowsError(string surname)
        {
            //arrange
            var newCustomer = new AddCustomerRequest { FirstName = "firstname", Surname = surname , CompanyId = 1, DateOfBirth = new DateTime(1989, 4, 10) };
            
            //act + assert
            Assert.Throws<ArgumentException>(() => _sut.AddCustomer(newCustomer));
        }       

        [Theory]       
        [InlineData("test")]
        [InlineData("test@12com")]
        [InlineData("test12.com")]
        public void AddCustomer_WhenEmailIsNullOrEmptyOrInvalid_ThrowsError(string email)
        {
            //arrange
            var newCustomer = new AddCustomerRequest { FirstName = "firstname", Surname = "surname", Email = email, CompanyId = 1 , DateOfBirth = new DateTime(1989, 4, 10) };
            
            //act + assert
            Assert.Throws<ArgumentException>(() => _sut.AddCustomer(newCustomer));
        }

        [Fact]
        public void AddCustomer_HasAdded_ReturnsTrue()
        {            
            //arrange
            var newCustomer = new AddCustomerRequest { FirstName = "firstname", Surname = "surname", Email = "email@123.com", CompanyId = 1, DateOfBirth = new DateTime(1989, 4, 10) };

            _mockCompanyRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company { Name = "Company1", Classification = Classification.Bronze, Id = 1 });

            //act
            var result = _sut.AddCustomer(newCustomer);

            //assert
            Assert.True(result);
        }
    }
}
