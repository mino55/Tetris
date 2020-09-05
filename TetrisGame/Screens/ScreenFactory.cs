namespace Tetris
{
    public class ScreenFactory
    {
        public ScreenFactory()
        {
            // TODO: in here we assign settings!
        }

        public MainScreen CreateMainScreen()
        {
            return new MainScreen(this);
        }

        public GameOverScreen CreateGameOverScreen(GameStats gameStats)
        {
            return new GameOverScreen(this, gameStats);
        }

        public GameScreen CreateGameScreen()
        {
            return new GameScreen(this);
        }

        public OptionsScreen CreateOptionsScreen()
        {
            return new OptionsScreen(this);
        }

        public HighscoreScreen CreateHighscoreScreen()
        {
            return new HighscoreScreen(this);
        }
    }
}
