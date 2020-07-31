namespace Tetris
{
    public class BoardOperator
    {
        public Board _board;

        public BoardOperator(Board board)
        {
            _board = board;
        }

        public void Fall()
        {
            foreach (ITile tile in _board.allTiles)
            {
                Point atPoint = _board.TilePoint(tile);
                if (atPoint.y != 0)
                {
                    _board.MoveTile(tile, new Point(0, 1));
                }
            }
        }
    }
}