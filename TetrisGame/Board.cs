using System;
using System.Collections.Generic;

namespace Tetris
{
  public class Board
  {
    public int width { get; private set; }
    public int height { get; private set; }

    private ITile[,] _tiles;
    public Dictionary<ITile, Point> _tilePoints;
    public List<ITile> _allTiles;
    public Board(int boardWidth, int boardHeight)
    {
        _allTiles = new List<ITile>();
        width = boardWidth;
        height = boardHeight;

        _tiles = new ITile[height, width];
        _tilePoints = new Dictionary<ITile, Point>();
    }

    public ITile TileAt(Point point) { return _tiles[point.y, point.x]; }

    public ITile[] AllTiles() {
        return _allTiles.ToArray();
    }

    public void AddTileAt(ITile tile, Point atPoint)
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
            ITile tile = TileAt(atPoint);
            UnplaceTileAt(tile, atPoint);
            _allTiles.Remove(tile);
        }
    }

    public void RemoveTile(ITile tile)
    {
        if (IsPlaced(tile))
        {
            Point atPoint = TilePoint(tile);
            UnplaceTileAt(tile, atPoint);
            _allTiles.Remove(tile);
        }
    }

    public Point TilePoint(ITile tile)
    {
        if (IsPlaced(tile)) return _tilePoints[tile];

        return null;
    }

    public void MoveTile(ITile tile, Point byPoint)
    {
        Point atPoint = TilePoint(tile);
        Point toPoint = Point.AddPoints(atPoint, byPoint);
        if (IsInsideBoard(toPoint) && IsEmptySpot(toPoint))
        {
            UnplaceTileAt(tile, atPoint);
            PlaceTileAt(tile, toPoint);
        }
    }

    public List<ITile[]> TilesInRows()
    {
        List<ITile[]> rows = new List<ITile[]>();
        for (int y = 0; y < height; y++)
        {
            if (HasRow(y)) rows.Add(Row(y));
        }
        return rows;
    }

    public bool IsEmptyBelowTile(ITile tile)
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

    private ITile[] Row(int atY)
    {
        ITile[] row = new ITile[width];
        for (int x = 0; x < width; x++)
        {
            Point atPoint = new Point(x, atY);
            row[x] = TileAt(atPoint);
        }
        return row;
    }

    private void PlaceTileAt(ITile tile, Point atPoint)
    {
        _tiles[atPoint.y, atPoint.x] = tile;
        _tilePoints[tile] = atPoint;
    }

    private void UnplaceTileAt(ITile tile, Point atPoint)
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

    private bool IsPlaced(ITile tile)
    {
        if (_allTiles.Contains(tile) && _tilePoints[tile] != null) return true;

        return false;
    }

    private void ValidateTilePlacement(ITile tile)
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