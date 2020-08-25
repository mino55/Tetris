using System;

namespace Tetris
{
    public class MenuScreen : IScreen
    {
        public void Render(int dTime, string input, Engine engine)
        {
            Console.Clear();
            Console.WriteLine("__________________");
            Console.WriteLine("_____ TETRIS _____");
            Console.WriteLine("Press enter to start playing...");

            if (input == "Enter") engine.SwitchScreen(new GameScreen());
        }
    }
}