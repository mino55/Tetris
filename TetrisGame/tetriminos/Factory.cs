using System;

namespace Tetris.Tetriminos
{
    class Factory
    {
        private ColorHelper _colorHelper = null;

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

        public Tetrimino Create(Tetriminos.Type type)
        {
            Block[] blocks = null;
            switch(type)
            {
                case Type.SHAPE_I:
                    blocks = createBlocks(" I ", Color.TEAL, Tetrimino.Size<Tetriminos.ShapeI>());
                    return new Tetriminos.ShapeI(Direction.UP, blocks);

                case Type.SHAPE_J:
                    blocks = createBlocks(" J ", Color.BLUE, Tetrimino.Size<Tetriminos.ShapeJ>());
                    return new Tetriminos.ShapeJ(Direction.UP, blocks);

                case Type.SHAPE_L:
                    blocks = createBlocks(" L ", Color.ORANGE, Tetrimino.Size<Tetriminos.ShapeL>());
                    return new Tetriminos.ShapeL(Direction.UP, blocks);

                case Type.SHAPE_O:
                    blocks = createBlocks(" O ", Color.WHITE, Tetrimino.Size<Tetriminos.ShapeO>());
                    return new Tetriminos.ShapeO(Direction.UP, blocks);

                case Type.SHAPE_S:
                    blocks = createBlocks(" S ", Color.GREEN, Tetrimino.Size<Tetriminos.ShapeS>());
                    return new Tetriminos.ShapeS(Direction.UP, blocks);

                case Type.SHAPE_T:
                    blocks = createBlocks(" T ", Color.PURPLE, Tetrimino.Size<Tetriminos.ShapeT>());
                    return new Tetriminos.ShapeT(Direction.UP, blocks);

                case Type.SHAPE_Z:
                    blocks = createBlocks(" Z ", Color.RED, Tetrimino.Size<Tetriminos.ShapeZ>());
                    return new Tetriminos.ShapeZ(Direction.UP, blocks);

                default:
                    return null;
            }
        }

        private Block[] createBlocks(String print, Color color, int number)
        {
            String coloredPrint = _colorHelper.ColorString(print, color);
            Block[] blocks = new Block[number];
            for (int i = 0; i < number; i++) { blocks[i] = new Block(coloredPrint); }
            return blocks;
        }
    }
}

