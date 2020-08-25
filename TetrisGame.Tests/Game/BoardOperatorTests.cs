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
                _board.AddBlockAt(new Block(), new Point(x, rowAt));
            }
        }

        public BoardOperatorTests()
        {
            _board = new Board(5, 5);
            _boardOperator = new BoardOperator(_board);
        }

        [Fact]
        public void NewCurrentBlock_NoCurrentBlock_setCurrentBlock()
        {
            Block block = new Block();
            Point point = new Point(0, 0);

            _boardOperator.NewCurrentBlock(block, point);

            Assert.Equal(block, _boardOperator.currentBlock);
        }

        [Fact]
        public void NewCurrentBlock_AlreadyCurrentBlock_ThrowException()
        {
            Block block = new Block();
            Point point = new Point(0, 0);
            _boardOperator.NewCurrentBlock(block, point);

            Assert.Throws<Exceptions.NoOverwriteBlockException>(() => _boardOperator.NewCurrentBlock(block, point));
        }

        [Fact]
        public void NewNextBlock_AlreadyNextBlock_ThrowException()
        {
            Block block = new Block();
            Point point = new Point(0, 0);
            _boardOperator.NewNextBlock(block, point);

            Assert.Throws<Exceptions.NoOverwriteBlockException>(() => _boardOperator.NewNextBlock(block, point));
        }

        [Fact]
        public void NextCurrentBlock_NextBlockSet_NextBlockBecomesCurrent()
        {
            Point pointA = new Point(0, 0);
            Block currentBlock = new Block();
            _boardOperator.NewCurrentBlock(currentBlock, pointA);
            Point pointB = new Point(0, 1);
            Block nextBlock = new Block();
            _boardOperator.NewNextBlock(nextBlock, pointB);

            _boardOperator.NextCurrentBlock();

            Assert.Equal(nextBlock, _boardOperator.currentBlock);
        }

        [Fact]
        public void NextCurrentBlock_MissingNextBlock_ThrowException()
        {
            Point pointA = new Point(0, 0);
            Block currentBlock = new Block();
            _boardOperator.NewCurrentBlock(currentBlock, pointA);

            Assert.Throws<Exceptions.MissingBlockException>(() => _boardOperator.NextCurrentBlock());
        }

        [Fact]
        public void DropCurrentBlock_AboveBottom_MoveBlockDown()
        {
            Block currentBlock = new Block();
            Point startPoint = new Point(3, 0);
            _boardOperator.NewCurrentBlock(currentBlock, startPoint);

            _boardOperator.DropCurrentBlock();

            Point endPoint = new Point(3, 1);
            Assert.Null(_board.BlockAt(startPoint));
            Assert.Equal(currentBlock, _board.BlockAt(endPoint));
        }

        [Fact]
        public void DropCurrentBlock_AboveBottom_BlockNotLocked()
        {
            Block currentBlock = new Block();
            Point startPoint = new Point(3, 0);
            _boardOperator.NewCurrentBlock(currentBlock, startPoint);

            _boardOperator.DropCurrentBlock();

            Assert.False(_boardOperator.currentBlockIsLocked);
        }

        [Fact]
        public void DropCurrentBlock_BlockAtBottom_DontMoveBlock()
        {
            Block currentBlock = new Block();
            Point startPoint = new Point(1, 4);
            _boardOperator.NewCurrentBlock(currentBlock, startPoint);

            _boardOperator.DropCurrentBlock();

            Point endPoint = new Point(1, 4);
            Assert.Equal(currentBlock, _board.BlockAt(endPoint));
        }

        [Fact]
        public void DropCurrentBlock_BlockAtBottom_LockCurrentBlock()
        {
            Block currentBlock = new Block();
            Point startPoint = new Point(1, 4);
            _boardOperator.NewCurrentBlock(currentBlock, startPoint);

            _boardOperator.DropCurrentBlock();

            Assert.True(_boardOperator.currentBlockIsLocked);
        }

        [Fact]
        public void SlamCurrentBlock_FromTop_MoveBlockToBottom()
        {
            Block currentBlock = new Block();
            Point startPoint = new Point(0, 0);
            _boardOperator.NewCurrentBlock(currentBlock, startPoint);

            _boardOperator.SlamCurrentBlock();

            Point endPoint = new Point(0, 4);
            Assert.Null( _board.BlockAt(startPoint));
            Assert.Equal(currentBlock, _board.BlockAt(endPoint));
        }

        [Fact]
        public void SlamCurrentBlock_OnOtherBlock_MoveBlockOnTop()
        {
            Block currentBlock = new Block();
            Point startPoint = new Point(0, 0);
            _boardOperator.NewCurrentBlock(currentBlock, startPoint);
            Block otherBlock = new Block();
            Point otherPoint = new Point(0, 4);
            _board.AddBlockAt(otherBlock, otherPoint);

            _boardOperator.SlamCurrentBlock();

            Point endPoint = new Point(0, 3);
            Assert.Null( _board.BlockAt(startPoint));
            Assert.Equal(currentBlock, _board.BlockAt(endPoint));
            Assert.Equal(otherBlock, _board.BlockAt(otherPoint));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Rows_WithRows_ReturnNumberOfRows(int numberOfRows)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                fillBoardRowAt(y);
            }

            int rows = _boardOperator.Rows();

            Assert.Equal(numberOfRows, rows);
        }

        [Fact]
        public void Rows_NoRows_returnZero()
        {
            fillBoardRowAt(0);
            _board.RemoveBlockAt(new Point(4, 0));
            Assert.Equal(0, _boardOperator.Rows());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CleanRows_WithRows_RemoveAllRows(int numberOfRows)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                fillBoardRowAt(y);
            }
            Block nonRowBlock = new Block();
            Point atPoint = new Point(4, 4);
            _board.AddBlockAt(nonRowBlock, atPoint);

            _boardOperator.CleanRows();

            Assert.Equal(0, _boardOperator.Rows());
            Assert.Single(_board.AllBlocks());
            Assert.Equal(nonRowBlock, _board.BlockAt(atPoint));
        }

        [Fact]
        public void CleanRows_WithoutRows_DoNothing()
        {
            Block nonRowBlock = new Block();
            Point atPoint = new Point(4, 4);
            _board.AddBlockAt(nonRowBlock, atPoint);

            _boardOperator.CleanRows();

            Assert.Equal(0, _boardOperator.Rows());
            Assert.Single(_board.AllBlocks());
            Assert.Equal(nonRowBlock, _board.BlockAt(atPoint));
        }

        [Fact]
        public void CleanRows_WithRows_FillGaps()
        {
            Block blockFarAboveGap = new Block();
            _board.AddBlockAt(blockFarAboveGap, new Point(0, 0));
            Block blockAboveGap = new Block();
            _board.AddBlockAt(blockAboveGap, new Point(4, 1));
            Block blockBelowGap = new Block();
            fillBoardRowAt(2);
            fillBoardRowAt(3);
            _board.AddBlockAt(blockBelowGap, new Point(0, 4));

            _boardOperator.CleanRows();

            Assert.Equal(new Point(0, 2), _board.BlockPoint(blockFarAboveGap));
            Assert.Equal(new Point(4, 3), _board.BlockPoint(blockAboveGap));
            Assert.Equal(new Point(0, 4), _board.BlockPoint(blockBelowGap));
        }
    }
}