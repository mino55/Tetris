namespace Tetris
{
    public class TetrisBoardOperator
    {
        public bool CurrentTetriminoIsLocked { get; private set; }
        public TetrisBoard _tetrisBoard;
        public Tetrimino CurrentTetrimino { get; private set; }
        public Tetrimino NextTetrimino { get; private set; }

        private Point _nextTetriminoStartPoint;

        public TetrisBoardOperator(TetrisBoard tetrisBoard)
        {
            _tetrisBoard = tetrisBoard;
        }

        public void NewCurrentTetrimino(Tetrimino tetrimino, Point startPoint)
        {
            ValidateCurrentTetriminoOverwrite();

            CurrentTetrimino = tetrimino;
            _tetrisBoard.AddTetriminoAt(tetrimino, startPoint);
            CurrentTetriminoIsLocked = false;
        }

        public void NewNextTetrimino(Tetrimino tetrimino, Point startPoint)
        {
            ValidateNextTetriminoOverwrite();

            NextTetrimino = tetrimino;
            _nextTetriminoStartPoint = startPoint;
        }

        public void NextCurrentTetrimino()
        {
            ValidateNextTetriminoMissing();

            CurrentTetrimino = null;
            NewCurrentTetrimino(NextTetrimino, _nextTetriminoStartPoint);
            NextTetrimino = null;
            _nextTetriminoStartPoint = null;
        }

        public void DropCurrentTetrimino()
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanMoveTetrimino(CurrentTetrimino, new Point(0, 1)))
            {
                _tetrisBoard.MoveTetrimino(CurrentTetrimino, new Point(0, 1));
            }
            else CurrentTetriminoIsLocked = true;
        }

        public void MoveCurrentTetriminoRight()
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanMoveTetrimino(CurrentTetrimino, new Point(1, 0)))
            {
                _tetrisBoard.MoveTetrimino(CurrentTetrimino, new Point(1, 0));
            }
        }

        public void MoveCurrentTetriminoLeft()
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanMoveTetrimino(CurrentTetrimino, new Point(-1, 0)))
            {
                _tetrisBoard.MoveTetrimino(CurrentTetrimino, new Point(-1, 0));
            }
        }

        public void RotateCurrentTetrimino(Rotation rotation)
        {
            ValidateCurrentTetriminoMissing();

            if (_tetrisBoard.CanRotate(CurrentTetrimino, rotation))
            {
                _tetrisBoard.Rotate(CurrentTetrimino, rotation);
            }
        }

        public void SlamCurrentTetrimino()
        {
            ValidateCurrentTetriminoMissing();

            while (!CurrentTetriminoIsLocked)
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
                int at_y = _tetrisBoard.BlockPoint(row[0]).Y;
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
            for (int y = _tetrisBoard.Height - 1; y >= 0; y--)
            {
                if (y > atY) continue;

                for (int x = 0; x < _tetrisBoard.Width; x++)
                {
                    Block block = _tetrisBoard.BlockAt(new Point(x, y));
                    if (block == null) continue;

                    _tetrisBoard.MoveBlock(block, new Point(0, 1));
                }
            }
        }

        private void ValidateCurrentTetriminoMissing()
        {
            if (CurrentTetrimino == null)
            {
                string msg = "Desired operation requires a 'current block' to be set.";
                throw new Exceptions.MissingTetriminoException(msg);
            }
        }

        private void ValidateNextTetriminoMissing()
        {
            if (NextTetrimino == null)
            {
                string msg = "Desired operation requires a 'next Tetrimino' to be set.";
                throw new Exceptions.MissingTetriminoException(msg);
            }
        }

        private void ValidateCurrentTetriminoOverwrite()
        {
            if (CurrentTetrimino != null)
            {
                string msg = "Trying to overwrite 'current Tetrimino': not allowed.";
                throw new Exceptions.NoOverwriteTetriminoException(msg);
            }
        }

        private void ValidateNextTetriminoOverwrite()
        {
            if (NextTetrimino != null)
            {
                string msg = "Trying to overwrite 'next Tetrimino': not allowed.";
                throw new Exceptions.NoOverwriteTetriminoException(msg);
            }
        }
    }
}
