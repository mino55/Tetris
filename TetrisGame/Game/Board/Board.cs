using System.Collections.Generic;

namespace Tetris
{
    public class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Block> _allBlocks = new List<Block>();

        private readonly Block[,] _tiles;
        private readonly Dictionary<Block, Point> _blockPoints = new Dictionary<Block, Point>();

        public Board(int boardWidth, int boardHeight)
        {
            Width = boardWidth;
            Height = boardHeight;

            _tiles = new Block[Height, Width];
        }

        public Block BlockAt(Point point) { return _tiles[point.Y, point.X]; }

        public Block[] AllBlocks()
        {
            return _allBlocks.ToArray();
        }

        public void AddBlockAt(Block block, Point atPoint)
        {
            if (!IsInsideBoard(atPoint)) throw new Exceptions.BlockOutsideBoardException();
            if (!IsEmptySpot(atPoint)) throw new Exceptions.NoOverwriteBlockException();

            PlaceBlockAt(block, atPoint);
            _allBlocks.Add(block);
        }

        public void RemoveBlockAt(Point atPoint)
        {
            if (!IsInsideBoard(atPoint)) throw new Exceptions.BlockOutsideBoardException();
            if (IsEmptySpot(atPoint)) throw new Exceptions.MissingBlockException();

            Block block = BlockAt(atPoint);
            UnplaceBlockAt(block, atPoint);
            _allBlocks.Remove(block);
        }

        public void RemoveBlock(Block block)
        {
            if (IsPlaced(block))
            {
                Point atPoint = BlockPoint(block);
                UnplaceBlockAt(block, atPoint);
                _allBlocks.Remove(block);
            }
        }

        public Point BlockPoint(Block block)
        {
            if (IsPlaced(block)) return _blockPoints[block];

            return null;
        }

        public void MoveBlock(Block bock, Point byPoint)
        {
            Point atPoint = BlockPoint(bock);
            Point toPoint = Point.AddPoints(atPoint, byPoint);
            if (IsInsideBoard(toPoint) && IsEmptySpot(toPoint))
            {
                UnplaceBlockAt(bock, atPoint);
                PlaceBlockAt(bock, toPoint);
            }
        }

        public List<Block[]> BlocksInRows()
        {
            List<Block[]> rows = new List<Block[]>();
            for (int y = 0; y < Height; y++)
            {
                if (HasRow(y)) rows.Add(Row(y));
            }
            return rows;
        }

        public bool IsEmptyBelowBlock(Block block)
        {
            ValidateBlockPlacement(block);

            Point blockPoint = BlockPoint(block);
            Point pointBelow = Point.AddPoints(blockPoint, new Point(0, 1));
            return IsInsideBoard(pointBelow) && IsEmptySpot(pointBelow);
        }

        private bool HasRow(int atY)
        {
            for (int x = 0; x < Width; x++)
            {
                Point atPoint = new Point(x, atY);
                if (BlockAt(atPoint) == null) return false;
            }
            return true;
        }

        private Block[] Row(int atY)
        {
            Block[] row = new Block[Width];
            for (int x = 0; x < Width; x++)
            {
                Point atPoint = new Point(x, atY);
                row[x] = BlockAt(atPoint);
            }
            return row;
        }

        protected void PlaceBlockAt(Block block, Point atPoint)
        {
            _tiles[atPoint.Y, atPoint.X] = block;
            _blockPoints[block] = atPoint;
        }

        protected void UnplaceBlockAt(Block block, Point atPoint)
        {
            _tiles[atPoint.Y, atPoint.X] = null;
            _blockPoints[block] = null;
        }

        protected bool IsInsideBoard(Point atPoint)
        {
            bool inside_x = atPoint.X < Width && atPoint.X >= 0;
            bool inside_y = atPoint.Y < Height && atPoint.Y >= 0;
            return inside_x && inside_y;
        }

        protected bool IsEmptySpot(Point atPoint)
        {
            if (BlockAt(atPoint) == null) return true;

            return false;
        }

        private bool IsPlaced(Block block)
        {
            if (_allBlocks.Contains(block) && _blockPoints[block] != null) return true;

            return false;
        }

        private void ValidateBlockPlacement(Block block)
        {
            if (BlockPoint(block) == null)
            {
                string msg = "The referenced block has not been placed on board.";
                throw new Exceptions.BlockNotPlacedException(msg);
            }
        }
    }
}
