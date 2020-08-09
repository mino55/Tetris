using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class BlockOutsideBoardException : System.Exception
    {
        public BlockOutsideBoardException() : base() { }
        public BlockOutsideBoardException(string message) : base(message) { }
        public BlockOutsideBoardException(string message, System.Exception inner) : base(message, inner) { }

        protected BlockOutsideBoardException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}