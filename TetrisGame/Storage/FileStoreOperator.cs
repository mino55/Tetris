namespace Tetris
{
    public class FileStoreOperator
    {
        private FileStore _fileStore;

        public FileStoreOperator(FileStore fileStore)
        {
            _fileStore = fileStore;

            _fileStore.Load();

            if (_fileStore.Keys().Length < DefaultStoreKeys().Length)
            {
                _fileStore.Drop();
                _fileStore.Add(DefaultStoreKeys(), DefaultStoreValues());
                _fileStore.Save();
            }
        }

        public string[] DefaultStoreKeys()
        {
            return new string[] {
                "fps", "controlls",
                "CPU", "CPU", "CPU", "CPU", "CPU"
            };
        }

        public string[] DefaultStoreValues()
        {
            return new string[] {
                "60", "simple",
                "1", "2", "3", "4", "5"
            };
        }
    }
}