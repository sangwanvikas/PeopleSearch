using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.ServiceFactory
{
    public class PersonManager
    {
        PersonContext _db;
        PersonService _personService;

        public PersonManager()
        {
            _db = new PersonContext();
            _personService = new PersonService(_db);
        }

        public int Create(PersonViewModel personViewModel, byte[] imageBytes)
        {
            try
            {
                Person person = GetPerson(personViewModel, imageBytes);
                return _personService.Create(person);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Person GetPerson(PersonViewModel personViewModel, byte[] imageBytes)
        {
            try
            {
                if (string.IsNullOrEmpty(personViewModel.FirstName) && string.IsNullOrEmpty(personViewModel.LastName))
                {
                    return null;
                }
                else
                {
                    var person = new Person();
                    person.FirstName = personViewModel.FirstName;
                    person.LastName = personViewModel.LastName;
                    person.Address = personViewModel.Address;
                    person.DateOfBirth = personViewModel.DateOfBirth;
                    person.Image = imageBytes;
                    person.Hobbies = personViewModel.Hobbies;
                    person.Gender = personViewModel.Gender;

                    return person;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonViewModel> Search(string name)
        {
            try
            {
                List<Person> persons = _personService.Search(name);
                List<PersonViewModel> personViewModelList = PopulatePersonViewModelList(persons);

                return personViewModelList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<PersonViewModel> PopulatePersonViewModelList(List<Person> persons)
        {
            List<PersonViewModel> personViewModelList = new List<PersonViewModel>();

            foreach (Person person in persons)
            {
                PersonViewModel personViewModel = new PersonViewModel();
                personViewModel.FirstName = person.FirstName;
                personViewModel.LastName = person.LastName;
                personViewModel.Address = person.Address;
                personViewModel.DateOfBirth = person.DateOfBirth;
                personViewModel.Hobbies = person.Hobbies;
                personViewModel.Gender = person.Gender;
                personViewModel.Age = (float)(DateTime.Now.Month - person.DateOfBirth.Month) / 12;

                if (person.Image != null)
                {
                    var base64 = Convert.ToBase64String(person.Image);
                    string imgSrc = String.Format("data:image/jpeg;base64,{0}", base64);

                    personViewModel.Image = imgSrc;
                }

                personViewModelList.Add(personViewModel);
            }

            return personViewModelList;
        }




        public string SaveSeedData()
        {
            try
            {
               int recordsCount = _personService.RecordsCount();

                return "Connection established successfully. Seed data [" + recordsCount.ToString() + "] records saved in database!";
            }
            catch (Exception ex)
            {

                return String.Format("{0}{1}","Error Occured while loading seed data. Please try again with correct connectionstring configurations",ex.Message);
            }
        }
    }
}