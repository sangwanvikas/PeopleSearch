using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace PeopleSearch.DAL
{
    public class PersonDBInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        protected override void Seed(PersonContext context)
        {
            IList<Person> defaultPersons = GetSeedPersons();

            foreach (Person person in defaultPersons)
                context.Persons.Add(person);

            base.Seed(context);
        }

        public List<Person> GetSeedPersons()
        {
            List<Person> seedPersons = new List<Person>();

            string imageDirectoryAbsolutePath = HttpContext.Current.Server.MapPath(Constants.ImageDirectoryLoalPath);

            string seedDataDirectoryAbsolutePath = HttpContext.Current.Server.MapPath(Constants.SeedDataDirectoryLocalFilePath);
            XDocument personXmlAbsoluteFilePath = XDocument.Load(Path.Combine(seedDataDirectoryAbsolutePath, Constants.PersonXmlFileName));

            foreach (var DetailNode in personXmlAbsoluteFilePath.Descendants(Constants.DetailsNode))
            {
                Person person = new Person();
                person.FirstName = DetailNode.Element(Constants.FirstNameNode).Value;
                person.LastName = DetailNode.Element(Constants.LastNameNode).Value;
                person.Address = DetailNode.Element(Constants.AddressNode).Value;
                person.Hobbies = DetailNode.Element(Constants.HobbiesNode).Value;
                person.DateOfBirth = Convert.ToDateTime(DetailNode.Element(Constants.DateOfBirthNode).Value);
                person.Gender = DetailNode.Element(Constants.GenderNode).Value;

                string imageName = DetailNode.Element(Constants.ImageNameNode).Value;
                string imageAbsolutePath = Path.Combine(imageDirectoryAbsolutePath, imageName);
                person.Image = GetImageBytes(imageAbsolutePath);

                seedPersons.Add(person);
            }

            return seedPersons;
        }

        public byte[] GetImageBytes(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            byte[] data = File.ReadAllBytes(path);

            return data;
        }
    }
}