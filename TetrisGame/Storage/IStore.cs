namespace Tetris
{
    interface IStore
    {
        void Add(string key, string value);

        void Add(string[] keys, string[] values);

        string Get(string key);

        string[] Keys();

        string[] Values();

        void Save();

        void Load();

        void Drop();
    }
}