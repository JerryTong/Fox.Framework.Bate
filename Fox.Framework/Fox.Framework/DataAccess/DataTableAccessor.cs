using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.DataAccess
{
    public class DataTableAccessor : IDataTableAccessor
    {
        /// <summary>
        /// DataTable To Collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToCollection<T>(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }

            return BindData<T>(dt);
        }

        /// <summary>
        /// Collection to DataTable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            if(items == null)
            {
                return null;
            }

            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// DataTable 資料繫結
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<T> BindData<T>(DataTable dt)
        {
            // Get all columns' name
            List<string> columns = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                columns.Add(dc.ColumnName);
            }

            // Get all fields
            var fields = typeof(T).GetFields();
            // Get all properties & attributes
            var propAttrs = GetObjectProperty<T>();

            List<T> response = new List<T>();
            foreach (DataRow tmpDr in dt.Rows)
            {
                // Create object
                var ob = Activator.CreateInstance<T>();

                // 字段名繫結
                foreach (var fieldInfo in fields)
                {
                    if (columns.Contains(fieldInfo.Name))
                    {
                        // Fill the data into the field
                        fieldInfo.SetValue(ob, tmpDr[fieldInfo.Name]);
                    }
                }

                // propertyInfo 屬性繫結
                foreach (var propertyInfo in propAttrs)
                {
                    if (propertyInfo.Value != null && columns.Contains(propertyInfo.Value.Name))
                    {
                        // Customer Mapping屬性繫結
                        if (tmpDr[propertyInfo.Value.Name] != DBNull.Value)
                        {
                            // Fill the data into the property
                            propertyInfo.Key.SetValue(ob, Convert.ChangeType(tmpDr[propertyInfo.Value.Name], propertyInfo.Key.PropertyType), null);
                        }
                    }
                    else if(columns.Contains(propertyInfo.Key.Name))
                    {
                        // Property name 屬性繫結]
                        if (tmpDr[propertyInfo.Key.Name] != DBNull.Value)
                        {
                            if (propertyInfo.Key.PropertyType.IsEnum)
                                propertyInfo.Key.SetValue(ob, Enum.Parse(propertyInfo.Key.PropertyType, Convert.ToString(tmpDr[propertyInfo.Key.Name])));
                            else
                                propertyInfo.Key.SetValue(ob, Convert.ChangeType(tmpDr[propertyInfo.Key.Name], propertyInfo.Key.PropertyType), null);
                        }
                    }
                }

                response.Add(ob);
            }

            return response;
        }

        /// <summary>
        /// 取得Customer Mapping 屬性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Dictionary<PropertyInfo, DataMapping> GetObjectProperty<T>()
        {
            Dictionary<PropertyInfo, DataMapping> propAttrs = new Dictionary<PropertyInfo, DataMapping>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var attrs = propertyInfo.GetCustomAttributes(true);
                DataMapping tmpMapping = null;
                foreach (var attr in attrs)
                {
                    tmpMapping = (attr as DataMapping);
                    if (tmpMapping != null)
                    {
                        break;
                    }
                }

                propAttrs.Add(propertyInfo, tmpMapping);
            }

            return propAttrs;
        }
    }

}
