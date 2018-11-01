using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class SenderModel
    {
        public PersonModel Person { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        public Enum AccountType { get; set; }
    }
}