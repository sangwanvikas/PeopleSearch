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
    }
}
