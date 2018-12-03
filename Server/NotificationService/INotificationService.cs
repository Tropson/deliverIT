using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NotificationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract =typeof(INotificationServiceCallback),SessionMode =SessionMode.Required)]
    public interface INotificationService
    {
        [OperationContract(IsOneWay = true)]
        void CallCallback();
        // TODO: Add your service operations here
    }
    [ServiceContract]
    public interface INotificationServiceCallback
    {
        [OperationContract(IsOneWay =true)]
        void SendNotification();
    }
    [DataContract]
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
    [DataContract]
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
}
