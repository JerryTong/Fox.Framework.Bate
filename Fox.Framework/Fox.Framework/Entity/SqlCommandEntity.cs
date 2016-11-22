using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.Entity
{
    public class SqlCommandEntity
    {
        /// <summary>
        /// 對SQL Script 添加 where in 條件。 pass sqlparameter to IN()
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <param name="values"></param>
        public static void AddWithGroupValue(SqlCommand cmd, string parameter, List<string> values)
        {
            List<string> parameterList = new List<string>();
            int index = 1;
            foreach(var value in values)
            {
                parameterList.Add(string.Format("@{0}{1}", parameter, index));
                cmd.Parameters.AddWithValue(parameterList[index -1], value);

                index++;
            }

            string newParameter = string.Join(",", parameterList.ToArray());
            cmd.CommandText = cmd.CommandText.Replace("@" + parameter, newParameter);
        }
    }
}
