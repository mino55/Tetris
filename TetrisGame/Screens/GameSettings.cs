using System.Collections.Generic;

namespace Tetris
{
    public class GameSettings
    {
        public int FPS { get; set; }
        public string Controlls { get; set; }

        private Dictionary<string, GameScreen.Action> _keyMapping;

        public GameSettings()
        {
            Controlls = "simple ";
            FPS = 60;
        }
    }
}