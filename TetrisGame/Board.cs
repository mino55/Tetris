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
    public List<ITile> allTiles { get; private set; }
    public Board(int boardWidth, int boardHeight)
    {
        allTiles = new List<ITile>();
        width = boardWidth;
        height = boardHeight;

        _tiles = new ITile[height, width];
        _tilePoints = new Dictionary<ITile, Point>();
    }

    public ITile TileAt(Point point) { return _tiles[point.y, point.x]; }

    public void AddTileAt(ITile tile, Point atPoint)
    {
        if (InsideBoard(atPoint) && EmptySpot(atPoint))
        {
            PlaceTileAt(tile, atPoint);
            allTiles.Add(tile);
        }
    }

    public Point TilePoint(ITile tile)
    {
        return _tilePoints[tile];
    }

    public void MoveTile(ITile tile, Point byPoint)
    {
        Point atPoint = TilePoint(tile);
        Point toPoint = Point.AddPoints(atPoint, byPoint);
        if (InsideBoard(toPoint) && EmptySpot(toPoint))
        {
            UnplaceTileAt(tile, atPoint);
            PlaceTileAt(tile, toPoint);
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
        bool inside_x = atPoint.x < width && atPoint.x >= 0;
        bool inside_y = atPoint.y < height && atPoint.y >= 0;
        return inside_x && inside_y;
    }

    private bool EmptySpot(Point atPoint)
    {
        if (TileAt(atPoint) == null) return true;

        return false;
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