using System;
using System.Threading;
using Tetris;

namespace Tetris
{
    class Program
    {
        static private FileStoreOperator _fileStoreOperator;
        static private GameSettings _gameSettings = new GameSettings();
        static private ScreenFactory _screenFactory;
        static void Main(string[] args)
        {
            FileStore fileStore = new FileStore(@"./tetris_store.txt");
            _fileStoreOperator = new FileStoreOperator(fileStore);

            _screenFactory = new ScreenFactory(_gameSettings, _fileStoreOperator);
            MainScreen menuScreen = _screenFactory.CreateMainScreen();
            Engine engine = new Engine(60, menuScreen);
            engine.Start();
        }
    }
}
