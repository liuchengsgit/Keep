using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WebApiTest.SPipeline.Common;
using WebApiTest.SPipeline.Context;

namespace WebApiTest.SPipeline.TestCases
{
    [TestFixture]
    [Category("API")]
    public class AnalysisModelGroups
    {
        private string baseUrl = TestConstants.Urls.BaseUrl;
        private CookieContainer cookieContainer = null;
        private string userName = TestConstants.User.userName;
        private string password = TestConstants.User.password;
        private string name = System.Guid.NewGuid().ToString();

        [OneTimeSetUp]
        public void Init()
        {
            var user = new User(this.userName, this.password);
            this.cookieContainer = Cookies.GetCookieContainer(user);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            /* ... */
        }

        [Test]
        public void AMGOverview()
        {
            var url = baseUrl + TestConstants.Apis.AMGOverview;

            //Web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.CookieContainer = this.cookieContainer;

            var response = (HttpWebResponse)request.GetResponse();
            Assert.AreEqual("OK", response.StatusCode.ToString());

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string readerContent = reader.ReadToEnd();

            JObject jObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
            Assert.IsTrue(jObject["groups"].Count<JToken>() > 0);
        }

        [Test]
        public void GetAnalysisModelGroups()
        {
            var url = baseUrl + TestConstants.Apis.AnalysisModelGroups;

            //Web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.CookieContainer = this.cookieContainer;

            var response = (HttpWebResponse)request.GetResponse();
            Assert.AreEqual("OK", response.StatusCode.ToString());

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string readerContent = reader.ReadToEnd();

            JArray jArray = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
            Assert.IsTrue(jArray.Count<JToken>() > 0);
        }

        [Test]
        [TestCase("", "")]
        public void PostAnalysisModelGroups(string oid, string name)
        {
            name = this.name;
            var url = baseUrl + TestConstants.Apis.AnalysisModelGroups;

            //var jsonParas = "{username: \"liucheng\", password:\"Password01\"}";
            string jsonParas = string.Format("{{oid: \"{0}\", name:\"{1}\", canDelete: true}}", oid, name);
            byte[] payload = Encoding.UTF8.GetBytes(jsonParas);

            //Web request
            //var url = baseUrl + TestConstants.Apis.LogIn;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;
            request.CookieContainer = this.cookieContainer;

            //CookieContainer cc = new CookieContainer();
            //request.CookieContainer = cc;

            Stream writer;
            try
            {
                writer = request.GetRequestStream();
            }
            catch (Exception)
            {
                writer = null;
                Console.WriteLine("failed");
            }

            writer.Write(payload, 0, payload.Length);
            writer.Close();

            HttpWebResponse response;
            try
            {
                //  获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Assert.AreEqual("NoContent", response.StatusCode.ToString());


            // Chect group exists or not
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.Method = "GET";
            request2.ContentType = "application/json";
            request2.CookieContainer = this.cookieContainer;

            var response2 = (HttpWebResponse)request2.GetResponse();
            Assert.AreEqual("OK", response2.StatusCode.ToString());

            Stream responseStream2 = response2.GetResponseStream();
            StreamReader reader2 = new StreamReader(responseStream2);
            string readerContent2 = reader2.ReadToEnd();

            JArray jArray = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent2);
            bool groupExist = false;
            foreach (var tp in jArray)
            {
                if (tp["name"].ToString().Equals(name))
                {
                    groupExist = true;
                    break;
                }
            }

            Assert.IsTrue(groupExist, string.Format("Don't include the group: {0}", name));
        }

        [Test]
        [TestCase("")]
        public void DeleteAnalysisModelGroups(string name)
        {
            name = this.name;

            List<string> oids = GetAnalysisModelGroupsOid(name);
            if (oids.Count > 0)
            {
                foreach (var oid in oids)
                {
                    var url = string.Format(@"{0}{1}/{2}", baseUrl, TestConstants.Apis.AnalysisModelGroups, oid);

                    //Web request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.CookieContainer = this.cookieContainer;

                    var response = (HttpWebResponse)request.GetResponse();
                    Assert.AreEqual("NoContent", response.StatusCode.ToString());

                    //Stream responseStream = response.GetResponseStream();
                    //StreamReader reader = new StreamReader(responseStream);
                    //string readerContent = reader.ReadToEnd();

                    //JObject jObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
                    //Assert.IsTrue(jObject["groups"].Count<JToken>() > 0);
                }

            }


            // Chect group exists or not
            var url2 = baseUrl + TestConstants.Apis.AnalysisModelGroups;
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
            request2.Method = "GET";
            request2.ContentType = "application/json";
            request2.CookieContainer = this.cookieContainer;

            var response2 = (HttpWebResponse)request2.GetResponse();
            Assert.AreEqual("OK", response2.StatusCode.ToString());

            Stream responseStream2 = response2.GetResponseStream();
            StreamReader reader2 = new StreamReader(responseStream2);
            string readerContent2 = reader2.ReadToEnd();

            JArray jArray = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent2);
            bool groupExist = false;
            foreach (var tp in jArray)
            {
                if (tp["name"].ToString().Equals(name))
                {
                    groupExist = true;
                    break;
                }
            }

            Assert.IsTrue(!groupExist, string.Format("The group: {0} isn't deleted", name));
        }

        private List<string> GetAnalysisModelGroupsOid(string name)
        {
            var url = baseUrl + TestConstants.Apis.AnalysisModelGroups;

            //Web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.CookieContainer = this.cookieContainer;

            var response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string readerContent = reader.ReadToEnd();

            JArray jArray = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
            List<string> oids = new List<string>();
            foreach (var tp in jArray)
            {
                if (tp["name"].ToString().Equals(name))
                {
                    oids.Add(tp["oid"].ToString());
                }
            }

            return oids;
        }
    }
}
