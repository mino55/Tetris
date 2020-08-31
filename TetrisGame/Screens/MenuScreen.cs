using System;

namespace Tetris
{
    public abstract class MenuScreen : IScreen
    {
        private Engine _engine;
        private PrintHelper _printHelper = new PrintHelper();
        private MenuSelections _menuSelections = new MenuSelections();

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
            SetupMenuSelection(_menuSelections);
        }

        public void Input(string input, int dTime)
        {
            if (input == "S") _menuSelections.SelectNext();
            else if (input == "W") _menuSelections.SelectPrevious();
            else
            {
                MenuSelections.Type type = _menuSelections.CurrentSelectionType();
                if (type == MenuSelections.Type.PICK) InputPick(input, dTime);
                if (type == MenuSelections.Type.SETTING) InputSetting(input, dTime);
            }
        }

        public string Render()
        {
            MenuLine[] menuPrint = new MenuLine[_height];
            RenderMenuItems(menuPrint);
            return MenuPrintToStr(menuPrint);
        }

        private string MenuPrintToStr(MenuLine[] menuPrint)
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

        protected string HighlightableString(string onSelection, string str, Color highlightColor)
        {
            if (onSelection == _menuSelections.CurrentSelection())
            {
                return _colorHelper.ColorString(str, highlightColor);
            }
            return str;
        }

        protected string StateOfSetting(string name)
        {
            return _menuSelections.StateOfSetting(name);
        }

        protected MenuLine CenterAlign(string str, int strWidth = -1)
        {
            if (strWidth == -1) strWidth = str.Length;
            return new MenuLine($"{_printHelper.PadOutStringCentered(str, strWidth, _width)}");
        }

        protected MenuLine LeftAlign(string str)
        {
            return new MenuLine(_printHelper.PadOutString(str, _width));
        }

        private void InputPick(string input, int dTime)
        {
            if (input == "Enter") OnPick(_menuSelections.CurrentSelection(), _engine);
            else UnhandledInput(input, dTime, _engine);
        }

        private void InputSetting(string input, int dTime)
        {
            if (input == "Enter"){
                _menuSelections.SelectedSettingNextState();
                OnSetting(_menuSelections.CurrentSelection(),
                          _menuSelections.SelectedSettingCurrentState(),
                          _engine);
            }
            else if (input == "D")
            {
                _menuSelections.SelectedSettingNextState();
                OnSetting(_menuSelections.CurrentSelection(),
                          _menuSelections.SelectedSettingCurrentState(),
                          _engine);
            }
            else if (input == "A")
            {
                _menuSelections.SelectedSettingPreviousState();
                OnSetting(_menuSelections.CurrentSelection(),
                          _menuSelections.SelectedSettingCurrentState(),
                          _engine);
            }
            else UnhandledInput(input, dTime, _engine);
        }

        protected abstract void SetupMenuSelection(MenuSelections menuSelection);

        protected abstract void RenderMenuItems(MenuLine[] menuPrint);

        protected abstract void OnPick(string name, Engine engine);

        protected abstract void OnSetting(string name, string state, Engine engine);

        protected abstract void UnhandledInput(string input, int dTime, Engine engine);

        // TOOD: move the methods doing the actual alignment into this class
        //       but wait until the point where we can remove color codes
        //       via regex.
        protected class MenuLine
        {
            public string _content;

            public MenuLine(string content)
            {
                _content = content;
            }

            public override string ToString()
            {
                return _content;
            }
        }
    }
}