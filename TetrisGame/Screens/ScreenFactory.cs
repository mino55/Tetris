namespace Tetris
{
    public class ScreenFactory
    {
        private readonly GameSettings _gameSettings;
        private readonly FileStoreOperator _fileStoreOperator;
        private readonly ColorHelper _colorHelper;

        public ScreenFactory(GameSettings gameSettings,
                             FileStoreOperator fileStoreOperator,
                             ColorHelper colorHelper)
        {
            _fileStoreOperator = fileStoreOperator;
            _gameSettings = gameSettings;
            _colorHelper = colorHelper;
        }

        public MainScreen CreateMainScreen()
        {
            return new MainScreen(this, _gameSettings, _colorHelper);
        }

        public GameOverScreen CreateGameOverScreen(GameStats gameStats)
        {
            return new GameOverScreen(this,
                                      gameStats,
                                      _fileStoreOperator,
                                      _colorHelper);
        }

        public GameScreen CreateGameScreen()
        {
            GameScreen.KeyMapping keyMapping;

            if (_gameSettings.Controlls == "simple ")
                keyMapping = GameScreen.KeyMapping.SIMPLE;
            else if (_gameSettings.Controlls == "complex")
                keyMapping = GameScreen.KeyMapping.COMPLEX;
            else throw new System.Exception("Non-existant keyMapping");

            TetrisBoard tetrisBoard = new TetrisBoard(10, 20);
            TetrisBoardOperator tetrisBoardOperator = new TetrisBoardOperator(tetrisBoard);

            Tetriminos.Factory tetriminoFactory = new Tetriminos.Factory(_colorHelper);

            return new GameScreen(this, keyMapping, tetrisBoardOperator, tetriminoFactory);
        }

        public OptionsScreen CreateOptionsScreen()
        {
            return new OptionsScreen(this, _gameSettings, _fileStoreOperator, _colorHelper);
        }

        public HighscoreScreen CreateHighscoreScreen()
        {
            return new HighscoreScreen(this, _fileStoreOperator, _colorHelper);
        }
    }
}
