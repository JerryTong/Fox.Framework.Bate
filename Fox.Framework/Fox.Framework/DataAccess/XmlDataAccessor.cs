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
        /// Register config path. Default: "/Configuration"
        /// </summary>
        /// <param name="path">New path.</param>
        public static void RegisterConfigPath(string path)
        {
            DataFolder = path;
        }

        /// <summary>
        /// Xml Document To Collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<T> LoadCollection<T>(string filePath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            filePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder, filePath);

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

            filePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder, filePath, fileName);

            StreamReader reader = new StreamReader(filePath);
            IEnumerable<T> response = (IEnumerable<T>)xs.Deserialize(reader);
            reader.Close();
            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadXml<T>(string fileName)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));

            var filePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder, fileName);

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

            if(string.IsNullOrEmpty(filePath))
                filePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder, fileName);
            else
                filePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder, filePath, fileName);

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
            Xmldoc.Load(Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder, fileName));

            XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
            DataSet ds = new DataSet();
            ds.ReadXml(Xmlreader);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// Xml Document with data table to collection .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<T> LoadCollectionWithTable<T>(string fileName)
        {
            return LoadCollectionWithTable<T>(Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, DataFolder), fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<T> LoadCollectionWithTable<T>(string root, string fileName)
        {
            XmlDocument Xmldoc = new XmlDocument();
            Xmldoc.Load(Path.Combine(root, fileName));

            XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
            DataSet ds = new DataSet();
            ds.ReadXml(Xmlreader);
            DataTable dt = ds.Tables[0];

            if (dt != null)
            {
                return DataTableAccessor.ToCollection<T>(dt);
            }

            return null;
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

        /// <summary>
        /// DataTable save to xml file.
        /// </summary>
        /// <param name="source">data table</param>
        /// <param name="file">file name</param>
        public static void Save(DataTable source, string file)
        {
            if (source == null)
            {
                throw new Exception("Save xml file error. The source can not null.");
            }

            string basePath = Path.Combine(System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase),
                                            DataFolder);

            Save(source, basePath, file);
        }

        /// <summary>
        /// DataTable save to xml file.
        /// </summary>
        /// <param name="source">data table</param>
        /// <param name="root">root path</param>
        /// <param name="file">file name</param>
        public static void Save(DataTable source, string root, string file)
        {
            if (source == null)
            {
                throw new Exception("Save xml file error. The source can not null.");
            }

            string basePath = Path.Combine(root, file);

            if (string.IsNullOrEmpty(basePath))
            {
                throw new Exception("Save xml file error. The path can not null or empty.");
            }

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            using (DataSet dataSet = new DataSet())
            {
                dataSet.Tables.Add(source.Copy());
                dataSet.Tables[0].TableName = "Table";
                dataSet.WriteXml(basePath, XmlWriteMode.WriteSchema);
            }
        }
    }

}

