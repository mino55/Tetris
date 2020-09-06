namespace Tetris
{
    public class FileStoreOperator
    {
        public FileStore Store { get; private set; }

        public FileStoreOperator(FileStore fileStore)
        {
            Store = fileStore;

            Store.Load();

            if (Store.Keys().Length < DefaultStoreKeys().Length)
            {
                Store.Drop();
                Store.Add(DefaultStoreKeys(), DefaultStoreValues());
                Store.Save();
            }
        }

        public string[] DefaultStoreKeys()
        {
            return new string[] {
                "fps", "controlls",
                "h1_name", "h2_name", "h3_name", "h4_name", "h5_name",
                "h6_name", "h7_name", "h8_name", "h9_name", "h10_name",
                "h1_value", "h2_value", "h3_value", "h4_value", "h5_value",
                "h6_value", "h7_value", "h8_value", "h9_value", "h10_value"
            };
        }

        public string[] DefaultStoreValues()
        {
            return new string[] {
                "60", "simple",
                "CPU", "CPU", "CPU", "CPU", "CPU",
                "CPU", "CPU", "CPU", "CPU", "CPU",
                "800", "400", "100", "60", "50",
                "40", "30", "20", "10", "-1"
            };
        }
    }
}