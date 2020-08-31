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

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            menuPrint[2] = CenterAlign("GAME OVER");
            menuPrint[4] = CenterAlign($"Score: {_gameStats.Score}");
            menuPrint[6] = CenterAlign($"Lines: {_gameStats.Lines}");
            menuPrint[8] = CenterAlign($"Level: {_gameStats.Level}");
            menuPrint[10] = CenterAlign($"Blocks: {_gameStats.Shapes}");
            menuPrint[14] = CenterAlign("Press enter to restart game...");
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}
    }
}