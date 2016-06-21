using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fox.Framework.DataAccess
{
    public class XmlDataAccessor
    {
        private static string DataFolder = "App_Data";

        /// <summary>
        /// Xml Document To Collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<T> LoadCollection<T>(string filePath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            filePath = Path.Combine(Environment.CurrentDirectory, DataFolder, filePath);

            StreamReader reader = new StreamReader(filePath);
            IEnumerable<T> response = (IEnumerable<T>)xs.Deserialize(reader);
            reader.Close();
            return response;
        }

        /// <summary>
        /// Xml Document To Collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<T> LoadCollection<T>(string filePath, string fileName)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            filePath = Path.Combine(Environment.CurrentDirectory, filePath, fileName);

            StreamReader reader = new StreamReader(filePath);
            IEnumerable<T> response = (IEnumerable<T>)xs.Deserialize(reader);
            reader.Close();
            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T LoadXml<T>(string filePath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            filePath = Path.Combine(Environment.CurrentDirectory, DataFolder, filePath);

            StreamReader reader = new StreamReader(filePath);
            T response = (T)xs.Deserialize(reader);
            reader.Close();
            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T LoadXml<T>(string filePath, string fileName)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            filePath = Path.Combine(Environment.CurrentDirectory, filePath, fileName);

            StreamReader reader = new StreamReader(filePath);
            T response = (T)xs.Deserialize(reader);
            reader.Close();
            return response;
        }

        /// <summary>
        /// Xml Document To DataTable.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable LoadDataTable(string fileName)
        {
            XmlDocument Xmldoc = new XmlDocument();
            Xmldoc.Load(Path.Combine(Environment.CurrentDirectory, DataFolder, fileName));

            XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
            DataSet ds = new DataSet();
            ds.ReadXml(Xmlreader);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// 儲存 Xml
        /// </summary>
        /// <param name="file"></param>
        /// <param name="obj"></param>
        public static void Save<T>(string file, object obj)
        {
            System.Xml.Serialization.XmlSerializer xsSubmit = new System.Xml.Serialization.XmlSerializer(typeof(T));

            System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                                        DataFolder,
                                                                        file));
            xsSubmit.Serialize(writer, obj);
            writer.Close();
        }

        /// <summary>
        /// 儲存 Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="root"></param>
        /// <param name="obj"></param>
        public static void Save<T>(string file, string root, object obj)
        {
            Save<T>(Path.Combine(root, file), obj);
        }
    }

}

