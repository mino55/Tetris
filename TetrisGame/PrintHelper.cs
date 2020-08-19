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

        public string HorizontalPrintConnect(string leftPrint, string rightPrint, int spaces)
        {
            string[] leftRows = leftPrint.Split("\n");
            string[] rightRows = rightPrint.Split("\n");
            string[] longerArray = Utils.LongerArrayOfTwoArrays<string>(rightRows, leftRows);

            string[] combinedRows = new string[longerArray.Length];
            for (int i = 0; i < longerArray.Length; i++)
            {
                string leftEntry;
                string rightEntry;

                bool hasLeftEntry = (i < leftRows.Length && leftRows[i].Length > 0);
                if (hasLeftEntry) leftEntry = leftRows[i];
                else leftEntry = RepeatingString(" ", leftRows[0].Length);

                bool hasRightEntry = (i < rightRows.Length && rightRows[i].Length > 0);
                if (hasRightEntry) rightEntry = rightRows[i];
                else rightEntry = RepeatingString(" ", rightRows[0].Length);

                string space = RepeatingString(" ", spaces);
                combinedRows[i] = $"{leftEntry}{space}{rightEntry}";
            }

            return StrArrToStr(combinedRows);
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