using System.Collections.Generic;

namespace saitho.Calculator
{
    internal class MemoryStore
    {
        protected Dictionary<string, string> dictionary = new Dictionary<string, string>();

        protected static MemoryStore instance = null;

        public static MemoryStore getInstance() {
            if (instance == null)
            {
                instance = new MemoryStore();
            }
            return instance;
        }

        public string get(string key)
        {
            return dictionary[key];
        }
        public void set(string key, string value) { dictionary[key] = value; }

        public void remove(string key) { dictionary.Remove(key);}
    }
}
