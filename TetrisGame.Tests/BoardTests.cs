using System;
using System.Collections.Generic;
using Xunit;

namespace Tetris.Tests
{
    public class BoardTests
    {
        private readonly Board _board;

        private void fillBoardRowAt(int rowAt)
        {
            for (int x = 0;  x < _board.width; x++ )
            {
                _board.AddTileAt(new Block(), new Point(x, rowAt));
            }
        }

        public BoardTests()
        {
            _board = new Board(5, 5);
        }

        [Fact]
        public void Tile_WhenSpotEmpty_ReturnNull()
        {
            Point point = new Point(1, 2);

            ITile result = _board.TileAt(point);

            Assert.Null(result);
        }

        [Fact]
        public void Tile_WhenSpotTaken_ReturnTile()
        {
            Point point = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, point);

            ITile tile = _board.TileAt(point);

            Assert.Equal(tile, block);
        }

        [Fact]
        public void AddTileAt_SpotEmpty_AddTile()
        {
            Point point = new Point(1, 2);
            Block block = new Block();

            _board.AddTileAt(block, point);

            Assert.Equal(_board.TileAt(point), block);
            Assert.Equal(_board.TilePoint(block), point);
        }

        [Fact]
        public void AddTileAt_SpotTaken_DontAddTile()
        {
            Point point = new Point(1, 2);
            Block firstBlock = new Block();
            _board.AddTileAt(firstBlock, point);

            Block secondBlock = new Block();
            _board.AddTileAt(secondBlock, point);

            ITile tile = _board.TileAt(point);
            Assert.Equal(tile, firstBlock);
        }

        [Fact]
        public void RemoveTileAt_SpotTaken_RemoveTile()
        {
            Point point = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, point);

            _board.RemoveTileAt(point);

            Assert.Null(_board.TileAt(point));
            Assert.Null(_board.TilePoint(block));
        }

        [Fact]
        public void RemoveTileAt_SpotEmpty_DoNothing()
        {
            Point point = new Point(1, 2);
            Block block = new Block();

            _board.RemoveTileAt(point);

            Assert.Null(_board.TileAt(point));
            Assert.Null(_board.TilePoint(block));
        }

        [Fact]
        public void RemoveTile_TilePlaced_RemoveTile()
        {
            Point point = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, point);

            _board.RemoveTile(block);

            Assert.Null(_board.TileAt(point));
            Assert.Null(_board.TilePoint(block));
        }

        [Fact]
        public void RemoveTile_TileUnplaced_DoNothing()
        {
            Block block = new Block();

            _board.RemoveTile(block);

            Assert.Null(_board.TilePoint(block));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void MoveTile_WithoutHindrance_MoveTile(int move_x, int move_y)
        {
            Point startPoint = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, startPoint);

            Point byPoint = new Point(move_x, move_y);
            _board.MoveTile(block, byPoint);

            Point endPoint = Point.AddPoints(startPoint, byPoint);
            Assert.Null(_board.TileAt(startPoint));
            Assert.Equal(_board.TileAt(endPoint), block);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void MoveTile_AgainstOtherTile_DontMoveTile(int move_x, int move_y)
        {
            Point startPoint = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, startPoint);

            Point byPoint = new Point(move_x, move_y);
            Point endPoint = Point.AddPoints(startPoint, byPoint);
            Block blockInTheWay = new Block();
            _board.AddTileAt(blockInTheWay, endPoint);
            _board.MoveTile(block, byPoint);

            Assert.Equal(_board.TileAt(startPoint), block);
            Assert.Equal(_board.TileAt(endPoint), blockInTheWay);
        }

        [Theory]
        [InlineData(0, 4, 0, 1)]
        [InlineData(4, 0, 1, 0)]
        [InlineData(0, 0, 0, -1)]
        [InlineData(0, 0, -1, 0)]
        public void MoveTile_AgainstBoundary_DontMoveTile(int start_x, int start_y, int move_x, int move_y)
        {
            Point startPoint = new Point(start_x, start_y);
            Block block = new Block();
            _board.AddTileAt(block, startPoint);

            Point byPoint = new Point(move_x, move_y);
            _board.MoveTile(block, byPoint);

            Point endPoint = Point.AddPoints(startPoint, byPoint);
            Assert.Equal(_board.TileAt(startPoint), block);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TilesInRows_ThereAreRows_ReturnRowList(int numberOfRows)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                fillBoardRowAt(y);
            }

            List<ITile[]> tileRows = _board.TilesInRows();

            Assert.Equal(tileRows.Count, numberOfRows);
        }

        [Fact]
        public void TilesInRows_ThereIsRow_RowHasTiles()
        {
            fillBoardRowAt(0);

            ITile[] tileRow = _board.TilesInRows()[0];

            for (int x = 0;  x < _board.width; x++ )
            {
                Assert.True(tileRow[x] is ITile);
            }
        }

        [Fact]
        public void TilesInRows_NoRows_ReturnEmptyRowList()
        {
            fillBoardRowAt(0);
            _board.RemoveTileAt(new Point(4, 0));

            List<ITile[]> tileRows = _board.TilesInRows();

            Assert.Empty(tileRows);
        }

        [Fact]
        public void IsEmptyBelowTile_SpotBelowEmpty_ReturnTrue()
        {
            Point point = new Point(0, 0);
            Block block = new Block();
            _board.AddTileAt(block, point);

            bool result = _board.IsEmptyBelowTile(block);

            Assert.True(result);
        }

        [Fact]
        public void IsEmptyBelowTile_SpotBelowTaken_ReturnFalse()
        {
            Point point = new Point(0, 0);
            Block block = new Block();
            _board.AddTileAt(block, point);
            Point otherPoint = new Point(0, 1);
            Block otherBlock = new Block();
            _board.AddTileAt(otherBlock, otherPoint);

            bool result = _board.IsEmptyBelowTile(block);

            Assert.False(result);
        }

        [Fact]
        public void IsEmptyBelowTile_AtBoundary_ReturnFalse()
        {
            Point point = new Point(0, 4);
            Block block = new Block();
            _board.AddTileAt(block, point);

            bool result = _board.IsEmptyBelowTile(block);

            Assert.False(result);
        }

        [Fact]
        public void IsEmptyBelowTile_UnplacedTile_ThrowsError()
        {
            Point point = new Point(0, 0);
            Block block = new Block();

            Assert.Throws<Tetris.Exceptions.TileNotPlacedException>(
                () => _board.IsEmptyBelowTile(block)
            );
        }
    }
}
