namespace Tetris
{
    public class MainScreen : MenuScreen
    {
        private readonly ScreenFactory _screenFactory = null;
        private readonly ColorHelper _colorHelper = new ColorHelper();

        public MainScreen(ScreenFactory screenFactory) : base()
        {
            _screenFactory = screenFactory;
        }

        protected override void SetupMenuSelection(MenuSelections menuSelection)
        {
            menuSelection.AddPick("start");
            menuSelection.AddPick("options");
            menuSelection.AddPick("highscore");
            menuSelection.AddPick("quit");
        }

        protected override void RenderMenuItems(MenuLine[] menuPrint)
        {
            string startGame = $"{HighlightableString("start", "Start Game", Color.RED)}";
            string options = $"{HighlightableString("options", "Options", Color.RED)}";
            string highscore = $"{HighlightableString("highscore", "Highscore", Color.RED)}";
            string quit = $"{HighlightableString("quit", "Quit", Color.RED)}";

            menuPrint[1] = CenterAlign(_colorHelper.ColorString("┏━━━━━┓                    ", Color.PURPLE));
            menuPrint[2] = CenterAlign(_colorHelper.ColorString("┗━┓ ┏━╋━━━┳━━━━━┳━━━┳━┳━━━┓", Color.PURPLE));
            menuPrint[3] = CenterAlign(_colorHelper.ColorString("  ┃ ┃ ┃ ━━╋━┓ ┏━┫ ┏━╋━┫ ━━┫", Color.PURPLE));
            menuPrint[4] = CenterAlign(_colorHelper.ColorString("  ┃ ┃ ┃ ━━┫ ┃ ┃ ┃ ┃ ┃ ┣━━ ┃", Color.PURPLE));
            menuPrint[5] = CenterAlign(_colorHelper.ColorString("  ┗━┛ ┗━━━┛ ┗━┛ ┗━┛ ┗━┻━━━┛", Color.PURPLE));

            menuPrint[10] = CenterAlign(startGame);
            menuPrint[12] = CenterAlign(options);
            menuPrint[14] = CenterAlign(highscore);
            menuPrint[16] = CenterAlign(quit);
        }

        protected override void OnPick(string selection, Engine engine)
        {
            switch(selection)
            {
                case "start":
                    engine.SwitchScreen(_screenFactory.CreateGameScreen());
                    break;
                case "options":
                    engine.SwitchScreen(_screenFactory.CreateOptionsScreen());
                    break;
                case "highscore":
                    engine.SwitchScreen(_screenFactory.CreateHighscoreScreen());
                    break;
                case "quit":
                    engine.Stop();
                    break;
            }
        }

        protected override void UnhandledInput(string input, int dTime, Engine engine) {}

        protected override void OnSetting(string name, string state, Engine engine)
        {}

        protected override void OnLeave(Engine engine)
        {}
    }
}
