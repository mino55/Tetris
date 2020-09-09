using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class MissingBlockException : Exception
    {
        public MissingBlockException() : base() { }
        public MissingBlockException(string message) : base(message) { }
        public MissingBlockException(string message, Exception inner) : base(message, inner) { }

        protected MissingBlockException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
