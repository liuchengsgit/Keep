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
    public class Access
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
        public void CurentAccess()
        {
            var url = baseUrl + TestConstants.Apis.Access;

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
            Assert.AreEqual(true.ToString().ToUpper(), jObject["configure"].ToString().ToUpper());
        }

        [Test]
        public void HasConfigureAccess()
        {
            var url = baseUrl + TestConstants.Apis.HasConfigureAccess;

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
            Assert.AreEqual(true.ToString().ToUpper(), readerContent.ToString().ToUpper());
        }
    }
}
