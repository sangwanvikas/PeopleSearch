using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.Models;
using PeopleSearch.ServiceFactory;
using PeopleSearch.DAL;
using System.Data.Entity;
using Moq;
using PeopleSearch.Controllers;

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
        public void TestCreateMethod_PersonService_SavePersonWithOutImage()
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

        //[TestMethod]
        //public void TestCreateMethod_PersonService_SavePersonWithImage()
        //{
        //    // Arrange
        //    Person inputPerson = new Person();
        //    inputPerson.FirstName = "vikas";
        //    inputPerson.LastName = "sangwan";
        //    inputPerson.Address = "my address";
        //    inputPerson.Gender = "Male";
        //    inputPerson.Hobbies = "my hobbies";
        //    inputPerson.DateOfBirth = DateTime.Now;

        //    Image img = System.Drawing.Image.FromFile("../../Resources/Images/Albert_Einstein.jpg");
        //    MemoryStream ms = new MemoryStream();
        //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    inputPerson.Image = ms.ToArray();

        //    var mockSet = new Mock<DbSet<Person>>();
        //    var mockContext = new Mock<PersonContext>();
        //    mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

        //    // Act
        //    var service = new PersonService(mockContext.Object);
        //    service.Create(inputPerson);

        //    // Assert
        //    mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());

        //}


        [TestMethod]
        public void TestCreateMethodWithNullObject_PersonService()
        {
            // Arrange
            Person inputPerson = null;
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);
            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());

        }

        [TestMethod]
        public void TestCreateMethodWithEmptyFields_PersonService_SavePersonWithOutImage()
        {
            // Arrange
            Person inputPerson = new Person();
            inputPerson.FirstName = "";
            inputPerson.LastName = "";
            inputPerson.Address = "";
            inputPerson.Gender = "";
            inputPerson.Hobbies = "";
            inputPerson.DateOfBirth = DateTime.Now;
            inputPerson.Image = null;

            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());

        }


        [TestMethod]
        public void TestCreateMethod_PersonService()
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
            service.Create(inputPerson);
            service.Create(inputPerson);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Exactly(3));
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(3));

        }

        [TestMethod]
        public void TestSearchMethodWithSearchString_PersonService()
        {
            var data = new List<Person>
            {
                new Person { FirstName = "Sangwan" ,LastName="Sam" },
                new Person { FirstName = "Monica",LastName="R" },
                new Person { FirstName = "Mahil",LastName="S" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            var service = new PersonService(mockContext.Object);

            List<Person> searchresults = service.Search("a");

            Assert.AreEqual(3, searchresults.Count());
            Assert.AreEqual("Sangwan", searchresults[0].FirstName);
            Assert.AreEqual("Monica", searchresults[1].FirstName);
            Assert.AreEqual("Mahil", searchresults[2].FirstName);
        }

        [TestMethod]
        public void TestSearchMethodWithEmptySearchString_PersonService()
        {
            // Arrange
            var data = new List<Person>().AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            string searchStr = string.Empty;
            List<Person> searchresults = service.Search(searchStr);

            // Assert
            Assert.AreEqual(null, searchresults);

        }

        [TestMethod]
        public void TestSearchMethodWithNullAsSearchString_PersonService()
        {
            // Arrange
            var data = new List<Person>().AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            string searchStr = null;
            List<Person> searchresults = service.Search(searchStr);

            // Assert
            Assert.AreEqual(null, searchresults);

        }

        [TestMethod]
        public void TestSearchMethodWithSpecialCharsAsSearchString_PersonService()
        {
            // Arrange
            var data = new List<Person>().AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSet.Object);

            // Act
            var service = new PersonService(mockContext.Object);
            string searchStr = "*>>>>{}/";
            List<Person> searchresults = service.Search(searchStr);

            // Assert
            Assert.AreEqual(0, searchresults.Count);

        }
    }
}
