using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PeopleSearch.ServiceFactory
{
    public static class PersonService
    {

        public static int Create(Person person)
        {
            return PersonProvider.Create(person);
        }

        public static List<Person> Search(string name)
        {
            return PersonProvider.SearchByName(name);
        }

    }
}