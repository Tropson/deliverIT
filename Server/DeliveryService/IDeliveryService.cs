using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace DeliveryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Name = "ISenderService")]
    public interface ISenderService
    {
        [OperationContract]
        int AddSender(UserModel sender);

        [OperationContract]
        UserModel[] GetAllUsers();

        [OperationContract]
        int AddApplication(ApplicationModel application);

        [OperationContract]
        ApplicationModel[] GetAllApplications();

        [OperationContract]
        int AddCourier(UserModel courier);

        [OperationContract]
        int DeleteApplication(ApplicationModel application,bool deletePerson);

        [OperationContract]
        void ClearDB();

        [OperationContract]
        int GetBalanceByUsername(string username);

        [OperationContract]
        void AddToBalance(string username, int amount);

        [OperationContract]
        VoucherModel[] GetAllVouchers();
    }

    [DataContract(Name = "VoucherResource")]
    public class VoucherModel
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public int? status { get; set; }
    }

    [DataContract(Name = "PersonResource")]
    public class PersonModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Cpr { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string City { get; set; }
    }

    [KnownType(typeof(PersonModel))]
    [DataContract(Name = "ApplicationResource")]
    public class ApplicationModel : PersonModel
    {
        [DataMember]
        public string CVPath { get; set; }
        [DataMember]
        public string IDPicturePath { get; set; }
        [DataMember]
        public string YellowCardPath { get; set; }
        [DataMember]
        public string GuidLine { get; set; }
    }

    [KnownType(typeof(PersonModel))]
    [DataContract(Name = "SenderResource")]
    public class UserModel : PersonModel
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public int AccountType { get; set; }
    }

    [DataContract(Name = "AccountTypeResource")]
    public enum AccountTypeEnum
    {
        [EnumMember]
        SENDER = 1,
        [EnumMember]
        COURIER = 2,
        [EnumMember]
        ADMIN = 3
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
