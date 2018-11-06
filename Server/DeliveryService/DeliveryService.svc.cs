using DeliveyService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(
        Name = "SenderService",
        InstanceContextMode = InstanceContextMode.Single)]
    public class SenderService : ISenderService
    {
        DataClasses1DataContext db = new DataClasses1DataContext(ConfigurationManager.ConnectionStrings["deliveryCon"].ConnectionString);
        public int AddSender(UserModel senderObj)
        {
            UserModel sender = senderObj;
            int nextPersonId = 0;
            try {
                nextPersonId = db.Persons.OrderBy(x => x.ID).ToList().Last().ID + 1;
            }
            catch (Exception e)
            {
                nextPersonId = 1;
            }
            Person person = new Person { Cpr = sender.Cpr,
                FirstName = sender.FirstName,
                LastName = sender.LastName,
                PhoneNumber =sender.PhoneNumber,
                Email = sender.Email,
                Address = sender.Address,
                ZipCode = sender.ZipCode,
                City = sender.City };
            User user = new User
            {
                Username = sender.Username,
                Password = sender.Password,
                Points = sender.Points,
                AccountTypeID = sender.AccountType,
                PersonID = nextPersonId
            };
            var senders = db.Persons;
            var users = db.Users;
            senders.InsertOnSubmit(person);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
            Task.Delay(1000);
            db.Connection.Close();
            users.InsertOnSubmit(user);
            try
            {
                db.Connection.Open();
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                db.Connection.Close();
            }
            return 1;
        }
        public int AddApplication(ApplicationModel applicationObj)
        {
            ApplicationModel application = applicationObj;
            int nextPersonId = 0;
            try
            {
                nextPersonId = db.Persons.OrderBy(x => x.ID).ToList().Last().ID + 1;
            }
            catch (Exception e)
            {
                nextPersonId = 1;
            }
            Person person = new Person
            {
                Cpr = application.Cpr,
                FirstName = application.FirstName,
                LastName = application.LastName,
                PhoneNumber = application.PhoneNumber,
                Email = application.Email,
                Address = application.Address,
                ZipCode = application.ZipCode,
                City = application.City
            };
            Application myApplication = new Application
            {
                IDPicturePath = application.IDPicturePath,
                CVPath = application.CVPath,
                PersonID = nextPersonId,
                YellowCardPath = application.YellowCardPath
            };
            var persons = db.Persons;
            var applications = db.Applications;
            persons.InsertOnSubmit(person);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
            Task.Delay(1000);
            db.Connection.Close();
            applications.InsertOnSubmit(myApplication);
            try
            {
                db.Connection.Open();
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                db.Connection.Close();
            }
            return 1;
        }

        public List<ApplicationModel> GetApplications()
        {
            var applications = db.Applications.ToList();
            var myApplications = applications.Select(x => new ApplicationModel
            {
                Address = x.Person.Address,
                City = x.Person.City,
                Cpr = x.Person.Cpr,
                CVPath = x.CVPath,
                Email = x.Person.Email,
                FirstName = x.Person.FirstName,
                IDPicturePath = x.IDPicturePath,
                LastName = x.Person.LastName,
                PhoneNumber = x.Person.PhoneNumber,
                YellowCardPath = x.YellowCardPath,
                ZipCode = x.Person.ZipCode
            }).ToList();
            return myApplications;
        }

        public int AddCourier()
        {

        }

        public void ClearDB()
        {
            db.ExecuteCommand("Delete FROM Person");
            db.ExecuteCommand("DBCC CHECKIDENT ('Person', RESEED, 1);");
        }
        public UserModel getSenderByCpr(string cpr)
        {
            var sender = db.Users.Single(x => x.Person.Cpr == cpr);
            var person = db.Persons.Single(x => x.Cpr == cpr);
            UserModel mySender = new UserModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Cpr = person.Cpr,
                Address = person.Address,
                City = person.City,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                ZipCode = person.ZipCode,
                AccountType = (int)sender.AccountTypeID,
                Password = sender.Password,
                Username = sender.Username,
                Points = (int)sender.Points,
            };
            return mySender;

        }
    }
}
