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
        public static string AlbertEinsteinImageName = @"Albert_Einstein.jpg";
        public static string ImagesReferencePath = @"Common\images";
        public static string ProjectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}