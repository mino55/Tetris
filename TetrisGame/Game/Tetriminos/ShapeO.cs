namespace Tetris.Tetriminos
{
    public class ShapeO : Tetrimino
    {
        public ShapeO() : base() { }
        public ShapeO(Direction direction, Block[] blocks) : base(direction, blocks) { }

        public override Point ShapeCenterOffset()
        {
            return new Point(1, 1);
        }

        protected override int[,] ShapeDirectionUp()
        {
            return new int[,] {
                { 1, 2 },
                { 4, 3 },
            };
        }

        protected override int[,] ShapeDirectionRight()
        {
            return new int[,] {
                { 4, 1 },
                { 3, 2 },
            };
        }

        protected override int[,] ShapeDirectionDown()
        {
            return new int[,] {
                { 3, 4 },
                { 2, 1 },
            };
        }

        protected override int[,] ShapeDirectionLeft()
        {
            return new int[,] {
                { 2, 3 },
                { 1, 4 },
            };
        }

        public override Type Type()
        {
            return Tetriminos.Type.SHAPE_O;
        }
    }
}
