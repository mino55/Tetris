using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class TileNotPlacedException : System.Exception
    {
        public TileNotPlacedException() : base() { }
        public TileNotPlacedException(string message) : base(message) { }
        public TileNotPlacedException(string message, System.Exception inner) : base(message, inner) { }

        protected TileNotPlacedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
