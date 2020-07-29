using System;
using System.Collections.Generic;

namespace Tetris
{
  public class Board
  {
    private int _width;
    private int _height;

    private ITile[,] _tiles;
    private List<ITile> _allTiles;
    private Dictionary<ITile, Point> _tilePoints;
    public Board(int boardWidth, int boardHeight)
    {
      _width = boardWidth;
      _height = boardHeight;
      _tiles = new ITile[_height, _width];
      _allTiles = new List<ITile>();
      _tilePoints = new Dictionary<ITile, Point>();
    }

    public ITile TileAt(Point point) { return _tiles[point.y, point.x]; }

    public void Fall()
    {
        foreach (ITile tile in _allTiles)
        {
            Point atPoint = TilePoint(tile);
            if (atPoint.y != 0)
            {
            MoveTile(tile, new Point(0, 1));
            }
        }
    }

    public void AddTileAt(ITile tile, Point atPoint)
    {
      if (InsideBoard(atPoint) && EmptySpot(atPoint))
      {
        _allTiles.Add(tile);
        _tiles[atPoint.y, atPoint.x] = tile;
        _tilePoints[tile] = atPoint;
      }
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

    private bool InsideBoard(Point atPoint)
    {
        bool inside_x = atPoint.x <_width && atPoint.x >= 0;
        bool inside_y = atPoint.y <_height && atPoint.y >= 0;
        return inside_x && inside_y;
    }

    private bool EmptySpot(Point atPoint)
    {
      if (TileAt(atPoint) == null) return true;

      return false;
    }

    private Point TilePoint(ITile tile)
    {
      return _tilePoints[tile];
    }

    private void MoveTile(ITile tile, Point byPoint)
    {
        Point atPoint = TilePoint(tile);
        Point toPoint = Point.AddPoints(atPoint, byPoint);
        if (InsideBoard(toPoint) && EmptySpot(toPoint))
        {
            UnplaceTileAt(tile, atPoint);
            PlaceTileAt(tile, toPoint);
        }
    }

    public string Print()
    {
      string tilesStr = "";
      for (int y = 0; y <= (_height - 1); y++)
        {
          for (int x = 0; x <= (_width - 1); x++)
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