namespace Tetris
{
    class MainScreen : MenuScreen
    {
        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddPick("start");
            menuSelection.AddPick("options");
            menuSelection.AddPick("highscore");
            menuSelection.AddPick("quit");
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            string startGame = $"{HighlightableString("start", "Start Game", Color.RED)}";
            string options = $"{HighlightableString("options", "Options", Color.RED)}";
            string highscore = $"{HighlightableString("highscore", "Highscore", Color.RED)}";
            string quit = $"{HighlightableString("quit", "Quit", Color.RED)}";

            menuPrint[1] = CenterAlign("______________________");
            menuPrint[2] = CenterAlign("_______ TETRIS _______");
            menuPrint[4] = CenterAlign(startGame);
            menuPrint[6] = CenterAlign(options);
            menuPrint[8] = CenterAlign(highscore);
            menuPrint[10] = CenterAlign(quit);
        }

        protected override void OnPick(string selection, Engine engine)
        {
            switch(selection)
            {
                case "start":
                    engine.SwitchScreen(new GameScreen());
                    break;
                case "options":
                    engine.SwitchScreen(new OptionsScreen());
                    break;
                case "highscore":
                    engine.SwitchScreen(new HighscoreScreen());
                    break;
                case "quit":
                    engine.Stop();
                    break;
            }
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine) {}

        protected override void OnSetting(string name, string state, Engine engine)
        {}
    }
}