namespace Tetris.Tetriminos
{
    public class ShapeT : Tetrimino
    {
        public ShapeT() : base() { }
        public ShapeT(Direction direction, Block[] blocks) : base(direction, blocks) { }

        public override Point ShapeCenterOffset()
        {
            return new Point(1, 1);
        }

        protected override int[,] ShapeDirectionUp()
        {
            return new int[,] {
                { 0, 1, 0 },
                { 2, 3, 4 },
                { 0, 0, 0 }
            };
        }

        protected override int[,] ShapeDirectionRight()
        {
            return new int[,] {
                { 0, 2, 0 },
                { 0, 3, 1 },
                { 0, 4, 0 }
            };
        }

        protected override int[,] ShapeDirectionDown()
        {
            return new int[,] {
                { 0, 0, 0 },
                { 4, 3, 2 },
                { 0, 1, 0 }
            };
        }

        protected override int[,] ShapeDirectionLeft()
        {
            return new int[,] {
                { 0, 4, 0 },
                { 1, 3, 0 },
                { 0, 2, 0 }
            };
        }

        public override Type Type()
        {
            return Tetriminos.Type.SHAPE_T;
        }
    }
}
