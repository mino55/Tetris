namespace Tetris
{
    public class HighscoreScreen : MenuScreen
    {
        private readonly ScreenFactory _screenFactory;
        private readonly FileStoreOperator _fileStoreOperator;

        public HighscoreScreen(ScreenFactory screenFactory,
                               FileStoreOperator fileStoreOperator) : base()
        {
            _screenFactory = screenFactory;
            _fileStoreOperator = fileStoreOperator;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddPick("back");
        }

        protected override void OnPick(string selection, Engine engine)
        {
            engine.SwitchScreen(_screenFactory.CreateMainScreen());
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            FileStore store = _fileStoreOperator.Store;
            menuPrint[2] = CenterAlign("HIGHSCORE");
            menuPrint[4] = CenterAlign($"1. {store.Get("h1_name")}: {store.Get("h1_value")}");
            menuPrint[5] = CenterAlign($"2. {store.Get("h2_name")}: {store.Get("h2_value")}");
            menuPrint[6] = CenterAlign($"3. {store.Get("h3_name")}: {store.Get("h3_value")}");
            menuPrint[7] = CenterAlign($"4. {store.Get("h4_name")}: {store.Get("h4_value")}");
            menuPrint[8] = CenterAlign($"5. {store.Get("h5_name")}: {store.Get("h5_value")}");
            menuPrint[9] = CenterAlign($"6. {store.Get("h6_name")}: {store.Get("h6_value")}");
            menuPrint[10] = CenterAlign($"7. {store.Get("h7_name")}: {store.Get("h7_value")}");
            menuPrint[11] = CenterAlign($"8. {store.Get("h8_name")}: {store.Get("h8_value")}");
            menuPrint[12] = CenterAlign($"9. {store.Get("h9_name")}: {store.Get("h9_value")}");
            menuPrint[13] = CenterAlign($"10. {store.Get("h10_name")}: {store.Get("h10_value")}");

            string back = $"{HighlightableString("back", "Back", Color.RED)}";
            menuPrint[18] = CenterAlign(back);
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine)
        {}

        protected override void OnSetting(string name, string state, Engine engine)
        {}

        protected override void OnLeave(Engine engine)
        {}
    }
}
