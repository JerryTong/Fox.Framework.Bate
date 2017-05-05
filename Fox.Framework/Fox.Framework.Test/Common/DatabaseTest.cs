using Fox.Framework.Configuration;
using Fox.Framework.DataAccess;
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
    public class DatabaseTest
    {
        [TestMethod]
        public void TestDatabaseConnectionString()
        {
            var actual = DatabaseConfigManager.Current;

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Environments);
            Assert.IsTrue(actual.Environments.Count == 1);
            Assert.AreEqual("HAGDB", actual.Environments[0].Name);
            Assert.AreEqual(@"Data Source=192.168.1.12,1074\TONG-13FD54C2D1\SQLEXPRESS;Initial Catalog=HAGDB;User id=misa;Password=tong@1234;", actual.Environments[0].ConnectionString);
        }

        [TestMethod]
        public void TestDatabaseConnection()
        {
            var actual = DataCommandAccessor.Get("GetHAGMember");

            using (SqlConnection connection = new SqlConnection(actual.Environment.ConnectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Assert.IsTrue(false);
                }
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestDataCommend()
        {
            var actual = DataCommandConfigManager.Current;

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.DataCommendConfig);
            Assert.IsTrue(actual.DataCommendConfig.Count == 1);
            Assert.AreEqual("GetHAGMember", actual.DataCommendConfig[0].Name);
            Assert.AreEqual("Hello, world!", actual.DataCommendConfig[0].CommendString);
            Assert.AreEqual("HAGDB", actual.DataCommendConfig[0].Database);
        }

        [TestMethod]
        public void TestDatabaseAccessor()
        {
            var actual = DataCommandAccessor.Get("GetHAGMember");

            Assert.IsNotNull(actual);
            Assert.AreEqual("GetHAGMember", actual.CommendName);
            Assert.AreEqual("Hello, world!", actual.SqlCommend);
            Assert.AreEqual("HAGDB", actual.Environment.Name);
            Assert.AreEqual(@"Data Source=192.168.1.12,1074\TONG-13FD54C2D1\SQLEXPRESS;Initial Catalog=HAGDB;User id=misa;Password=tong@1234;", actual.Environment.ConnectionString);

        }
    }
}
