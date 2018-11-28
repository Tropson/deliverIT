using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class PackageModel
    {
        public int StatusID { get; set; }
        public int SenderID { get; set; }
        public int CourierID { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public string ToAddress { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public string FromAddress { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public double Width { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public double Height { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please use only English letters")]
        public string ReceiverFirstName { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please use only English letters")]
        public string ReceiverLastName { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        [Phone]
        public string ReceiverPhoneNumber { get; set; }
        public string Distance { get; set; }
        public string Price { get; set; }
    }
}