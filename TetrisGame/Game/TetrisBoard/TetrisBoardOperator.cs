namespace Tetris
{
    public class TetrisBoardOperator
    {
        public bool CurrentTetriminoIsLocked { get; private set; }
        public TetrisBoard TetrisBoard { get; private set; }
        public Tetrimino CurrentTetrimino { get; private set; }
        public Tetrimino NextTetrimino { get; private set; }

        private Point _nextTetriminoStartPoint;

        public TetrisBoardOperator(TetrisBoard tetrisBoard)
        {
            TetrisBoard = tetrisBoard;
        }

        public void NewCurrentTetrimino(Tetrimino tetrimino, Point startPoint)
        {
            ValidateCurrentTetriminoOverwrite();

            CurrentTetrimino = tetrimino;
            TetrisBoard.AddTetriminoAt(tetrimino, startPoint);
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

            if (TetrisBoard.CanMoveTetrimino(CurrentTetrimino, new Point(0, 1)))
            {
                TetrisBoard.MoveTetrimino(CurrentTetrimino, new Point(0, 1));
            }
            else CurrentTetriminoIsLocked = true;
        }

        public void MoveCurrentTetriminoRight()
        {
            ValidateCurrentTetriminoMissing();

            if (TetrisBoard.CanMoveTetrimino(CurrentTetrimino, new Point(1, 0)))
            {
                TetrisBoard.MoveTetrimino(CurrentTetrimino, new Point(1, 0));
            }
        }

        public void MoveCurrentTetriminoLeft()
        {
            ValidateCurrentTetriminoMissing();

            if (TetrisBoard.CanMoveTetrimino(CurrentTetrimino, new Point(-1, 0)))
            {
                TetrisBoard.MoveTetrimino(CurrentTetrimino, new Point(-1, 0));
            }
        }

        public void RotateCurrentTetrimino(Rotation rotation)
        {
            ValidateCurrentTetriminoMissing();

            if (TetrisBoard.CanRotate(CurrentTetrimino, rotation))
            {
                TetrisBoard.Rotate(CurrentTetrimino, rotation);
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
            return TetrisBoard.BlocksInRows().Count;
        }

        public void CleanRows()
        {
            foreach (Block[] row in TetrisBoard.BlocksInRows())
            {
                int at_y = TetrisBoard.BlockPoint(row[0]).Y;
                ClearRow(row);
                FillRowGapAt(at_y);
            }
        }

        private void ClearRow(Block[] row)
        {
            foreach (Block tile in row)
            {
                TetrisBoard.RemoveBlock(tile);
            }
        }

        private void FillRowGapAt(int atY)
        {
            for (int y = TetrisBoard.Height - 1; y >= 0; y--)
            {
                if (y > atY) continue;

                for (int x = 0; x < TetrisBoard.Width; x++)
                {
                    Block block = TetrisBoard.BlockAt(new Point(x, y));
                    if (block == null) continue;

                    TetrisBoard.MoveBlock(block, new Point(0, 1));
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
