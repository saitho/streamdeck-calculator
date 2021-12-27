using System.Collections.Generic;
using System.IO;

namespace saitho.Calculator
{
    internal class DataStorage
    {
        protected Dictionary<string, string> dictionary = new Dictionary<string, string>();

        protected static DataStorage instance = null;

        // File names
        protected string resultFileName = "result.txt";

        public static DataStorage getInstance()
        {
            if (instance == null)
            {
                instance = new DataStorage();
            }
            return instance;
        }

        public string readMemory(string key)
        {
            return dictionary[key];
        }

        public bool hasMemory(string key)
        {
            return dictionary.ContainsKey(key);
        }
        public void readMemory(string key, string value) { dictionary[key] = value; }

        public void deleteMemory(string key) { dictionary.Remove(key); }

        protected string buildFullFilePath(string filePath)
        {
            string roaming = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string pluginPath = Path.Combine(roaming, "Elgato", "StreamDeck", "Plugins", "com.saitho.calculator.sdPlugin", "_data");
            return Path.Combine(pluginPath, filePath);
        }

        public bool hasFile(string filePath)
        {
            string fullFilePath = this.buildFullFilePath(filePath);
            return File.Exists(fullFilePath);
        }

        public string readFile(string filePath)
        {
            string fullFilePath = this.buildFullFilePath(filePath);
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

        public string readResultFile()
        {
            return readFile(resultFileName);
        }

        public void writeResultFile(string content)
        {
            writeFile(resultFileName, content);
        }
    }
}
