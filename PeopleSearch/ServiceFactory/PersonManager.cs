using PeopleSearch.DAL;
using PeopleSearch.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.ServiceFactory
{
    public static class PersonManager
    {
        public static int Create(PersonViewModel personViewModel, byte[] imageBytes)
        {
            try
            {
                Person person = GetPerson(personViewModel, imageBytes);
                return PersonProvider.Create(person);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Person GetPerson(PersonViewModel personViewModel, byte[] imageBytes)
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

        public static List<PersonViewModel> Search(string name)
        {
            try
            {
                List<Person> persons = PersonService.Search(name);
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
    }
}