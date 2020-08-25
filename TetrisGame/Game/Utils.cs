namespace Tetris
{
    public class Utils
    {
        public static T[] LongerArrayOfTwoArrays<T>(T[] arrA, T[] arrB)
        {
            if (arrA.Length >= arrB.Length) return arrA;
            return arrB;
        }

        public static string LongerStringOfTwoStrings(string strA, string strB)
        {
            if (strA.Length >= strB.Length) return strA;
            return strB;

        }

        public static string LongestStringInTwoArrays(string[] arrA, string[] arrB)
        {
            int entryLength = -1;
            string longestEntry = "";

            foreach (string entry in arrA)
            {
                if (entry.Length > entryLength)
                {
                    entryLength = entry.Length;
                    longestEntry = entry;
                }
            }

            foreach (string entry in arrB)
            {
                if (entry.Length > entryLength)
                {
                    entryLength = entry.Length;
                    longestEntry = entry;
                }
            }

            return longestEntry;
        }

        public static string PadOutString(string str, int toLength)
        {
            // str += RepeatingString(" ", (toLength - str.Length));
            return "";
        }
    }
}