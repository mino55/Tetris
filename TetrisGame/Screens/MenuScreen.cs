using System;

namespace Tetris
{
    public class MenuScreen : IScreen
    {
        private Engine _engine;
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
            string header = (
                $"{Padding(6)}______________________\n" +
                $"{Padding(6)}_______ TETRIS _______\n" +
                "\n"
            );
            string selection = PrintSelection();
            return (header + selection);
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
            return $"{Padding(12)}{HighlightableString(0, "Start Game", Color.RED)}\n\n" +
                   $"{Padding(12)}{HighlightableString(1, " Options", Color.RED)}\n\n" +
                   $"{Padding(12)}{HighlightableString(2, "Highscore", Color.RED)}\n\n" +
                   $"{Padding(12)}{HighlightableString(3, "  Quit", Color.RED)}\n\n";
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