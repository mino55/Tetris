using System;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(10, 25);
            board.AddTileAt(new Block(), new Point(1, 2));
            Console.WriteLine(board.Print());
            board.Fall();
            board.Fall();
            board.Fall();
            Console.WriteLine(board.Print());
        }
    }
}
