using System.Collections.Generic;
using Xunit;

namespace Tetris.Tests
{
    public class BoardTests
    {
        private readonly Board _board;

        private void FillBoardRowAt(int rowAt)
        {
            for (int x = 0; x < _board.Width; x++ )
            {
                _board.AddBlockAt(new Block(), new Point(x, rowAt));
            }
        }

        public BoardTests()
        {
            _board = new Board(5, 5);
        }

        [Fact]
        public void BlockAt_WhenSpotEmpty_ReturnNull()
        {
            Point point = new Point(1, 2);

            Block result = _board.BlockAt(point);

            Assert.Null(result);
        }

        [Fact]
        public void BlockAt_WhenSpotTaken_ReturnBlock()
        {
            Point point = new Point(1, 2);
            Block block = new Block();
            _board.AddBlockAt(block, point);

            Block result = _board.BlockAt(point);

            Assert.Equal(result, block);
        }

        [Fact]
        public void AddBlockAt_SpotEmpty_AddBlock()
        {
            Point point = new Point(1, 2);
            Block block = new Block();

            _board.AddBlockAt(block, point);

            Assert.Equal(_board.BlockAt(point), block);
            Assert.Equal(_board.BlockPoint(block), point);
        }

        [Fact]
        public void AddBlockAt_SpotTaken_ThrowException()
        {
            Point point = new Point(1, 2);
            Block firstBlock = new Block();
            _board.AddBlockAt(firstBlock, point);

            Block secondBlock = new Block();

            Assert.Throws<Exceptions.NoOverwriteBlockException>(
               () => _board.AddBlockAt(secondBlock, point)
            );
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(5, 0)]
        [InlineData(0, 5)]
        public void AddBlockAt_OutsideBoard_ThrowException(int x, int y)
        {
            Point point = new Point(x, y);
            Block block = new Block();

            Assert.Throws<Exceptions.BlockOutsideBoardException>(
               () => _board.AddBlockAt(block, point)
            );
        }

        [Fact]
        public void RemoveBlockAt_SpotTaken_RemoveBlock()
        {
            Point point = new Point(1, 2);
            Block block = new Block();
            _board.AddBlockAt(block, point);

            _board.RemoveBlockAt(point);

            Assert.Null(_board.BlockAt(point));
            Assert.Null(_board.BlockPoint(block));
        }

        [Fact]
        public void RemoveBlockAt_SpotEmpty_ThrowException()
        {
            Point point = new Point(1, 2);
            Block block = new Block();

            Assert.Throws<Exceptions.MissingBlockException>(
               () => _board.RemoveBlockAt(point)
            );
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(5, 0)]
        [InlineData(0, 5)]
        public void RemoveBlockAt_OutsideBoard_ThrowException(int x, int y)
        {
            Point point = new Point(x, y);

            Assert.Throws<Exceptions.BlockOutsideBoardException>(
               () => _board.RemoveBlockAt(point)
            );
        }

        [Fact]
        public void RemoveBlock_BlockPlaced_RemoveBlock()
        {
            Point point = new Point(1, 2);
            Block block = new Block();
            _board.AddBlockAt(block, point);

            _board.RemoveBlock(block);

            Assert.Null(_board.BlockAt(point));
            Assert.Null(_board.BlockPoint(block));
        }

        [Fact]
        public void RemoveBlock_BlockUnplaced_DoNothing()
        {
            Block block = new Block();

            _board.RemoveBlock(block);

            Assert.Null(_board.BlockPoint(block));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void MoveBlock_WithoutHindrance_MoveBlock(int move_x, int move_y)
        {
            Point startPoint = new Point(1, 2);
            Block block = new Block();
            _board.AddBlockAt(block, startPoint);

            Point byPoint = new Point(move_x, move_y);
            _board.MoveBlock(block, byPoint);

            Point endPoint = Point.AddPoints(startPoint, byPoint);
            Assert.Null(_board.BlockAt(startPoint));
            Assert.Equal(_board.BlockAt(endPoint), block);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void MoveBlock_AgainstOtherBlock_DontMoveBlock(int move_x, int move_y)
        {
            Point startPoint = new Point(1, 2);
            Block block = new Block();
            _board.AddBlockAt(block, startPoint);

            Point byPoint = new Point(move_x, move_y);
            Point endPoint = Point.AddPoints(startPoint, byPoint);
            Block blockInTheWay = new Block();
            _board.AddBlockAt(blockInTheWay, endPoint);
            _board.MoveBlock(block, byPoint);

            Assert.Equal(_board.BlockAt(startPoint), block);
            Assert.Equal(_board.BlockAt(endPoint), blockInTheWay);
        }

        [Theory]
        [InlineData(0, 4, 0, 1)]
        [InlineData(4, 0, 1, 0)]
        [InlineData(0, 0, 0, -1)]
        [InlineData(0, 0, -1, 0)]
        public void MoveBlock_AgainstBoundary_DontMoveBlock(int start_x, int start_y, int move_x, int move_y)
        {
            Point startPoint = new Point(start_x, start_y);
            Block block = new Block();
            _board.AddBlockAt(block, startPoint);

            Point byPoint = new Point(move_x, move_y);
            _board.MoveBlock(block, byPoint);

            Assert.Equal(_board.BlockAt(startPoint), block);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void BlocksInRows_ThereAreRows_ReturnRowList(int numberOfRows)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                FillBoardRowAt(y);
            }

            List<Block[]> blockRows = _board.BlocksInRows();

            Assert.Equal(blockRows.Count, numberOfRows);
        }

        [Fact]
        public void BlocksInRows_ThereIsRow_RowHasBlocks()
        {
            FillBoardRowAt(0);

            Block[] blockRow = _board.BlocksInRows()[0];

            for (int x = 0;  x < _board.Width; x++ )
            {
                Assert.True(blockRow[x] is Block);
            }
        }

        [Fact]
        public void BlocksInRows_NoRows_ReturnEmptyRowList()
        {
            FillBoardRowAt(0);
            _board.RemoveBlockAt(new Point(4, 0));

            List<Block[]> blockRows = _board.BlocksInRows();

            Assert.Empty(blockRows);
        }

        [Fact]
        public void IsEmptyBelowBlock_SpotBelowEmpty_ReturnTrue()
        {
            Point point = new Point(0, 0);
            Block block = new Block();
            _board.AddBlockAt(block, point);

            bool result = _board.IsEmptyBelowBlock(block);

            Assert.True(result);
        }

        [Fact]
        public void IsEmptyBelowBlock_SpotBelowTaken_ReturnFalse()
        {
            Point point = new Point(0, 0);
            Block block = new Block();
            _board.AddBlockAt(block, point);
            Point otherPoint = new Point(0, 1);
            Block otherBlock = new Block();
            _board.AddBlockAt(otherBlock, otherPoint);

            bool result = _board.IsEmptyBelowBlock(block);

            Assert.False(result);
        }

        [Fact]
        public void IsEmptyBelowBlock_AtBoundary_ReturnFalse()
        {
            Point point = new Point(0, 4);
            Block block = new Block();
            _board.AddBlockAt(block, point);

            bool result = _board.IsEmptyBelowBlock(block);

            Assert.False(result);
        }

        [Fact]
        public void IsEmptyBelowBlock_UnplacedBlock_ThrowsError()
        {
            Point point = new Point(0, 0);
            Block block = new Block();

            Assert.Throws<Exceptions.BlockNotPlacedException>(
                () => _board.IsEmptyBelowBlock(block)
            );
        }
    }
}
