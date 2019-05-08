using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keep.UtilityTools.Utilities
{
    public class LCString
    {
        #region 字符串中多个连续空格转为一个空格
        /// <summary>
        /// 字符串中多个连续空格转为一个空格
        /// </summary>
        /// <param name="str">待处理的字符串</param>
        /// <returns>合并空格后的字符串</returns>
        public static string MergeSpace(string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 0)
            {
                str = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(str, " ");
            }
            return str;
        }
        #endregion

        #region GUID（全局统一标识符）
        /// <summary>
        /// GUID（全局统一标识符）是指在一台机器上生成的数字，它保证对在同一时空中的所有机器都是唯一的。
        /// 通常平台会提供生成GUID的API。生成算法很有意思，用到了以太网卡地址、纳秒级时间、芯片ID码和许多可能的数字。
        /// GUID的唯一缺陷在于生成的结果串会比较大.
        /// 1. 一个GUID为一个128位的整数(16字节)，在使用唯一标识符的情况下，你可以在所有计算机和网络之间使用这一整数。
        /// 2. GUID 的格式为“xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx”，其中每个 x 是 0-9 或 a-f 范围内的一个十六进制的数字。例如：337c7f2b-7a34-4f50-9141-bab9e6478cc8 即为有效的 GUID 值。
        /// 3. 世界上（Koffer注：应该是地球上）的任何两台计算机都不会生成重复的 GUID 值。GUID 主要用于在拥有多个节点、多台计算机的网络或系统中，分配必须具有唯一性的标识符。
        /// 4. 在 Windows 平台上，GUID 应用非常广泛：注册表、类及接口标识、数据库、甚至自动生成的机器名、目录名等。
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            var guid = new Guid();
            guid = Guid.NewGuid();
            var str = guid.ToString();
            return str;
        }
        #endregion

    }
}
