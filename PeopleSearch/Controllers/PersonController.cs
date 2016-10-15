using System.Linq;
using System.Web.Mvc;
using PeopleSearch.Models;
using System.IO;
using PeopleSearch.ViewModels;
using PeopleSearch.ServiceFactory;
using System.Collections.Generic;
using System;
using PeopleSearch.Helper;

namespace PeopleSearch.Controllers
{
    public class PersonController : Controller
    {
        public static byte[] imageBytes;

        public ActionResult Index()
        {
            // db.Person.ToList()
            IEnumerable<Person> persons = new List<Person>();
            return View(persons);
            // return PartialView("~/Views/Person/_Search.cshtml", new Person("fname", "lname", "dob", "add", new byte[10]));
        }

        public ActionResult Register()
        {
            return View(new Person());
        }

        #region Search-Main page, and Find-Partial page
        public ActionResult Search()
        {
            IEnumerable<Person> persons = new List<Person>();
            return View(persons);
        }

        [HttpGet]
        public ActionResult Find(string name)
        {
            List<Person> persons = PersonService.Find(name);
            List<PersonViewModel> resultPersons = PersonManager.GetPersonViewModels(persons);

            return View(resultPersons);
        }

        #endregion


        #region Create record in DB
        [HttpPost]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            Person person = PersonManager.GetPerson(personViewModel, imageBytes);
            PersonService.Create(person);

            return Json(new Person());
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            Stream stream = ImageHelper.GetAttachmentStream(Request.Files);
            imageBytes = ImageHelper.GetImageBytes(stream);

            return Json("File uploaded successfully");
        }

        #endregion

        public ActionResult Test(Person person)
        {
            return Json(person);
        }
    }
}