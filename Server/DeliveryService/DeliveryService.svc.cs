using DeliveryService.Models;
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
        public int AddSender(Object senderObj)
        {
            SenderModel sender = senderObj as SenderModel;

            int nextPersonId = (int)db.Persons.Last().ID + 1;
            Person person = new Person { Cpr = sender.Person.Cpr,
                FirstName = sender.Person.FirstName,
                LastName = sender.Person.LastName,
                PhoneNumber =sender.Person.PhoneNumber,
                Email = sender.Person.Email,
                Address = sender.Person.Address,
                ZipCode = sender.Person.ZipCode,
                City = sender.Person.City };
            User user = new User
            {
                Username = sender.Username,
                Password = sender.Password,
                Points = sender.Points,
                AccountTypeID = (int)sender.AccountType,
                PersonID = nextPersonId
            };
            var senders = db.Persons;
            var users = db.Users;
            users.InsertOnSubmit(user);
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
            db.ExecuteCommand("DBCC CHECKIDENT ('Person', RESEED, 1);");
        }
        public SenderModel getSenderByCpr(string cpr)
        {
            var sender = db.Users.Single(x => x.Person.Cpr == cpr);
            SenderModel mySender = new SenderModel
            {
                AccountType = (AccountTypeEnum)sender.AccountTypeID,
                Password = sender.Password,
                Username = sender.Username,
                Points = (int)sender.Points,
                Person = new PersonModel
                {
                    Cpr = sender.Person.Cpr,
                    Address = sender.Person.Address,
                    City = sender.Person.City,
                    Email = sender.Person.Email,
                    FirstName = sender.Person.FirstName,
                    LastName = sender.Person.LastName,
                    PhoneNumber = sender.Person.PhoneNumber,
                    ZipCode = sender.Person.ZipCode
                }
            };
            return mySender;

        }
    }
}
