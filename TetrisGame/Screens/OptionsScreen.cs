using System;

namespace Tetris
{
    public class OptionsScreen : MenuScreen
    {
        protected override void Pick(int selection, Engine engine)
        {
            engine.SwitchScreen(new MainScreen());
        }

        protected override void RenderMenuItems(string[] menuPrint)
        {
            menuPrint[4] = PadOutString("   OPTIONS");
            menuPrint[5] = PadOutString("   Under construction!");
            menuPrint[9] = PadOutString("   Press enter to go back...");
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}
    }
}