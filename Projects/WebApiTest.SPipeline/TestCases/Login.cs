using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WebApiTest.SPipeline.Common;
using WebApiTest.SPipeline.Context;

namespace WebApiTest.SPipeline.TestCases
{
    [TestFixture]
    [Category("demo")]
    public class Login
    {
        private string baseUrl = TestConstants.Urls.BaseUrl;
        private string userName = TestConstants.User.userName;
        private string password = TestConstants.User.password;
        private string loginName = TestConstants.User.loginName;

        [OneTimeSetUp]
        public void Init()
        {
            //var user = new User(this.userName, this.password);
            //this.cookieContainer = Cookies.GetCookieContainer(user);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            /* ... */
        }

        [Test]
        public void LogIn()
        {
            //var jsonParas = "{username: \"liucheng\", password:\"Password01\"}";
            string jsonParas = string.Format("{{username: \"{0}\", password:\"{1}\"}}", this.userName, this.password);
            byte[] payload = Encoding.UTF8.GetBytes(jsonParas);

            //Web request
            var url = baseUrl + TestConstants.Apis.LogIn;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;

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

            Assert.AreEqual("OK", response.StatusCode.ToString());

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string readerContent = reader.ReadToEnd();
            Assert.IsTrue(readerContent.ToString().Contains(this.loginName));
        }
    }
}
