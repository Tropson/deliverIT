using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DeliveryService;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void RetriveTheListOfApplications()
        {
            var serviceStub = new Mock<ISenderService>();

            serviceStub.Setup(x => x.GetApplications()).Returns(new ApplicationModel[] { new ApplicationModel { Cpr = "12345" } });

            var sut = new AdminController(serviceStub);

            var resPage = sut.Index() as ViewResult;

            var model = resPage.ViewData.Model as IEnumerable<ApplicationModel>;

            Assert.IsTrue(model.Count() == 1);

        }

        [TestMethod]
        public void CreateAccountWhenAccepted()
        {
            var serviceStub = new Mock<ISenderService>();
            var applicationStub = new Mock<ApplicationModel>();
            applicationStub.Setup(x => x.Address).Returns("Mock address");
            applicationStub.Setup(x => x.City).Returns("Mock City");
            applicationStub.Setup(x => x.Cpr).Returns("Mock Cpr");
            applicationStub.Setup(x => x.CVPath).Returns("Mock CV Path");
            applicationStub.Setup(x => x.Email).Returns("Mock Email");
            applicationStub.Setup(x => x.FirstName).Returns("Mock First name");
            applicationStub.Setup(x => x.IDPicturePath).Returns("Mock ID Path");
            applicationStub.Setup(x => x.LastName).Returns("Mock Lastname");
            applicationStub.Setup(x => x.PhoneNumber).Returns("Mock PhoneNumber");
            applicationStub.Setup(x => x.YellowCardPath).Returns("Mock Yellow Card");
            applicationStub.Setup(x => x.ZipCode).Returns("Mock Zip Code");

            ApplicationModel app = null;

            serviceStub.Setup(x => x.AcceptCourier(It.IsAny<ApplicationModel>())).Callback<ApplicationModel>(x => app = x);

            var sut = new AdminController(serviceStub.Object);


            sut.CreateOnAccept(applicationStub.Object);


            serviceStub.Verify(x => x.AcceptCourier(It.IsAny<ApplicationModel>(), Times.Once()));

            Assert.IsTrue(app.Address == "Mock address" && app.City == "Mock City" && app.Cpr == "Mock Cpr" && app.CVPath == "Mock CV Path" && app.Email == "Mock Email" && app.FirstName == "Mock First name" && app.IDPicturePath == "Mock Id Path" && app.LastName == "Mock Last Name" && app.PhoneNumber == "Mock PhoneNumber" && app.YellowCardPath == "Mock Yellow Card" && app.ZipCode == "Mock Zip Code");
            

        }
    }
}
