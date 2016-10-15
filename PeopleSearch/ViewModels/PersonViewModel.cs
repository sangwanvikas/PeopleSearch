using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeopleSearch.ViewModels
{
    public class PersonViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Hobbies { get; set; }
        public string Gender { get; set; }
        public float Age { get; set; }

    }
}