using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.SPipeline.Common
{
    internal class TestConstants
    {
        internal static class Urls
        {
            public static readonly string BaseUrl = "http://dev-trans-si-sql-test.dnvgl.com";
            public static readonly string LogIn = BaseUrl + "Login.aspx";
            public static readonly string Home = BaseUrl;
        }

        internal struct User
        {
            public static readonly string userName = "liucheng";
            public static readonly string password = "Password01";
            public static readonly string loginName = "Liu, Cheng";
        }

        internal static class Apis
        {
            #region Access
            public static readonly string Access = @"/api/Access";
            public static readonly string HasConfigureAccess = @"/api/Access/HasConfigureAccess";
            #endregion

            #region AnalysisModelGroups
            public static readonly string AMGOverview = @"/api/AnalysisModelGroups/Overview";
            public static readonly string AnalysisModelGroups = @"/api/AnalysisModelGroups";
            public static readonly string Delete = @"/api/AnalysisModelGroups";
            #endregion

            #region Login
            public static readonly string LogIn = @"/api/Login";
            #endregion

            #region User
            public static readonly string CurentUser = @"/api/currentuser";
            public static readonly string Users = @"/api/users";
            #endregion
        }
    }
}
