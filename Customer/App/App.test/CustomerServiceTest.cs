using App.Contracts;
using App.DataServices;
using App.Domain.Entity;
using App.BusinessLogic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.test
{
    [TestFixture ]
    public class CustomerServiceTest
    {
        private Mock<ICompanyDataService> _companyDataService;
        private Mock<ICompanyDataProvider> _companyDataProvider;
        private Mock<ICustomerDataService> _customerDataService;
        private Mock<ICompanyMapper> _companyMapper;
        private Mock<IDataBaseConnection> _dataBaseConnection;
        private Mock<ICustomerCreditService> _customerCreditService;
        private ICustomerService _customerService;
         private Mock<ICustomerValidator> _customerValidator;
        private Mock<ICustomerProvider> _CustomerProvider;

        [SetUpAttribute]
        public void StartUp()
        {
            _companyMapper = new Mock<ICompanyMapper>();
            _dataBaseConnection = new Mock<IDataBaseConnection>();
            _companyDataProvider = new Mock<ICompanyDataProvider>();
            _companyDataService = new Mock<ICompanyDataService>();
            _customerCreditService = new Mock<ICustomerCreditService>();
            _customerDataService = new Mock<ICustomerDataService>();
            _customerValidator = new Mock<ICustomerValidator>();
            _CustomerProvider = new Mock<ICustomerProvider>();
            _customerService = new CustomerService(_companyDataService.Object, _companyDataProvider.Object, _customerDataService.Object, _companyMapper.Object, _dataBaseConnection.Object, _customerCreditService.Object,_CustomerProvider.Object, _customerValidator.Object);
           

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
            _CustomerProvider.Setup(a => a.CreateCustomer(String.Empty, "Test1", "test@test.com",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(false);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer(String.Empty, "Test1", "test@test.com", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer(String.Empty, "Test1", "test@test.com",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer), Times.Never);

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
                Firstname = "test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = "String.Empty"
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("Test1", String.Empty, "test@test.com",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(false);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("Test1", String.Empty, "test@test.com", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("Test1", String.Empty, "test@test.com",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer), Times.Never);

            Assert.IsFalse(result);
        }

        [Test]
        public void EmailContainsNoAmpersandTest()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "VeryImportantClient" },
                CreditLimit = 0,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "testTestcom",
                Firstname = "test1",
                HasCreditLimit = false,
                Id = 1,
                Surname = "test2"
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("test1", "test2", "testTestcom",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(false);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("test1", "test2", "testTestcom", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("test1", "test2", "testTestcom",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer), Times.Never);
            Assert.IsFalse(result);
        }

        [Test]
        public void EmailContainsNoDotTest()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "VeryImportantClient" },
                CreditLimit = 0,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "testTestcom",
                Firstname = "test1",
                HasCreditLimit = false,
                Id = 1,
                Surname = "test2"
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("test1", "test2", "testTestcom",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(false);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("test1", "test2", "testTestcom", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("test1", "test2", "testTestcom",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer), Times.Never);
            Assert.IsFalse(result);

        }

        [Test]
        public void AgeLess21Test()
        {
            var dateOfBirth = DateTime.Now;
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "VeryImportantClient" },
                CreditLimit = 0,
                DateOfBirth = dateOfBirth,
                EmailAddress = "test@Test.com",
                Firstname = "test1",
                HasCreditLimit = false,
                Id = 1,
                Surname = "test2"
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("test1", "test2", "testTestcom",
                dateOfBirth, 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(false);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("test1", "test2", "testTestcom", dateOfBirth, 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("test1", "test2", "testTestcom",
               dateOfBirth, 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer), Times.Never);
            Assert.IsFalse(result);

        }

        [Test]
        public void ImportantClientTest()
        {
            
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "VeryImportantClient" },
                CreditLimit = 0,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test@Test.com",
                Firstname = "test1",
                HasCreditLimit = false,
                Id = 1,
                Surname = "test2"
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("test1", "test2", "test@test.com",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(true);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("test1", "test2", "test@test.com", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("test1", "test2", "test@test.com",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer));
            Assert.IsTrue(result);

        }

        [Test]
        public void NormalClientTest()
        {

            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "NormalClient" },
                CreditLimit = 0,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test@Test.com",
                Firstname = "test1",
                HasCreditLimit = false,
                Id = 1,
                Surname = "test2"
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("test1", "test2", "test@test.com",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(true);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("test1", "test2", "test@test.com", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("test1", "test2", "test@test.com",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer));
            Assert.IsTrue(result);

        }

        [Test]
        public void CreditLimitLess500Test()
        {
            Customer customer = new Customer
            {
                Company = new Company { Id = 99, Name = "XX" },
                CreditLimit = 1,
                DateOfBirth = new DateTime(1985, 07, 01),
                EmailAddress = "test@Test.com",
                Firstname = "test1",
                HasCreditLimit = true,
                Id = 1,
                Surname = "test2"
                
            };
            _CustomerProvider.Setup(a => a.CreateCustomer("test1", "test2", "test@test.com",
                new DateTime(1985, 07, 01), 99)).Returns(customer);
            _customerValidator.Setup(b => b.CheckCustomer(customer)).Returns(false);
            _customerDataService.Setup(c => c.AddCustomer(customer));
            var result = _customerService.AddCustomer("test1", "test2", "test@test.com", new DateTime(1985, 07, 01), 99);

            _CustomerProvider.Verify(a => a.CreateCustomer("test1", "test2", "test@test.com",
               new DateTime(1985, 07, 01), 99));
            _customerValidator.Verify(b => b.CheckCustomer(customer));
            _customerDataService.Verify(c => c.AddCustomer(customer), Times.Never);
            Assert.IsFalse(result);
        }


    }
}
