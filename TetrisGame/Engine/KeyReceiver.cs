using System;



namespace Tetris
{
    public class KeyReceiver
    {
        public bool isNewKey  { get; private set; }
        private string _key;

        public KeyReceiver()
        {
            isNewKey = false;
        }

        public string Key()
        {
            isNewKey = false;
            return _key;
        }

        public void ReceiveKey(String key)
        {
            isNewKey = true;
            _key = key;
        }
    }
}