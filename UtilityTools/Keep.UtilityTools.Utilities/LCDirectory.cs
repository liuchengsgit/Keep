using System.IO;

namespace Keep.UtilityTools.Utilities
{
    public class LCDirectory
    {
        public static void ClearFolderFiles(string path)
        {
            var dInfo = new DirectoryInfo(path);

            // Delete files
            foreach (var fs in dInfo.GetFiles())
            {
                var filePath = Path.Combine(path, fs.Name);

                if (fs.IsReadOnly)
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                }

                File.Delete(filePath);
            }
        }

        public static string GetVersionInformation(string filePath)
        {
            //Assembly assembly = Assembly.LoadFile(filePath);			
            //var text =  assembly.GetName().Version.ToString();

            var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);
            return info.FileVersion;
        }

        public static void ClearFolderIncludeSubFolders(string path)
        {

            var dInfo = new DirectoryInfo(path);

            // Delete files
            foreach (var fs in dInfo.GetFiles())
            {
                var filePath = Path.Combine(path, fs.Name);
                if (fs.IsReadOnly)
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                }

                File.Delete(filePath);
            }

            // delete folders
            var subDirs = Directory.GetDirectories(path);
            foreach (var sub in subDirs)
            {
                ClearFolderIncludeSubFolders(sub);
                Directory.Delete(sub);  // delete the folder
            }
        }
    }
}
