using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PeopleSearch
{
    public class Constants
    {
        // To load seed data (Location ~/Resources)
        public static string ImageDirectoryPath = @"~/Resources/Images/";
        public static string SeedDataXmlFilePath = @"~/Resources/SeedData/";
        public static string PersonXmlFileName = @"Persons.xml";
        public static string PersonNode = @"person";
        public static string DetailsNode = @"detail";
        public static string FirstNameNode = @"firstname";
        public static string LastNameNode = @"lastname";
        public static string AddressNode = @"address";
        public static string DateOfBirthNode = @"dataofbirth";
        public static string HobbiesNode = @"hobbies";
        public static string GenderNode = @"gender";
        public static string ImageNameNode = @"imagename";

        public static string ConnectionString = @"ConnectionString";
        public static string Equal = @"=";
        public static string DataSource = "DataSource";
        public static string Database = "Database";
        public static string IntegratedSecurity = @"IntegratedSecurity";
        public static string SemiColon = @";";
        public static string ForwardSlash { get; set; }




    }
}