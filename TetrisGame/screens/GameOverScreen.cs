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

        public void Render(int dTime, string input, Engine engine)
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.WriteLine("Score: " + _gameStats.Score);
            Console.WriteLine("Lines: " + _gameStats.Lines);
            Console.WriteLine("Level: " + _gameStats.Level);
            Console.WriteLine("Blocks: " + _gameStats.Shapes);
            Console.WriteLine("Press enter to restart game...");

            if (input == "Enter") engine.SwitchScreen(new GameScreen());
        }
    }
}