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
        [DataRow("tropson")]
        [TestMethod]
        public void GetBalanceTest(string username)
        {
            LoginPassModel log = new LoginPassModel { Username =username };
            var serviceStub = new Mock<ISenderService>();
            serviceStub.Setup(x => x.GetAllUsers()).Returns(new SenderResource[] { new SenderResource {Username=username, Password = "password", Points=100, PassSalt="passSalt", AccountType=2} });
            var sut = new SenderController(serviceStub.Object);


            var resPage = sut.LoggedInPost(log) as ViewResult;

            var model = resPage.ViewData.Model as SenderModel;

            Assert.AreEqual(100, model.Points);
        }


        //[TestMethod]
        //public void AddPointsToUseerAccount()
        //{
        //    var voucher = new VoucherModel { code = "test2", amount = 200 };
        //    var username = "tropson2";

        //    var serviceMock = new Mock<ISenderService>();
           
        //    serviceMock.Setup(x=> x.GetBalanceByUsername())
            

        //    SenderController senderContr = new SenderController(serv as SenderServiceReference1.ISenderService);

        //    senderContr.Voucher(voucher);

        //    int resPoints = serv.GetBalanceByUsername(username);

        //    Assert.Equals(points, resPoints + 50);
            
        //}
    }
}
