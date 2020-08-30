using System;

namespace Tetris

{
    public class HighscoreScreen : IScreen
    {
        private Engine _engine;

        public void Mount(Engine engine)
        {
            _engine = engine;
        }

        public void Input(string input, int dTime)
        {
            if (input == "Enter") _engine.SwitchScreen(new MenuScreen());
        }

        public string Render()
        {
            return (
                "HIGHSCORE\n" +
                "Under construction!\n" +
                "Press enter to go back..."
            );
        }
    }
}