namespace Tetris
{
    public class PrintHelper
    {
        public string BoardPrint(Board board)
        {
            int width = board.width;
            int height = board.height;
            string boardPrint = "";

            for (int y = 0; y < height; y++)
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
                boardPrint += $"{row}";
                if (y < (height - 1)) boardPrint += "\n";
            }

            return boardPrint;
        }

        public string PrintWithFrame(string print, int printWidth)
        {
            string framedPrint = "";
            string[] printRows = print.Split("\n");
            framedPrint += $"┌{PrintLine(printWidth)}┐\n";
            for (int i = 0; i < printRows.Length; i++) {
                // if (row.Length == 0) continue;
                string row = printRows[i];
                if (row.Length < printWidth)
                {
                    row += RepeatingString(" ", (printWidth - row.Length));
                }
                framedPrint += $"│{row}│\n";
            }
            framedPrint += $"└{PrintLine(printWidth)}┘\n";
            return framedPrint;
        }

        public string ConnectPrintsHorizontally(string leftPrint, string rightPrint, int spaceBetween)
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

                string space = RepeatingString(" ", spaceBetween);
                combinedRows[i] = $"{leftEntry}{space}{rightEntry}";
            }

            return StrArrToStr(combinedRows);
        }

        public string ConnectPrintsVertically(string upPrint, string downPrint, int spaceBetween)
        {
            string[] downRows = downPrint.Split("\n");
            string[] upRows = upPrint.Split("\n");

            string combinedRows = "";

            string longestEntry = Utils.LongerStringOfTwoStrings(downRows[0], upRows[0]);

            for (int i = 0; i < upRows.Length; i++)
            {
                string padding = "";
                if (upRows[i].Length == 0) padding += RepeatingString(" ", longestEntry.Length);
                else padding += RepeatingString(" ", (longestEntry.Length - upRows[0].Length));
                combinedRows += $"{upRows[i]}{padding}\n";
            }

            for (int i = 0; i < spaceBetween; i++) {
                combinedRows += $"{RepeatingString(" ", longestEntry.Length)}\n";
            }

            for (int i = 0; i < downRows.Length; i++)
            {
                string padding = "";
                if (downRows[i].Length == 0) padding += RepeatingString(" ", longestEntry.Length);
                else padding += RepeatingString(" ", (longestEntry.Length - downRows[0].Length));
                combinedRows += $"{downRows[i]}{padding}\n";
            }

            return combinedRows;
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