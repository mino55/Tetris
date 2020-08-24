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

            string scoreTitle = PadOutString(" SCORE", 12);
            string scoreAmount = PadOutString($" {gameStats.Score}", 12);

            string lines = PadOutString($" Lines: {gameStats.Lines}", 12);
            string blocks = PadOutString($" Blocks: {gameStats.Shapes}", 12);
            string level = PadOutString($" Level: {gameStats.Level}", 12);

            string blankLine14 = PadOutString(14);
            string blankLine12 = PadOutString(12);

            string gameFieldPrint =
            $"┌{PrintLine(12)}┐ "        + $" ┌{PrintLine(30)}┐\n" +
            $"│{nextTetriminoRows[0]}│ " + $" │{boardRows[0]}│\n" +
            $"│{nextTetriminoRows[1]}│ " + $" │{boardRows[1]}│\n" +
            $"│{nextTetriminoRows[2]}│ " + $" │{boardRows[2]}│\n" +
            $"│{nextTetriminoRows[3]}│ " + $" │{boardRows[3]}│\n" +
            $"└{PrintLine(12)}┘ "        + $" │{boardRows[4]}│\n" +
            $"{blankLine14} "            + $" │{boardRows[5]}│\n" +
            $"{blankLine14} "            + $" │{boardRows[6]}│\n" +
            $"┌{PrintLine(12)}┐ "        + $" │{boardRows[7]}│\n" +
            $"│{scoreTitle}│ "           + $" │{boardRows[8]}│\n" +
            $"│{scoreAmount}│ "          + $" │{boardRows[9]}│\n" +
            $"│{blankLine12}│ "          + $" │{boardRows[10]}│\n" +
            $"│{lines}│ "                + $" │{boardRows[11]}│\n" +
            $"│{blocks}│ "               + $" │{boardRows[12]}│\n" +
            $"│{level}│ "                + $" │{boardRows[13]}│\n" +
            $"│{blankLine12}│ "          + $" │{boardRows[14]}│\n" +
            $"└{PrintLine(12)}┘ "        + $" │{boardRows[15]}│\n" +
            $"{blankLine14} "            + $" │{boardRows[16]}│\n" +
            $"{blankLine14} "            + $" │{boardRows[17]}│\n" +
            $"{blankLine14} "            + $" │{boardRows[18]}│\n" +
            $"{blankLine14} "            + $" │{boardRows[19]}│\n" +
            $"{blankLine14} "            + $" └{PrintLine(30)}┘\n";

            return gameFieldPrint;
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
    }
}