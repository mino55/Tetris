namespace Tetris
{
    public class Block
    {
        private readonly string _print;

        public Block()
        {
            _print = " B ";
        }

        public Block(string print)
        {
            _print = print;
        }

        public string Print() => _print;
    }
}
