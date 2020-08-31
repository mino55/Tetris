using System;

namespace Tetris
{
    public class OptionsScreen : MenuScreen
    {
        protected override void Pick(int selection, Engine engine)
        {
            engine.SwitchScreen(new MainScreen());
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            menuPrint[4] = LeftAlign("   OPTIONS");
            menuPrint[5] = LeftAlign("   Under construction!");
            menuPrint[9] = LeftAlign("   Press enter to go back...");
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}
    }
}