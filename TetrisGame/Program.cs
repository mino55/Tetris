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

        private static Board _board = null;
        private static BoardOperator _boardOperator = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Menu();

                StartGame();

                GameLoop();
            }
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("__________________");
            Console.WriteLine("_____ TETRIS _____");
            Console.WriteLine("Press enter to start paying...");
            Console.ReadLine();
        }

        private static void StartGame()
        {
            _score = 0;
            _level = 0;
            _blocks = 1;

            _board = new Board(5, 5);
            _centerPoint = new Point(_board.width / 2, 0);

            _boardOperator = new BoardOperator(_board);
            _boardOperator.NewCurrentBlock(new Block(), _centerPoint);
            _boardOperator.NewNextBlock(new Block(), _centerPoint);
        }

        private static void GameLoop()
        {
            while (true)
            {
                DrawGame();

                if (_boardOperator.currentBlockIsLocked)
                {
                    ResolveRows();

                    if (_board.TileAt(_centerPoint) != null) {
                        GameOver();
                        break;
                    }

                    NextBlock();
                }
                else
                {
                    string input = PromptInput();
                    ProcessInput(input);
                }
            }
        }

        private static void DrawGame()
        {
            Console.Clear();
            Console.WriteLine($"Score: {_score}, Blocks: {_blocks}, Level: {_level}");
            Console.WriteLine("");
            Console.WriteLine(_board.Print());
        }

        private static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.WriteLine("Score: " + _score);
            Console.WriteLine("Level: " + _level);
            Console.WriteLine("Blocks: " + _blocks);
            Console.WriteLine("Press enter to restart game...");
            Console.ReadLine();
        }

        private static void ResolveRows()
        {
            if (_boardOperator.Rows() > 0)
            {
                _score += (_boardOperator.Rows() * 100);
                _boardOperator.CleanRows();
            }
        }

        private static void NextBlock()
        {
            _boardOperator.NextCurrentBlock();
            _boardOperator.NewNextBlock(new Block(), _centerPoint);
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
                    _boardOperator.DropCurrentBlock();
                    break;
                case "a":
                    _boardOperator.MoveCurrentBlockLeft();
                    break;
                case "d":
                    _boardOperator.MoveCurrentBlockRight();
                    break;
                case "w":
                    // _boardOperator.RotateCurrentBlock();
                    break;
                case "s":
                    _boardOperator.SlamCurrentBlock();
                    break;
                default:
                    break;
            }
        }
    }
}
