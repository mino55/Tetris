using System;
using System.Threading;

namespace Tetris
{
    public class Engine
    {
        public int FPS { get; set; }

        private bool _started = false;

        private string _lastRenderedPrint = "";

        private KeyReceiver _keyReceiver = new KeyReceiver();

        public IScreen _currentScreen;

        public Engine(int defaultFPS, IScreen startScreen)
        {
            FPS = defaultFPS;
            MountScreen(startScreen);
        }


        public void Start()
        {
            _keyReceiver.startListening();
            _started = true;
            while(_started)
            {
                Loop();
            }
        }

        public void Stop()
        {
            _keyReceiver.stopListening();
            _started = false;
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

        private void Loop()
        {
            int dTime = (1000 / FPS);

            string input = GetKeyInput();
            _currentScreen.Input(input, dTime);

            string print = _currentScreen.Render();
            Render(print);

            Thread.Sleep(dTime);
        }

        private void Render(string print)
        {
            if (_lastRenderedPrint != print)
            {
                _lastRenderedPrint = print;
                Console.Clear();
                Console.WriteLine(print);
                Console.WriteLine(FPS);  // TODO: Purely for debugging -- remove
            }
        }

        private string GetKeyInput()
        {
            string key = null;
            if (_keyReceiver.isNewKey)
            {
                ConsoleKeyInfo keyInput = _keyReceiver.Key();
                key = keyInput.Key.ToString();
                Console.WriteLine($"Input: {key}"); // TODO: Purely for debugging -- remove
            }
            return key;
        }
    }
}