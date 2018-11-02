using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClientMVC.Models
{
    [DataContract]
    public class PersonModel
    {
        
        public PersonModel(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city)
        {
            this.Cpr = cpr;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phone;
            this.Email = email;
            this.Address = address;
            this.ZipCode = zipCode;
            this.City = city;
        }
        private int ID { get; set; }
        [DataMember]
        public virtual string Cpr { get; set; }
        [DataMember]
        public virtual string FirstName { get; set; }
        [DataMember]
        public virtual string LastName { get; set; }
        [DataMember]
        public virtual string PhoneNumber { get; set; }
        [DataMember]
        public virtual string Email { get; set; }
        [DataMember]
        public virtual string Address { get; set; }
        [DataMember]
        public virtual string ZipCode { get; set; }
        [DataMember]
        public virtual string City { get; set; }
    }
}