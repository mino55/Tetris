using System;

namespace Tetris
{
    public class HighscoreScreen : IScreen
    {
        public void Render(int dTime, string input, Engine engine)
        {
            Console.Clear();
            Console.WriteLine("HIGHSCORE");
            Console.WriteLine("Under construction!");
            Console.WriteLine("Press enter to go back...");

            if (input == "Enter") engine.SwitchScreen(new MenuScreen());
        }
    }
}