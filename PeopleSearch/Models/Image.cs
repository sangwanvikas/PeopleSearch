using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.Models
{
    public class Image
    {
        public string Path { get; set; }
        public string Description { get; set; }

        public Image(string path, string desc)
        {
            Path = path;
            Description = desc;

        }
        
    }
}