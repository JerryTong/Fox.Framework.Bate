using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fox.Framework.Test.DataAccessTest.Model
{
    [XmlRoot("people")]
    public class People
    {
        [XmlElement("man")]
        public List<Man> Man { get; set; }
    }

    public class Man
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText()]
        public string Description { get; set; }
    }
}
