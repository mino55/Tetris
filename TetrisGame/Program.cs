using System;
using System.Threading;
using Tetris;

namespace Tetris
{
    class Program
    {
        private static int _FPS = 60;

        private static Point _centerPoint = null;

        private static TetrisBoard _tetrisBoard = null;
        private static TetrisBoard _nextTetrimino = null;
        private static TetrisBoardOperator _tetrisBoardOperator = null;

        private static KeyReceiver _keyReceiver = null;

        private static Tetriminos.Factory _tetriminoFactory = null;

        private static GameStats _gameStats;

        private static PrintHelper _printHelper;

        private static int _dropTimer = 0;

        private static int LastChange = 0;

        static void Main(string[] args)
        {
            _keyReceiver = new KeyReceiver();
            _keyReceiver.startListening();

            _printHelper = new PrintHelper();

            while (true)
            {
                Menu();

                StartGame();

                GameLoop();
            }
        }

        private static Tetrimino CreateTetrimino()
        {
            return _tetriminoFactory.Random();
        }

        private static void Menu()
        {
            ColorHelper colorHelper = new ColorHelper();
            _tetriminoFactory = new Tetriminos.Factory(colorHelper);

            Console.Clear();
            Console.WriteLine("__________________");
            Console.WriteLine("_____ TETRIS _____");
            Console.WriteLine("Press enter to start playing...");
            Console.ReadLine();
        }

        private static void StartGame()
        {
            _gameStats = new GameStats(startLevel: 0,
                                       linesPerLevel: 10,
                                       effectLevelLimit: 10,
                                       speedIncreasePerEffectLevel: 90);

            _tetrisBoard = new TetrisBoard(10, 20);
            int spawnX = (_tetrisBoard.width / 2);
            int spawnY = 1;
            _centerPoint = new Point(spawnX, spawnY);

            _tetrisBoardOperator = new TetrisBoardOperator(_tetrisBoard);

            _tetrisBoardOperator.NewCurrentTetrimino(CreateTetrimino(), _centerPoint);
            _tetrisBoardOperator.NewNextTetrimino(CreateTetrimino(), _centerPoint);
            UpdateNextTetrmino();
        }

        private static void GameLoop()
        {
            while (true)
            {
                int dTime = (1000 / _FPS);

                DrawGame();

                if (_tetrisBoardOperator.CurrentTetriminoIsLocked)
                {
                    ResolveRows();
                    try { NextBlock(); }
                    catch (Exceptions.NoOverwriteBlockException)
                    {
                        DrawGameOver();
                        break;
                    }
                    UpdateNextTetrmino();
                }
                else MoveBlock(dTime);

                Thread.Sleep(dTime);
            }
        }

        private static void DrawGame()
        {
            if (LastChange != _tetrisBoard.ChangeCount)
            {
                Console.Clear();

                string gameFieldPrint = _printHelper.SimplePrint(_tetrisBoard, _nextTetrimino, _gameStats);
                Console.WriteLine(gameFieldPrint);

                LastChange = _tetrisBoard.ChangeCount;
            }
        }

        private static void MoveBlock(int dTime)
        {
            ProcessInput();
            DropTimer(dTime);
        }

        private static void ResolveRows()
        {
            if (_tetrisBoardOperator.Rows() > 0)
            {
                int lines = _tetrisBoardOperator.Rows();
                _gameStats.ScoreLines(lines);
                _tetrisBoardOperator.CleanRows();
            }
        }

        private static void DrawGameOver()
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.WriteLine("Score: " + _gameStats.Score);
            Console.WriteLine("Lines: " + _gameStats.Lines);
            Console.WriteLine("Level: " + _gameStats.Level);
            Console.WriteLine("Blocks: " + _gameStats.Shapes);
            Console.WriteLine("Press enter to restart game...");
            Console.ReadLine();
        }

        private static void NextBlock()
        {
            _tetrisBoardOperator.NextCurrentTetrimino();
            _tetrisBoardOperator.NewNextTetrimino(CreateTetrimino(), _centerPoint);
            _gameStats.RegisterTetrimino(_tetrisBoardOperator.CurrentTetrimino.Type());
        }

        private static void UpdateNextTetrmino()
        {
            Tetrimino current = _tetrisBoardOperator.NextTetrimino;
            _nextTetrimino = new TetrisBoard(4, 4);
            _nextTetrimino.AddTetriminoAt(current, new Point(1, 2));
        }

        private static void ProcessInput()
        {
            string key = null;
            if (_keyReceiver.isNewKey)
            {
                ConsoleKeyInfo keyInput = _keyReceiver.Key();
                key = keyInput.Key.ToString();
            }

            switch (key)
            {
                case "A":
                    _tetrisBoardOperator.MoveCurrentTetriminoLeft();
                    break;
                case "D":
                    _tetrisBoardOperator.MoveCurrentTetriminoRight();
                    break;
                case "W":
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.FLIP);
                    break;
                case "Q":
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.REVERSE);
                    break;
                case "E":
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.CLOCKWISE);
                    break;
                case "S":
                    _tetrisBoardOperator.DropCurrentTetrimino();
                    break;
                case "Spacebar":
                    _tetrisBoardOperator.SlamCurrentTetrimino();
                    break;
                default:
                    break;
            }
        }

        private static void DropTimer(int dTime)
        {
            int dropTime = 1000 - (100 * _gameStats.Level);

            _dropTimer -= dTime;
            if (_dropTimer <= 0)
            {
                 _tetrisBoardOperator.DropCurrentTetrimino();
                 _dropTimer = dropTime;
            }
        }

        private static void AcceptInput() {}
    }
}
