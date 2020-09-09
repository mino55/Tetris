namespace Tetris
{
    class Program
    {
        static void Main()
        {
            FileStore fileStore = new FileStore(@"./tetris_store.txt");
            GameSettings gameSettings = new GameSettings();
            FileStoreOperator fileStoreOperator = new FileStoreOperator(fileStore);

            ScreenFactory screenFactory = new ScreenFactory(gameSettings,
                                                            fileStoreOperator);
            MainScreen menuScreen = screenFactory.CreateMainScreen();

            KeyReceiver keyReceiver = new KeyReceiver();
            Engine engine = new Engine(60, menuScreen, keyReceiver);
            KeyListener keyListener = new KeyListener(keyReceiver);

            keyListener.Start();
            engine.Start();
            while (engine.Started)
            {
                engine.Loop();
            }
            keyListener.Stop();
        }
    }
}
