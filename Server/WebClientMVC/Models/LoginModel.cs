using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class LoginModel
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

    }
}