using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PeopleSearch.DAL
{
    public class PersonProvider
    {
        private PersonContext _db;

        public PersonProvider()
        {
        }

        public PersonProvider(PersonContext db)
        {
            _db = db;
        }

        public int Create(Person person)
        {
            try
            {
                _db.Persons.Add(person);
                _db.SaveChanges();

                return person.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Person> SearchByName(string name)
        {
            try
            {
                var persons = from person in _db.Persons
                              where person.FirstName.ToLower().Trim().Contains(name.ToLower().Trim())
                              || person.LastName.ToLower().Contains(name.ToLower().Trim())
                              select person;

                return persons.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Person> SearchById(int id)
        {
            try
            {
                var persons = from person in _db.Persons
                              where person.Id == id
                              select person;

                return persons.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}