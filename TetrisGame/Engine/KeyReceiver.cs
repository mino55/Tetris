using System;
using System.Threading;


namespace Tetris
{
    public class KeyReceiver
    {
        public bool isNewKey  { get; private set; }
        private ConsoleKeyInfo _key;
        private Thread _thread;
        public bool Listening { get; private set; }

        public KeyReceiver()
        {
            isNewKey = false;
        }

        public ConsoleKeyInfo Key()
        {
            isNewKey = false;
            return _key;
        }

        public void startListening()
        {
            _thread = new Thread(() => { ReceiveKey(); });
            _thread.IsBackground = true;
            _thread.Start();
            Listening = true;
        }

        public void stopListening()
        {
            try {
                _thread.Abort();
            } catch(Exception) {}
            Listening = false;
        }

        private void ReceiveKey()
        {
            while (true)
            {
                _key = System.Console.ReadKey(true);
                isNewKey = true;
            }
        }
    }
}