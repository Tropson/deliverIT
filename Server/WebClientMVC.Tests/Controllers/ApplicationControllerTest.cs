﻿using System;
using System.Net;
using System.Web.Mvc;
using DeliveryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebClientMVC.Controllers;
using WebClientMVC.Models;
using WebClientMVC.SenderServiceReference;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class ApplicationControllerTest
    {
        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg", "CVpath", "IDPicture Path", "Yellow Card Path")]
        [TestMethod]
        public void AddApplicationToDB(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city, string cvpath, string idpicturepath, string yellowcardpath)
        {
          
                //setup
                var courierServiceMock = new Mock<DeliveryService.ISenderService>();
                var app = new Models.ApplicationModel(cpr, firstName, lastName, phone, email, address, zipCode, city)  { CVPath = cvpath, IDPicturePath = idpicturepath, YellowCardPath = yellowcardpath };
                

                //senderServiceMock.Setup(x => x.AddSender(senderToService)).Returns(1);
                courierServiceMock.Setup(x => x.AddApplication(It.IsAny<DeliveryService.ApplicationModel>())).Returns(1);

                var sut = new ApplicationController(courierServiceMock.Object);

                var res = sut.Create(app);

                //assert
                Assert.AreNotEqual(app, new HttpStatusCodeResult(HttpStatusCode.InternalServerError));
                
          
        }


        
    }
}