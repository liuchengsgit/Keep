using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keep.UtilityTools.Utilities
{
    public class LCEnvironment
    {
        public static string GetEnvironmentVariable()
        {
            // 环境变量 TEMP
            return Environment.GetEnvironmentVariable("TEMP");
        }

        public static void SetEnvironmentVariable()
        {
            // 环境变量 TEMP
            var value = @"C:\Users\liucheng\AppData\Local\Temp";
            var old = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("TEMP", value, EnvironmentVariableTarget.User);
            var old2 = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);
        }
    }
}
