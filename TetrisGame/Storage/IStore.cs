namespace Tetris
{
    interface IStore
    {
        void Add(string key, string value);

        void Add(string[] keys, string[] values);

        string Get(string key);

        void Set(string key, string value);

        string[] Keys();

        string[] Values();

        void Save();

        void Load();

        void Drop();
    }
}
