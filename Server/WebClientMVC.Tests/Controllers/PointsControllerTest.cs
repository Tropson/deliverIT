using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebClientMVC.SenderServiceReference1;
using WebClientMVC.Models;
using System.Web.Mvc;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class PointsControllerTest
    {
        [TestMethod]
        public void GetBalanceTest(string username)
        {
            var serviceStub = new Mock<ISenderService>();
            serviceStub.Setup(x => x.GetBalanceByUsername(username)).Returns(new SenderModel { Points = 100 });
            var sut = new PointsController(serviceStub.Object);


            var resPage = sut.Index() as ViewResult;

            var model = resPage.ViewData as SenderModel;

            Assert.AreEqual(100, model.Points);
        }

        [TestMethod]
        public void AddPointsToUseerAccount(string username)
        {
            SenderServiceClient serv = new SenderServiceClient();

            int points = serv.GetBalanceByUsername(username);

            PointsController pointsCtr = new PointsController();

            pointsCtr.AddPoints(50);

            int resPoints = serv.GetBalanceByUsername(username);

            Assert.Equals(points, resPoints + 50);
            
        }
    }
}
