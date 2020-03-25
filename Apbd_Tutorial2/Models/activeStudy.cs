using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;

namespace Apbd_Tutorial2.Models
{
    [XmlRootAttribute("studies")]
   public class activeStudy 
    {

        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute(attributeName: "numberOfStudents")]
        public Int32 numOfStudents { get; set; } = 0;

        
    }
}
