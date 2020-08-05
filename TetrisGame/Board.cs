using System;
using System.Collections.Generic;

namespace Tetris
{
  public class Board
  {
    public int width { get; private set; }
    public int height { get; private set; }

    private Block[,] _tiles;
    public Dictionary<Block, Point> _tilePoints;
    public List<Block> _allTiles;
    public Board(int boardWidth, int boardHeight)
    {
        _allTiles = new List<Block>();
        width = boardWidth;
        height = boardHeight;

        _tiles = new Block[height, width];
        _tilePoints = new Dictionary<Block, Point>();
    }

    public Block TileAt(Point point) { return _tiles[point.y, point.x]; }

    public Block[] AllTiles() {
        return _allTiles.ToArray();
    }

    public void AddTileAt(Block tile, Point atPoint)
    {
        if (IsInsideBoard(atPoint) && IsEmptySpot(atPoint))
        {
            PlaceTileAt(tile, atPoint);
            _allTiles.Add(tile);
        }
    }

    public void RemoveTileAt(Point atPoint)
    {
        if (IsInsideBoard(atPoint) && !IsEmptySpot(atPoint))
        {
            Block tile = TileAt(atPoint);
            UnplaceTileAt(tile, atPoint);
            _allTiles.Remove(tile);
        }
    }

    public void RemoveTile(Block tile)
    {
        if (IsPlaced(tile))
        {
            Point atPoint = TilePoint(tile);
            UnplaceTileAt(tile, atPoint);
            _allTiles.Remove(tile);
        }
    }

    public Point TilePoint(Block tile)
    {
        if (IsPlaced(tile)) return _tilePoints[tile];

        return null;
    }

    public void MoveTile(Block tile, Point byPoint)
    {
        Point atPoint = TilePoint(tile);
        Point toPoint = Point.AddPoints(atPoint, byPoint);
        if (IsInsideBoard(toPoint) && IsEmptySpot(toPoint))
        {
            UnplaceTileAt(tile, atPoint);
            PlaceTileAt(tile, toPoint);
        }
    }

    public List<Block[]> TilesInRows()
    {
        List<Block[]> rows = new List<Block[]>();
        for (int y = 0; y < height; y++)
        {
            if (HasRow(y)) rows.Add(Row(y));
        }
        return rows;
    }

    public bool IsEmptyBelowTile(Block tile)
    {
        ValidateTilePlacement(tile);

        Point tilePoint = TilePoint(tile);
        Point pointBelow = Point.AddPoints(tilePoint, new Point(0, 1));
        return IsInsideBoard(pointBelow) && IsEmptySpot(pointBelow);
    }

    private bool HasRow(int atY)
    {
        for (int x = 0; x < width; x++)
        {
            Point atPoint = new Point(x, atY);
            if (TileAt(atPoint) == null) return false;
        }
        return true;
    }

    private Block[] Row(int atY)
    {
        Block[] row = new Block[width];
        for (int x = 0; x < width; x++)
        {
            Point atPoint = new Point(x, atY);
            row[x] = TileAt(atPoint);
        }
        return row;
    }

    private void PlaceTileAt(Block tile, Point atPoint)
    {
        _tiles[atPoint.y, atPoint.x] = tile;
        _tilePoints[tile] = atPoint;
    }

    private void UnplaceTileAt(Block tile, Point atPoint)
    {
        _tiles[atPoint.y, atPoint.x] = null;
        _tilePoints[tile] = null;
    }

    private bool IsInsideBoard(Point atPoint)
    {
        bool inside_x = atPoint.x < width && atPoint.x >= 0;
        bool inside_y = atPoint.y < height && atPoint.y >= 0;
        return inside_x && inside_y;
    }

    private bool IsEmptySpot(Point atPoint)
    {
        if (TileAt(atPoint) == null) return true;

        return false;
    }

    private bool IsPlaced(Block tile)
    {
        if (_allTiles.Contains(tile) && _tilePoints[tile] != null) return true;

        return false;
    }

    private void ValidateTilePlacement(Block tile)
    {
        if (TilePoint(tile) == null)
        {
            string msg = "The referenced tile has not been placed on board.";
            throw new Tetris.Exceptions.TileNotPlacedException(msg);
        }
    }

    public string Print()
    {
        string tilesStr = "";
        for (int y = 0; y <= (height - 1); y++)
            {
            for (int x = 0; x <= (width - 1); x++)
            {
                Point atPoint = new Point(x, y);
                if (TileAt(atPoint) == null) tilesStr += " _ ";
                else tilesStr += TileAt(atPoint).Print();
            }
            tilesStr += "\n";
            }
        return tilesStr;
        }
  }
}