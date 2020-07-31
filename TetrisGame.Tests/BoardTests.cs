using System;
using Xunit;

namespace Tetris.Tests
{
    public class BoardTests
    {
        private readonly Board _board;

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
        public void PlaceTile_SpotEmpty_PlaceTile()
        {
            Point point = new Point(1, 2);
            Block block = new Block();

            _board.AddTileAt(block, point);

            ITile tile = _board.TileAt(point);
            Assert.Equal(tile, block);
        }

        [Fact]
        public void PlaceTile_SpotTaken_DontPlaceTile()
        {
            Point point = new Point(1, 2);
            Block firstBlock = new Block();
            _board.AddTileAt(firstBlock, point);

            Block secondBlock = new Block();
            _board.AddTileAt(secondBlock, point);

            ITile tile = _board.TileAt(point);
            Assert.Equal(tile, firstBlock);
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
    }
}
