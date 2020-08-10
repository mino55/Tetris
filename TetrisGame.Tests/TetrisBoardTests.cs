using Xunit;

namespace Tetris
{
    public class TetrisBoardTests
    {
        private readonly TetrisBoard _tetrisBoard;

        public TetrisBoardTests()
        {
            _tetrisBoard = new TetrisBoard(5, 5);
        }

        [Theory]
        [InlineData(Direction.UP, 2, 1)]
        [InlineData(Direction.RIGHT, 3, 2)]
        [InlineData(Direction.DOWN, 2, 3)]
        [InlineData(Direction.LEFT, 1, 2 )]
        public void AddTetriminoAt_WithSpace_AddTetriminoBlocks(Direction direction,
                                                                int noseX,
                                                                int noseY)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(direction, blocks);
            Point point = new Point(2, 2);

            _tetrisBoard.AddTetriminoAt(tetrimino, point);

            Assert.Equal(2, _tetrisBoard.AllBlocks().Length);
            Assert.Equal(blocks[0], _tetrisBoard.BlockAt(new Point(noseX, noseY)));
            Assert.Equal(blocks[1], _tetrisBoard.BlockAt(point));
        }

        [Fact]
        public void AddTetriminoAt_TakenSpot_ThrowException()
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            Point point = new Point(2, 2);
            _tetrisBoard.AddBlockAt(new Block(), new Point (2, 2));

            Assert.Throws<Tetris.Exceptions.NoOverwriteBlockException>(
                () => _tetrisBoard.AddTetriminoAt(tetrimino, point)
            );
        }

        [Fact]
        public void AddTetriminoAt_OutsideSpot_ThrowException()
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            Point point = new Point(0, 0);

            Assert.Throws<Tetris.Exceptions.BlockOutsideBoardException>(
                () => _tetrisBoard.AddTetriminoAt(tetrimino, point)
            );
        }

        [Fact]
        public void RemoveTetrimino_TetriminoPlaced_RemoveTetriminoAndBlocks()
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point (2, 2));

            _tetrisBoard.RemoveTetrimino(tetrimino);

            Assert.Empty(_tetrisBoard.AllBlocks());
            Assert.Empty(_tetrisBoard.AllTetriminos());
            Assert.Null(_tetrisBoard.TetriminoPoint(tetrimino));
            Assert.Null(_tetrisBoard.BlockAt(new Point(2, 2)));
            Assert.Null(_tetrisBoard.BlockAt(new Point(2, 3)));
        }

        [Fact]
        public void RemoveTetrimino_TetriminoUnplaced_ThrowException()
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            Point point = new Point(2, 2);

            Assert.Throws<Tetris.Exceptions.MissingTetrinoException>(
                () => _tetrisBoard.RemoveTetrimino(tetrimino)
            );
        }

        [Fact]
        private void ReleaseTetrimino_TerminoPlaced_RemoveTetriminoButKeepBlocks()
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point (2, 2));

            _tetrisBoard.ReleaseTetrimino(tetrimino);

            Assert.Empty(_tetrisBoard.AllTetriminos());
            Assert.Null(_tetrisBoard.TetriminoPoint(tetrimino));
            Assert.Equal(2, _tetrisBoard.AllBlocks().Length);
            Assert.Equal(blocks[1], _tetrisBoard.BlockAt(new Point(2, 2)));
            Assert.Equal(blocks[0], _tetrisBoard.BlockAt(new Point(2, 1)));
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE)]
        [InlineData(Rotation.REVERSE)]
        [InlineData(Rotation.FLIP)]
        public void CanRotate_WithSpace_ReturnTrue(Rotation rotation)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point(2, 2));

            bool result = _tetrisBoard.CanRotate(tetrimino, rotation);

            Assert.True(result);
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE, 1, 2)]
        [InlineData(Rotation.REVERSE, 3, 2)]
        [InlineData(Rotation.FLIP, 2, 1)]
        public void CanRotate_BlockInTheWay_ReturnFalse(Rotation rotation, int x, int y)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point(x, y));
            _tetrisBoard.AddBlockAt(new Block(), new Point(2, 2));

            bool result = _tetrisBoard.CanRotate(tetrimino, rotation);

            Assert.False(result);
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE, 4, 2)]
        [InlineData(Rotation.REVERSE, 0, 2)]
        [InlineData(Rotation.FLIP, 2, 4)]
        public void CanRotate_BoundaryInTheWay_ReturnFalse(Rotation rotation, int x, int y)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point(x, y));

            bool result = _tetrisBoard.CanRotate(tetrimino, rotation);

            Assert.False(result);
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE, 3, 2)]
        [InlineData(Rotation.REVERSE, 1, 2)]
        [InlineData(Rotation.FLIP, 2, 3)]
        public void Rotate_WithSpace_ReplaceBlocks(Rotation rotation, int noseX, int noseY)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point(2, 2));

            _tetrisBoard.Rotate(tetrimino, rotation);

            Assert.Equal(2, _tetrisBoard.AllBlocks().Length);
            Assert.Equal(blocks[0], _tetrisBoard.BlockAt(new Point(noseX, noseY)));
            Assert.Equal(blocks[1], _tetrisBoard.BlockAt(new Point(2, 2)));
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE, 1, 2)]
        [InlineData(Rotation.REVERSE, 3, 2)]
        [InlineData(Rotation.FLIP, 2, 1)]
        public void Rotate_BlockInTheWay_ThrowException(Rotation rotation, int x, int y)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point(x, y));
            _tetrisBoard.AddBlockAt(new Block(), new Point(2, 2));

            Assert.Throws<Tetris.Exceptions.NoOverwriteBlockException>(
               () => _tetrisBoard.Rotate(tetrimino, rotation)
            );
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE, 4, 2)]
        [InlineData(Rotation.REVERSE, 0, 2)]
        [InlineData(Rotation.FLIP, 2, 4)]
        public void Rotate_BoundaryInTheWay_ThrowException(Rotation rotation, int x, int y)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);
            _tetrisBoard.AddTetriminoAt(tetrimino, new Point(x, y));

            Assert.Throws<Tetris.Exceptions.BlockOutsideBoardException>(
               () => _tetrisBoard.Rotate(tetrimino, rotation)
            );
        }

        [Theory]
        [InlineData(Rotation.CLOCKWISE)]
        [InlineData(Rotation.REVERSE)]
        [InlineData(Rotation.FLIP)]
        public void Rotate_UnplacedTetrimino_ThrowException(Rotation rotation)
        {
            Block[] blocks = { new Block(), new Block() };
            Tetrimino tetrimino = new Tetrimino(Direction.UP, blocks);

            Assert.Throws<Tetris.Exceptions.MissingTetrinoException>(
               () => _tetrisBoard.Rotate(tetrimino, rotation)
            );
        }
    }
}