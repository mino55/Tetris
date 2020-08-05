using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class BlockNotPlacedException : System.Exception
    {
        public BlockNotPlacedException() : base() { }
        public BlockNotPlacedException(string message) : base(message) { }
        public BlockNotPlacedException(string message, System.Exception inner) : base(message, inner) { }

        protected BlockNotPlacedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
