using System.Collections.Generic;

namespace Tetris
{
    public class FileStore : IStore
    {
        private readonly string _path;
        private List<string> _keys = new List<string>();
        private List<string> _values = new List<string>();

        public FileStore(string path)
        {
            _path = path;
        }

        public void Add(string key, string value)
        {
            _keys.Add(key);
            _values.Add(value);
        }

        public void Add(string[] keys, string[] values)
        {
            ValidateKeyValuePairs(keys, values);

            for (int i = 0; i < keys.Length; i++)
            {
                Add(keys[i], values[i]);
            }
        }

        public string Get(string key)
        {
            return _values[_keys.IndexOf(key)];
        }

        public void Set(string key, string value)
        {
            _values[_keys.IndexOf(key)] = value;
        }

        public string[] Keys()
        {
            return _keys.ToArray();
        }

        public string[] Values()
        {
            return _values.ToArray();
        }

        public void Save()
        {
            ValidateKeyValuePairs(_keys.ToArray(), _values.ToArray());

            string[] keyValuePairs = new string[_keys.Count];
            for (int i = 0; i < keyValuePairs.Length; i++)
            {
                keyValuePairs[i] = $"{_keys[i]}:{_values[i]}";
            }

            System.IO.File.WriteAllLines(_path, keyValuePairs);
        }

        public void Load()
        {
            Drop();

            try
            {
                string[] lines = System.IO.File.ReadAllLines(_path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] keyValuePairs = lines[i].Split(":");
                    string key = keyValuePairs[0];
                    string value = keyValuePairs[1];
                    _keys.Add(key);
                    _values.Add(value);
                }
            }
            catch (System.IO.FileNotFoundException) { }
        }

        private void ValidateKeyValuePairs(string[] keys, string[] values)
        {
            if (keys.Length != values.Length)
                throw new System.Exception("One value required per key.");
        }

        public void Drop()
        {
            _keys = new List<string>();
            _values = new List<string>();
        }
    }
}
