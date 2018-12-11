using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClientMVC;
using WebClientMVC.Controllers;
using WebClientMVC.SenderServiceReference1;
using Moq;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var serviceMock = new Mock<ISenderService>();
            HomeController controller = new HomeController(serviceMock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            var serviceMock = new Mock<ISenderService>();
            HomeController controller = new HomeController(serviceMock.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            var serviceMock = new Mock<ISenderService>();
            HomeController controller = new HomeController(serviceMock.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
