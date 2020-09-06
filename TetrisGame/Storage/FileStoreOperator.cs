using System.Collections.Generic;

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

        public void InsertHighscore(string name, int highscore)
        {
            int place = GetHighscorePlace(highscore);
            if (place == -1) return;

            List<string> NewNameOrder = new List<string>();
            List<string> NewValueOrder = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                NewNameOrder.Add(Store.Get($"h{i}_name"));
                NewValueOrder.Add(Store.Get($"h{i}_value"));
            }

            NewNameOrder.Insert((place - 1), name);
            NewNameOrder.RemoveAt(10);

            NewValueOrder.Insert((place - 1), $"{highscore}");
            NewValueOrder.RemoveAt(10);

            for (int i = 1; i <= 10; i++)
            {
                Store.Set($"h{i}_name", NewNameOrder[i - 1]);
                Store.Set($"h{i}_value", NewValueOrder[i - 1]);
            }
        }

        public int GetHighscorePlace(int score)
        {
            for (int i = 1; i <= 10; i++)
            {
                int placementScore = int.Parse(Store.Get($"h{i}_value"));
                if (score > placementScore) return i;
            }

            return -1;
        }

        private string[] DefaultStoreKeys()
        {
            return new string[] {
                "fps", "controlls",
                "h1_name", "h2_name", "h3_name", "h4_name", "h5_name",
                "h6_name", "h7_name", "h8_name", "h9_name", "h10_name",
                "h1_value", "h2_value", "h3_value", "h4_value", "h5_value",
                "h6_value", "h7_value", "h8_value", "h9_value", "h10_value"
            };
        }

        private string[] DefaultStoreValues()
        {
            return new string[] {
                "60", "simple ",
                "CPU", "CPU", "CPU", "CPU", "CPU",
                "CPU", "CPU", "CPU", "CPU", "CPU",
                "800", "400", "100", "60", "50",
                "40", "30", "20", "10", "-1"
            };
        }
    }
}