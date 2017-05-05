using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fox.Framework.RestfulAPI
{
    public class RestfulClient<T>
    {
        private string HOST;
        public NameValueCollection Parameter { get; }

        public RestfulClient(string url)
        {
            HOST = url;
            Parameter = new NameValueCollection();
        }

        /// <summary>
        /// 添加參數
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParam(string name, object value)
        {
            if(value == null)
            {
                value = string.Empty;
            }

            Parameter.Add(name, value.ToString());
        }

        public T Get()
        {
            var url = GetReqeustUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        public T Post(object body)
        {
            var url = GetReqeustUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            using(var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(body);

                streamWriter.Write(json);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        public T Put(object body)
        {
            var url = GetReqeustUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Put";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(body);

                streamWriter.Write(json);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        private string GetReqeustUrl()
        {
            if(Parameter.Count == 0)
            {
                return HOST;
            }

            string url = HOST + "?";
            foreach(string key in Parameter.Keys)
            {
                url += string.Format("{0}={1}&", key, Parameter[key]);
            }

            return url;
        }

    }
}
