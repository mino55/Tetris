using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class InvalidDirectionException : System.Exception
    {
        public InvalidDirectionException() : base() { }
        public InvalidDirectionException(string message) : base(message) { }
        public InvalidDirectionException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidDirectionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}