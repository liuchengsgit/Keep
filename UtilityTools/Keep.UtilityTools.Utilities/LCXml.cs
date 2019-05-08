using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Keep.UtilityTools.Utilities
{
    public class LCXml
    {
        #region xml
        public static void XmlLoadString()
        {
            var label = @"Total torque capacity (bolt         connection + shear pin)";
            var debugDirectory = System.Environment.CurrentDirectory;  // 获取当前Debug目录
            var sourceDirectory = Directory.GetParent(debugDirectory).Parent.FullName.ToString();

            var fileName = "cpm_results.xml";
            var filePath = Path.Combine(sourceDirectory, "Files", fileName);

            StreamReader sr = new StreamReader(filePath);
            var str = sr.ReadToEnd();

            // 也可以用File.ReadAllText(filePath)方法读取文件内容
            //var originLines = File.ReadLines(filePath);
            //var str1 = File.ReadAllText(filePath);
            //bool isSame = str1 == str;

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(str);
            sr.Close();

            XmlNodeList nodes = xd.GetElementsByTagName("Paragraph");
            XmlNode node = null;
            XmlNode value = null;
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count - 5; i++)
                {
                    if (nodes[i].InnerText == label)
                    {
                        node = nodes[i];
                        value = nodes[i + 5];
                        break;
                    }
                }
            }

            Console.WriteLine(string.Format("Load xml document from string"));
            Console.WriteLine(string.Format("{0}: {1}", node.InnerText, value.InnerText));
            Console.ReadLine();
        }

        public static void XmlLoadFile()
        {
            var label = @"Total torque capacity (bolt         connection + shear pin)";
            var debugDirectory = System.Environment.CurrentDirectory;  // 获取当前Debug目录
            var sourceDirectory = Directory.GetParent(debugDirectory).Parent.FullName.ToString();

            var fileName = "cpm_results.xml";
            var filePath = Path.Combine(sourceDirectory, "Files", fileName);
            var originLines = File.ReadLines(filePath);

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(filePath);

            XmlNodeList nodes = xd.GetElementsByTagName("Paragraph");
            XmlNode node = null;
            XmlNode value = null;
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count - 5; i++)
                {
                    if (nodes[i].InnerText == label)
                    {
                        node = nodes[i];
                        value = nodes[i + 5];
                        break;
                    }
                }
            }

            Console.WriteLine(string.Format("Load xml document from file directly"));
            Console.WriteLine(string.Format("{0}: {1}", node.InnerText, value.InnerText));
            Console.ReadLine();
        }

        public static void XmlLoad()
        {
            //var label = @"Total torque capacity (bolt         connection + shear pin)";
            var debugDirectory = System.Environment.CurrentDirectory;  // 获取当前Debug目录
            var sourceDirectory = Directory.GetParent(debugDirectory).Parent.FullName.ToString();

            //var path = @"C:\Users\liucheng\Downloads";
            var fileName = "Station series.xml";
            var filePath = Path.Combine(sourceDirectory, "Files", fileName);
            var originLines = File.ReadLines(filePath);

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(filePath);
            var d = xd.DocumentElement;

            xd.DocumentElement.SetAttribute("name", "Test");
            //xd.Save(filePath);
        }

        public static void XmlLoadFileTvc()
        {
            //var label = @"Total torque capacity (bolt         connection + shear pin)";
            var debugDirectory = System.Environment.CurrentDirectory;  // 获取当前Debug目录
            var sourceDirectory = Directory.GetParent(debugDirectory).Parent.FullName.ToString();

            var fileName = "5RTflex50D-TV-14a.xml";
            var filePath = Path.Combine(sourceDirectory, "Files", fileName);
            var originLines = File.ReadLines(filePath);

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(filePath);

            XmlNodeList nodes = xd.GetElementsByTagName("Element");
            //XmlNode node = null;
            //XmlNode value = null;
            //if (nodes.Count > 0)
            //{
            //    for (int i = 0; i < nodes.Count - 5; i++)
            //    {
            //        if (nodes[i].InnerText == label)
            //        {
            //            node = nodes[i];
            //            value = nodes[i + 5];
            //            break;
            //        }
            //    }
            //}

            //Console.WriteLine(string.Format("Load xml document from file directly"));
            //Console.WriteLine(string.Format("{0}: {1}", node.InnerText, value.InnerText));
            Console.ReadLine();
            var nsmgr = new XmlNamespaceManager(xd.NameTable);
            nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
            var elasticCouplingNodesList = xd.SelectSingleNode("//Element[@xsi:type='Propeller']", nsmgr);
            Console.ReadLine();

        }
        #endregion
    }
}
