using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fox.Framework.RestfulAPI;
using System.Collections.Generic;

namespace Fox.Framework.Test
{
    [TestClass]
    public class RestfulAPITest
    {
        [TestMethod]
        public void TestMethod_Get()
        {
            var restful = new RestfulClient<List<Model>>("http://localhost:13915/api/portal/solr/dataimport/list");
            restful.AddParam("test", 123);
            var actual = restful.Get();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void TestMethod_Post()
        {
            var restful = new RestfulClient<dynamic>("http://localhost:13915/api/video/add");
            var actual = restful.Post(new { test = 123 });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMethod_Put()
        {
            var restful = new RestfulClient<dynamic>("http://localhost:13915/api/portal/solr/dataimport");
            var actual = restful.Put(new { InDate = DateTime.Now, Style = 2 });

            Assert.IsTrue(true);
        }
    }

    public class Model
    {
        public int Status { get; set; }
    }
}
