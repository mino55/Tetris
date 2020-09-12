namespace Tetris
{
    public abstract class MenuScreen : IScreen
    {
        private Engine _engine;
        private readonly int _width;
        private readonly int _height;
        private readonly PrintHelper _printHelper = new PrintHelper();
        private readonly MenuSelections _menuSelections = new MenuSelections();
        private readonly ColorHelper _colorHelper = new ColorHelper();

        public MenuScreen(GameSettings gameSettings)
        {
            // TODO: extract width and height into the constructor
            _width = 52;
            _height = 20;
            bool colorEnabled = gameSettings.Color == "full";
            _colorHelper = new ColorHelper(colorEnabled);
        }

        public void Mount(Engine engine)
        {
            _engine = engine;
            SetupMenuSelection(_menuSelections);
        }

        public void Input(string input, int dTime)
        {
            if (input == "S" || input == "DownArrow") _menuSelections.SelectNext();
            else if (input == "W" || input == "UpArrow") _menuSelections.SelectPrevious();
            else
            {
                MenuSelections.Type type = _menuSelections.CurrentSelectionType();
                if (type == MenuSelections.Type.PICK) InputPick(input, dTime);
                if (type == MenuSelections.Type.SETTING) InputSetting(input, dTime);
            }
        }

        public string[] Render()
        {
            MenuLine[] menuPrint = new MenuLine[_height];
            RenderMenuItems(menuPrint);
            return MenuPrintToStr(menuPrint);
        }

        public void Unmount(Engine engine)
        {
            OnLeave(engine);
        }

        private string[] MenuPrintToStr(MenuLine[] menuPrint)
        {
            string[] print = new string[menuPrint.Length + 2];
            string blankLine = _printHelper.PadOutString(_width);

            print[0] = $"┌{_printHelper.PrintLine(_width)}┐";
            for (int i = 0; i < menuPrint.Length; i++) {
                if (menuPrint[i] == null) print[i + 1] = $"|{blankLine}|";
                else print[i + 1] += $"|{menuPrint[i]}|";
            }
            print[menuPrint.Length + 1] = $"└{_printHelper.PrintLine(_width)}┘";

            return print;
        }

        protected string HighlightableString(string onSelection, string str, Color highlightColor)
        {
            if (onSelection == _menuSelections.CurrentSelection())
            {
                if (_colorHelper.ColorEnabled)
                    return _colorHelper.ColorString(str, highlightColor);
                else
                    return $">> {str} <<";
            }
            return str;
        }

        protected bool IsSelected(string name)
        {
            return _menuSelections.IsSelected(name);
        }

        protected string GetSettingState(string name)
        {
            return _menuSelections.GetSettingState(name);
        }

        protected void SetSettingState(string name, string state)
        {
            _menuSelections.SetSettingState(name, state);
            OnSetting(name, state, _engine);
        }

        protected MenuLine CenterAlign(string str)
        {
            return new MenuLine(str, _width, Alignment.CENTER, _printHelper);
        }

        protected MenuLine LeftAlign(string str)
        {
            return new MenuLine(str, _width, Alignment.LEFT, _printHelper);
        }

        protected void SetColor(bool enabled)
        {
            _colorHelper.ColorEnabled = enabled;
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
            else if (input == "D" || input == "RightArrow")
            {
                _menuSelections.SelectedSettingNextState();
                OnSetting(_menuSelections.CurrentSelection(),
                          _menuSelections.SelectedSettingCurrentState(),
                          _engine);
            }
            else if (input == "A" || input == "LeftArrow")
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

        protected abstract void OnLeave(Engine engine);

        protected enum Alignment
        {
            RIGHT,
            CENTER,
            LEFT
        }

        protected class MenuLine
        {
            readonly int _length;
            private readonly string _content;
            private readonly Alignment _alignment;
            private readonly PrintHelper _printHelper;

            public MenuLine(string content, int length, Alignment alignment, PrintHelper printHelper)
            {
                _content = content;
                _alignment = alignment;
                _length = length;
                _printHelper = printHelper;
            }

            public override string ToString()
            {
                return _alignment switch
                {
                    Alignment.RIGHT => "",
                    Alignment.CENTER => _printHelper.PadOutStringCentered(_content, _length),
                    Alignment.LEFT => _printHelper.PadOutString(_content, _length),
                    _ => _content,
                };
            }
        }
    }
}
