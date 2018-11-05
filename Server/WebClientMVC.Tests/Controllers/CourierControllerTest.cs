using System;
using DeliveryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class CourierControllerTest
    {
        [TestMethod]
        public void AddApplicationToDB(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city, string cvpath, string idpicturepath, string yellowcardpath)
        {
          
                //setup
                var courierServiceMock = new Mock<ISenderService>();
                var app = new ApplicationModel {CVpath = cvpath, IDPicturePath = idpicturepath, YellowCardPath = yellowcardpath } : base{ Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber = phone, Email = email, Address = address, ZipCode = zipCode, City = city};
                

                //senderServiceMock.Setup(x => x.AddSender(senderToService)).Returns(1);
                courierServiceMock.Setup(x => x.AddApplication(It.IsAny<DeliveryService.SenderModel>())).Returns(1);

                var sut = new ApplicationController(courierServiceMock.Object);

                var res = sut.Create(app);

                //assert
                Assert.AreNotEqual(app, new HttpStatusCodeResult(HttpStatusCode.InternalServerError));s
                //afte
          
        }
    }
}
