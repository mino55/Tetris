namespace Tetris
{
    public class ScreenFactory
    {
        GameSettings _gameSettings = new GameSettings();

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
            return new GameScreen(this, _gameSettings);
        }

        public OptionsScreen CreateOptionsScreen()
        {
            return new OptionsScreen(this, _gameSettings);
        }

        public HighscoreScreen CreateHighscoreScreen()
        {
            return new HighscoreScreen(this);
        }
    }
}
