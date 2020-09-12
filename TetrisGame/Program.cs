namespace Tetris
{
    class Program
    {
        static void Main()
        {
            GameSettings gameSettings = new GameSettings();
            ScreenFactory screenFactory = InitScreenFactory(gameSettings);
            MainScreen mainScreen = screenFactory.CreateMainScreen();

            KeyReceiver keyReceiver = new KeyReceiver();
            KeyListener keyListener = new KeyListener(keyReceiver);
            Engine engine = new Engine(gameSettings.FPS, mainScreen, keyReceiver);

            keyListener.Start();
            engine.Start();
            while (engine.Started)
            {
                engine.Loop();
            }
            keyListener.Stop();
        }

        static private ScreenFactory InitScreenFactory(GameSettings gameSettings)
        {
            FileStore fileStore = new FileStore(@"./tetris_store.txt");
            FileStoreOperator fileStoreOperator = new FileStoreOperator(fileStore);
            LoadGameSettingsFromStore(gameSettings, fileStoreOperator);

            bool colorEnabled = gameSettings.Color == "full";
            ColorHelper colorHelper = new ColorHelper(colorEnabled);

            ScreenFactory screenFactory = new ScreenFactory(gameSettings,
                                                            fileStoreOperator,
                                                            colorHelper);
            return screenFactory;
        }

        static private void LoadGameSettingsFromStore(GameSettings gameSettings,
                                                      FileStoreOperator fileStoreOperator)
        {
            gameSettings.FPS = int.Parse(fileStoreOperator.Store.Get("fps"));
            gameSettings.Controlls = fileStoreOperator.Store.Get("controlls");
            gameSettings.Color = fileStoreOperator.Store.Get("color");
            gameSettings.Unicode = fileStoreOperator.Store.Get("unicode");
        }
    }
}
