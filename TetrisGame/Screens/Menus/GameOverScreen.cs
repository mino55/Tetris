namespace Tetris
{
    public class GameOverScreen : MenuScreen
    {
        private string _name;
        private readonly GameStats _gameStats;
        private readonly ScreenFactory _screenFactory;
        private readonly FileStoreOperator _fileStoreOperator;
        private readonly int _place;

        public GameOverScreen(ScreenFactory screenFactory,
                              GameStats gameStats,
                              GameSettings gameSettings,
                              FileStoreOperator fileStoreOperator)
        : base(gameSettings)
        {
            _screenFactory = screenFactory;
            _gameStats = gameStats;
            _fileStoreOperator = fileStoreOperator;
            _place = _fileStoreOperator.GetHighscorePlace(_gameStats.Score);
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            if (_place > -1)
            {
                string[] letters = new string[] {
                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                    "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
                };

                menuSelection.AddSetting("letter 1", letters);
                menuSelection.AddSetting("letter 2", letters);
                menuSelection.AddSetting("letter 3", letters);
            }

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

            if (_place > -1) PromptForName(menuPrint);
            else menuPrint[9] = CenterAlign("You did not make it on the list.");

            string back = $"{HighlightableString("back", "Continue", Color.RED)}";
            menuPrint[18] = CenterAlign(back);
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}

        protected override void OnLeave(Engine engine)
        {
            if (_place > -1)
            {
                _fileStoreOperator.InsertHighscore(_name, _gameStats.Score);
                _fileStoreOperator.Store.Save();
            }
        }

        private void PromptForName(MenuLine[] menuPrint)
        {
            string letter1 = GetSettingState("letter 1");
            string letter1Hl = $"{HighlightableString("letter 1", $"{letter1}", Color.RED)}";

            string letter2 = GetSettingState("letter 2");
            string letter2Hl = $"{HighlightableString("letter 2", $"{letter2}", Color.RED)}";

            string letter3 = GetSettingState("letter 3");
            string letter3Hl = $"{HighlightableString("letter 3", $"{letter3}", Color.RED)}";

            menuPrint[10] = CenterAlign($"Enter your name:");
            menuPrint[12] = CenterAlign($"{letter1Hl} {letter2Hl} {letter3Hl}");
            _name = $"{letter1}{letter2}{letter3}";
        }
    }
}
