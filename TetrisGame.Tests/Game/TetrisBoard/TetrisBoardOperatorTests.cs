using System;
using Xunit;

namespace Tetris.Tests
{
    public class TetrisBoardOperatorTests
    {
        private readonly TetrisBoard _tetrisBoard;
        private readonly TetrisBoardOperator _tetrisBoardOperator;
        private readonly Block[] _blocks;

        private Tetrimino CreateTetrimino()
        {
            Tetrimino tetrimino = new Tetrimino(Direction.UP, _blocks);
            return tetrimino;
        }

        private void FillBoardRowAt(int rowAt)
        {
            for (int x = 0; x < _tetrisBoard.Width; x++)
            {
                _tetrisBoard.AddBlockAt(new Block(), new Point(x, rowAt));
            }
        }

        public TetrisBoardOperatorTests()
        {
            _blocks = new Block[] { new Block(), new Block() };
            _tetrisBoard = new TetrisBoard(5, 5);
            _tetrisBoardOperator = new TetrisBoardOperator(_tetrisBoard);
        }

        [Fact]
        public void NewCurrentTetrimino_NoCurrentTetrimino_setCurrentBlock()
        {
            Tetrimino tetrimino = CreateTetrimino();
            Point point = new Point(2, 1);

            _tetrisBoardOperator.NewCurrentTetrimino(tetrimino, point);

            Assert.Equal(tetrimino, _tetrisBoardOperator.CurrentTetrimino);
        }

        [Fact]
        public void NewCurrentTetrimino_AlreadyCurrentTetrimino_ThrowException()
        {
            Tetrimino tetrimino = CreateTetrimino();
            Point point = new Point(2, 1);
            _tetrisBoardOperator.NewCurrentTetrimino(tetrimino, point);

            Assert.Throws<Exceptions.NoOverwriteTetriminoException>(() => _tetrisBoardOperator.NewCurrentTetrimino(tetrimino, point));
        }

        [Fact]
        public void NewNextTetrimino_AlreadyNextBlock_ThrowException()
        {
            Tetrimino tetrimino = CreateTetrimino();
            Point point = new Point(2, 1);
            _tetrisBoardOperator.NewNextTetrimino(tetrimino, point);

            Assert.Throws<Exceptions.NoOverwriteTetriminoException>(() => _tetrisBoardOperator.NewNextTetrimino(tetrimino, point));
        }

        [Fact]
        public void NextCurrentTetrimino_NextBlockSet_NextBlockBecomesCurrent()
        {
            Point pointA = new Point(2, 1);
            Tetrimino currentTetrimino = CreateTetrimino();
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, pointA);
            Point pointB = new Point(2, 4);
            Tetrimino nextTetrimino = CreateTetrimino();
            _tetrisBoardOperator.NewNextTetrimino(nextTetrimino, pointB);

            _tetrisBoardOperator.NextCurrentTetrimino();

            Assert.Equal(nextTetrimino, _tetrisBoardOperator.CurrentTetrimino);
        }

        [Fact]
        public void NextCurrentTetrimino_MissingNextBlock_ThrowException()
        {
            Point pointA = new Point(2, 1);
            Tetrimino currentTetrimino = CreateTetrimino();
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, pointA);

            Assert.Throws<Exceptions.MissingTetriminoException>(() => _tetrisBoardOperator.NextCurrentTetrimino());
        }

        [Fact]
        public void DropCurrentTetrimino_AboveBottom_MoveBlockDown()
        {
            Tetrimino currentTetrimino = CreateTetrimino();
            Point startPoint = new Point(3, 1);
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, startPoint);

            _tetrisBoardOperator.DropCurrentTetrimino();
            _tetrisBoardOperator.DropCurrentTetrimino();

            Point endPoint = new Point(3, 3);
            Assert.Null(_tetrisBoard.BlockAt(startPoint));
            Assert.Equal(_blocks[1], _tetrisBoard.BlockAt(endPoint));
        }

        [Fact]
        public void DropCurrentTetrimino_AboveBottom_BlockNotLocked()
        {
            Tetrimino currentTetrimino = CreateTetrimino();
            Point startPoint = new Point(3, 2);
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, startPoint);

            _tetrisBoardOperator.DropCurrentTetrimino();

            Assert.False(_tetrisBoardOperator.CurrentTetriminoIsLocked);
        }

        [Fact]
        public void DropCurrentTetrimino_BlockAtBottom_DontMoveBlock()
        {
            Tetrimino currentTetrimino = CreateTetrimino();
            Point startPoint = new Point(1, 4);
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, startPoint);

            _tetrisBoardOperator.DropCurrentTetrimino();

            Point endPoint = new Point(1, 4);
            Assert.Equal(_blocks[1], _tetrisBoard.BlockAt(endPoint));
        }

        [Fact]
        public void DropCurrentTetrimino_BlockAtBottom_LockCurrentTetrimino()
        {
            Tetrimino currentTetrimino = CreateTetrimino();
            Point startPoint = new Point(1, 4);
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, startPoint);

            _tetrisBoardOperator.DropCurrentTetrimino();

            Assert.True(_tetrisBoardOperator.CurrentTetriminoIsLocked);
        }

        [Fact]
        public void SlamCurrentTetrimino_FromTop_MoveBlockToBottom()
        {
            Tetrimino currentTetrimino = CreateTetrimino();
            Point startPoint = new Point(0, 1);
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, startPoint);

            _tetrisBoardOperator.SlamCurrentTetrimino();

            Point endPoint = new Point(0, 4);
            Assert.Null( _tetrisBoard.BlockAt(startPoint));
            Assert.Equal(_blocks[1], _tetrisBoard.BlockAt(endPoint));
        }

        [Fact]
        public void SlamCurrentTetrimino_OnOtherBlock_MoveBlockOnTop()
        {
            Tetrimino currentTetrimino = CreateTetrimino();
            Point startPoint = new Point(0, 1);
            _tetrisBoardOperator.NewCurrentTetrimino(currentTetrimino, startPoint);
            Block otherBlock = new Block();
            Point otherPoint = new Point(0, 4);
            _tetrisBoard.AddBlockAt(otherBlock, otherPoint);

            _tetrisBoardOperator.SlamCurrentTetrimino();

            Point endPoint = new Point(0, 3);
            Assert.Null( _tetrisBoard.BlockAt(startPoint));
            Assert.Equal(_blocks[1], _tetrisBoard.BlockAt(endPoint));
            Assert.Equal(otherBlock, _tetrisBoard.BlockAt(otherPoint));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Rows_WithRows_ReturnNumberOfRows(int numberOfRows)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                FillBoardRowAt(y);
            }

            int rows = _tetrisBoardOperator.Rows();

            Assert.Equal(numberOfRows, rows);
        }

        [Fact]
        public void Rows_NoRows_returnZero()
        {
            FillBoardRowAt(0);
            _tetrisBoard.RemoveBlockAt(new Point(4, 0));
            Assert.Equal(0, _tetrisBoardOperator.Rows());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CleanRows_WithRows_RemoveAllRows(int numberOfRows)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                FillBoardRowAt(y);
            }
            Block nonRowBlock = new Block();
            Point atPoint = new Point(4, 4);
            _tetrisBoard.AddBlockAt(nonRowBlock, atPoint);

            _tetrisBoardOperator.CleanRows();

            Assert.Equal(0, _tetrisBoardOperator.Rows());
            Assert.Single(_tetrisBoard.AllBlocks());
            Assert.Equal(nonRowBlock, _tetrisBoard.BlockAt(atPoint));
        }

        [Fact]
        public void CleanRows_WithoutRows_DoNothing()
        {
            Block nonRowBlock = new Block();
            Point atPoint = new Point(4, 4);
            _tetrisBoard.AddBlockAt(nonRowBlock, atPoint);

            _tetrisBoardOperator.CleanRows();

            Assert.Equal(0, _tetrisBoardOperator.Rows());
            Assert.Single(_tetrisBoard.AllBlocks());
            Assert.Equal(nonRowBlock, _tetrisBoard.BlockAt(atPoint));
        }

        [Fact]
        public void CleanRows_WithRows_FillGaps()
        {
            Block blockFarAboveGap = new Block();
            _tetrisBoard.AddBlockAt(blockFarAboveGap, new Point(0, 0));
            Block blockAboveGap = new Block();
            _tetrisBoard.AddBlockAt(blockAboveGap, new Point(4, 1));
            Block blockBelowGap = new Block();
            FillBoardRowAt(2);
            FillBoardRowAt(3);
            _tetrisBoard.AddBlockAt(blockBelowGap, new Point(0, 4));

            _tetrisBoardOperator.CleanRows();

            Assert.Equal(new Point(0, 2), _tetrisBoard.BlockPoint(blockFarAboveGap));
            Assert.Equal(new Point(4, 3), _tetrisBoard.BlockPoint(blockAboveGap));
            Assert.Equal(new Point(0, 4), _tetrisBoard.BlockPoint(blockBelowGap));
        }
    }
}
