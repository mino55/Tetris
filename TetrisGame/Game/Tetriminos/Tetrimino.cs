using System.Collections.Generic;

namespace Tetris
{
    public class Tetrimino
    {
        public Block[] Blocks { get; private set; }

        public Direction Direction { get; private set; }

        public Tetrimino(Direction direction, Block[] blocks)
        {
            this.Direction = direction;
            this.Blocks = blocks;
        }

        public Tetrimino()
        {
            this.Direction = Direction.UP;
            this.Blocks = null;
        }

        public static int Size<T>()
            where T : Tetrimino, new()
        {
            T tetrimino = new T();
            int blockCount = 0;

            int[,] shape = tetrimino.Shape(Direction.UP);
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    int number = shape[y, x];
                    if (number > blockCount) blockCount = number;
                }
            }

            return blockCount;
        }

        public Direction ClockwiseRotation()
        {
            return Direction switch
            {
                Direction.UP => Direction.RIGHT,
                Direction.RIGHT => Direction.DOWN,
                Direction.DOWN => Direction.LEFT,
                Direction.LEFT => Direction.UP,
                _ => throw new Exceptions.InvalidDirectionException(),
            };
        }

        public Direction ReverseRotation()
        {
            return Direction switch
            {
                Direction.UP => Direction.LEFT,
                Direction.LEFT => Direction.DOWN,
                Direction.DOWN => Direction.RIGHT,
                Direction.RIGHT => Direction.UP,
                _ => throw new Exceptions.InvalidDirectionException(),
            };
        }

        public Direction FlipRotation()
        {
            return Direction switch
            {
                Direction.UP => Direction.DOWN,
                Direction.LEFT => Direction.RIGHT,
                Direction.DOWN => Direction.UP,
                Direction.RIGHT => Direction.LEFT,
                _ => throw new Exceptions.InvalidDirectionException(),
            };
        }

        public void RotateTo(Direction direction)
        {
            this.Direction = direction;
        }

        public Block ShapeBlockAt(Point shapePoint, Direction dir)
        {
            int[,] shape = Shape(dir);
            int blockIndex = shape[shapePoint.Y, shapePoint.X] - 1;
            if (blockIndex == -1) return null;

            return Blocks[blockIndex];
        }

        public Point[] ShapePoints(Direction dir)
        {
            List<Point> shapePoints = new List<Point>();
            int[,] shape = Shape(dir);
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    shapePoints.Add(new Point(x, y));
                }
            }

            return shapePoints.ToArray();
        }

        public int[,] Shape(Direction dir)
        {
            return dir switch
            {
                Direction.UP => ShapeDirectionUp(),
                Direction.RIGHT => ShapeDirectionRight(),
                Direction.DOWN => ShapeDirectionDown(),
                Direction.LEFT => ShapeDirectionLeft(),
                _ => throw new Exceptions.InvalidDirectionException(),
            };
        }

        public virtual Point ShapeCenterOffset()
        {
          return new Point(1, 1);
        }

        public bool ContainsBlock(Block block)
        {
            foreach (Block b in Blocks) { if (block == b) return true; }

            return false;
        }

        protected virtual int[,] ShapeDirectionUp()
        {
          return new int[,] {
            { 0, 1, 0 },
            { 0, 2, 0 },
            { 0, 0, 0 }
          };
        }

        protected virtual int[,] ShapeDirectionRight()
        {
          return new int[,] {
            { 0, 0, 0 },
            { 0, 2, 1 },
            { 0, 0, 0 }
          };
        }

        protected virtual int[,] ShapeDirectionDown()
        {
          return new int[,] {
            { 0, 0, 0 },
            { 0, 2, 0 },
            { 0, 1, 0 }
          };
        }

        protected virtual int[,] ShapeDirectionLeft()
        {
          return new int[,] {
            { 0, 0, 0 },
            { 1, 2, 0 },
            { 0, 0, 0 }
          };
        }

        public virtual Tetriminos.Type Type()
        {
            string msg = "Provided tetrimino type is invalid";
            throw new Exceptions.MissingTetriminoException(msg);
        }
    }
}
