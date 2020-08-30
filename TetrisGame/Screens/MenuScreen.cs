using System;

namespace Tetris
{
    public class MenuScreen : IScreen
    {
        private Engine _engine;
        private PrintHelper _printHelper = new PrintHelper();
        private int _selections = 4;
        private int _currentSelection = 0;

        private ColorHelper _colorHelper = new ColorHelper();

        public void Mount(Engine engine)
        {
            _engine = engine;
        }

        public void Input(string input, int dTime)
        {
            if (input == "Enter") Pick(_currentSelection);
            if (input == "S") selectNext();
            if (input == "W") selectPrevious();
        }

        public string Render()
        {
            string selection = PrintSelection();
            return selection;
        }

        private void selectNext()
        {
            if (_currentSelection >= (_selections - 1)) _currentSelection = 0;
            else _currentSelection += 1;
        }

        private void selectPrevious()
        {
            if (_currentSelection <= 0) _currentSelection = (_selections - 1);
            else _currentSelection -= 1;
        }

        private void Pick(int selection)
        {
            switch(selection)
            {
                case 0:
                    _engine.SwitchScreen(new GameScreen());
                    break;
                case 1:
                    _engine.SwitchScreen(new OptionsScreen());
                    break;
                case 2:
                    _engine.SwitchScreen(new HighscoreScreen());
                    break;
                case 3:
                    _engine.Stop();
                    break;
            }
        }

        private string PrintSelection()
        {
            string[] selection = Selection();
            int width = 52;

            string print = "";
            string blankLine = _printHelper.PadOutString(width);

            print += $"┌{_printHelper.PrintLine(width)}┐\n";
            for (int i = 0; i < 20; i++)
            {
                switch (i)
                {
                    case 0:
                        print += $"│{_printHelper.PadOutStringCentered("______________________", 22, width)}│\n";
                        print += $"│{_printHelper.PadOutStringCentered("_______ TETRIS _______", 22, width)}│\n";
                        break;
                    case 4:
                        print += $"│{_printHelper.PadOutStringCentered(selection[0], 10, width)}│\n";
                        break;
                    case 6:
                        print += $"│{_printHelper.PadOutStringCentered(selection[1], 7, width)}│\n";
                        break;
                    case 8:
                        print += $"│{_printHelper.PadOutStringCentered(selection[2], 9, width)}│\n";
                        break;
                    case 10:
                        print += $"│{_printHelper.PadOutStringCentered(selection[3], 4, width)}│\n";
                        break;
                    default:
                        print += $"│{blankLine}│\n";
                        break;
                }
            }
            print += $"└{_printHelper.PrintLine(width)}┘\n";

            return print;
        }

        private string[] Selection()
        {
            return new string[] {
                $"{HighlightableString(0, "Start Game", Color.RED)}",
                $"{HighlightableString(1, "Options", Color.RED)}",
                $"{HighlightableString(2, "Highscore", Color.RED)}",
                $"{HighlightableString(3, "Quit", Color.RED)}"
            };
        }

        private string HighlightableString(int highlightOnSelection, string str, Color highlightColor)
        {
            if (highlightOnSelection == _currentSelection)
            {
                return _colorHelper.ColorString(str, highlightColor);
            }
            return str;
        }

        private string Padding(int amount)
        {
            string padding = "";
            for (int n = 0; n < amount; n++) padding += " ";
            return padding;
        }
    }
}