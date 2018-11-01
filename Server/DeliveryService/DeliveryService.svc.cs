using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DeliveryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SenderService : ISenderService
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public int AddSender(string cpr, string firstName, string lastName, string phoneNumber, string email, string address, string zipCode, string city)
        {
            Person person = new Person { Cpr = cpr, FirstName = firstName, LastName = lastName, PhoneNumber=phoneNumber, Email = email, Address = address, ZipCode = zipCode, City = city };
            var senders = db.Persons;
            senders.InsertOnSubmit(person);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
            return 1;
        }
        public void ClearDB()
        {
            db.ExecuteCommand("Delete FROM Person");
        }
    }
}
