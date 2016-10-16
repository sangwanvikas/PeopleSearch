using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PeopleSearch.DAL
{
    public class PersonContext : DbContext
    {
        public PersonContext() : base("PersonContext")
        {
            Database.SetInitializer(new PersonDBInitializer());

        }

        public DbSet<Person> Persons { get; set; }

        //public int Create(Person person)
        //{
        //    Persons.Add(person);
        //    SaveChanges();

        //    return person.Id;
            
        //}

    }
}