using System;
using Xunit;

namespace Tetris.Tests
{


    public class TetriminoTests
    {
        private readonly Tetrimino _tetrimino;

        public TetriminoTests()
        {
            Block[] blocks = new Block[Tetrimino.Size<Tetrimino>()];
            // for (int i = 0; i < blocks.Length; i++ ) { blocks[i] = new Block(); }
            _tetrimino = new Tetrimino(Direction.UP, new Block[] { new Block(), new Block()});
        }

        [Theory]
        [InlineData(Direction.UP, Direction.RIGHT)]
        [InlineData(Direction.RIGHT, Direction.DOWN)]
        [InlineData(Direction.DOWN, Direction.LEFT)]
        [InlineData(Direction.LEFT, Direction.UP)]
        public void ClockwiseRotation_ReturnNextDir(Direction startDir, Direction expectedDir)
        {
            _tetrimino.RotateTo(startDir);

            Direction dir = _tetrimino.ClockwiseRotation();

            Assert.Equal(expectedDir, dir);
        }

        [Theory]
        [InlineData(Direction.UP, Direction.LEFT)]
        [InlineData(Direction.LEFT, Direction.DOWN)]
        [InlineData(Direction.DOWN, Direction.RIGHT)]
        [InlineData(Direction.RIGHT, Direction.UP)]
        public void ReverseRotation_ReturnPrevDir(Direction startDir, Direction expectedDir)
        {
            _tetrimino.RotateTo(startDir);

            Direction dir = _tetrimino.ReverseRotation();

            Assert.Equal(expectedDir, dir);
        }

        [Theory]
        [InlineData(Direction.UP, Direction.DOWN)]
        [InlineData(Direction.DOWN, Direction.UP)]
        [InlineData(Direction.RIGHT, Direction.LEFT)]
        [InlineData(Direction.LEFT, Direction.RIGHT)]
        public void FlipRotation_ReturnOppositeDir(Direction startDir, Direction expectedDir)
        {
            _tetrimino.RotateTo(startDir);

            Direction dir = _tetrimino.FlipRotation();

            Assert.Equal(expectedDir, dir);
        }

        [Fact]
        public void Shape_DirectionUp_ReturnUpShape()
        {
            int[,] shape = _tetrimino.Shape(Direction.UP);

            int[,] expectedShape = new int[,] {
                { 0, 1, 0 },
                { 0, 2, 0 },
                { 0, 0, 0 }
            };
            Assert.Equal(expectedShape, shape);
        }

        [Fact]
        public void Shape_DirectionRight_ReturnRightShape()
        {
            int[,] shape = _tetrimino.Shape(Direction.RIGHT);

            int[,] expectedShape = new int[,] {
                { 0, 0, 0 },
                { 0, 2, 1 },
                { 0, 0, 0 }
            };
            Assert.Equal(expectedShape, shape);
        }

        [Fact]
        public void Shape_DirectionDown_ReturnDownShape()
        {
            int[,] shape = _tetrimino.Shape(Direction.DOWN);

            int[,] expectedShape = new int[,] {
                { 0, 0, 0 },
                { 0, 2, 0 },
                { 0, 1, 0 }
            };
            Assert.Equal(expectedShape, shape);
        }

        [Fact]
        public void Shape_DirectionLeft_ReturnLeftShape()
        {
            int[,] shape = _tetrimino.Shape(Direction.LEFT);

            int[,] expectedShape = new int[,] {
                { 0, 0, 0 },
                { 1, 2, 0 },
                { 0, 0, 0 }
            };
            Assert.Equal(expectedShape, shape);
        }

        [Fact]
        public void Size_ReturnNumberOfBlocks()
        {
            Assert.Equal(2, Tetrimino.Size<Tetrimino>());
        }
    }
}