using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SourceCodeCounter
{
    public class CountLog
    {
        private static CountLog _instance = null;
        public static CountLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    string file = Application.StartupPath + @"\log.txt";
                    _instance = new CountLog(file);
                }
                return _instance;
            }
        }

        private string _logFile;
        public CountLog(string logFile)
        {
            _logFile = logFile;
        }

        public void Write(string fileName, int count, DateTime processTime)
        {
            string content = fileName + "\r\n";
            content = content + "行数:" + count.ToString() + "  at:" + processTime.ToString() + "\r\n";
            File.AppendAllText(_logFile, content, Encoding.Default);
        }
    }
}
