namespace Domein.Exceptions
{
    public class DagplanException : Exception
    {
        public DagplanException()
        {
        }

        public DagplanException(string? message) : base(message)
        {
        }
    }
}