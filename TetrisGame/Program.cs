using System;
using System.Threading;
using Tetris;

namespace Tetris
{
    class Program
    {
        private static int _FPS = 60;
        private static int _score = 0;
        private static int _level = 0;
        private static int _blocks = 0;

        private static Point _centerPoint = null;

        private static TetrisBoard _tetrisBoard = null;
        private static TetrisBoardOperator _tetrisBoardOperator = null;

        private static KeyReceiver _keyReceiver = null;

        private static int _dropTimer = 0;

        static void Main(string[] args)
        {
            _keyReceiver = new KeyReceiver();
            _keyReceiver.startListening();

            while (true)
            {
                Menu();

                StartGame();

                GameLoop();
            }
        }

        private static Tetrimino CreateTetrimino()
        {
            return new Tetriminos.Factory().Random();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("__________________");
            Console.WriteLine("_____ TETRIS _____");
            Console.WriteLine("Press enter to start playing...");
            Console.ReadLine();
        }

        private static void StartGame()
        {
            _score = 0;
            _level = 0;
            _blocks = 1;

            _tetrisBoard = new TetrisBoard(10, 20);
            int spawnX = (_tetrisBoard.width / 2);
            int spawnY = 1;
            _centerPoint = new Point(spawnX, spawnY);

            _tetrisBoardOperator = new TetrisBoardOperator(_tetrisBoard);

            _tetrisBoardOperator.NewCurrentTetrimino(CreateTetrimino(), _centerPoint);
            _tetrisBoardOperator.NewNextTetrimino(CreateTetrimino(), _centerPoint);
        }

        private static void GameLoop()
        {
            while (true)
            {
                int dTime = (1000 / _FPS);

                DrawGame();

                if (_tetrisBoardOperator.currentTetriminoIsLocked)
                {
                    ResolveRows();
                    try { NextBlock(); }
                    catch (Exceptions.NoOverwriteBlockException)
                    {
                        DrawGameOver();
                        break;
                    }
                }
                else MoveBlock(dTime);

                Thread.Sleep(dTime);
            }
        }

        private static void DrawGame()
        {
            Console.Clear();
            Console.WriteLine($"Score: {_score}, Blocks: {_blocks}, Level: {_level}");
            Console.WriteLine("");
            Console.WriteLine(_tetrisBoard.Print());
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
                _score += (_tetrisBoardOperator.Rows() * 100);
                _tetrisBoardOperator.CleanRows();
            }
        }

        private static void DrawGameOver()
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.WriteLine("Score: " + _score);
            Console.WriteLine("Level: " + _level);
            Console.WriteLine("Blocks: " + _blocks);
            Console.WriteLine("Press enter to restart game...");
            Console.ReadLine();
        }

        private static void NextBlock()
        {
            _tetrisBoardOperator.NextCurrentTetrimino();
            _tetrisBoardOperator.NewNextTetrimino(CreateTetrimino(), _centerPoint);
            _blocks++;
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
            int dropTime = 1000 - (100 * _level);

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
