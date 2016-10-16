using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch;
using PeopleSearch.Controllers;
using PeopleSearch.Models;
using PeopleSearch.ServiceFactory;
using PeopleSearch.ViewModels;
using PeopleSearch.DAL;

namespace PeopleSearch.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            PersonController controller = new PersonController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateMethod_PersonManagerClass_SavePersonWithImage()
        {
            // Arrange
            Person inputPerson = new Person();
            inputPerson.FirstName = "vikas";
            inputPerson.LastName = "sangwan";
            inputPerson.Address = "my address";
            inputPerson.Gender = "Male";
            inputPerson.Hobbies = "my hobbies";
            inputPerson.DateOfBirth = DateTime.Now;

            PersonViewModel inputPersonViewModel = new PersonViewModel(inputPerson.FirstName,
                inputPerson.LastName, inputPerson.DateOfBirth, inputPerson.Address, "", inputPerson.Gender);

            // Act
            int personId = PersonManager.Create(inputPersonViewModel, new byte[1]);
            List<Person> resultPersons = PersonProvider.SearchById(personId);

            // Assert
            if (resultPersons.Count != 1)
            {
                Assert.Fail();
            }

            Assert.IsTrue(inputPerson.Equals(resultPersons));

            
        }


    }
}
