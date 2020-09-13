using System;



namespace Tetris
{
    public class KeyReceiver
    {
        public bool IsNewKey { get; private set; }
        private string _key;

        public KeyReceiver()
        {
            IsNewKey = false;
        }

        public string Key()
        {
            IsNewKey = false;
            return _key;
        }

        public void ReceiveKey(string key)
        {
            IsNewKey = true;
            _key = key;
        }
    }
}
