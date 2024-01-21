using System.Runtime.Serialization;

namespace QueflityMVC.Domain.Errors
{
    public class ItemAlreadyInSetException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "This item is already part of that set!";

        public ItemAlreadyInSetException() : this(DEFAULT_ERROR_MESSAGE)
        {
        }

        public ItemAlreadyInSetException(string? message) : base(message)
        {
        }

        public ItemAlreadyInSetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ItemAlreadyInSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}