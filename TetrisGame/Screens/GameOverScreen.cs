using System;

namespace Tetris
{
    public class GameOverScreen : MenuScreen
    {
        private Engine _engine;
        private GameStats _gameStats;
        private ScreenFactory _screenFactory;

        public GameOverScreen(ScreenFactory screenFactory, GameStats gameStats)
        {
            _screenFactory = screenFactory;
            _gameStats = gameStats;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddPick("Continue");
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(_screenFactory.CreateMainScreen());
        }

        protected override void OnSetting(string name, string state, Engine engine)
        {}

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