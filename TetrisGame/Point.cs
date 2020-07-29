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

    public int x { get; private set; }
    public int y { get; private set; }

    public Point(int atX, int atY)
    {
      x = atX;
      y = atY;
    }
  }
}