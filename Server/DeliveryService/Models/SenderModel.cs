using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DeliveryService.Models
{
    [DataContract]
    [KnownType(typeof(PersonModel))]
    [KnownType(typeof(AccountTypeEnum))]
    public class SenderModel
    {
        [DataMember]
        public PersonModel Person { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public AccountTypeEnum AccountType { get; set; }
    }
}