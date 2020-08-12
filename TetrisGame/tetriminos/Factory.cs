using System;

namespace Tetris.Tetriminos
{
    class Factory
    {
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
                case Type.ShapeI:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeI>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeI(Direction.UP, blocks);

                case Type.ShapeJ:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeJ>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeJ(Direction.UP, blocks);

                case Type.ShapeL:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeL>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeL(Direction.UP, blocks);

                case Type.ShapeO:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeO>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeO(Direction.UP, blocks);

                case Type.ShapeS:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeS>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeS(Direction.UP, blocks);

                case Type.ShapeT:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeT>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeT(Direction.UP, blocks);

                case Type.ShapeZ:
                    blocks = new Block[Tetrimino.Size<Tetriminos.ShapeI>()];
                    for (int i = 0; i < blocks.Length; i++) { blocks[i] = new Block(); }
                    return new Tetriminos.ShapeI(Direction.UP, blocks);

                default:
                    return null;
            }
        }
    }
}

