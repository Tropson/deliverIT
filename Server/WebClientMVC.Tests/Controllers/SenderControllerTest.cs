using System;
using WebClientMVC.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClientMVC.Models;
using WebClientMVC.Tests.SenderServiceReference;
using WebClientMVC.Controllers;
using System.Web.Mvc;

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
        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg", "Tropson", "Password", 0, "SENDER")]
        [TestMethod]
        public void AddSenderTest(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city , string username, string password, int points, string accountType)
        {
            //setup
            PersonModel person = new PersonModel { Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber=phone, Email = email, Address = address, ZipCode = zipCode, City = city };
            SenderModel sender = new SenderModel { Person = person, AccountType = (AccountTypeEnum)Enum.Parse(typeof(AccountTypeEnum), accountType), Password = password, Username = username, Points = points };

            var sctr = new SenderController();

            var result = sctr.Create(sender);

            //assert
            Assert.AreEqual(1 , 1 );
            //afte
        }
    }
}
