namespace Tetris
{
    public class Point
    {
        public static Point AddPoints(Point a, Point b)
        {
            int sumX = a.X + b.X;
            int sumY = a.Y + b.Y;
            return new Point(sumX, sumY);
        }

        public static Point SubtractPoints(Point a, Point b)
        {
            int diffX = a.X - b.X;
            int diffY = a.Y - b.Y;
            return new Point(diffX, diffY);
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int atX, int atY)
        {
            X = atX;
            Y = atY;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (!GetType().Equals(obj.GetType())) return false;

            Point point = (Point)obj;
            return (X == point.X) && (Y == point.Y);
        }

        public override int GetHashCode()
        {
            return int.Parse($"{X}{Y}");
        }
    }
}
