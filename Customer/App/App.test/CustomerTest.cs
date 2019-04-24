using App.Domain.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.test
{
    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void CreateCustomerTest()
        {
            Company company = new Company { Id = 99, Classification = Classification.Silver, Name = "Client" };
            Customer customer = new Customer();
            var result = customer.CreateCustomer("test1Name", "Test1", "test@test.com", new DateTime(1985, 12, 31), company);
            Assert.AreEqual("test1Name", result.Firstname);
            Assert.AreEqual("Test1", result.Surname);
            Assert.AreEqual("test@test.com", result.EmailAddress);
            Assert.AreEqual(new DateTime(1985, 12, 31), result.DateOfBirth);
            Assert.AreEqual(0, result.CreditLimit);
            Assert.AreEqual(false, result.HasCreditLimit);
            Assert.AreEqual(99, result.Company.Id);
            Assert.AreEqual(Classification.Silver, result.Company.Classification);
            Assert.AreEqual("Client", result.Company.Name);
        }

    }
}
