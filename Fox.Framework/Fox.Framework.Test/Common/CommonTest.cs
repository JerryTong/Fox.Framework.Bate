using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.Test.Common
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void TestToInt()
        {
            string str1 = "25";
            string str2 = "abc";

            Assert.AreEqual(25, str1.ToInt());
            Assert.AreEqual(0, str2.ToInt());
            Assert.AreEqual(-1, str2.ToInt(-1));
        }

       
    }
}
