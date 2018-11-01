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
        [DataRow()]
        public void AddSenderTest()
        {
            
        }
    }
}
