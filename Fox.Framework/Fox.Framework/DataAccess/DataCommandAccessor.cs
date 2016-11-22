using Fox.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.DataAccess
{
    public class DataCommandAccessor
    {
        private static string CONFIG_PATH = "Configurations";

        private static Dictionary<string, EntityDataCommend> m_EntityDataCommends;
        private static Dictionary<string, EntityDatabaseEnvironment> m_EntityDatabaseEnvironment;

        /// <summary>
        /// 獲取SQL DataCommend 訊息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EntityDataCommend Get(string name)
        {
            if(m_EntityDataCommends == null)
            {
                BuildEntity();
            }

            return m_EntityDataCommends[name];
        }

        /// <summary>
        /// 建構Data entity
        /// </summary>
        private static void BuildEntity()
        {
            if(DatabaseConfigManager.Current != null)
            {
                if (DatabaseConfigManager.Current.Environments != null
                    && DatabaseConfigManager.Current.Environments.Count > 0)
                {
                    m_EntityDatabaseEnvironment = new Dictionary<string, EntityDatabaseEnvironment>();
                    DatabaseConfigManager.Current.Environments.ForEach(e =>
                    {
                        m_EntityDatabaseEnvironment.Add(e.Name, new EntityDatabaseEnvironment
                        {
                            Name = e.Name,
                            ConnectionString = e.ConnectionString,
                        });
                    });
                }
            }

            if (DataCommandConfigManager.Current != null && m_EntityDatabaseEnvironment != null)
            {
                if (DataCommandConfigManager.Current.DataCommendConfig != null 
                    && DataCommandConfigManager.Current.DataCommendConfig.Count > 0)
                {
                    m_EntityDataCommends = new Dictionary<string, EntityDataCommend>();
                    DataCommandConfigManager.Current.DataCommendConfig.ForEach(d => {

                        m_EntityDataCommends.Add(d.Name, new EntityDataCommend
                        {
                            SqlCommend = d.CommendString,
                            CommendName = d.Name,
                            Environment = m_EntityDatabaseEnvironment[d.Database],
                        });
                    });
                }
            }
        }
    }

    public class EntityDataCommend
    {
        public EntityDatabaseEnvironment Environment { get; set; }

        public string CommendName { get; set; }

        public string SqlCommend { get; set; }
    }

    public class EntityDatabaseEnvironment
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }
    }
}
