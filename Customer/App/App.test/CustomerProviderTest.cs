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
    public class CustomerProviderTest
    {
        private Mock<ICompanyDataService> _companyDataService;
        private Mock<ICompanyDataProvider> _companyDataProvider;
        private Mock<ICompanyMapper> _companyMapper;
        private Mock<ICustomerCreditService> _customerCreditService;

        private ICustomerProvider _CustomerProvider;

        [SetUpAttribute]
        public void StartUp()
        {
            _companyMapper = new Mock<ICompanyMapper>();
            _companyDataProvider = new Mock<ICompanyDataProvider>();
            _companyDataService = new Mock<ICompanyDataService>();
            _customerCreditService = new Mock<ICustomerCreditService>();
            _CustomerProvider = new CustomerProvider(_companyDataService.Object, _customerCreditService.Object);

        }

        [Test]
        public void CreateVeryImportantCustomerTest()
        {
            Company company = new Company { Id = 99, Classification = Classification.Silver, Name = "VeryImportantClient" };
            _companyDataService.Setup(a => a.GetById(It.IsAny<int>())).Returns(company);
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(500);
            var result = _CustomerProvider.CreateCustomer("test1Name", "Test1", "test@test.com", new DateTime(1985,12,31), 99);
            _companyDataService.Verify(a => a.GetById(It.IsAny<int>()));
            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            Assert.AreEqual("test1Name",result.Firstname);
            Assert.AreEqual("Test1", result.Surname);
            Assert.AreEqual("test@test.com", result.EmailAddress);
            Assert.AreEqual(new DateTime(1985, 12, 31), result.DateOfBirth);
            Assert.AreEqual(0, result.CreditLimit);
            Assert.AreEqual(false, result.HasCreditLimit);
            Assert.AreEqual(99, result.Company.Id);
            Assert.AreEqual(Classification.Silver, result.Company.Classification);
            Assert.AreEqual("VeryImportantClient", result.Company.Name);
        }

        [Test]
        public void CreateImportantCustomerTest()
        {
            Company company = new Company { Id = 99, Classification = Classification.Silver, Name = "ImportantClient" };
            _companyDataService.Setup(a => a.GetById(It.IsAny<int>())).Returns(company);
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(500);
            var result = _CustomerProvider.CreateCustomer("test1Name", "Test1", "test@test.com", new DateTime(1985, 12, 31), 99);
            _companyDataService.Verify(a => a.GetById(It.IsAny<int>()));
            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
            Assert.AreEqual("test1Name", result.Firstname);
            Assert.AreEqual("Test1", result.Surname);
            Assert.AreEqual("test@test.com", result.EmailAddress);
            Assert.AreEqual(new DateTime(1985, 12, 31), result.DateOfBirth);
            Assert.AreEqual(1000, result.CreditLimit);
            Assert.AreEqual(true, result.HasCreditLimit);
            Assert.AreEqual(99, result.Company.Id);
            Assert.AreEqual(Classification.Silver, result.Company.Classification);
            Assert.AreEqual("ImportantClient", result.Company.Name);
        }

        [Test]
        public void CreateCustomerTest()
        {
            Company company = new Company { Id = 99, Classification = Classification.Silver, Name = "Client" };
            _companyDataService.Setup(a => a.GetById(It.IsAny<int>())).Returns(company);
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(500);
            var result = _CustomerProvider.CreateCustomer("test1Name", "Test1", "test@test.com", new DateTime(1985, 12, 31), 99);
            _companyDataService.Verify(a => a.GetById(It.IsAny<int>()));
            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
            Assert.AreEqual("test1Name", result.Firstname);
            Assert.AreEqual("Test1", result.Surname);
            Assert.AreEqual("test@test.com", result.EmailAddress);
            Assert.AreEqual(new DateTime(1985, 12, 31), result.DateOfBirth);
            Assert.AreEqual(500, result.CreditLimit);
            Assert.AreEqual(true, result.HasCreditLimit);
            Assert.AreEqual(99, result.Company.Id);
            Assert.AreEqual(Classification.Silver, result.Company.Classification);
            Assert.AreEqual("Client", result.Company.Name);
        }
    }
}
