using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class NoOverwriteTetriminoException : Exception
    {
        public NoOverwriteTetriminoException() : base() { }
        public NoOverwriteTetriminoException(string message) : base(message) { }
        public NoOverwriteTetriminoException(string message, Exception inner) : base(message, inner) { }

        protected NoOverwriteTetriminoException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
