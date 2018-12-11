using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClientMVC.Models
{
    public class SenderModel:PersonModel
    {
        public SenderModel(string cpr, string firstName, string lastName, string phone, string email, string address, string zipCode, string city) : base(cpr,firstName,lastName,phone,email,address,zipCode,city)
        {           
        }
        public SenderModel() { }
        public virtual string Username { get; set; }
        
        public virtual string Password { get; set; }
        
        public virtual int Points { get; set; }
        
        public virtual int AccountType { get; set; }

        public virtual string PassSalt { get; set; }
    }
}