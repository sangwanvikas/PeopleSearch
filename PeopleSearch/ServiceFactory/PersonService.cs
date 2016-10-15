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

        public static void Create(Person person)
        {
            PersonProvider.Store(person);
        }

        public static List<Person> Find(string name)
        {
            return PersonProvider.GetPersonByName(name);
        }

    }
}