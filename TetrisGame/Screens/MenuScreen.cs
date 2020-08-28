using System;

namespace Tetris
{
    public class MenuScreen : IScreen
    {
        private int _selections = 4;
        private int _currentSelection = 0;

        private ColorHelper _colorHelper = new ColorHelper();

        public void Render(int dTime, string input, Engine engine)
        {
            Console.Clear();
            Console.WriteLine($"{Padding(6)}______________________");
            Console.WriteLine($"{Padding(6)}_______ TETRIS _______");
            Console.WriteLine("");

            if (input == "Enter") Pick(_currentSelection, engine);
            if (input == "S") selectNext();
            if (input == "W") selectPrevious();

            Console.WriteLine(PrintSelection());
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

        private void Pick(int selection, Engine engine)
        {
            switch(selection)
            {
                case 0:
                    engine.SwitchScreen(new GameScreen());
                    break;
                case 1:
                    engine.SwitchScreen(new OptionsScreen());
                    break;
                case 2:
                    engine.SwitchScreen(new HighscoreScreen());
                    break;
                case 3:
                    engine.Stop(); // TOOD: Google: Thread abort is not supported on this platform
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