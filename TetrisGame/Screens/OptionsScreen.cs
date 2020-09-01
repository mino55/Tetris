using System;

namespace Tetris
{
    public class OptionsScreen : MenuScreen
    {
        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddSetting("fps", new string[] {"20", "30", "60"});
            menuSelection.AddPick("back");
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(new MainScreen());
        }

        protected override void OnSetting(string name, string state, Engine engine)
        {}

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            string back = $"{HighlightableString("back", "Back", Color.RED)}";

            string fpsState = StateOfSetting("fps");
            string fps = $"{HighlightableString("fps", $"FPS: {fpsState}", Color.RED)}";

            menuPrint[4] = CenterAlign(fps);
            menuPrint[6] = CenterAlign(back);
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}
    }
}