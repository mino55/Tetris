using System;
using System.Threading;
using Tetris;

namespace Tetris
{
    class Program
    {
        static private ScreenFactory _screenFactory = new ScreenFactory();
        static void Main(string[] args)
        {
            MainScreen menuScreen = _screenFactory.CreateMainScreen();
            Engine engine = new Engine(60, menuScreen);
            engine.Start();
        }
    }
}
