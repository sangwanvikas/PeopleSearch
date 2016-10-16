using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.DAL
{
    public static class PersonProvider
    {
        public static int Create(Person person)
        {
            try
            {
                using (var db = new PersonContext())
                {
                    db.Persons.Add(person);
                    db.SaveChanges();
                    return person.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<Person> SearchByName(string name)
        {
            try
            {
                using (var context = new PersonContext())
                {
                    var persons = from person in context.Persons
                                  where person.FirstName.ToLower().Trim().Contains(name.ToLower().Trim())
                                  || person.LastName.ToLower().Contains(name.ToLower().Trim())
                                  select person;

                    return persons.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Person> SearchById(int id)
        {
            try
            {
                using (var context = new PersonContext())
                {
                    var persons = from person in context.Persons
                                  where person.Id == id
                                  select person;

                    return persons.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}