using System;

namespace Tetris
{
    public class BoardOperator
    {
        public Board _board;
        public Block currentBlock { get; private set; }
        private Block _nextBlock;
        private Point _nextBlockStartPoint;

        public bool currentBlockIsLocked { get; private set; }

        public BoardOperator(Board board)
        {
            _board = board;
        }

        public void NewCurrentBlock(Block block, Point startPoint)
        {
            ValidateCurrentBlockOverwrite();

            currentBlock = block;
            _board.AddBlockAt(block, startPoint);
            currentBlockIsLocked = false;
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

            currentBlock = null;
            NewCurrentBlock(_nextBlock, _nextBlockStartPoint);
            _nextBlock = null;
            _nextBlockStartPoint = null;
        }

        public void DropCurrentBlock()
        {
            ValidateCurrentBlockMissing();

            if (_board.IsEmptyBelowBlock(currentBlock))
            {
                _board.MoveBlock(currentBlock, new Point(0, 1));
            }
            else currentBlockIsLocked = true;
        }

        public void MoveCurrentBlockRight()
        {
            ValidateCurrentBlockMissing();

            _board.MoveBlock(currentBlock, new Point(1, 0));
        }

        public void MoveCurrentBlockLeft()
        {
            ValidateCurrentBlockMissing();

            _board.MoveBlock(currentBlock, new Point(-1, 0));
        }

        public void RorateCurrentBlock() {}

        public void SlamCurrentBlock()
        {
            ValidateCurrentBlockMissing();

            while (!currentBlockIsLocked)
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
                int at_y = _board.BlockPoint(row[0]).y;
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
            for (int y = (_board.height - 1); y >= 0; y--)
            {
                if (y > atY) continue;

                for (int x = 0; x < _board.width; x++)
                {
                    Block block = _board.BlockAt(new Point(x, y));
                    if (block == null) continue;

                    _board.MoveBlock(block, new Point(0, 1));
                }
            }
        }

        private void ValidateCurrentBlockMissing()
        {
            if (currentBlock == null)
            {
                string msg = "Desired operation requires a 'current block' to be set.";
                throw new Tetris.Exceptions.MissingBlockException(msg);
            }
        }

        private void ValidateNextBlockMissing()
        {
            if (_nextBlock == null)
            {
                string msg = "Desired operation requires a 'next block' to be set.";
                throw new Tetris.Exceptions.MissingBlockException(msg);
            }
        }

        private void ValidateCurrentBlockOverwrite()
        {
            if (currentBlock != null)
            {
                string msg = "Trying to overwrite 'current block': not allowed.";
                throw new Tetris.Exceptions.NoOverwriteBlockException(msg);
            }
        }

        private void ValidateNextBlockOverwrite()
        {
            if (_nextBlock != null)
            {
                string msg = "Trying to overwrite 'next block': not allowed.";
                throw new Tetris.Exceptions.NoOverwriteBlockException(msg);
            }
        }
    }
}