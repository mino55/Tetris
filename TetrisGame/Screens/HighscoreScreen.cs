using System;

namespace Tetris

{
    public class HighscoreScreen : MenuScreen
    {
        private ScreenFactory _screenFactory;
        private FileStoreOperator _fileStoreOperator;

        public HighscoreScreen(ScreenFactory screenFactory,
                               FileStoreOperator fileStoreOperator) : base()
        {
            _screenFactory = screenFactory;
            _fileStoreOperator = fileStoreOperator;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddPick("Continue");
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(_screenFactory.CreateMainScreen());
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            menuPrint[4] = LeftAlign("   HIGHSCORE");
            menuPrint[5] = LeftAlign("   Under construction!");
            menuPrint[9] = LeftAlign("   Press enter to go back...");
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}

        protected override void OnSetting(string name, string state, Engine engine)
        {}
    }
}