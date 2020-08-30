namespace Tetris
{
    class MainScreen : MenuScreen
    {
        protected override void RenderMenuItems(string[] menuPrint)
        {
            string startGame = $"{HighlightableString(0, "Start Game", Color.RED)}";
            string options = $"{HighlightableString(1, "Options", Color.RED)}";
            string highscore = $"{HighlightableString(2, "Highscore", Color.RED)}";
            string quit = $"{HighlightableString(3, "Quit", Color.RED)}";

            menuPrint[1] += CenterString("______________________", 22);
            menuPrint[2] += CenterString("_______ TETRIS _______", 22);
            menuPrint[4] += CenterString(startGame, 10);
            menuPrint[6] += CenterString(options, 7);
            menuPrint[8] += CenterString(highscore, 9);
            menuPrint[10] += CenterString(quit, 4);
        }

        protected override void Pick(int selection, Engine engine)
        {
            switch(selection)
            {
                case 0:
                    engine.SwitchScreen(new GameScreen());
                    break;
                case 1:
                    engine.SwitchScreen(new OptionsScreen());
                    break;
                case 2:
                    engine.SwitchScreen(new HighscoreScreen());
                    break;
                case 3:
                    engine.Stop();
                    break;
            }
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine) {}
    }
}