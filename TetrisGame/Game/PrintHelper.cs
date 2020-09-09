using System.Text.RegularExpressions;

namespace Tetris
{
    public class PrintHelper
    {
        public string[] PrintBoard(Board board)
        {
            int width = board.Width;
            int height = board.Height;
            string[] boardPrint = new string[height];

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
                boardPrint[y] = $"{row}";
            }

            return boardPrint;
        }

        public string[] PrintGame(TetrisBoard tetrisBoard, TetrisBoard nextTetrimino, GameStats gameStats)
        {
            string[] boardRows = PrintBoard(tetrisBoard);
            string[] nextTetriminoRows = PrintBoard(nextTetrimino);

            int totalWidth = 14 + 3;
            int innerWidth = 12 + 3;

            string scoreTitle = PadOutString(" SCORE", innerWidth);
            string scoreAmount = PadOutString($" {gameStats.Score}", innerWidth);

            string lines = PadOutString($" Lines: {gameStats.Lines}", innerWidth);
            string blocks = PadOutString($" Blocks: {gameStats.Shapes}", innerWidth);
            string level = PadOutString($" Level: {gameStats.Level}", innerWidth);

            string blankLine14 = PadOutString(totalWidth);

            string[] gameFieldPrint = {
                $"┌{PrintLine(innerWidth)}┐ " + $" ┌{PrintLine(30)}┐",
                $"│{nextTetriminoRows[0]}│ "  + $" │{boardRows[0]}│",
                $"│{nextTetriminoRows[1]}│ "  + $" │{boardRows[1]}│",
                $"│{nextTetriminoRows[2]}│ "  + $" │{boardRows[2]}│",
                $"│{nextTetriminoRows[3]}│ "  + $" │{boardRows[3]}│",
                $"└{PrintLine(innerWidth)}┘ " + $" │{boardRows[4]}│",
                $"{blankLine14} "             + $" │{boardRows[5]}│",
                $"┌{PrintLine(innerWidth)}┐ " + $" │{boardRows[6]}│",
                $"│{scoreTitle}│ "            + $" │{boardRows[7]}│",
                $"│{scoreAmount}│ "           + $" │{boardRows[8]}│",
                $"└{PrintLine(innerWidth)}┘ " + $" │{boardRows[9]}│",
                $"{blankLine14} "             + $" │{boardRows[10]}│",
                $"┌{PrintLine(innerWidth)}┐ " + $" │{boardRows[11]}│",
                $"│{lines}│ "                 + $" │{boardRows[12]}│",
                $"│{blocks}│ "                + $" │{boardRows[13]}│",
                $"│{level}│ "                 + $" │{boardRows[14]}│",
                $"└{PrintLine(innerWidth)}┘ " + $" │{boardRows[15]}│",
                $"{blankLine14} "             + $" │{boardRows[16]}│",
                $"{blankLine14} "             + $" │{boardRows[17]}│",
                $"{blankLine14} "             + $" │{boardRows[18]}│",
                $"{blankLine14} "             + $" │{boardRows[19]}│",
                $"{blankLine14} "             + $" └{PrintLine(30)}┘"
            };

            return gameFieldPrint;
        }

        public string PadOutString(string str, int toLength)
        {
            return str + RepeatingString(" ", toLength - str.Length);
        }

        public string PadOutStringCentered(string str, int toLength)
        {
            int strWidth = CleanString(str).Length;

            int lengthCenter = toLength / 2;
            int strCenter = strWidth / 2;
            int leftPaddingLength = lengthCenter - strCenter;
            int rightPaddingLength = toLength - leftPaddingLength - strWidth;

            string leftPadding = PadOutString(leftPaddingLength);
            string rightPadding = PadOutString(rightPaddingLength);
            return $"{leftPadding}{str}{rightPadding}";
        }

        public string PadOutString(int toLength)
        {
            return RepeatingString(" ", toLength);
        }

        public string PrintLine(int length)
        {
            string line = "";
            for(int i = 0; i < length; i++) { line += "─"; }
            return line;
        }

        public string CleanString(string str)
        {
            string pattern = @"\u001b\[[0-9]*m";
            return Regex.Replace(str, pattern, "");
        }

        private string RepeatingString(string str, int repeats)
        {
            string repeatedString = "";
            for (int i = 0; i < repeats; i++) { repeatedString += str; }
            return repeatedString;
        }
    }
}