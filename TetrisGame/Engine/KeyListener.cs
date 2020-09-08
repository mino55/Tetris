using System;
using System.Threading;

namespace Tetris
{
    class KeyListener
    {
        private Thread _thread;
        private KeyReceiver _keyReceiver;

        public KeyListener(KeyReceiver keyReceiver)
        {
            _keyReceiver = keyReceiver;
        }

        public void Start()
        {
            _thread = new Thread(() => {
                while (true)
                {
                    ConsoleKeyInfo keyInput = System.Console.ReadKey(true);
                    _keyReceiver.ReceiveKey(keyInput.Key.ToString());
                }
            });
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Stop()
        {
            try {
                _thread.Abort();
            } catch(Exception) {}
        }
    }
}