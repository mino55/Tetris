namespace Tetris
{
    public class OptionsScreen : MenuScreen
    {
        private readonly ScreenFactory _screenFactory;
        private readonly GameSettings _gameSettings;
        private readonly FileStore _store;

        public OptionsScreen(ScreenFactory screenFactory,
                             GameSettings gameSettings,
                             FileStoreOperator fileStoreOperator) : base()
        {
            _screenFactory = screenFactory;
            _gameSettings = gameSettings;
            _store = fileStoreOperator.Store;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddSetting("controlls", new string[] {"simple ", "complex"});
            menuSelection.AddSetting("fps", new string[] {"60", "30", "20"});
            menuSelection.AddPick("back");

            _gameSettings.FPS = int.Parse(_store.Get("fps"));
            _gameSettings.Controlls = _store.Get("controlls");

            menuSelection.SetSettingState("fps", $"{_gameSettings.FPS}");
            menuSelection.SetSettingState("controlls", _gameSettings.Controlls);
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
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            string controllsState = GetSettingState("controlls");
            string controlls = HighlightableString("controlls",
                                                   $"Controlls: {controllsState}",
                                                   Color.RED);

            string fpsState = GetSettingState("fps");
            string fps = HighlightableString("fps", $"FPS: {fpsState}", Color.RED);

            string back = HighlightableString("back", "Back", Color.RED);

            menuPrint[2] = CenterAlign("OPTIONS");
            menuPrint[6] = CenterAlign(controlls);
            menuPrint[8] = CenterAlign(fps);
            menuPrint[10] = CenterAlign(back);

            RenderDescription(menuPrint);
        }

        private void RenderDescription(MenuLine[] menuPrint)
        {
            if (IsSelected("fps"))
            {
                menuPrint[16] = CenterAlign("  FPS determines input FPS");
            }

            if (IsSelected("controlls") && GetSettingState("controlls") == "complex")
            {
                menuPrint[14] = LeftAlign("  (P: Pause)");
                menuPrint[16] = LeftAlign("  (UP: Flip) (DOWN: Quick drop) (Q: Rotate left)");
                menuPrint[18] = LeftAlign("  (E: Rotate right) (SPACE/ENTER: Slam)");
            }

            if (IsSelected("controlls") && GetSettingState("controlls") == "simple ")
            {
                menuPrint[14] = LeftAlign("  (P: Pause)");
                menuPrint[16] = LeftAlign("  (UP: Rotate right) (DOWN: Quick drop)");
                menuPrint[18] = LeftAlign("  (SPACE/ENTER: Slam)");
            }
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}

        protected override void OnLeave(Engine engine)
        {
            _store.Set("fps", $"{_gameSettings.FPS}");
            _store.Set("controlls", _gameSettings.Controlls);
            _store.Save();
        }
    }
}
