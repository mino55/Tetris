using System;
using Xunit;
using Tetris;

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
        }

        [Fact]
        public void Fall_TilesAboveBottom_MoveTilesDown()
        {
            Point start_point = new Point(1, 2);
            Block block = new Block();
            _board.AddTileAt(block, start_point);

            _board.Fall();

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

            _board.Fall();

            Point end_point = new Point(1, 4);
            Assert.Equal(_board.TileAt(end_point), block);
        }
    }
}
