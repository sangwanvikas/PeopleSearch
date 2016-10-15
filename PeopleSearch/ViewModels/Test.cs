using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeopleSearch.ViewModels
{
    public class Test
    {
        //[Required]
        //[EmailAddress]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        //[Display(Name = "Upload image")]
        public HttpPostedFileBase Image { get; set; }

        public string Hobbies { get; set; }

        public string Gender { get; set; }
    }
}