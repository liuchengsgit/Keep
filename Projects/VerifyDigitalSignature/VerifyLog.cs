using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VerifyDigitalSignature
{
    public class VerifyLog
    {
        private static VerifyLog _Instance = null;
        public static VerifyLog Instance
        {
            get
            {
                if (_Instance == null)
                {
                    string file = Application.StartupPath + @"\log.txt";
                    _Instance = new VerifyLog(file);
                }
                return _Instance;
            }
        }

        private string _logFile;
        public VerifyLog(string logFile)
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
