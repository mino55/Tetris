using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class NoOverwriteBlockException : Exception
    {
        public NoOverwriteBlockException() : base() { }
        public NoOverwriteBlockException(string message) : base(message) { }
        public NoOverwriteBlockException(string message, Exception inner) : base(message, inner) { }

        protected NoOverwriteBlockException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
