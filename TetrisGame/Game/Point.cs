using System;

namespace Tetris
{
  public class Point
  {
    public static Point AddPoints(Point a, Point b)
    {
        int sumX = a.x + b.x;
        int sumY = a.y + b.y;
        return new Point(sumX, sumY);
    }

    public static Point SubtractPoints(Point a, Point b)
    {
        int diffX = a.x - b.x;
        int diffY = a.y - b.y;
        return new Point(diffX, diffY);
    }

    public int x { get; private set; }
    public int y { get; private set; }

    public Point(int atX, int atY)
    {
      x = atX;
      y = atY;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null) return false;

        if (!this.GetType().Equals(obj.GetType())) return false;

        Point point = (Point) obj;
        return (x == point.x) && (y == point.y);
    }

    public override int GetHashCode()
    {
        return int.Parse($"{x}{y}");
    }
  }
}