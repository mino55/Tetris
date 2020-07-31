using System;
using Xunit;

namespace Tetris.Tests
{
    public class BoardOperatorTests
    {
        private readonly Board _board;
        private readonly BoardOperator _boardOperator;

        private void fillBoardRowAt(int rowAt)
        {
            for (int x = 0;  x < _board.width; x++ )
            {
                _board.AddTileAt(new Block(), new Point(x, rowAt));
            }
        }

        public BoardOperatorTests()
        {
            _board = new Board(5, 5);
            _boardOperator = new BoardOperator(_board);
        }

        [Fact]
        public void Fall_TilesAboveBottom_MoveTilesDown()
        {
            Point start_point = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, start_point);

            _boardOperator.Fall();

            Point end_point = new Point(1, 3);
            Assert.Null(_board.TileAt(start_point));
            Assert.Equal(_board.TileAt(end_point), block);
        }

        [Fact]
        public void Fall_TilesAtBottom_DontMoveTiles()
        {
            Point start_point = new Point(1, 4);
            Block block = new Block();
            _board.AddTileAt(block, start_point);

            _boardOperator.Fall();

            Point end_point = new Point(1, 4);
            Assert.Equal(_board.TileAt(end_point), block);
        }
    }
}