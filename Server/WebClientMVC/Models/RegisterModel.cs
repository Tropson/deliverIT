using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class RegisterModel
    {
        [Required]
        public virtual string Cpr { get; set; }
        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }
        [Required]
        public virtual string PhoneNumber { get; set; }
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual string Address { get; set; }
        [Required]
        public virtual string ZipCode { get; set; }
        [Required]
        public virtual string City { get; set; }
        [Required]
        public virtual string Username { get; set; }
        [Required]
        public virtual string Password { get; set; }
    }
}