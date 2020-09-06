using System;

namespace Tetris
{
    public class GameOverScreen : MenuScreen
    {
        private Engine _engine;
        private GameStats _gameStats;
        private ScreenFactory _screenFactory;
        private FileStoreOperator _fileStoreOperator;
        string _name;

        public GameOverScreen(ScreenFactory screenFactory,
                              GameStats gameStats,
                              FileStoreOperator fileStoreOperator)
        {
            _screenFactory = screenFactory;
            _gameStats = gameStats;
            _fileStoreOperator = fileStoreOperator;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            // TODO:
            // compare score with all other scores staring from the top
            // only setup the below menuSelection if it fits in somewhere
            // otherwise print msg that user didnt make it on the list

            string[] letters = new string[] {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
            };

            menuSelection.AddSetting("letter 1", letters);
            menuSelection.AddSetting("letter 2", letters);
            menuSelection.AddSetting("letter 3", letters);
            menuSelection.AddPick("back");
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(_screenFactory.CreateMainScreen());
        }

        protected override void OnSetting(string name, string state, Engine engine)
        {}

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            menuPrint[2] = CenterAlign("GAME OVER");
            menuPrint[4] = CenterAlign($"Score: {_gameStats.Score}");
            menuPrint[5] = CenterAlign($"Lines: {_gameStats.Lines}");
            menuPrint[6] = CenterAlign($"Level: {_gameStats.Level}");
            menuPrint[7] = CenterAlign($"Blocks: {_gameStats.Shapes}");

            string letter1 = GetSettingState("letter 1");
            string letter1Hl = $"{HighlightableString("letter 1", $"{letter1}", Color.RED)}";

            string letter2 = GetSettingState("letter 2");
            string letter2Hl = $"{HighlightableString("letter 2", $"{letter2}", Color.RED)}";

            string letter3 = GetSettingState("letter 3");
            string letter3Hl = $"{HighlightableString("letter 3", $"{letter3}", Color.RED)}";

            menuPrint[10] = CenterAlign($"Enter your name:");
            menuPrint[12] = CenterAlign($"{letter1Hl} {letter2Hl} {letter3Hl}");
            _name = $"{letter1}{letter2}{letter3}";

            string back = $"{HighlightableString("back", "Continue", Color.RED)}";
            menuPrint[18] = CenterAlign(back);
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}

        protected override void OnLeave(Engine engine)
        {
            FileStore store = _fileStoreOperator.Store;
            store.Set("h1_name", _name);
            store.Set("h1_value", $"{_gameStats.Score}");
            store.Save();
        }
    }
}