using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fox.Framework.Test.DataAccessTest.Model
{
    [XmlRoot("configuration")]
    public class Config
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("descript")]
        public string Description { get; set; }
    }
}
