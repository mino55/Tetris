using System;

namespace Tetris
{
    public class GameOverScreen : IScreen
    {
        private GameStats _gameStats;

        public GameOverScreen(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        public string Render(int dTime, string input, Engine engine)
        {
            if (input == "Enter") engine.SwitchScreen(new GameScreen());

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