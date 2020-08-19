namespace Tetris
{
    public class Utils
    {
        public static T[] LongerArrayOfTwoArrays<T>(T[] arrA, T[] arrB)
        {
            if (arrA.Length >= arrB.Length) return arrA;
            return arrB;
        }
    }
}