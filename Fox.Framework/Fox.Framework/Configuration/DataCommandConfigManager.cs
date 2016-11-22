using Fox.Framework.DataAccess;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Fox.Framework.Configuration
{
    public class DataCommandConfigManager
    {
        private static DataCommendConfiguration m_DataCommendConfiguration = null;

        public static DataCommendConfiguration Current
        {
            get
            {
                if (m_DataCommendConfiguration == null)
                {
                    m_DataCommendConfiguration = ConfigAccessor.LoadConfig<DataCommendConfiguration>(@"Data", "DataCommend.config");
                }

                return m_DataCommendConfiguration;
            }
        }
    }

    [XmlRoot("DataCommendConfiguration")]
    public class DataCommendConfiguration
    {
        [XmlElement("DataCommend")]
        public List<DataCommend> DataCommendConfig { get; set; }
    }

    public class DataCommend
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("database")]
        public string Database { get; set; }

        [XmlText()]
        public string CommendString { get; set; }
    }
}
