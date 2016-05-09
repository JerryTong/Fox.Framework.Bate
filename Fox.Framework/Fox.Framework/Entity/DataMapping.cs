using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.Entity
{
    /// <summary>
    /// Mapping 屬性。適用於DataTable.
    /// </summary>
    public class DataMapping : System.Attribute
    {
        private string m_name;
        public DataMapping(string name)
        {
            this.m_name = name;
        }

        public string Name
        {
            get { return this.m_name; }
        }
    }
}
