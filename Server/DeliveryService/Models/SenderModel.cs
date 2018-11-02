using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DeliveryService.Models
{
    [KnownType(typeof(PersonModel))]
    [DataContract(Name ="SenderResource")]
    public class SenderModel:PersonModel
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
}