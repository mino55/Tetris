using System;

namespace Tetris
{
    public abstract class MenuScreen : IScreen
    {
        private Engine _engine;
        private PrintHelper _printHelper = new PrintHelper();
        private int _selections = 4;
        private int _currentSelection = 0;

        private int _width;
        private int _height;

        private ColorHelper _colorHelper = new ColorHelper();

        public MenuScreen()
        {
            _width = 52;
            _height = 20;
        }

        public void Mount(Engine engine)
        {
            _engine = engine;
        }

        public void Input(string input, int dTime)
        {
            if (input == "Enter") Pick(_currentSelection, _engine);
            else if (input == "S") selectNext();
            else if (input == "W") selectPrevious();
            else UnhandledInput(input, dTime, _engine);
        }

        public string Render()
        {
            string[] menuPrint = new string[_height];
            RenderMenuItems(menuPrint);
            return MenuPrintToStr(menuPrint);
        }

        private string MenuPrintToStr(string[] menuPrint)
        {
            string str = "";
            string blankLine = _printHelper.PadOutString(_width);

            str += $"┌{_printHelper.PrintLine(_width)}┐\n";
            for (int i = 0; i < menuPrint.Length; i++) {
                if (menuPrint[i] == null) str += $"|{blankLine}|\n";
                else str += $"|{menuPrint[i]}|\n";
            }
            str += $"└{_printHelper.PrintLine(_width)}┘\n";

            return str;
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

        protected string HighlightableString(int highlightOnSelection, string str, Color highlightColor)
        {
            if (highlightOnSelection == _currentSelection)
            {
                return _colorHelper.ColorString(str, highlightColor);
            }
            return str;
        }

        protected string CenterString(string str, int strWidth = -1)
        {
            if (strWidth == -1) strWidth = str.Length;
            return $"{_printHelper.PadOutStringCentered(str, strWidth, _width)}";
        }

        protected string PadOutString(string str)
        {
            return _printHelper.PadOutString(str, _width);
        }

        protected abstract void RenderMenuItems(string[] menuPrint);

        protected abstract void Pick(int selection, Engine engine);

        protected abstract void UnhandledInput(string input, int dTime, Engine engine);
    }
}