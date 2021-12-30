using System.Collections.Generic;
using System.IO;

namespace saitho.Calculator
{
    public class DataStorage
    {
        protected static DataStorage instance = null;

        // File names
        protected string resultFileName = "result.txt";

        protected string fileStorageName = "";

        public static DataStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataStorage();
                }
                return instance;
            }
        }

        public void setFileStorageName(string fileStorageName)
        {
            this.fileStorageName = fileStorageName;
        }

        public string getFileStorageName()
        {
            return this.fileStorageName;
        }
        protected string buildFullFilePath(string filePath, string overrideStorageName = "")
        {
            string roaming = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string pluginPath = Path.Combine(roaming, "Elgato", "StreamDeck", "Plugins", "com.saitho.calculator.sdPlugin", "_data");

            if (overrideStorageName != "")
            {
                pluginPath = Path.Combine(pluginPath, overrideStorageName);
            } else if (this.fileStorageName != "")
            {
                pluginPath = Path.Combine(pluginPath, this.fileStorageName);
            }

            return Path.Combine(pluginPath, filePath);
        }

        public bool hasFile(string filePath)
        {
            string fullFilePath = this.buildFullFilePath(filePath);
            return File.Exists(fullFilePath);
        }

        public string readFile(string filePath, string storageName = "")
        {
            string fullFilePath = this.buildFullFilePath(filePath, storageName);
            string content = "";

            using (var fs = File.Open(fullFilePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    content += sr.ReadToEnd();
                }
                fs.Close();
            }
            return content;
        }

        public void writeFile(string filePath, string content)
        {
            string fullFilePath = this.buildFullFilePath(filePath);
            string fullFileDirName = Path.GetDirectoryName(fullFilePath);
            if (!Directory.Exists(fullFileDirName))
            {
                Directory.CreateDirectory(fullFileDirName);
            }

            File.WriteAllText(fullFilePath, content);
        }

        public bool hasResultFile()
        {
            return hasFile(resultFileName);
        }

        public string readResultFile(string storageName = "")
        {
            return readFile(resultFileName, storageName);
        }

        public void writeResultFile(string content)
        {
            writeFile(resultFileName, content);
        }
    }
}
