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

            List<string> newNameOrder = new List<string>();
            List<string> newValueOrder = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                newNameOrder.Add(Store.Get($"h{i}_name"));
                newValueOrder.Add(Store.Get($"h{i}_value"));
            }

            newNameOrder.Insert(place - 1, name);
            newNameOrder.RemoveAt(10);

            newValueOrder.Insert(place - 1, $"{highscore}");
            newValueOrder.RemoveAt(10);

            for (int i = 1; i <= 10; i++)
            {
                Store.Set($"h{i}_name", newNameOrder[i - 1]);
                Store.Set($"h{i}_value", newValueOrder[i - 1]);
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
                "fps", "controlls", "color", "unicode",
                "h1_name", "h2_name", "h3_name", "h4_name", "h5_name",
                "h6_name", "h7_name", "h8_name", "h9_name", "h10_name",
                "h1_value", "h2_value", "h3_value", "h4_value", "h5_value",
                "h6_value", "h7_value", "h8_value", "h9_value", "h10_value"
            };
        }

        private string[] DefaultStoreValues()
        {
            return new string[] {
                "60", "simple ", "none", "limited",
                "CPU", "CPU", "CPU", "CPU", "CPU",
                "CPU", "CPU", "CPU", "CPU", "CPU",
                "800", "400", "100", "60", "50",
                "40", "30", "20", "10", "-1"
            };
        }
    }
}
