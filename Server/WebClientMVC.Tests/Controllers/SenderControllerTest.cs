using System;
using WebClientMVC.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClientMVC.Models;
using WebClientMVC.Tests.SenderServiceReference;

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
        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg")]
        [TestMethod]
        public void AddSenderTest(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city)
        {
            //setup
            var proxy = new SenderServiceReference.SenderServiceClient();
            PersonModel person = new PersonModel { Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber=phone, Email = email, Address = address, ZipCode = zipCode, City = city };
            //addToDB
            var result = proxy.AddSender(person.Cpr, person.FirstName,person.LastName,person.PhoneNumber,person.Email,person.Address,person.ZipCode, person.City);
            Console.WriteLine(result);
            //assert
            Assert.AreEqual(result, 1);
            //after
            proxy.ClearDB();
            proxy.Close();
        }
    }
}
