using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.IO;
using WebApiTest.SPipeline.Context;

namespace WebApiTest.SPipeline.Common
{
    class Cookies
    {
        public static CookieContainer GetCookieContainer(User user)
        {
            //var user = new User("liucheng", "Password01");
            var url = TestConstants.Urls.BaseUrl + TestConstants.Apis.LogIn;

            //var jsonParas = "{username: \"liucheng\", password:\"Password01\"}";
            string jsonParas = string.Format("{{username: \"{0}\", password:\"{1}\"}}", user.UserName, user.Password);
            byte[] payload = Encoding.UTF8.GetBytes(jsonParas);

            //Web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;

            CookieContainer cc = new CookieContainer();
            request.CookieContainer = cc;

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

            Stream s = response.GetResponseStream();
            StreamReader reader = new StreamReader(s);
            string str = reader.ReadToEnd();
            Console.WriteLine(str);

            CookieContainer cc2 = new CookieContainer();
            foreach (Cookie ck in response.Cookies)
            {
                cc2.Add(ck);
            }

            return cc2;
        }
    }
}
