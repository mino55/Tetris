namespace Tetris.Tetriminos
{
    public class ShapeS : Tetrimino
    {
        public ShapeS() : base() {}
        public ShapeS(Direction direction, Block[] blocks) : base(direction, blocks) {}

        public override Point ShapeCenterOffset()
        {
          return new Point(1, 1);
        }

        protected override int[,] ShapeDirectionUp()
        {
          return new int[,] {
            { 0, 2, 1 },
            { 4, 3, 0 },
            { 0, 0, 0 }
          };
        }

        protected override int[,] ShapeDirectionRight()
        {
          return new int[,] {
            { 0, 4, 0 },
            { 0, 3, 2 },
            { 0, 0, 1 }
          };
        }

        protected override int[,] ShapeDirectionDown()
        {
          return new int[,] {
            { 0, 0, 0 },
            { 0, 3, 4 },
            { 1, 2, 0 }
          };
        }

        protected override int[,] ShapeDirectionLeft()
        {
          return new int[,] {
            { 1, 0, 0 },
            { 2, 3, 0 },
            { 0, 4, 0 }
          };
        }
      }
}