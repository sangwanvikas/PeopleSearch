using System.Linq;
using System.Web.Mvc;
using System.IO;
using PeopleSearch.ViewModels;
using PeopleSearch.ServiceFactory;
using System.Collections.Generic;
using System;
using PeopleSearch.Helper;
using Microsoft.Win32;
using System.Web.Configuration;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using PeopleSearch.Areas.AreaPerson.Models;

namespace PeopleSearch.Areas.AreaPerson.Controllers
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
                conStringVM = ConnectionStringHelper.GetDefaultConnectionString();

                return View(conStringVM);
            }
            catch (Exception)
            {

                return View(new ConnectionStringViewModel());
            }

        }

        #region ConnectionString
        [HttpGet]
        public ActionResult ConfigurationResult(ConnectionString conStringObj)
        {

            string finalConnectionString = conStringObj.ToString();
            string provider = conStringObj.ProviderName;

            var settings = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName];
            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(settings, false);

            settings.ConnectionString = finalConnectionString;
            settings.ProviderName = conStringObj.ProviderName;

            _personManager = new PersonManager();
            string successOrErrorMsg = _personManager.SaveSeedData();
            conStringObj.Message = successOrErrorMsg;

            return View(conStringObj);

            //if (true)
            //{
            //    conString.Status = "SUCCESS";
            //    conString.Message = "Valid connection string. On the top right corenr of the page, click on Register link to add new person or click on Search link to find persons from seed data.";
            //    return View(conString);
            //}
            //else
            //{
            //    conString.Status = "ERROR";
            //    conString.Message = "Not a valid connection string. Please go back to configuration page and try with new connection string";
            //    return View(conString);
            //}  
        }
        #endregion


        #region Navigation buttons
        public ActionResult Register()
        {
            return View("Register");
          //  return View(new Person());

        }

        public ActionResult Search()
        {
            return View("Search");
            //IEnumerable<Person> persons = new List<Person>();
            //return View(persons);
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