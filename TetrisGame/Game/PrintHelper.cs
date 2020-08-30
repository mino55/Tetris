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

        public string SimplePrint(TetrisBoard tetrisBoard, TetrisBoard nextTetrimino, GameStats gameStats)
        {
            string[] boardRows = BoardPrint(tetrisBoard).Split("\n");
            string[] nextTetriminoRows = BoardPrint(nextTetrimino).Split("\n");

            int totalWidth = 14 + 3;
            int innerWidth = 12 + 3;

            string scoreTitle = PadOutString(" SCORE", innerWidth);
            string scoreAmount = PadOutString($" {gameStats.Score}", innerWidth);

            string lines = PadOutString($" Lines: {gameStats.Lines}", innerWidth);
            string blocks = PadOutString($" Blocks: {gameStats.Shapes}", innerWidth);
            string level = PadOutString($" Level: {gameStats.Level}", innerWidth);

            string blankLine14 = PadOutString(totalWidth);
            string blankLine12 = PadOutString(innerWidth);

            string gameFieldPrint =
            $"┌{PrintLine(innerWidth)}┐ " + $" ┌{PrintLine(30)}┐\n" +
            $"│{nextTetriminoRows[0]}│ "  + $" │{boardRows[0]}│\n" +
            $"│{nextTetriminoRows[1]}│ "  + $" │{boardRows[1]}│\n" +
            $"│{nextTetriminoRows[2]}│ "  + $" │{boardRows[2]}│\n" +
            $"│{nextTetriminoRows[3]}│ "  + $" │{boardRows[3]}│\n" +
            $"└{PrintLine(innerWidth)}┘ " + $" │{boardRows[4]}│\n" +
            $"{blankLine14} "             + $" │{boardRows[5]}│\n" +
            $"┌{PrintLine(innerWidth)}┐ " + $" │{boardRows[6]}│\n" +
            $"│{scoreTitle}│ "            + $" │{boardRows[7]}│\n" +
            $"│{scoreAmount}│ "           + $" │{boardRows[8]}│\n" +
            $"└{PrintLine(innerWidth)}┘ " + $" │{boardRows[9]}│\n" +
            $"{blankLine14} "             + $" │{boardRows[10]}│\n" +
            $"┌{PrintLine(innerWidth)}┐ " + $" │{boardRows[11]}│\n" +
            $"│{lines}│ "                 + $" │{boardRows[12]}│\n" +
            $"│{blocks}│ "                + $" │{boardRows[13]}│\n" +
            $"│{level}│ "                 + $" │{boardRows[14]}│\n" +
            $"└{PrintLine(innerWidth)}┘ " + $" │{boardRows[15]}│\n" +
            $"{blankLine14} "             + $" │{boardRows[16]}│\n" +
            $"{blankLine14} "             + $" │{boardRows[17]}│\n" +
            $"{blankLine14} "             + $" │{boardRows[18]}│\n" +
            $"{blankLine14} "             + $" │{boardRows[19]}│\n" +
            $"{blankLine14} "             + $" └{PrintLine(30)}┘\n";

            return gameFieldPrint;
        }

        public string PadOutString(string str, int toLength)
        {
            return str + RepeatingString(" ", (toLength - str.Length));
        }

        public string PadOutStringCentered(string str, int strWidth, int toLength)
        {
            // TODO: create a method to clean out octal codes from strings
            // https://stackoverflow.com/questions/7149601/how-to-remove-replace-ansi-color-codes-from-a-string-in-javascript/7150870
            // Then we no longer need to pass strWidth as an argument

            int lengthCenter = toLength / 2;
            int strCenter = strWidth / 2;
            int leftPaddingLength = lengthCenter - strCenter;
            int rightPaddingLength = (toLength - leftPaddingLength - strWidth);

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

        private string RepeatingString(string str, int repeats)
        {
            string repeatedString = "";
            for (int i = 0; i < repeats; i++) { repeatedString += str; }
            return repeatedString;
        }
    }
}