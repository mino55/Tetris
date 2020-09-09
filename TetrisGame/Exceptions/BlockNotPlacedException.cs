using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class BlockNotPlacedException : Exception
    {
        public BlockNotPlacedException() : base() { }
        public BlockNotPlacedException(string message) : base(message) { }
        public BlockNotPlacedException(string message, Exception inner) : base(message, inner) { }

        protected BlockNotPlacedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
