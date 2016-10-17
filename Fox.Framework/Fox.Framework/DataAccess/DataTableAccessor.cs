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
