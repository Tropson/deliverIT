using System;
using UnitTestProject1.ServiceReference1;
using Moq;
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
            var proxy = new ServiceReference1.SenderServiceClient();

            //assert
            Assert.IsNotNull(proxy);
        }
        [DataRow("2611982375", "David", "Szoke", "91933260", "tropson90@gmail.com", "Fredensgade 7, 2st", "9000", "Aalborg")]
        [TestMethod]
        public void AddSenderTest(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city)
        {
            //setup
            var proxy = new ServiceReference1.SenderServiceClient();
            var mock = new Mock<Person>();
            Person person = new Person { Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber=phone, Email = email, Address = address, ZipCode = zipCode, City = city };
            //addToDB
            var result = proxy.AddSender(person.Cpr, person.FirstName,person.LastName,person.PhoneNumber,person.Email,person.Address,person.ZipCode, person.City);
            Console.WriteLine(result);
            //assert
            Assert.Equals(result, 1);
            //after
            //proxy.ClearDB();
        }
    }
}
