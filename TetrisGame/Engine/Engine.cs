using System;
using System.Threading;

namespace Tetris
{
    public class Engine
    {
        public int FPS { get; set; }

        public bool Started { get; private set; }

        private string _lastRenderedPrint = "";

        private KeyReceiver _keyReceiver;

        public IScreen _currentScreen;

        public Engine(int defaultFPS, IScreen startScreen, KeyReceiver keyReceiver)
        {
            FPS = defaultFPS;
            MountScreen(startScreen);
            Started = false;
            _keyReceiver = keyReceiver;
        }

        public void Start()
        {
            Started = true;
        }

        public void Stop()
        {
            Started = false;
        }

        public void Loop()
        {
            int dTime = (1000 / FPS);

            string input = GetKeyInput();
            _currentScreen.Input(input, dTime);

            string[] print = _currentScreen.Render();
            Render(print);

            Thread.Sleep(dTime);
        }

        public void SwitchScreen(IScreen screen)
        {
            MountScreen(screen);
        }

        private void MountScreen(IScreen screen)
        {
            if (_currentScreen != null) _currentScreen.Unmount(this);
            _currentScreen = screen;
            _currentScreen.Mount(this);
        }

        private void Render(string[] print)
        {
            string printString = printToPrintString(print);
            if (_lastRenderedPrint != printString)
            {
                _lastRenderedPrint = printString;
                Console.Clear();
                Console.WriteLine(printString);
                Console.WriteLine(FPS);  // TODO: Purely for debugging -- remove
            }
        }

        private string printToPrintString(string[] print)
        {
            string printString = "";
            for (int i = 0; i < print.Length; i++)
            {
                printString += print[i];
                bool lastIndex = (i == (print.Length - 1));
                if (!lastIndex) printString += "\n";
            }
            return printString;
        }

        private string GetKeyInput()
        {
            string key = null;
            if (_keyReceiver.isNewKey)
            {
                key = _keyReceiver.Key();
                Console.WriteLine($"Input: {key}"); // TODO: Purely for debugging -- remove
            }
            return key;
        }
    }
}