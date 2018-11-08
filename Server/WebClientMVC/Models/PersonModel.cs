using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClientMVC.Models
{
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
        public PersonModel() { }
        private int ID { get; set; }
        
        public virtual string Cpr { get; set; }
        
        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }
        
        public virtual string PhoneNumber { get; set; }
        
        public virtual string Email { get; set; }
        
        public virtual string Address { get; set; }
        
        public virtual string ZipCode { get; set; }
        
        public virtual string City { get; set; }
    }
}