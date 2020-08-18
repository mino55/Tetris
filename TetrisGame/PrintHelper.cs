namespace Tetris
{
    public class PrintHelper
    {
        public string PrintBoard(Board board)
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

        private string PrintLine(int length)
        {
            string line = "";
            for(int i = 0; i < length; i++) { line += "─"; }
            return line;
        }
    }
}