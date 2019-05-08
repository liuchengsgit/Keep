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
    public class Users
    {
        private string baseUrl = TestConstants.Urls.BaseUrl;
        private CookieContainer cookieContainer = null;
        private string userName = TestConstants.User.userName;
        private string password = TestConstants.User.password;

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
        public void CurentUser()
        {
            var url = baseUrl + TestConstants.Apis.CurentUser;
            //var user = new User(username, password);
            //CookieContainer cc = Cookies.GetCookieContainer(user);

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
            Assert.AreEqual(this.userName.ToUpper(), jObject["key"].ToString().ToUpper());
        }

        [Test]
        public void AllUsers()
        {
            var url = baseUrl + TestConstants.Apis.Users;
            //var user = new User(username, password);
            //CookieContainer cc = Cookies.GetCookieContainer(user);

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

            //JObject jObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
            //Assert.AreEqual(this.userName.ToUpper(), jObject["key"].ToString().ToUpper());

            bool containUser = false;
            JArray jArray = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
            foreach (var tp in jArray)
            {
                if (tp["key"].ToString().ToUpper().Equals(this.userName.ToUpper()))
                {
                    containUser = true;
                    break;
                }
            }

            Assert.IsTrue(containUser, string.Format("Don't include the user: {0}", this.userName));

        }

        //[Test]
        //[Ignore("Only used for test the TestCase Attribute")]
        //[TestCase("liucheng", "Password")]
        public void TestCurentUser(string username, string password)
        {
            var url = baseUrl + TestConstants.Apis.CurentUser;
            var user = new User(username, password);
            CookieContainer cc = Cookies.GetCookieContainer(user);

            //Web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.CookieContainer = cc;   // use containaer by current user

            var response = (HttpWebResponse)request.GetResponse();
            Assert.AreEqual("OK", response.StatusCode.ToString());

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string readerContent = reader.ReadToEnd();

            JObject jObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(readerContent);
            Assert.AreEqual(this.userName.ToUpper(), jObject["key"].ToString().ToUpper());
        }
    }
}
