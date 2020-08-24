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
                string row = printRows[i];
                framedPrint += $"│{PadOutString(row, printWidth)}│\n";
            }
            framedPrint += $"└{PrintLine(printWidth)}┘\n";
            return framedPrint;
        }

        public string ConnectPrintsHorizontally(string leftPrint, string rightPrint, int spaceBetween)
        {
            string[] leftRows = leftPrint.Split("\n");
            string[] rightRows = rightPrint.Split("\n");
            int maxHeight = Utils.LongerArrayOfTwoArrays<string>(rightRows, leftRows).Length;

            string[] combinedRows = new string[maxHeight];
            int leftMinWidth = leftRows[0].Length;
            int righMintWidth = rightRows[0].Length;

            for (int i = 0; i < maxHeight; i++)
            {
                string leftEntry;
                string rightEntry;

                bool hasLeftEntry = (i < leftRows.Length && leftRows[i].Length > 0);
                if (hasLeftEntry) leftEntry = leftRows[i];
                else leftEntry = PadOutString(leftMinWidth);

                bool hasRightEntry = (i < rightRows.Length && rightRows[i].Length > 0);
                if (hasRightEntry) rightEntry = rightRows[i];
                else rightEntry = PadOutString(righMintWidth);

                string space = PadOutString(spaceBetween);
                combinedRows[i] = $"{leftEntry}{space}{rightEntry}";
            }

            return StrArrToStr(combinedRows);
        }

        public string ConnectPrintsVertically(string upPrint, string downPrint, int spaceBetween)
        {
            string[] downRows = downPrint.Split("\n");
            string[] upRows = upPrint.Split("\n");

            string combinedRows = "";

            int minWidth = Utils.LongerStringOfTwoStrings(downRows[0], upRows[0]).Length;

            for (int i = 0; i < upRows.Length; i++)
            {
                int width = upRows[0].Length;
                if (upRows[i].Length == 0) combinedRows += $"{PadOutString(minWidth)}\n";
                else combinedRows += $"{upRows[i]}{PadOutString(minWidth - width)}\n";

            }

            for (int i = 0; i < spaceBetween; i++) {
                combinedRows += $"{PadOutString(minWidth)}\n";
            }

            for (int i = 0; i < downRows.Length; i++)
            {
                int width = downRows[0].Length;
                if (downRows[i].Length == 0) combinedRows += $"{PadOutString(minWidth)}\n";
                else combinedRows += $"{downRows[i]}{PadOutString(minWidth - width)}\n";

            }

            return combinedRows;
        }

        private string RepeatingString(string str, int repeats)
        {
            string repeatedString = "";
            for (int i = 0; i < repeats; i++) { repeatedString += str; }
            return repeatedString;
        }

        private string PadOutString(string str, int toLength)
        {
            return str + RepeatingString(" ", (toLength - str.Length));
        }

        private string PadOutString(int toLength)
        {
            return RepeatingString(" ", toLength);
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