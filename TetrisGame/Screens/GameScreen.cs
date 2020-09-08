using System;
using System.Collections.Generic;

namespace Tetris
{
    public class GameScreen : IScreen
    {
        Engine _engine;

        private Point _centerPoint = null;
        private TetrisBoard _tetrisBoard = new TetrisBoard(10, 20);
        private TetrisBoard _nextTetrimino = null;
        private TetrisBoardOperator _tetrisBoardOperator = null;
        private Tetriminos.Factory _tetriminoFactory = new Tetriminos.Factory(new ColorHelper());
        private GameStats _gameStats = new GameStats(startLevel: 0,
                                                     linesPerLevel: 10,
                                                     effectLevelLimit: 10,
                                                     speedIncreasePerEffectLevel: 90);
        private PrintHelper _printHelper = new PrintHelper();
        private Dictionary<String, Action> _keyMapping;
        private ScreenFactory _screenFactory;
        private GameSettings _gameSettings;

        private int _dropTimer = 0;
        private int LastChange = 0;
        private bool _paused = false;

        public GameScreen(ScreenFactory screenFactory,
                          GameSettings gameSettings,
                          KeyMapping keyMapping)
        {
            _screenFactory = screenFactory;

            _gameSettings = gameSettings;

            SetKeyMapping(keyMapping);

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
                if (input != null &&_keyMapping.ContainsKey(input)) {
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

        public void Unmount(Engine engine) {}

        public void SetKeyMapping(KeyMapping keyMapping)
        {
            switch(keyMapping)
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

        private Dictionary<String, Action> CreateSimpleKeyMapping()
        {
            Dictionary<String, Action> mapping = new Dictionary<String, Action>();

            mapping["A"] = Action.MOVE_LEFT;
            mapping["D"] = Action.MOVE_RIGHT;
            mapping["W"] = Action.ROTATE_CLOCKWISE;
            mapping["S"] = Action.DROP;

            mapping["LeftArrow"] = Action.MOVE_LEFT;
            mapping["RightArrow"] = Action.MOVE_RIGHT;
            mapping["UpArrow"] = Action.ROTATE_CLOCKWISE;
            mapping["DownArrow"] = Action.DROP;

            mapping["Spacebar"] = Action.SLAM;
            mapping["Enter"] = Action.SLAM;

            return mapping;
        }

        private Dictionary<String, Action> CreateComplexKeyMapping()
        {
            Dictionary<String, Action> mapping = new Dictionary<String, Action>();

            mapping["A"] = Action.MOVE_LEFT;
            mapping["D"] = Action.MOVE_RIGHT;
            mapping["W"] = Action.ROTATE_FLIP;
            mapping["S"] = Action.DROP;

            mapping["LeftArrow"] = Action.MOVE_LEFT;
            mapping["RightArrow"] = Action.MOVE_RIGHT;
            mapping["UpArrow"] = Action.ROTATE_FLIP;
            mapping["DownArrow"] = Action.DROP;

            mapping["E"] = Action.ROTATE_CLOCKWISE;
            mapping["Q"] = Action.ROTATE_REVERSE;
            mapping["Spacebar"] = Action.SLAM;
            mapping["Enter"] = Action.SLAM;

            return mapping;
        }

        public enum KeyMapping {
            SIMPLE,
            COMPLEX
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