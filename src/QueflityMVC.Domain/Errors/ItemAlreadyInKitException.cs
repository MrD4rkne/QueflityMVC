using System.Runtime.Serialization;

namespace QueflityMVC.Domain.Errors;

public class ItemAlreadyInKitException : Exception
{
    private const string DEFAULT_ERROR_MESSAGE = "This item is already part of that set!";

    public ItemAlreadyInKitException() : this(DEFAULT_ERROR_MESSAGE)
    {
    }

    public ItemAlreadyInKitException(string? message) : base(message)
    {
    }

    public ItemAlreadyInKitException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ItemAlreadyInKitException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}