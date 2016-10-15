using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.DAL
{
    public static class PersonProvider
    {
        public static void Create(Person person)
        {
            try
            {
                using (var db = new PersonContext())
                {
                    db.Persons.Add(person);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static List<Person> SearchByName(string name)
        {
            try
            {
                using (var context = new PersonContext())
                {
                    var persons = from b in context.Persons
                                  where b.FirstName.ToLower().Trim().Contains(name.ToLower().Trim())
                                  || b.LastName.ToLower().Contains(name.ToLower().Trim())
                                  select b;

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