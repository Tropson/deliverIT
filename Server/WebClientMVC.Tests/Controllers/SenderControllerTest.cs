using System;
using WebClientMVC.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClientMVC.Models;
using WebClientMVC.ServiceReference;
using WebClientMVC.Controllers;
using System.Web.Mvc;
using Moq;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class SenderControllerTest
    {
        [TestMethod]
        public void ServiceConnectionTest()
        {
            //setup
            var proxy = new SenderServiceClient();

            //assert
            proxy.Close();
            Assert.IsNotNull(proxy);
            proxy.Close();
        }
        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg", "Tropson", "Password", 0, "SENDER")]
        [TestMethod]
        public void AddSenderTest(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city, string username, string password, int points, string accountType)
        {
            //setup
            var senderServiceMock = new Mock<ISenderService>();
            var personStub = new PersonModel { Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber = phone, Email = email, Address = address, ZipCode = zipCode, City = city };
            var senderStub = new SenderModel { Person = personStub, AccountType = (AccountTypeEnum)Enum.Parse(typeof(AccountTypeEnum), accountType), Password = password, Username = username, Points = points };

            
            senderServiceMock.Setup(x => x.AddSender(senderStub)).Returns(1);

            var sut = new SenderController(senderServiceMock.Object);

            var res=sut.Create(senderStub);

            //assert
            Assert.AreEqual(res, 1);
            //afte
        }
    }
}
