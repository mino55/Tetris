using System;
using System.Collections.Generic;

namespace Tetris
{
  public class Board
  {
    public int width { get; private set; }
    public int height { get; private set; }

    private Block[,] _tiles;
    private Dictionary<Block, Point> _blockPoints;
    public List<Block> _allBlocks;
    public int ChangeCount { get; private set; }
    public Board(int boardWidth, int boardHeight)
    {
        _allBlocks = new List<Block>();
        width = boardWidth;
        height = boardHeight;

        _tiles = new Block[height, width];
        _blockPoints = new Dictionary<Block, Point>();
        ChangeCount = 0;
    }

    public Block BlockAt(Point point) { return _tiles[point.y, point.x]; }

    public Block[] AllBlocks() {
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
        for (int y = 0; y < height; y++)
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
        for (int x = 0; x < width; x++)
        {
            Point atPoint = new Point(x, atY);
            if (BlockAt(atPoint) == null) return false;
        }
        return true;
    }

    private Block[] Row(int atY)
    {
        Block[] row = new Block[width];
        for (int x = 0; x < width; x++)
        {
            Point atPoint = new Point(x, atY);
            row[x] = BlockAt(atPoint);
        }
        return row;
    }

    protected void PlaceBlockAt(Block block, Point atPoint)
    {
        _tiles[atPoint.y, atPoint.x] = block;
        _blockPoints[block] = atPoint;
        ChangeCount++;
    }

    protected void UnplaceBlockAt(Block block, Point atPoint)
    {
        _tiles[atPoint.y, atPoint.x] = null;
        _blockPoints[block] = null;
        ChangeCount++;
    }

    protected bool IsInsideBoard(Point atPoint)
    {
        bool inside_x = atPoint.x < width && atPoint.x >= 0;
        bool inside_y = atPoint.y < height && atPoint.y >= 0;
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
            throw new Tetris.Exceptions.BlockNotPlacedException(msg);
        }
    }

    public string Print()
    {
        // Framed with:
        // ┌───────────────────────┐
        // │	                   │
        // └───────────────────────┘
        string tilesStr = "";
        tilesStr += $"┌{DrawLine(width * 3)}┐\n";
        for (int y = 0; y <= (height - 1); y++)
            {
            tilesStr += "│";
            for (int x = 0; x <= (width - 1); x++)
            {
                Point atPoint = new Point(x, y);
                if (BlockAt(atPoint) == null)
                {
                    if (x % 2 == 0) tilesStr += " . ";
                    else tilesStr += "   ";
                }
                else tilesStr += BlockAt(atPoint).Print();
            }
            tilesStr += "│\n";
            }
        tilesStr += $"└{DrawLine(width * 3)}┘\n";
        return tilesStr;
        }

        private string DrawLine(int length)
        {
            string line = "";
            for(int i = 0; i < length; i++) { line += "─"; }
            return line;
        }
    }
}