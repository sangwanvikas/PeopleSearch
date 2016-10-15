using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PeopleSearch.Models
{
    public class ImageModel : List<Image>
    {
        public ImageModel()
        {
            string directoryOfImage = HttpContext.Current.Server.MapPath("~/Resources/images/");
            XDocument imageData = XDocument.Load(directoryOfImage + @"/ImageMetaData.xml");
            var images = from image in imageData.Descendants("image")
                         select new Image(String.Format("{0}{1}",directoryOfImage,image.Element("filename").Value), image.Element("description").Value);
            this.AddRange(images.ToList<Image>());
        }
    }
}