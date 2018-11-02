using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class RegisterModel
    {
        public virtual string Cpr { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Email { get; set; }

        public virtual string Address { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string City { get; set; }

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }
    }
}