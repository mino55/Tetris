namespace Tetris.Tetriminos
{
    public class ShapeJ : Tetrimino
    {
        public ShapeJ() : base() { }
        public ShapeJ(Direction direction, Block[] blocks) : base(direction, blocks) { }

        public override Point ShapeCenterOffset()
        {
            return new Point(1, 1);
        }

        protected override int[,] ShapeDirectionUp()
        {
            return new int[,] {
                { 1, 0, 0 },
                { 2, 3, 4 },
                { 0, 0, 0 },
            };
        }

        protected override int[,] ShapeDirectionRight()
        {
            return new int[,] {
                { 0, 2, 1 },
                { 0, 3, 0 },
                { 0, 4, 0 },
            };
        }

        protected override int[,] ShapeDirectionDown()
        {
            return new int[,] {
                { 0, 0, 0 },
                { 4, 3, 2 },
                { 0, 0, 1 },
            };
        }

        protected override int[,] ShapeDirectionLeft()
        {
            return new int[,] {
                { 0, 4, 0 },
                { 0, 3, 0 },
                { 1, 2, 0 },
            };
        }

        public override Type Type()
        {
            return Tetriminos.Type.SHAPE_J;
        }
    }
}
