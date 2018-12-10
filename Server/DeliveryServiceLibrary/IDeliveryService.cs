using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DeliveryServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
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
        int DeleteApplication(ApplicationModel application, bool deletePerson);

        [OperationContract]
        void ClearDB();

        [OperationContract]
        int GetBalanceByUsername(string username);

        [OperationContract]
        void AddToBalance(string username, int amount);

        [OperationContract]
        void UseVoucher(string username, string code);

        [OperationContract]
        VoucherModel[] GetAllVouchers();

        [OperationContract]
        VouchersUsedModel[] GetAllUsedVouchers();

        [OperationContract]
        PackageModel[] GetAllPackages();

        [OperationContract]
        DeliveryModel GetDeliveryByPackageBarcode(double barcode);

        [OperationContract]
        int AddPackage(PackageModel model, string Username, DeliveryModel delivery);

        [OperationContract]
        int TakePackage(double barcode, int courierId);

        [OperationContract]
        int ChangeStatus(double barcode);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "DeliveryServiceLibrary.ContractType".
    [DataContract(Name = "VoucherResource")]
    public class VoucherModel
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public int amount { get; set; }
    }
    [DataContract(Name = "VouchersUsedResource")]
    public class VouchersUsedModel
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string code { get; set; }
    }

    [DataContract(Name = "PackageModel")]
    public class PackageModel
    {
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public int SenderID { get; set; }
        [DataMember]
        public int? CourierID { get; set; }
        [DataMember]
        public string ToAddress { get; set; }
        [DataMember]
        public string FromAddress { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double Width { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public string ReceiverFirstName { get; set; }
        [DataMember]
        public string ReceiverLastName { get; set; }
        [DataMember]
        public string ReceiverPhoneNumber { get; set; }
        [DataMember]
        public double barcode { get; set; }
    }

    [DataContract(Name = "DeliveryModel")]
    public class DeliveryModel
    {
        [DataMember]
        public double Distance { get; set; }
        [DataMember]
        public int Price { get; set; }
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
        [DataMember]
        public int IDInDB { get; set; }
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
}
