using System.Linq;
using System.Web.Mvc;
using PeopleSearch.Models;
using System.IO;
using PeopleSearch.ViewModels;
using PeopleSearch.ServiceFactory;
using System.Collections.Generic;
using System;
using PeopleSearch.Helper;
using Microsoft.Win32;
using System.Web.Configuration;
using System.Configuration;

namespace PeopleSearch.Controllers
{
    public class PersonController : Controller
    {
        public static byte[] imageBytes;

        PersonManager _personManager;

        public ActionResult Index()
        {
            try
            {
                ConnectionStringViewModel conStringVM = new ConnectionStringViewModel();
                conStringVM.ServerNameValue = ConnectionStringHelper.GetServerName();
                conStringVM.SqlServerInstances = ConnectionStringHelper.GetSqlInstanceNames();
                conStringVM.DatabaseValue = @"person";
                conStringVM.IntegratedSecurityValue = true;

                return View(conStringVM);
            }
            catch (Exception)
            {

                throw;
            }

        }

        #region ConnectionString
        public ActionResult SetConnectionString(ConnectionString conStringVM)
        {
            return Json(conStringVM);
        }
        #endregion


        #region Navigation buttons
        public ActionResult Register()
        {
            return View(new Person());
        }

        public ActionResult Search()
        {
            IEnumerable<Person> persons = new List<Person>();
            return View(persons);
        }

        #endregion

        #region Search and Results

        [HttpGet]
        public ActionResult Result(string name)
        {
            _personManager = new PersonManager();
            List<PersonViewModel> resultPersons = _personManager.Search(name);

            return View(resultPersons);
        }

        #endregion

        #region Create record in DB
        [HttpPost]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            _personManager = new PersonManager();
            int id = _personManager.Create(personViewModel, imageBytes);

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