using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClientMVC.Models
{
    [DataContract]
    [KnownType(typeof(PersonModel))]
    [KnownType(typeof(AccountTypeEnum))]
    public class SenderModel:PersonModel
    {
        public SenderModel(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city) : base(cpr,firstName,lastName,phone,email,address,zipCode,city)
        {           
        }
        [DataMember]
        public virtual string Username { get; set; }
        [DataMember]
        public virtual string Password { get; set; }
        [DataMember]
        public virtual int Points { get; set; }
        [DataMember]
        public virtual AccountTypeEnum AccountType { get; set; }
    }
}