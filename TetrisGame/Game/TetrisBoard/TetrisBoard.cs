using System.Collections.Generic;

namespace Tetris
{
    public class TetrisBoard : Board
    {
        private readonly Dictionary<Tetrimino, Point> _tetriminoPoints;

        private readonly List<Tetrimino> _allTetriminos;

        public TetrisBoard(int boardWidth, int boardHeight) : base(boardWidth, boardHeight)
        {
            _allTetriminos = new List<Tetrimino>();
            _tetriminoPoints = new Dictionary<Tetrimino, Point>();
        }

        public Tetrimino[] AllTetriminos()
        {
            return _allTetriminos.ToArray();
        }

        public void AddTetriminoAt(Tetrimino tetrimino, Point atPoint)
        {
            PlaceTetriminoAt(tetrimino, atPoint);
            _allTetriminos.Add(tetrimino);
        }

        public void RemoveTetrimino(Tetrimino tetrimino)
        {
            ValidateTetriminoAdded(tetrimino);

            UnplaceTetrimino(tetrimino);
            _allTetriminos.Remove(tetrimino);
        }

        public void ReleaseTetrimino(Tetrimino tetrimino)
        {
            ValidateTetriminoAdded(tetrimino);

            _allTetriminos.Remove(tetrimino);
            _tetriminoPoints[tetrimino] = null;
        }

        public Point TetriminoPoint(Tetrimino tetrimino)
        {
            if (!_allTetriminos.Contains(tetrimino)) return null;

            return _tetriminoPoints[tetrimino];
        }

        public bool CanMoveTetrimino(Tetrimino tetrimino, Point byPoint)
        {
            Point atPoint = TetriminoPoint(tetrimino);
            Point toPoint = Point.AddPoints(atPoint, byPoint);
            return TetriminoFitsAt(tetrimino, toPoint, tetrimino.Direction);
        }

        public void MoveTetrimino(Tetrimino tetrimino, Point byPoint)
        {
            ValidateTetriminoAdded(tetrimino);

            Point atPoint = TetriminoPoint(tetrimino);
            Point toPoint = Point.AddPoints(atPoint, byPoint);
            UnplaceTetrimino(tetrimino);
            PlaceTetriminoAt(tetrimino, toPoint);
        }

        public bool CanRotate(Tetrimino tetrimino, Rotation rotation)
        {
            return rotation switch
            {
                Rotation.CLOCKWISE => CanRotateDir(tetrimino, tetrimino.ClockwiseRotation()),
                Rotation.REVERSE => CanRotateDir(tetrimino, tetrimino.ReverseRotation()),
                Rotation.FLIP => CanRotateDir(tetrimino, tetrimino.FlipRotation()),
                _ => false,
            };
        }

        public void Rotate(Tetrimino tetrimino, Rotation rotation)
        {
            ValidateTetriminoAdded(tetrimino);

            switch (rotation)
            {
                case Rotation.CLOCKWISE:
                    RotateDir(tetrimino, tetrimino.ClockwiseRotation());
                    break;
                case Rotation.REVERSE:
                    RotateDir(tetrimino, tetrimino.ReverseRotation());
                    break;
                case Rotation.FLIP:
                    RotateDir(tetrimino, tetrimino.FlipRotation());
                    break;
                default:
                    break;
            }
        }

        private bool CanRotateDir(Tetrimino tetrimino, Direction dir)
        {
            Point atPoint = TetriminoPoint(tetrimino);
            return TetriminoFitsAt(tetrimino, atPoint, dir);
        }

        private void RotateDir(Tetrimino tetrimino, Direction dir)
        {
            tetrimino.RotateTo(dir);
            Point atPoint = TetriminoPoint(tetrimino);
            UnplaceTetrimino(tetrimino);
            PlaceTetriminoAt(tetrimino, atPoint);
        }

        private void PlaceTetriminoAt(Tetrimino tetrimino, Point atPoint)
        {
            foreach (Point shapePoint in tetrimino.ShapePoints(tetrimino.Direction))
            {
                Block shapeBlock = tetrimino.ShapeBlockAt(shapePoint, tetrimino.Direction);

                if (shapeBlock == null) continue;

                Point boardPoint = ShapePointToBoardPoint(shapePoint, tetrimino, atPoint);

                AddBlockAt(shapeBlock, boardPoint);
            }

            _tetriminoPoints[tetrimino] = atPoint;
        }

        private void UnplaceTetrimino(Tetrimino tetrimino)
        {
            foreach (Block block in tetrimino.Blocks)
            {
                Point atPoint = BlockPoint(block);
                RemoveBlockAt(atPoint);
            }

            _tetriminoPoints[tetrimino] = null;
        }

        private Point ShapeStartingPoint(Point centerOffset, Point atCenterPoint)
        {
            return Point.SubtractPoints(atCenterPoint, centerOffset);
        }

        private bool TetriminoFitsAt(Tetrimino tetrimino, Point atPoint, Direction direction)
        {
            foreach (Point shapePoint in tetrimino.ShapePoints(direction))
            {
                Block shapeBlock = tetrimino.ShapeBlockAt(shapePoint, direction);

                if (shapeBlock == null) continue;

                Point boardPoint = ShapePointToBoardPoint(shapePoint, tetrimino, atPoint);

                if (TetriminoCollisionAt(tetrimino, boardPoint)) return false;
            }
            return true;
        }

        private Point ShapePointToBoardPoint(Point shapePoint,
                                             Tetrimino tetrimino,
                                             Point atPoint)
        {
            Point centerOffset = tetrimino.ShapeCenterOffset();
            Point shapeStartingPoint = ShapeStartingPoint(centerOffset, atPoint);
            return Point.AddPoints(shapeStartingPoint, shapePoint);
        }

        private bool TetriminoCollisionAt(Tetrimino tetrimino, Point atPoint)
        {
            if (!IsInsideBoard(atPoint)) return true;

            if (!IsEmptySpot(atPoint))
            {
                Block blockAtPoint = BlockAt(atPoint);
                if (!tetrimino.ContainsBlock(blockAtPoint)) return true;
            }

            return false;
        }

        private void ValidateTetriminoAdded(Tetrimino tetrimino)
        {
            if (!_allTetriminos.Contains(tetrimino))
            {
                string msg = "The target Tetrimino has not been added to the board";
                throw new Exceptions.MissingTetriminoException(msg);
            }
        }
    }
}
