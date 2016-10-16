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
    public class PersonService
    {
        PersonProvider _provider;
       // PersonContext _context;

        public PersonService()
        {
         //   _context = new PersonContext();
            _provider = new PersonProvider();
        }

        public PersonService(PersonContext db)
        {
            //   _context = new PersonContext();
            _provider = new PersonProvider(db);
        }

        public int Create(Person person)
        {
            return _provider.Create(person);
        }

        public List<Person> Search(string name)
        {
            return _provider.SearchByName(name);
        }

        public List<Person> Search(int id)
        {
            return _provider.SearchById(id);
        }

    }
}