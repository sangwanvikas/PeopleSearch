using PeopleSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PeopleSearch.DAL
{
    public class PersonDBInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Names.txt");
        string fPath = HttpContext.Current.Server.MapPath("~/Resources/images/Albert_Einstein.jpg");
        ImageModel im = new ImageModel();

        protected override void Seed(PersonContext context)
        {
            IList<Person> defaultPersons = new List<Person>();

            foreach(Image image in im)
            {
                string[] name = image.Description.Split(' ');
                defaultPersons.Add(new Person()
                {
                    FirstName = name[0],
                    LastName = name[1],
                    DateOfBirth = DateTime.Now,
                    Address = "75 st alphonsus street, 90001, cityview at longwood, Boston, MA, 02120",
                    Hobbies = "Swimmig, Ping Pong, Running, Reading",
                    Image = GetImageBytes(image.Path)
                });
            }

            //defaultPersons.Add(new Person()
            //{
            //    FirstName = "Standard 1",
            //    LastName = "First Standard",
            //    DateOfBirth = DateTime.Now,
            //    Address = "75 st alphonsus street, 90001, cityview at longwood, Boston, MA, 02120",
            //    Hobbies = "Swimmig, Ping Pong, Running, Reading",
            //    Image = GetImageBytes(im)
            //});
           

            foreach (Person person in defaultPersons)
                context.Persons.Add(person);

            base.Seed(context);
        }



        public byte[] GetImageBytes(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            byte[] data = new byte[fileInfo.Length];

            return data;
        }
    }
}