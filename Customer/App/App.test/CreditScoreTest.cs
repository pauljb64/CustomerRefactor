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
            var applicant = new Customer("test1Name", "Test1", "test@test.com", new DateTime(1985, 12, 31), 99);

            _customerCreditService.Setup(d => d.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(500);
            var result = _creditScore.CalculateCustomerCreditLimit(applicant);
          
            _customerCreditService.Verify(d => d.GetCreditLimit(It.IsAny<string>(),
                          It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            Assert.AreEqual(1000, result);

        }

    }
}
