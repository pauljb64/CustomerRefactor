using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Contracts;
using NUnit.Framework;
using App.Domain.Entity;

namespace App.test
{
    public class CreditScoreTest
    {
        private Mock<DataServices.ICustomerCreditService> _customerCreditService;
        private ICreditScore _creditScore;

        [SetUpAttribute]
    public void StartUp()
    {
        
        _customerCreditService = new Mock<DataServices.ICustomerCreditService>();
         _creditScore = new BusinessLogic.CreditScore(_customerCreditService.Object);

    }
        [Test]
        public void CreateVeryImportantCustomerTest()
        {
            Domain.Entity.Company company = new Company { Id = 99, Classification = Classification.Bronze, Name = "ImportantClient" };
          
            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(500);
            var result = _creditScore.CalculateCustomerCreditLimit(company);
          
            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(),
                          It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            Assert.AreEqual(500, result);

        }

    }
}
