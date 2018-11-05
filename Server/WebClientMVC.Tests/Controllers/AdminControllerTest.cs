using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DeliveryService;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebClientMVC.Controllers;
using WebClientMVC.Models;
using System.Web;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void RetriveTheListOfApplications()
        {
            var serviceStub = new Mock<ISenderService>();

            serviceStub.Setup(x => x.GetApplications()).Returns(new DeliveryService.ApplicationModel[] { new DeliveryService.ApplicationModel { Cpr = "12345" } });

            var sut = new AdminController(serviceStub.Object);

            var resPage = sut.Index() as ViewResult;

            var model = resPage.ViewData.Model as IEnumerable<DeliveryService.ApplicationModel>;

            Assert.IsTrue(model.Count() == 1);

        }

        [TestMethod]
        public void CreateAccountWhenAccepted()
        {
            var serviceStub = new Mock<ISenderService>();
            var userStub = new Mock<Models.SenderModel>();

            DeliveryService.ApplicationModel app = null;

            serviceStub.Setup(x => x.AcceptCourier(It.IsAny<DeliveryService.ApplicationModel>())).Callback<DeliveryService.ApplicationModel>(x => app = x);

            var sut = new AdminController(serviceStub.Object);


            sut.CreateOnAccept(applicationStub.Object);


            serviceStub.Verify(x => x.AcceptCourier(It.IsAny<Models.ApplicationModel>(), Times.Once()));

            Assert.IsTrue(app.Address == "Mock address" && app.City == "Mock City" && app.Cpr == "Mock Cpr" && app.Email == "Mock Email" && app.FirstName == "Mock First name" && app.LastName == "Mock Last Name" && app.PhoneNumber == "Mock PhoneNumber" && app.ZipCode == "Mock Zip Code");
            

        }
    }
}
