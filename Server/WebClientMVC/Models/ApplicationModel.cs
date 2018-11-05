using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class ApplicationModel
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(10, ErrorMessage = "Too long")]
        [RegularExpression(@"[0-3][0-9][0-1][1-9]\d{2}\d{4}?[^ 0 - 9]*", ErrorMessage = "Not a valid CPR number")]
        public virtual string Cpr { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(15, ErrorMessage = "Too long")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(15, ErrorMessage = "Too long")]
        public virtual string LastName { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [Phone(ErrorMessage = "Not a valid phone number")]
        public virtual string PhoneNumber { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [EmailAddress(ErrorMessage = "Not a valid email address")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(30, ErrorMessage = "Too long")]
        public virtual string Address { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(10, ErrorMessage = "Too long")]
        public virtual string ZipCode { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(30, ErrorMessage = "Too long")]
        public virtual string City { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public virtual HttpPostedFileBase[] files { get; set; }
    }
}