using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SourceCodeCounter
{
    public class CountStrategy : ICountStrategy
    {
        private bool _IgnoreEmptyLine = true;
        private bool _IgnoreUsing = true;
        private bool _IgnoreCommend = true;
        private bool _IgnoreDesignCS = true;
        private bool _CurrentFileInCounting = true;

        public CountStrategy(bool ignoreEmptyLine, bool ignoreUsing, bool ignoreCommend, bool ignoreDesignCS)
        {
            _IgnoreEmptyLine = ignoreEmptyLine;
            _IgnoreUsing = ignoreUsing;
            _IgnoreCommend = ignoreCommend;
            _IgnoreDesignCS = ignoreDesignCS;
        }

        public int GetSourceCodeLines(string file)
        {
            //不处理.Designer.cs
            if (_IgnoreDesignCS && IsDesignFile(file))
            {
                _CurrentFileInCounting = false;
                return 0;
            }
            else _CurrentFileInCounting = true;

            string[] oriLines = File.ReadAllLines(file, Encoding.Default);

            int count = 0;
            foreach (string line in oriLines)
            {
                if (_IgnoreEmptyLine && line.Trim() == "") continue;
                if (_IgnoreUsing && ConainsUsingKeyWord(line)) continue;
                if (_IgnoreCommend && IsCommend(line)) continue;
                count++;
            }

            return count; //返回统计数
        }

        private bool _commendPrefixFound = false;
        private bool IsCommend(string line)
        {
            //
            //处理代码注释,这里可以用正规式处理。
            //
            // -> //
            // -> /**/
            // -> ///
            if (line.Trim().IndexOf("//") >= 0) return true;
            if (line.Trim().IndexOf("///") >= 0) return true; //可以与上行代码合并成一行,///包含///

            // /*注释开始
            if (line.Trim().IndexOf("/*") >= 0)
            {
                _commendPrefixFound = true;
                return true;
            }

            // /*注释结束
            if (line.Trim().IndexOf("*/") >= 0)
            {
                _commendPrefixFound = false;
                return true;
            }

            // /*注释中间部分
            if (_commendPrefixFound) return true;

            return false;
        }

        //不处理.Designer.cs
        private bool IsDesignFile(string file)
        {
            //加上##避免处理变态文件名如: abc.Designer.cs.Test.cs
            return (file + "##").ToUpper().IndexOf(".DESIGNER.CS##") > 0;
        }

        private bool ConainsUsingKeyWord(string line)
        {
            //保留Using后面的空格
            return line.ToUpper().Trim().IndexOf("USING ") == 0;
        }

        /// <summary>
        /// 当前处理的文件是否已统计
        /// </summary>
        public bool CurrentFileInCounting
        {
            get
            {
                return _CurrentFileInCounting;
            }
        }
    }
}
