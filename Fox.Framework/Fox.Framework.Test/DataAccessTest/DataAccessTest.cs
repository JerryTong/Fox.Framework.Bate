using Fox.Framework.DataAccess;
using Fox.Framework.Test.DataAccessTest.Model;
using Fox.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
            var fields = typeof(Pokemon).GetFields();
            List<Pokemon> actual = XmlDataAccessor.LoadCollectionWithTable<Pokemon>("PokemonData.xml").ToList();
            
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual!= null);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual("妙蛙種子", actual[0].Name);
        }

        [TestMethod]
        public void TestSaveXmlData()
        {
            bool success = false;
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));

                DataRow newdr = dt.NewRow();
                newdr["Id"] = 1;
                newdr["Name"] = "妙蛙種子";
                DataRow newdr2 = dt.NewRow();
                newdr2["Id"] = 2;
                newdr2["Name"] = "妙蛙草";

                dt.Rows.Add(newdr);
                dt.Rows.Add(newdr2);

                XmlDataAccessor.Save(dt, "PokemonData.xml");
                success = true;
            }
            catch(Exception ex)
            {
                success = false;
            }

            Assert.IsTrue(success == true);
        }
    }
}
