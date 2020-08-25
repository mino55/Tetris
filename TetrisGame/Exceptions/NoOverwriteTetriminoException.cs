using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class NoOverwriteTetriminoException : System.Exception
    {
        public NoOverwriteTetriminoException() : base() { }
        public NoOverwriteTetriminoException(string message) : base(message) { }
        public NoOverwriteTetriminoException(string message, System.Exception inner) : base(message, inner) { }

        protected NoOverwriteTetriminoException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}