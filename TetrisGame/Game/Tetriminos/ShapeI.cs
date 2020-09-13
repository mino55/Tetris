namespace Tetris.Tetriminos
{
    public class ShapeI : Tetrimino
    {
        public ShapeI() : base() { }
        public ShapeI(Direction direction, Block[] blocks) : base(direction, blocks) { }

        public override Point ShapeCenterOffset()
        {
            return new Point(1, 1);
        }

        protected override int[,] ShapeDirectionUp()
        {
            return new int[,] {
                { 0, 0, 0, 0 },
                { 1, 2, 3, 4 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
        }

        protected override int[,] ShapeDirectionRight()
        {
            return new int[,] {
                { 0, 0, 1, 0 },
                { 0, 0, 2, 0 },
                { 0, 0, 3, 0 },
                { 0, 0, 4, 0 }
            };
        }

        protected override int[,] ShapeDirectionDown()
        {
            return new int[,] {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 4, 3, 2, 1 },
                { 0, 0, 0, 0 }
            };
        }

        protected override int[,] ShapeDirectionLeft()
        {
            return new int[,] {
                { 0, 4, 0, 0 },
                { 0, 3, 0, 0 },
                { 0, 2, 0, 0 },
                { 0, 1, 0, 0 }
            };
        }

        public override Type Type()
        {
            return Tetriminos.Type.SHAPE_I;
        }
    }
}
