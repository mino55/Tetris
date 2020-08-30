using System;

namespace Tetris
{
    public class GameOverScreen : IScreen
    {
        private Engine _engine;
        private GameStats _gameStats;

        public GameOverScreen(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

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
                "GAME OVER" +
                $"Score: {_gameStats.Score}\n" +
                $"Lines: {_gameStats.Lines}\n" +
                $"Level: {_gameStats.Level}\n" +
                $"Blocks: {_gameStats.Shapes}\n" +
                "Press enter to restart game..."
            );
        }
    }
}