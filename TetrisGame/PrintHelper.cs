namespace Tetris
{
    public class PrintHelper
    {
        public string BoardPrint(Board board)
        {
            int width = board.width;
            int height = board.height;
            string boardPrint = "";

            for (int y = 0; y <= (height - 1); y++)
            {
                string row = "";
                for (int x = 0; x <= (width - 1); x++)
                {
                    Point atPoint = new Point(x, y);
                    if (board.BlockAt(atPoint) == null)
                    {
                        if (x % 2 == 0) row += " . ";
                        else row += "   ";
                    }
                    else row += board.BlockAt(atPoint).Print();
                }
                boardPrint += $"{row}\n";
            }

            return boardPrint;
        }

        public string PrintWithFrame(string print, int printWidth)
        {
            string framedPrint = "";
            string[] printRows = print.Split("\n");
            framedPrint += $"┌{PrintLine(printWidth)}┐\n";
            foreach(string row in printRows) {
                if (row.Length > 0) framedPrint += $"│{row}│\n";
            }
            framedPrint += $"└{PrintLine(printWidth)}┘\n";
            return framedPrint;
        }

        public string VerticalPrintCombine(string leftPrint, string rightPrint, int spaces)
        {
            string[] leftRows = leftPrint.Split("\n");
            string[] rightRows = rightPrint.Split("\n");

            string[] shorterArray = ShorterArrayOfTwoArrays(leftRows, rightRows);
            string[] longerArray = LongerArrayOfTwoArrays(rightRows, leftRows);

            string[] shorterArrayPadded = PadOutArrayLength(shorterArray, longerArray.Length);

            for (int i = (shorterArray.Length - 1); i < shorterArrayPadded.Length; i++)
            {
                shorterArrayPadded[i] = RepeatingString(" ", shorterArray[0].Length);
            }

            string[] combinedRows = new string[longerArray.Length];
            for (int i = 0; i < longerArray.Length; i++)
            {
                string space = RepeatingString(" ", spaces);
                combinedRows[i] = $"{shorterArrayPadded[i]}{space}{longerArray[i]}";
            }

            return StrArrToStr(combinedRows);
        }

        private string[] ShorterArrayOfTwoArrays(string[] arrA, string[] arrB)
        {
            if (arrA.Length <= arrB.Length) return arrA;
            return arrB;
        }

        private string[] LongerArrayOfTwoArrays(string[] arrA, string[] arrB)
        {
            if (arrA.Length >= arrB.Length) return arrA;
            return arrB;
        }

        private string[] PadOutArrayLength(string[] array, int toLength)
        {
            string[] paddedOutArray = new string[toLength];
            for (int i = 0; i < array.Length; i++)
            {
                paddedOutArray[i] = array[i];
            }
            return paddedOutArray;
        }

        private string RepeatingString(string str, int repeats)
        {
            string repeatedString = "";
            for (int i = 0; i < repeats; i++) { repeatedString += str; }
            return repeatedString;
        }

        private string PrintLine(int length)
        {
            string line = "";
            for(int i = 0; i < length; i++) { line += "─"; }
            return line;
        }

        private string StrArrToStr(string[] strArr)
        {
            string str = "";
            foreach (string row in strArr) { str += $"{row}\n"; }
            return str;
        }
    }
}