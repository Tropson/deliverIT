using System;
using WebClientMVC.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClientMVC.Models;
using WebClientMVC.SenderServiceReference;
using WebClientMVC.Controllers;
using System.Web.Mvc;
using WebClientMVC.Models;
using Moq;
using System.Net;

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
        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg", "Tropson", "Password")]
        [TestMethod]
        public void AddSenderTest(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city, string username, string password)
        {
            //setup
            var senderServiceMock = new Mock<ISenderService>();
            var reg = new RegisterModel { Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber = phone, Email = email, Address = address, ZipCode = zipCode, City = city, Username= username, Password = password };
            //SenderModel sender = new SenderModel(reg.Cpr, reg.FirstName, reg.LastName, reg.PhoneNumber, reg.Email, reg.Address, reg.ZipCode, reg.City) { AccountType = (int)AccountTypeEnum.SENDER, Password = reg.Password, Username = reg.Username, Points = 0 };
            //DeliveryService.SenderModel senderToService = new DeliveryService.SenderModel { AccountType = sender.AccountType, Address = sender.Address, City = sender.City, Cpr = sender.Cpr, Email = sender.Email, FirstName = sender.FirstName, LastName = sender.LastName, Password = sender.Password, PhoneNumber = sender.PhoneNumber, Points = sender.Points, Username = sender.Username, ZipCode = sender.ZipCode });

            //senderServiceMock.Setup(x => x.AddSender(senderToService)).Returns(1);
            senderServiceMock.Setup(x => x.AddSender(It.IsAny<DeliveryService.SenderModel>())).Returns(1);

            var sut = new SenderController(senderServiceMock.Object);

            var res=sut.Create(reg);

            //assert
            Assert.AreNotEqual(res, new HttpStatusCodeResult(HttpStatusCode.InternalServerError));
            //afte
        }
    }
}
