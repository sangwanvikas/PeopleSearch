using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PeopleSearch.DAL
{
    public interface IPersonContext
    {
        IDbSet<Person> Persons { get; set; }
    }
}