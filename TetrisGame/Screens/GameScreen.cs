using System.Collections.Generic;

namespace Tetris
{
    public class GameScreen : IScreen
    {
        private int _dropTimer = 0;
        private bool _paused = false;
        private Engine _engine;
        private TetrisBoard _nextTetrimino;
        private Dictionary<string, Action> _keyMapping;
        private readonly Point _centerPoint;
        private readonly TetrisBoardOperator _tetrisBoardOperator;
        private readonly TetrisBoard _tetrisBoard;
        private readonly ScreenFactory _screenFactory;
        private readonly Tetriminos.Factory _tetriminoFactory;
        private readonly GameStats _gameStats = new GameStats(startLevel: 0,
                                                     linesPerLevel: 10,
                                                     effectLevelLimit: 10,
                                                     speedIncreasePerEffectLevel: 90);
        private readonly PrintHelper _printHelper = new PrintHelper();

        public GameScreen(ScreenFactory screenFactory,
                          KeyMapping keyMapping,
                          TetrisBoardOperator tetrisBoardOperator,
                          Tetriminos.Factory tetriminoFactory)
        {
            _screenFactory = screenFactory;
            _tetriminoFactory = tetriminoFactory;

            SetKeyMapping(keyMapping);

            int spawnX = tetrisBoardOperator.TetrisBoard.Width / 2;
            int spawnY = 1;
            _centerPoint = new Point(spawnX, spawnY);

            _tetrisBoardOperator = tetrisBoardOperator;
            _tetrisBoard = _tetrisBoardOperator.TetrisBoard;
            _tetrisBoardOperator.NewCurrentTetrimino(CreateTetrimino(), _centerPoint);
            _tetrisBoardOperator.NewNextTetrimino(CreateTetrimino(), _centerPoint);
            UpdateNextTetrimino();
        }

        public void Mount(Engine engine)
        {
            _engine = engine;
        }

        public void Input(string input, int dTime)
        {
            if (input == "P") _paused = !_paused;

            if (_paused) return;

            if (_tetrisBoardOperator.CurrentTetriminoIsLocked)
            {
                ResolveRows();
                SpawnNextTetrimino();
                UpdateNextTetrimino();
            }
            else
            {
                if (input != null && _keyMapping.ContainsKey(input))
                {
                    MoveTetriminoOnInput(_keyMapping[input]);
                }
                CountDropTimer(dTime);
            }
        }

        public string[] Render()
        {
            string[] gameFieldPrint = _printHelper.PrintGame(_tetrisBoard, _nextTetrimino, _gameStats);

            if (_paused) RenderPauseScreen(gameFieldPrint);

            return gameFieldPrint;
        }

        public void Unmount(Engine engine) { }

        public void SetKeyMapping(KeyMapping keyMapping)
        {
            switch (keyMapping)
            {
                case KeyMapping.SIMPLE:
                    _keyMapping = CreateSimpleKeyMapping();
                    break;
                case KeyMapping.COMPLEX:
                    _keyMapping = CreateComplexKeyMapping();
                    break;
            }
        }

        private void SpawnNextTetrimino()
        {
            try
            {
                NextBlock();
            }
            catch (Exceptions.NoOverwriteBlockException)
            {
                _engine.SwitchScreen(_screenFactory.CreateGameOverScreen(_gameStats));
            }
        }

        private Tetrimino CreateTetrimino()
        {
            return _tetriminoFactory.Random();
        }

        private void ResolveRows()
        {
            if (_tetrisBoardOperator.Rows() > 0)
            {
                int lines = _tetrisBoardOperator.Rows();
                _gameStats.ScoreLines(lines);
                _tetrisBoardOperator.CleanRows();
            }
        }

        private void NextBlock()
        {
            _tetrisBoardOperator.NextCurrentTetrimino();
            _tetrisBoardOperator.NewNextTetrimino(CreateTetrimino(), _centerPoint);
            _gameStats.RegisterTetrimino(_tetrisBoardOperator.CurrentTetrimino.Type());
        }

        private void UpdateNextTetrimino()
        {
            Tetrimino current = _tetrisBoardOperator.NextTetrimino;
            _nextTetrimino = new TetrisBoard(5, 4);
            _nextTetrimino.AddTetriminoAt(current, new Point(2, 2));
        }

        private void RenderPauseScreen(string[] gameFieldPrint)
        {
            int lineLength = gameFieldPrint[0].Length;
            string line = _printHelper.PrintLine(lineLength);
            string msg = "Press P to unpause";
            gameFieldPrint[10] = line;
            gameFieldPrint[11] = _printHelper.PadOutStringCentered("PAUSED", lineLength);
            gameFieldPrint[12] = _printHelper.PadOutStringCentered(msg, lineLength);
            gameFieldPrint[13] = line;
        }

        private void MoveTetriminoOnInput(Action action)
        {
            switch (action)
            {
                case Action.MOVE_LEFT:
                    _tetrisBoardOperator.MoveCurrentTetriminoLeft();
                    break;
                case Action.MOVE_RIGHT:
                    _tetrisBoardOperator.MoveCurrentTetriminoRight();
                    break;
                case Action.ROTATE_FLIP:
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.FLIP);
                    break;
                case Action.ROTATE_CLOCKWISE:
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.CLOCKWISE);
                    break;
                case Action.ROTATE_REVERSE:
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.REVERSE);
                    break;
                case Action.DROP:
                    _tetrisBoardOperator.DropCurrentTetrimino();
                    break;
                case Action.SLAM:
                    _tetrisBoardOperator.SlamCurrentTetrimino();
                    break;
                default:
                    break;
            }
        }

        private void CountDropTimer(int dTime)
        {
            int dropTime = 1000 - (100 * _gameStats.Level);

            _dropTimer -= dTime;
            if (_dropTimer <= 0)
            {
                _tetrisBoardOperator.DropCurrentTetrimino();
                _dropTimer = dropTime;
            }
        }

        private Dictionary<string, Action> CreateSimpleKeyMapping()
        {
            Dictionary<string, Action> mapping = new Dictionary<string, Action>
            {
                ["A"] = Action.MOVE_LEFT,
                ["D"] = Action.MOVE_RIGHT,
                ["W"] = Action.ROTATE_CLOCKWISE,
                ["S"] = Action.DROP,

                ["LeftArrow"] = Action.MOVE_LEFT,
                ["RightArrow"] = Action.MOVE_RIGHT,
                ["UpArrow"] = Action.ROTATE_CLOCKWISE,
                ["DownArrow"] = Action.DROP,

                ["Spacebar"] = Action.SLAM,
                ["Enter"] = Action.SLAM
            };

            return mapping;
        }

        private Dictionary<string, Action> CreateComplexKeyMapping()
        {
            Dictionary<string, Action> mapping = new Dictionary<string, Action>
            {
                ["A"] = Action.MOVE_LEFT,
                ["D"] = Action.MOVE_RIGHT,
                ["W"] = Action.ROTATE_FLIP,
                ["S"] = Action.DROP,

                ["LeftArrow"] = Action.MOVE_LEFT,
                ["RightArrow"] = Action.MOVE_RIGHT,
                ["UpArrow"] = Action.ROTATE_FLIP,
                ["DownArrow"] = Action.DROP,

                ["E"] = Action.ROTATE_CLOCKWISE,
                ["Q"] = Action.ROTATE_REVERSE,
                ["Spacebar"] = Action.SLAM,
                ["Enter"] = Action.SLAM
            };

            return mapping;
        }

        public enum KeyMapping
        {
            SIMPLE,
            COMPLEX
        }

        public enum Action
        {
            MOVE_LEFT,
            MOVE_RIGHT,
            DROP,
            ROTATE_CLOCKWISE,
            ROTATE_REVERSE,
            ROTATE_FLIP,
            SLAM
        }
    }
}
