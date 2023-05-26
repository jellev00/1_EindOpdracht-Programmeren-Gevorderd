using System.Runtime.Serialization;

namespace Domein.Exceptions
{
    [Serializable]
    internal class DagplanException : Exception
    {
        public DagplanException()
        {
        }

        public DagplanException(string? message) : base(message)
        {
        }

        public DagplanException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DagplanException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}