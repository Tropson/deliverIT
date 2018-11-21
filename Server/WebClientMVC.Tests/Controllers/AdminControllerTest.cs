using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebClientMVC.SenderServiceReference1;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebClientMVC.Controllers;
using WebClientMVC.Models;
using System.Web;
using System.Net;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void RetriveTheListOfApplications()
        {
            //Setup
            var serviceStub = new Mock<ISenderService>();
            serviceStub.Setup(x => x.GetAllApplications()).Returns(new List<DeliveryService.ApplicationModel> { new DeliveryService.ApplicationModel { Cpr = "123" } });
            var sut = new AdminController(serviceStub.Object);

            //Act
            var resPage = sut.Index() as ViewResult;

            //Assert
            var model = resPage.ViewData.Model as IEnumerable<Models.ApplicationModel>;

            Assert.IsTrue(model.Count() == 1);

        }


        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg")]
        [TestMethod]
        public void AddCourierTest(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city)
        {
            //setup
            var senderServiceMock = new Mock<ISenderService>();
            var app = new Models.ApplicationModel { Address = address, City = city, Cpr = cpr, Email = email, FirstName = firstName, LastName = lastName, PhoneNumber = phone, ZipCode = zipCode };
            

            senderServiceMock.Setup(x => x.AddSender(It.IsAny<DeliveryService.UserModel>())).Returns(1);

            var sut = new AdminController(senderServiceMock.Object);

            var res = sut.CreateOnAccept(app);

            //assert
            Assert.AreNotEqual(res, new HttpStatusCodeResult(HttpStatusCode.InternalServerError));
            //afte
        }


        //TODO : This freaking test idk how to even start it kurwa

        //[TestMethod]
        //public void CreateAccountWhenAccepted()
        //{
        //    var serviceStub = new Mock<ISenderService>();
        //    var userStub = new Mock<Models.SenderModel>();

        //    DeliveryService.ApplicationModel app = null;

        //    serviceStub.Setup(x => x.AcceptCourier(It.IsAny<DeliveryService.ApplicationModel>())).Callback<DeliveryService.ApplicationModel>(x => app = x);

        //    var sut = new AdminController(serviceStub.Object);


        //    sut.CreateOnAccept(applicationStub.Object);


        //    serviceStub.Verify(x => x.AcceptCourier(It.IsAny<Models.ApplicationModel>(), Times.Once()));

        //    Assert.IsTrue(app.Address == "Mock address" && app.City == "Mock City" && app.Cpr == "Mock Cpr" && app.Email == "Mock Email" && app.FirstName == "Mock First name" && app.LastName == "Mock Last Name" && app.PhoneNumber == "Mock PhoneNumber" && app.ZipCode == "Mock Zip Code");


        //}
    }
}
