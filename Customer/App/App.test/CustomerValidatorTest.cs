using App.BusinessLogic;
using App.Contracts;
using App.DataServices;
using App.Domain.Entity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App.test
{
    [TestFixture]
    public class CustomerValidatorTest
    {
        private Mock<ICompanyDataService> _companyDataService;
        private Mock<ICompanyDataProvider> _companyDataProvider;
        private Mock<ICustomerDataService> _customerDataService;
        private Mock<ICompanyMapper> _companyMapper;
        private Mock<ICustomerCreditService> _customerCreditService;

        private ICustomerValidator _customerValidator;

        [SetUpAttribute]
        public void StartUp()
        {
            _companyMapper = new Mock<ICompanyMapper>();
            _companyDataProvider = new Mock<ICompanyDataProvider>();
            _companyDataService = new Mock<ICompanyDataService>();
            _customerCreditService = new Mock<ICustomerCreditService>();
            _customerDataService = new Mock<ICustomerDataService>();
           
            _customerValidator = new CustomerValidator(_customerCreditService.Object);

        }
        /// <summary>
        /// ToDo tidy up repeated code and make it a bit easier to deal with
        /// </summary>
        [Test]
        public void NoFirstNameTest()
        {
            Customer customer = new Customer { Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1000, DateOfBirth = new DateTime(1985, 07, 01), EmailAddress = "test@Test.com",
                Firstname = String.Empty, HasCreditLimit = true, Id = 1, Surname = "test1" };
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);


            var result = _customerValidator.CheckCustomer(customer);


            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            Assert.IsFalse(result);
        }

        [Test]
        public void NoSurnameTest()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1000,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test@Test.com",
                Firstname = "Test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = String.Empty
            };

            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);


            var result = _customerValidator.CheckCustomer(customer);


            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            Assert.IsFalse(result);
        }

        [Test]
        public void EmailContainsNoAmpersandTest()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1000,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test£Test£com",
                Firstname = "Test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = "Test1"
            };
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);


            var result = _customerValidator.CheckCustomer(customer);


            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            Assert.IsFalse(result);
        }

        [Test]
        public void EmailContainsNoDotTest()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1000,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test£Test£com",
                Firstname = "Test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = "Test1"
            };
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);


            var result = _customerValidator.CheckCustomer(customer);


            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            Assert.IsFalse(result);

        }

        [Test]
        public void AgeLess21Test()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1000,
                DateOfBirth = DateTime.Now,
                EmailAddress = "test@Test.com",
                Firstname = "Test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = "Test1"
            };
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);


            var result = _customerValidator.CheckCustomer(customer);


            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            Assert.IsFalse(result);

        }

        [Test]
        public void CustomerValidationTest()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1000,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test@Test.com",
                Firstname = "Test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = "Test1"
            };
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(0);


            var result = _customerValidator.CheckCustomer(customer);


            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            Assert.IsTrue(result);

        }

    }
}
