using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.ATM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Formats.Asn1;
using Application.ATM;
using Domain.ATMTests.Fake;

namespace Domain.ATM.Tests
{
    [TestClass()]
    public class Tests_LoginService
    {
        [TestMethod()]
        public void Test_Login()
        {
            // Arrange
            ILoginService target = new LoginService(new FakeLoginServiceRepository());
            bool? expected = true;
            bool? actual = null;
            Account account = new Account()
            {
                Aid = "F123831081",
                Password = "QWHVG1oxljkkN3X52L6MU/G9VzX2mQ3ER3WMa96ISU2e+hbsNV4x2DT81bFvSn7mbIaLkc5LUJKVZ8cCtLHI4PzhARsoPzw3nvp7AVwydeAIiv8bInOHOJONRaRtdjF/+4Q6oWh6F3Wjq9XiEMakzTUmawTJrIG1+vDaP/dHYRU=",
            };

            // Act
            actual = target.Login(account);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}