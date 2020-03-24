using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Apbd_Tutorial2.Models
{
   public class Student

    {

        
        [XmlAttribute]
        public string indexNumber { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }

        public DateTime BirthDate { get; set; }
        public  string Email { get; set; }
        

        [XmlElement(elementName:"mothersName'")]
        public string Mother { get; set; }

        [XmlElement(elementName: "fathersName'")]
        public string  Father { get; set; }

        XElement xmlTree = new XElement("Root",
         new XElement("Child1", 1),
          new XElement("Child2", 2),
         new XElement("Child3", 3),
        new XElement("Child4", 4),
         new XElement("Child5", 5)
    );



    }
}
