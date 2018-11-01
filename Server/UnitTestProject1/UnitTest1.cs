using System;
using UnitTestProject1.ServiceReference1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ServiceConnectionTest()
        {
            //setup
            var proxy = new ServiceReference1.DeliveryServiceClient();

            //assert
            Assert.IsNotNull(proxy);
        }
        [TestMethod]
        [DataRow("2611982375","Dávid","Szőke","91933260","tropson90@gmail.com","Fredensgade 7, 2st","9000","Aalborg")]
        public void AddSenderTest(string cpr, string firstName, string lastName, string email, string address, string zipCode, string city)
        {
            //setup
            var proxy = new ServiceReference1.DeliveryServiceClient();
            Person person = { Cpr = cpr, FirstName = firstName, LastName = lastName, Email = email, Address = address, ZipCode = zipCode, City = city };

            //addToDB
            var result = proxy.AddPerson(person);

            //assert
            Assert.Equals(result, 1);
        }
    }
}
