using System;
using System.Threading;
using Tetris;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuScreen menuScreen = new MenuScreen();
            Engine engine = new Engine(60, menuScreen);
            engine.Start();
        }
    }
}
