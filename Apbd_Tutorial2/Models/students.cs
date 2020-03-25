using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Apbd_Tutorial2.Models
{
    public class students
    {
        [XmlElement(elementName: "student")]
        public HashSet<Student> listOfStudents = new HashSet<Student>(new customComparer());
    }
}
