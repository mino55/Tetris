namespace Tetris
{
    public class Tetrimino
    {
        public Block[] blocks { get; private set; }

        public Direction direction { get; private set; }

        public Tetrimino(Direction direction, Block[] blocks)
        {
            this.direction = direction;
            this.blocks = blocks;
        }

        public Tetrimino()
        {
            this.direction = Direction.UP;
            this.blocks = null;
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
            switch(direction)
            {
                case Direction.UP:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.DOWN;
                case Direction.DOWN:
                    return Direction.LEFT;
                case Direction.LEFT:
                    return Direction.UP;
                default:
                    throw new Exceptions.InvalidDirectionException();
            }
        }

        public Direction ReverseRotation()
        {
            switch(direction)
            {
                case Direction.UP:
                    return Direction.LEFT;
                case Direction.LEFT:
                    return Direction.DOWN;
                case Direction.DOWN:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.UP;
                default:
                    throw new Exceptions.InvalidDirectionException();
            }
        }

        public Direction FlipRotation()
        {
            switch(direction)
            {
                case Direction.UP:
                    return Direction.DOWN;
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.DOWN:
                    return Direction.UP;
                case Direction.RIGHT:
                    return Direction.LEFT;
                default:
                    throw new Exceptions.InvalidDirectionException();
            }
        }

        public void RotateTo(Direction direction)
        {
            this.direction = direction;
        }

        public int[,] Shape()
        {
            return Shape(direction);
        }

        public int[,] Shape(Direction dir)
        {
            switch(dir)
            {
                case Direction.UP:
                    return ShapeDirectionUp();
                case Direction.RIGHT:
                    return ShapeDirectionRight();
                case Direction.DOWN:
                    return ShapeDirectionDown();
                case Direction.LEFT:
                    return ShapeDirectionLeft();
                default:
                    throw new Exceptions.InvalidDirectionException();
            }
        }

        public virtual Point ShapeCenterOffset()
        {
          return new Point(1, 1);
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
    }
}