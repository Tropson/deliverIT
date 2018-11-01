using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DeliveryService.Models
{
    [DataContract]
    public class PersonModel
    {
            private int ID { get; set; }
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
}