using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class MissingTetriminoException : System.Exception
    {
        public MissingTetriminoException() : base() { }
        public MissingTetriminoException(string message) : base(message) { }
        public MissingTetriminoException(string message, System.Exception inner) : base(message, inner) { }

        protected MissingTetriminoException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}