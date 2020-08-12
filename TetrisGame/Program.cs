using System;
using Tetris;

namespace Tetris
{
    class Program
    {
        private static int _score = 0;
        private static int _level = 0;
        private static int _blocks = 0;

        private static Point _centerPoint = null;

        private static TetrisBoard _tetrisBoard = null;
        private static TetrisBoardOperator _tetrisBoardOperator = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Menu();

                StartGame();

                GameLoop();
            }
        }

        private static Tetrimino CreateTetrimino()
        {
            Block[] blocks = new Block[Tetrimino.Size<Tetrimino>()];
            for(int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
            return new Tetrimino(Direction.UP, blocks);
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

            _tetrisBoard = new TetrisBoard(5, 6);
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
                else MoveBlock();
            }
        }

        private static void DrawGame()
        {
            Console.Clear();
            Console.WriteLine($"Score: {_score}, Blocks: {_blocks}, Level: {_level}");
            Console.WriteLine("");
            Console.WriteLine(_tetrisBoard.Print());
        }

        private static void MoveBlock()
        {
            string input = PromptInput();
            ProcessInput(input);
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

        private static string PromptInput()
        {
            // (left/right/rotate/slam)
            Console.WriteLine("(a/d/w/s):");
            string input = Console.ReadLine();
            return input;
        }

        private static void ProcessInput(String input)
        {
            switch (input)
            {
                case "":
                    _tetrisBoardOperator.DropCurrentTetrimino();
                    break;
                case "a":
                    _tetrisBoardOperator.MoveCurrentTetriminoLeft();
                    break;
                case "d":
                    _tetrisBoardOperator.MoveCurrentTetriminoRight();
                    break;
                case "w":
                    _tetrisBoardOperator.RotateCurrentTetrimino(Rotation.CLOCKWISE);
                    break;
                case "s":
                    _tetrisBoardOperator.SlamCurrentTetrimino();
                    break;
                default:
                    break;
            }
        }
    }
}
