using System;

namespace Tetris.Exceptions
{
    [Serializable()]
    public class MissingTetrinoException : System.Exception
    {
        public MissingTetrinoException() : base() { }
        public MissingTetrinoException(string message) : base(message) { }
        public MissingTetrinoException(string message, System.Exception inner) : base(message, inner) { }

        protected MissingTetrinoException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}