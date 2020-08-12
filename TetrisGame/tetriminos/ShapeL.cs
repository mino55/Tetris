namespace Tetris.Tetriminos
{
    public class ShapeL : Tetrimino
    {
        public ShapeL() : base() {}
        public ShapeL(Direction direction, Block[] blocks) : base(direction, blocks) {}

        public override Point ShapeCenterOffset()
        {
          return new Point(1, 1);
        }

        protected override int[,] ShapeDirectionUp()
        {
          return new int[,] {
            { 0, 0, 1 },
            { 4, 3, 2 },
            { 0, 0, 0 },
          };
        }

        protected override int[,] ShapeDirectionRight()
        {
          return new int[,] {
            { 0, 4, 0 },
            { 0, 3, 0 },
            { 0, 2, 1 },
          };
        }

        protected override int[,] ShapeDirectionDown()
        {
          return new int[,] {
            { 0, 0, 0 },
            { 2, 3, 4 },
            { 1, 0, 0 },
          };
        }

        protected override int[,] ShapeDirectionLeft()
        {
          return new int[,] {
            { 1, 2, 0 },
            { 0, 3, 0 },
            { 0, 4, 0 },
          };
        }
      }
}