using Fox.Framework.DataAccess;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Fox.Framework.Configuration
{
    public class DatabaseConfigManager
    {
        private static DatabaseConfiguration m_DatabaseConfiguration = null;

        public static DatabaseConfiguration Current
        {
            get
            {
                if (m_DatabaseConfiguration == null)
                {
                    m_DatabaseConfiguration = ConfigAccessor.LoadConfig<DatabaseConfiguration>(@"Data", "Database.config");
                }

                return m_DatabaseConfiguration;
            }
        }
    }

    [XmlRoot("DatabaseConfiguration")]
    public class DatabaseConfiguration
    {
        [XmlElement("Environment")]
        public List<DatabaseConfigEnvironment> Environments { get; set; }
    }

    public class DatabaseConfigEnvironment
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText()]
        public string ConnectionString { get; set; }
    }
}
