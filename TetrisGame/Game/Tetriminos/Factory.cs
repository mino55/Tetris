using System;

namespace Tetris.Tetriminos
{
    public class Factory
    {
        private readonly ColorHelper _colorHelper = null;

        public Factory(ColorHelper colorHelper)
        {
            _colorHelper = colorHelper;
        }

        public Tetrimino Random()
        {
            Array types = Enum.GetValues(typeof(Type));
            Random random = new Random();
            Type randomType = (Type)types.GetValue(random.Next(types.Length));
            return Create(randomType);
        }

        public Tetrimino Create(Type type)
        {
            Block[] blocks;
            switch (type)
            {
                case Type.SHAPE_I:
                    blocks = CreateBlocks(" I ", Color.TEAL, Tetrimino.Size<ShapeI>());
                    return new ShapeI(Direction.UP, blocks);

                case Type.SHAPE_J:
                    blocks = CreateBlocks(" J ", Color.BLUE, Tetrimino.Size<ShapeJ>());
                    return new ShapeJ(Direction.UP, blocks);

                case Type.SHAPE_L:
                    blocks = CreateBlocks(" L ", Color.ORANGE, Tetrimino.Size<ShapeL>());
                    return new ShapeL(Direction.UP, blocks);

                case Type.SHAPE_O:
                    blocks = CreateBlocks(" O ", Color.WHITE, Tetrimino.Size<ShapeO>());
                    return new ShapeO(Direction.UP, blocks);

                case Type.SHAPE_S:
                    blocks = CreateBlocks(" S ", Color.GREEN, Tetrimino.Size<ShapeS>());
                    return new ShapeS(Direction.UP, blocks);

                case Type.SHAPE_T:
                    blocks = CreateBlocks(" T ", Color.PURPLE, Tetrimino.Size<ShapeT>());
                    return new ShapeT(Direction.UP, blocks);

                case Type.SHAPE_Z:
                    blocks = CreateBlocks(" Z ", Color.RED, Tetrimino.Size<ShapeZ>());
                    return new ShapeZ(Direction.UP, blocks);

                default:
                    return null;
            }
        }

        private Block[] CreateBlocks(string print, Color color, int number)
        {
            string coloredPrint = _colorHelper.ColorString(print, color);
            Block[] blocks = new Block[number];
            for (int i = 0; i < number; i++) { blocks[i] = new Block(coloredPrint); }
            return blocks;
        }
    }
}
