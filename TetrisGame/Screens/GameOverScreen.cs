using System;

namespace Tetris
{
    public class GameOverScreen : MenuScreen
    {
        private Engine _engine;
        private GameStats _gameStats;

        public GameOverScreen(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        protected override void Pick(int selection, Engine engine)
        {
            engine.SwitchScreen(new MainScreen());
        }

        protected override void RenderMenuItems(string[] menuPrint)
        {
            menuPrint[2] = CenterString("GAME OVER");
            menuPrint[4] = CenterString($"Score: {_gameStats.Score}");
            menuPrint[6] = CenterString($"Lines: {_gameStats.Lines}");
            menuPrint[8] = CenterString($"Level: {_gameStats.Level}");
            menuPrint[10] = CenterString($"Blocks: {_gameStats.Shapes}");
            menuPrint[14] = CenterString("Press enter to restart game...");
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}
    }
}