using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Keep.UtilityTools.Utilities
{
    public class LCText
    {
        public static void GetTextFileContent()
        {
            var folder = @"C:\Repos\Machinery.GuiTest\NMCP_UIAutomation\NMCP_Regression\TestFiles\WorkflowManager";
            var fileName = "CalTool_NonEmpty.txt";
            var filePath = Path.Combine(folder, fileName);

            // method 1
            var str1 = File.ReadAllText(filePath);
            var str2 = File.ReadAllText(filePath, Encoding.UTF8);

            // method 2 
            var originLines = File.ReadLines(filePath);
            var allLines = new List<string>();
            allLines.Clear();
            foreach (var line in originLines)
            {
                allLines.Add(line);
            }

            // method 3
            var sr1 = new StreamReader(filePath);
            var str3 = sr1.ReadToEnd();
        }

        public static void ReadAndWriteTextFile()
        {
            var debugDirectory = System.Environment.CurrentDirectory;  // 获取当前Debug目录
            var directoryInfo = Directory.GetParent(debugDirectory).Parent;
            if (directoryInfo != null)
            {
                var sourceDirectory = directoryInfo.FullName;


                var fileName = "Cylinder025.txt";
                var filePath = Path.Combine(sourceDirectory, "Files", fileName);
                var originLines = File.ReadLines(filePath);

                var AllLines = new List<string>();
                AllLines.Clear();
                foreach (string line in originLines)
                {
                    AllLines.Add(line);
                }

                var fileName2 = "Cylinder500.txt";
                string path = Path.Combine(sourceDirectory, "Files", fileName2);
                try
                {
                    if (File.Exists(path))
                    {
                        // 如果文件为可读属性，则将其设为一般属性，然后删除
                        FileInfo fs = new FileInfo(path);
                        if (fs.IsReadOnly)
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                        }

                        File.Delete(path);
                    }

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        for (int i = 0; i < AllLines.Count / 20; i++)
                        {
                            string temp = AllLines[20 * i];
                            sw.WriteLine(temp);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
        }

        public bool IsFileInUse(string fileName)
        {
            bool inUse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                inUse = false;
            }
            catch
            {

            }
            finally
            {
                fs?.Close();
            }

            return inUse;
        }

        private void GetTextResults(string path, List<double> results)
        {
            if (File.Exists(path))
            {
                FileInfo fs = new FileInfo(path);
                if (fs.IsReadOnly)
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                }

                File.Delete(path);
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < results.Count / 8; i++)
                {
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 0]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 1]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 2]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 3]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 4]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 5]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 6]));
                    sw.Write(string.Format("{0:F9}, ", results[i * 8 + 7]));
                    sw.WriteLine();
                }
            }
        }

        public static void ClearTextContent(string txtPath)
        {
            using (FileStream stream = File.Open(txtPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                stream.Close();
            }  
        }

        public static void SaveTextContent(string txtPath, string content, bool saOrAp = false)
        {
            using (StreamWriter sw = new StreamWriter(txtPath, saOrAp)) // True: Append, False: overwrite
            {
                sw.WriteLine(content);
                sw.Close();
            }
        }
    }
}
