using System;
using System.Threading;

namespace Tetris
{
    public class Engine
    {
        private int _FPS;

        private bool _started = false;

        private string _lastRenderedPrint = "";

        private KeyReceiver _keyReceiver = new KeyReceiver();

        public IScreen _currentScreen;

        public Engine(int fps, IScreen startScreen)
        {
            _FPS = fps;
            _currentScreen = startScreen;
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
            _currentScreen = screen;
        }

        private void Loop()
        {
            int dTime = (1000 / _FPS);

            string input = GetKeyInput();

            string print = _currentScreen.Render(dTime, input, this);
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
            }
        }

        private string GetKeyInput()
        {
            string key = null;
            if (_keyReceiver.isNewKey)
            {
                ConsoleKeyInfo keyInput = _keyReceiver.Key();
                key = keyInput.Key.ToString();
            }
            return key;
        }
    }
}