using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Apbd_Tutorial2.Models
{
    public class University
    {
        [XmlAttribute(attributeName: "createdAt")]
        public DateTime time { get; set; }

        [XmlAttribute]
        public string author { get; set; }
        public students students { get; set; }
        public List<activeStudy> ActiveStudies { get; set; }
    }
}
