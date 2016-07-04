using Fox.Framework.DataAccess;
using Fox.Framework.Test.DataAccessTest.Model;
using Fox.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.Test.DataAccessTest
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void TestLoadConfig()
        {
            var actual = ConfigAccessor.LoadConfig<Config>(@"..\..\App_Data", "Config.config");
           
            Assert.IsNotNull(actual);
            Assert.AreEqual("Jerry", actual.Name);
            Assert.AreEqual("Hello World", actual.Description);
        }

        [TestMethod]
        public void TestLoadXmlData()
        {
            People actual = XmlDataAccessor.LoadXml<People>(@"..\..\App_Data", "People.xml");

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Man != null);
            Assert.AreEqual("Jerry", actual.Man[0].Name);
            Assert.AreEqual("Hello World", actual.Man[0].Description);
        }
    }
}
