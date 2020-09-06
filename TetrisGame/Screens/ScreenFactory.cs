namespace Tetris
{
    public class ScreenFactory
    {
        GameSettings _gameSettings;
        FileStoreOperator _fileStoreOperator;

        public ScreenFactory(GameSettings gameSettings,
                             FileStoreOperator fileStoreOperator)
        {
            _fileStoreOperator = fileStoreOperator;
            _gameSettings = gameSettings;
        }

        public MainScreen CreateMainScreen()
        {
            return new MainScreen(this);
        }

        public GameOverScreen CreateGameOverScreen(GameStats gameStats)
        {
            return new GameOverScreen(this, gameStats, _fileStoreOperator);
        }

        public GameScreen CreateGameScreen()
        {
            GameScreen.KeyMapping keyMapping;

            if (_gameSettings.Controlls == "simple ")
                keyMapping = GameScreen.KeyMapping.SIMPLE;
            else if (_gameSettings.Controlls == "complex")
                keyMapping = GameScreen.KeyMapping.COMPLEX;
            else throw new System.Exception("Non-existant keyMapping");

            return new GameScreen(this, _gameSettings, keyMapping);
        }

        public OptionsScreen CreateOptionsScreen()
        {
            return new OptionsScreen(this, _gameSettings, _fileStoreOperator);
        }

        public HighscoreScreen CreateHighscoreScreen()
        {
            return new HighscoreScreen(this, _fileStoreOperator);
        }
    }
}
