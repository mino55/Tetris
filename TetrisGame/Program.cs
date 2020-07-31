using System;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(10, 25);
            board.AddTileAt(new Block(), new Point(1, 2));
            BoardOperator boardOperator = new BoardOperator(board);
            Console.WriteLine(board.Print());
            boardOperator.Fall();
            boardOperator.Fall();
            boardOperator.Fall();
            Console.WriteLine(board.Print());
        }
    }
}
