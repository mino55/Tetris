using System;
using System.Collections.Generic;

namespace Tetris
{
    public class GameScreen : IScreen
    {
        Engine _engine;

        private Point _centerPoint = null;
        private TetrisBoard _tetrisBoard = null;
        private TetrisBoard _nextTetrimino = null;
        private TetrisBoardOperator _tetrisBoardOperator = null;
        private Tetriminos.Factory _tetriminoFactory = null;
        private GameStats _gameStats;
        private PrintHelper _printHelper;
        private Dictionary<String, Action> _keyMapping;

        private int _dropTimer = 0;
        private int LastChange = 0;

        public GameScreen()
        {
            _printHelper = new PrintHelper();

            _keyMapping = CreateMapping();

            _gameStats = new GameStats(startLevel: 0,
                                       linesPerLevel: 10,
                                       effectLevelLimit: 10,
                                       speedIncreasePerEffectLevel: 90);

            ColorHelper colorHelper = new ColorHelper();
            _tetriminoFactory = new Tetriminos.Factory(colorHelper);

            _tetrisBoard = new TetrisBoard(10, 20);
            int spawnX = (_tetrisBoard.width / 2);
            int spawnY = 1;
            _centerPoint = new Point(spawnX, spawnY);

            _tetrisBoardOperator = new TetrisBoardOperator(_tetrisBoard);

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
            if (_tetrisBoardOperator.CurrentTetriminoIsLocked)
            {
                ResolveRows();
                SpawnNextTetrimino();
                UpdateNextTetrimino();
            }
            else
            {
                if (input != null &&_keyMapping.ContainsKey(input)) {
                    MoveTetriminoOnInput(_keyMapping[input]);
                }
                CountDropTimer(dTime);
            }
        }

        public string Render()
        {
            string gameFieldPrint = _printHelper.SimplePrint(_tetrisBoard, _nextTetrimino, _gameStats);
            return gameFieldPrint;
        }

        private void SpawnNextTetrimino()
        {
            try
            {
                NextBlock();
            }
            catch (Exceptions.NoOverwriteBlockException)
            {
                _engine.SwitchScreen(new GameOverScreen(_gameStats));
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

        private Dictionary<String, Action> CreateMapping()
        {
            Dictionary<String, Action> mapping = new Dictionary<String, Action>();

            mapping["A"] = Action.MOVE_LEFT;
            mapping["D"] = Action.MOVE_RIGHT;
            mapping["W"] = Action.ROTATE_FLIP;
            mapping["E"] = Action.ROTATE_CLOCKWISE;
            mapping["Q"] = Action.ROTATE_REVERSE;
            mapping["S"] = Action.DROP;
            mapping["Spacebar"] = Action.SLAM;

            return mapping;
        }

        public enum Action {
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