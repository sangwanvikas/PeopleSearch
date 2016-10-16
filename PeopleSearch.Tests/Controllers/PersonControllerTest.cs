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
//using EntityFramework;
using System.Data.Entity;
using Moq;

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
        public void TestCreateMethod_PersonServiceClass_SavePersonWithImage()
        {
            // Arrange
            Person inputPerson = new Person();
            inputPerson.FirstName = "vikas";
            inputPerson.LastName = "sangwan";
            inputPerson.Address = "my address";
            inputPerson.Gender = "Male";
            inputPerson.Hobbies = "my hobbies";
            inputPerson.DateOfBirth = DateTime.Now;
            
            
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);



            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());







        }



    }
}
