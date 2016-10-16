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

        PersonManager manager;
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Person> persons = new List<Person>();
                return View(persons);
            }
            catch (Exception)
            {

                throw;
            }

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
        public ActionResult Result(string name)
        {
            manager = new PersonManager();
            List<PersonViewModel> resultPersons = manager.Search(name);

            return View(resultPersons);
        }

        #endregion

        #region Create record in DB
        [HttpPost]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            manager = new PersonManager();
            int id = manager.Create(personViewModel, imageBytes);

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

    }
}