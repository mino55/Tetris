namespace Tetris
{
    public class BoardOperator
    {
        public Board _board;
        public Block CurrentBlock { get; private set; }
        public bool CurrentBlockIsLocked { get; private set; }

        private Block _nextBlock;
        private Point _nextBlockStartPoint;

        public BoardOperator(Board board)
        {
            _board = board;
        }

        public void NewCurrentBlock(Block block, Point startPoint)
        {
            ValidateCurrentBlockOverwrite();

            CurrentBlock = block;
            _board.AddBlockAt(block, startPoint);
            CurrentBlockIsLocked = false;
        }

        public void NewNextBlock(Block block, Point startPoint)
        {
            ValidateNextBlockOverwrite();

            _nextBlock = block;
            _nextBlockStartPoint = startPoint;
        }

        public void NextCurrentBlock()
        {
            ValidateNextBlockMissing();

            CurrentBlock = null;
            NewCurrentBlock(_nextBlock, _nextBlockStartPoint);
            _nextBlock = null;
            _nextBlockStartPoint = null;
        }

        public void DropCurrentBlock()
        {
            ValidateCurrentBlockMissing();

            if (_board.IsEmptyBelowBlock(CurrentBlock))
            {
                _board.MoveBlock(CurrentBlock, new Point(0, 1));
            }
            else CurrentBlockIsLocked = true;
        }

        public void MoveCurrentBlockRight()
        {
            ValidateCurrentBlockMissing();

            _board.MoveBlock(CurrentBlock, new Point(1, 0));
        }

        public void MoveCurrentBlockLeft()
        {
            ValidateCurrentBlockMissing();

            _board.MoveBlock(CurrentBlock, new Point(-1, 0));
        }

        public void RorateCurrentBlock() {}

        public void SlamCurrentBlock()
        {
            ValidateCurrentBlockMissing();

            while (!CurrentBlockIsLocked)
            {
                DropCurrentBlock();
            }
        }

        public int Rows()
        {
            return _board.BlocksInRows().Count;
        }

        public void CleanRows()
        {
            foreach (Block[] row in _board.BlocksInRows())
            {
                int at_y = _board.BlockPoint(row[0]).Y;
                ClearRow(row);
                FillRowGapAt(at_y);
            }
        }

        private void ClearRow(Block[] row)
        {
            foreach (Block tile in row)
            {
                _board.RemoveBlock(tile);
            }
        }

        private void FillRowGapAt(int atY)
        {
            for (int y = _board.Height - 1; y >= 0; y--)
            {
                if (y > atY) continue;

                for (int x = 0; x < _board.Width; x++)
                {
                    Block block = _board.BlockAt(new Point(x, y));
                    if (block == null) continue;

                    _board.MoveBlock(block, new Point(0, 1));
                }
            }
        }

        private void ValidateCurrentBlockMissing()
        {
            if (CurrentBlock == null)
            {
                string msg = "Desired operation requires a 'current block' to be set.";
                throw new Exceptions.MissingBlockException(msg);
            }
        }

        private void ValidateNextBlockMissing()
        {
            if (_nextBlock == null)
            {
                string msg = "Desired operation requires a 'next block' to be set.";
                throw new Exceptions.MissingBlockException(msg);
            }
        }

        private void ValidateCurrentBlockOverwrite()
        {
            if (CurrentBlock != null)
            {
                string msg = "Trying to overwrite 'current block': not allowed.";
                throw new Exceptions.NoOverwriteBlockException(msg);
            }
        }

        private void ValidateNextBlockOverwrite()
        {
            if (_nextBlock != null)
            {
                string msg = "Trying to overwrite 'next block': not allowed.";
                throw new Exceptions.NoOverwriteBlockException(msg);
            }
        }
    }
}
