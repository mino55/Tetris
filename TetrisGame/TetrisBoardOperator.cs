using System;

namespace Tetris
{
    public class TetrisBoardOperator
    {
        public TetrisBoard _tetrisBoard;
        public Tetrimino currentTetrimino { get; private set; }
        private Tetrimino _nextTetrimino;
        private Point _nextTetriminoStartPoint;

        public bool currentTetriminoIsLocked { get; private set; }

        public TetrisBoardOperator(TetrisBoard tetrisBoard)
        {
            _tetrisBoard = tetrisBoard;
        }

        public void NewCurrentTetrimino(Tetrimino tetrimino, Point startPoint)
        {
            ValidateCurrentTetriminoOverwrite();

            currentTetrimino = tetrimino;
            _tetrisBoard.AddTetriminoAt(tetrimino, startPoint);
            currentTetriminoIsLocked = false;
        }

        public void NewNextTetrimino(Tetrimino tetrimino, Point startPoint)
        {
            ValidateNextTetriminoOverwrite();

            _nextTetrimino = tetrimino;
            _nextTetriminoStartPoint = startPoint;
        }

        public void NextCurrentTetrimino()
        {
            ValidateNextTetriminoMissing();

            currentTetrimino = null;
            NewCurrentTetrimino(_nextTetrimino, _nextTetriminoStartPoint);
            _nextTetrimino = null;
            _nextTetriminoStartPoint = null;
        }

        public void DropCurrentTetrimino()
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanMoveTetrimino(currentTetrimino, new Point(0, 1)))
            {
                _tetrisBoard.MoveTetrimino(currentTetrimino, new Point(0, 1));
            }
            else currentTetriminoIsLocked = true;
        }

        public void MoveCurrentTetriminoRight()
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanMoveTetrimino(currentTetrimino, new Point(1, 0)))
            {
                _tetrisBoard.MoveTetrimino(currentTetrimino, new Point(1, 0));
            }
        }

        public void MoveCurrentTetriminoLeft()
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanMoveTetrimino(currentTetrimino, new Point(-1, 0)))
            {
                _tetrisBoard.MoveTetrimino(currentTetrimino, new Point(-1, 0));
            }
        }

        public void RotateCurrentTetrimino(Rotation rotation)
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanRotate(currentTetrimino, rotation))
            {
                _tetrisBoard.Rotate(currentTetrimino, rotation);
            }
        }

        public void SlamCurrentTetrimino()
        {
            ValidateCurrentTetriminoMissing();

            while (!currentTetriminoIsLocked)
            {
                DropCurrentTetrimino();
            }
        }

        public int Rows()
        {
            return _tetrisBoard.BlocksInRows().Count;
        }

        public void CleanRows()
        {
            foreach (Block[] row in _tetrisBoard.BlocksInRows())
            {
                int at_y = _tetrisBoard.BlockPoint(row[0]).y;
                ClearRow(row);
                FillRowGapAt(at_y);
            }
        }

        private void ClearRow(Block[] row)
        {
            foreach (Block tile in row)
            {
                _tetrisBoard.RemoveBlock(tile);
            }
        }

        private void FillRowGapAt(int atY)
        {
            for (int y = (_tetrisBoard.height - 1); y >= 0; y--)
            {
                if (y > atY) continue;

                for (int x = 0; x < _tetrisBoard.width; x++)
                {
                    Block block = _tetrisBoard.BlockAt(new Point(x, y));
                    if (block == null) continue;

                    _tetrisBoard.MoveBlock(block, new Point(0, 1));
                }
            }
        }

        private void ValidateCurrentTetriminoMissing()
        {
            if (currentTetrimino == null)
            {
                string msg = "Desired operation requires a 'current block' to be set.";
                throw new Tetris.Exceptions.MissingTetriminoException(msg);
            }
        }

        private void ValidateNextTetriminoMissing()
        {
            if (_nextTetrimino == null)
            {
                string msg = "Desired operation requires a 'next Tetrimino' to be set.";
                throw new Tetris.Exceptions.MissingTetriminoException(msg);
            }
        }

        private void ValidateCurrentTetriminoOverwrite()
        {
            if (currentTetrimino != null)
            {
                string msg = "Trying to overwrite 'current Tetrimino': not allowed.";
                throw new Tetris.Exceptions.NoOverwriteTetriminoException(msg);
            }
        }

        private void ValidateNextTetriminoOverwrite()
        {
            if (_nextTetrimino != null)
            {
                string msg = "Trying to overwrite 'next Tetrimino': not allowed.";
                throw new Tetris.Exceptions.NoOverwriteTetriminoException(msg);
            }
        }
    }
}