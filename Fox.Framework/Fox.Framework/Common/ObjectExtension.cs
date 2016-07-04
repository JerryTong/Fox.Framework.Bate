using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework
{
    public static class ObjectExtension
    {
        public static int ToInt(this string obj, int defaultValue = 0)
        {
            if(obj == null)
                return defaultValue;

            int result = 0;
            if (Int32.TryParse(obj.ToString(), out result))
                return result;

            return defaultValue;
        }
    }
}
