using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebClientMVC.SenderServiceReference1;
using WebClientMVC.Models;
using System.Web.Mvc;
using WebClientMVC.Controllers;

namespace WebClientMVC.Tests.Controllers
{
    [TestClass]
    public class PointsControllerTest
    {
        [TestMethod]
        public void GetBalanceTest(string username)
        {
            LoginPassModel log = new LoginPassModel { Username = "tropson2" };
            var serviceStub = new Mock<ISenderService>();
            serviceStub.Setup(x => x.GetBalanceByUsername(username)).Returns(100);
            var sut = new SenderController(serviceStub.Object);


            var resPage = sut.LoggedInPost(log) as ViewResult;

            var model = resPage.ViewData as SenderModel;

            Assert.AreEqual(100, model.Points);
        }


        [TestMethod]
        public void AddPointsToUseerAccount()
        {
            var voucher = new VoucherModel { code = "test2", amount = 200 };
            var username = "tropson2";
            SenderServiceReference.SenderServiceClient serv = new SenderServiceReference.SenderServiceClient();

            int points = serv.GetBalanceByUsername(username);

            SenderController senderContr = new SenderController(serv as SenderServiceReference1.ISenderService);

            senderContr.Voucher(voucher);

            int resPoints = serv.GetBalanceByUsername(username);

            Assert.Equals(points, resPoints + 50);
            
        }
    }
}
