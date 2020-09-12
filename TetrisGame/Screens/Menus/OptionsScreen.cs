namespace Tetris
{
    public class OptionsScreen : MenuScreen
    {
        private readonly ScreenFactory _screenFactory;
        private readonly GameSettings _gameSettings;
        private readonly FileStore _store;

        public OptionsScreen(ScreenFactory screenFactory,
                             GameSettings gameSettings,
                             FileStoreOperator fileStoreOperator)
        : base(gameSettings)
        {
            _screenFactory = screenFactory;
            _gameSettings = gameSettings;
            _store = fileStoreOperator.Store;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddSetting("controlls", new string[] { "simple ", "complex" });
            menuSelection.AddSetting("fps", new string[] { "60", "30", "20" });
            menuSelection.AddSetting("color", new string[] { "full", "none" });
            menuSelection.AddSetting("unicode", new string[] { "full", "limited" });
            menuSelection.AddPick("back");

            menuSelection.SetSettingState("fps", $"{_gameSettings.FPS}");
            menuSelection.SetSettingState("controlls", _gameSettings.Controlls);
            menuSelection.SetSettingState("color", $"{_gameSettings.Color}");
            menuSelection.SetSettingState("unicode", _gameSettings.Unicode);
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            string controllsState = GetSettingState("controlls");
            string controlls = HighlightableString("controlls",
                                                   $"Controlls: {controllsState}",
                                                   Color.RED);

            string fpsState = GetSettingState("fps");
            string fps = HighlightableString("fps", $"FPS: {fpsState}", Color.RED);

            string colorState = GetSettingState("color");
            string color = HighlightableString("color", $"Color support: {colorState}", Color.RED);

            string unicodeState = GetSettingState("unicode");
            string unicode = HighlightableString("unicode", $"Unicode support: {unicodeState}", Color.RED);

            string back = HighlightableString("back", "Back", Color.RED);

            menuPrint[2] = CenterAlign("OPTIONS");
            menuPrint[5] = CenterAlign(controlls);
            menuPrint[7] = CenterAlign(fps);
            menuPrint[9] = CenterAlign(color);
            menuPrint[11] = CenterAlign(unicode);
            menuPrint[13] = CenterAlign(back);

            RenderDescription(menuPrint);
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(_screenFactory.CreateMainScreen());
        }

        protected override void OnSetting(string name, string state, Engine engine)
        {
            if (name == "fps")
            {
                engine.FPS = int.Parse(state);
                _gameSettings.FPS = int.Parse(state);
            }

            if (name == "controlls")
            {
                _gameSettings.Controlls = state;
            }

            if (name == "color")
            {
                _gameSettings.Color = state;
                SetColor(_gameSettings.Color == "full");
            }

            if (name == "unicode")
            {
                _gameSettings.Unicode = state;
            }
        }

        private void RenderDescription(MenuLine[] menuPrint)
        {
            if (IsSelected("fps"))
            {
                menuPrint[17] = CenterAlign("  FPS determines input FPS");
            }

            if (IsSelected("controlls") && GetSettingState("controlls") == "complex")
            {
                menuPrint[16] = LeftAlign("  (P: Pause)");
                menuPrint[17] = LeftAlign("  (UP: Flip) (DOWN: Quick drop) (Q: Rotate left)");
                menuPrint[18] = LeftAlign("  (E: Rotate right) (SPACE/ENTER: Slam)");
            }

            if (IsSelected("controlls") && GetSettingState("controlls") == "simple ")
            {
                menuPrint[16] = LeftAlign("  (P: Pause)");
                menuPrint[17] = LeftAlign("  (UP: Rotate right) (DOWN: Quick drop)");
                menuPrint[18] = LeftAlign("  (SPACE/ENTER: Slam)");
            }

            if (IsSelected("color") && GetSettingState("color") == "none")
            {
                menuPrint[17] = CenterAlign("Disable colors for maximum terminal support");
            }

            if (IsSelected("color") && GetSettingState("color") == "full")
            {
                menuPrint[17] = CenterAlign("Allow colors (ANSI escape codes)");
            }

            if (IsSelected("unicode") && GetSettingState("unicode") == "limited")
            {
                menuPrint[17] = CenterAlign("Only allow simple box-drawing characters");
            }

            if (IsSelected("unicode") && GetSettingState("unicode") == "full")
            {
                menuPrint[17] = LeftAlign("  Support for complex box-drawing characters");
                menuPrint[18] = LeftAlign("  such as '┫' and '╋'");
            }
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}

        protected override void OnLeave(Engine engine)
        {
            _store.Set("fps", $"{_gameSettings.FPS}");
            _store.Set("controlls", _gameSettings.Controlls);
            _store.Set("unicode", _gameSettings.Unicode);
            _store.Set("color", $"{_gameSettings.Color}");
            _store.Save();
        }
    }
}
