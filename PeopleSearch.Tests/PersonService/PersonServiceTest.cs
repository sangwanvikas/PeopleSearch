using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ServiceFactory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PeopleSearch.Tests
{
    [TestClass]
    public class PersonServiceTestMethods
    {
        [TestMethod]
        public void TestCreateMethod_SavePersonWithOutImage()
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

        [TestMethod]
        public void TestCreateMethodWithNullObject()
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
        public void TestCreateMethodWithEmptyFields()
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
        public void TestCreateMethod()
        {
            // Arrange

            Person inputPerson1 = new Person();
            inputPerson1.FirstName = "vikas";
            inputPerson1.LastName = "sangwan";
            inputPerson1.Address = "Boston";
            inputPerson1.Gender = "Male";
            inputPerson1.Hobbies = "reading books";
            inputPerson1.DateOfBirth = DateTime.Now;

            Person inputPerson2 = new Person();
            inputPerson2.FirstName = "sam";
            inputPerson2.LastName = "sangwan";
            inputPerson2.Address = "Sydney";
            inputPerson2.Gender = "Female";
            inputPerson2.Hobbies = "cooking";
            inputPerson2.DateOfBirth = DateTime.Now;


            Person inputPerson3 = new Person();
            inputPerson3.FirstName = "virat";
            inputPerson3.LastName = "sangwan";
            inputPerson3.Address = "Boston";
            inputPerson3.Gender = "Male";
            inputPerson3.Hobbies = "playing cricket";
            inputPerson3.DateOfBirth = DateTime.Now;

            Person inputPerson4 = new Person();
            inputPerson4.FirstName = "Isha";
            inputPerson4.LastName = "sangwan";
            inputPerson4.Address = "Sydney";
            inputPerson4.Gender = "Male";
            inputPerson4.Hobbies = "playing tennis";
            inputPerson4.DateOfBirth = DateTime.Now;


            var mockSet = new Mock<DbSet<Person>>();

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);


            // Act
            var service = new PersonService(mockContext.Object);
            service.Create(inputPerson1);
            service.Create(inputPerson2);
            service.Create(inputPerson3);
            service.Create(inputPerson4);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.AtLeast(4));
            mockContext.Verify(m => m.SaveChanges(), Times.AtLeast(4));

        }

        [TestMethod]
        public void TestSearchMethodWithSearchString()
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
        public void TestSearchMethodWithEmptySearchString()
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
        public void TestSearchMethodWithNullAsSearchString()
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
        public void TestSearchMethodWithSpecialCharsAsSearchString()
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
    }
}