using System;

namespace Tetris
{
    public class OptionsScreen : IScreen
    {
        public string Render(int dTime, string input, Engine engine)
        {
            if (input == "Enter") engine.SwitchScreen(new MenuScreen());

            return (
                "OPTIONS\n" +
                "Under construction!\n" +
                "Press enter to go back..."
            );
        }
    }
}