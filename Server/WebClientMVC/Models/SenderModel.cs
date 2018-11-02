using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class SenderModel
    {
        public virtual PersonModel Person { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual int Points { get; set; }
        public virtual AccountTypeEnum AccountType { get; set; }
    }
}