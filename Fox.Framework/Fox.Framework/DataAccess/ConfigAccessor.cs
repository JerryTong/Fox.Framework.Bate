using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.DataAccess
{
    public class ConfigAccessor
    {
        private static string CONFIG_PATH = "Configurations";

        /// <summary>
        /// Register config path. Default: "/Configuration"
        /// </summary>
        /// <param name="path">New path.</param>
        public static void RegisterConfigPath(string path)
        {
            CONFIG_PATH = path;
        }

        /// <summary>
        /// Load Config.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static T LoadConfig<T>(string config)
        {
            if (string.IsNullOrEmpty(config))
            {
                return default(T);
            }

            return LoadConfig<T>(Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, CONFIG_PATH), config);
        }

        public static T LoadConfig<T>(string path, string config)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(config))
            {
                return default(T);
            }

            return InternalLoadConfig<T>(path, config);
        }

        private static T InternalLoadConfig<T>(string path, string config)
        {
            return XmlDataAccessor.LoadXml<T>(path, config);
        }
    }


}
