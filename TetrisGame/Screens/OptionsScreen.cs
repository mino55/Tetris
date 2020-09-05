using System;

namespace Tetris
{
    public class OptionsScreen : MenuScreen
    {
        ScreenFactory _screenFactory;

        public OptionsScreen(ScreenFactory screenFactory) : base()
        {
            _screenFactory = screenFactory;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddSetting("controlls", new string[] {"simple ", "complex"});
            menuSelection.AddSetting("fps", new string[] {"20", "30", "60"});
            menuSelection.AddPick("back");
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(_screenFactory.CreateMainScreen());
        }

        protected override void OnSetting(string name, string state, Engine engine)
        {}

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            string controllsState = StateOfSetting("controlls");
            string controlls = HighlightableString("controlls",
                                                   $"Controlls: {controllsState}",
                                                   Color.RED);

            string fpsState = StateOfSetting("fps");
            string fps = HighlightableString("fps", $"FPS: {fpsState}", Color.RED);

            string back = HighlightableString("back", "Back", Color.RED);

            menuPrint[2] = CenterAlign("OPTIONS");
            menuPrint[4] = CenterAlign("(Options have no effect yet!)");
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

            if (IsSelected("controlls") && StateOfSetting("controlls") == "complex")
            {
                menuPrint[16] = LeftAlign("  (UP: Flip) (DOWN: Quick drop) (Q: Rotate left)");
                menuPrint[18] = LeftAlign("  (E: Rotate right) (SPACE: Slam)");
            }

            if (IsSelected("controlls") && StateOfSetting("controlls") == "simple ")
            {
                menuPrint[16] = LeftAlign("      (UP: Rotate right) (DOWN: Quick drop)");
                menuPrint[18] = LeftAlign("      (SPACE: Slam)");
            }
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}
    }
}