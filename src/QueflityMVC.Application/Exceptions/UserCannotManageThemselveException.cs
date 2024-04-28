using System.Runtime.Serialization;

namespace QueflityMVC.Application.Exceptions;

public class UserCannotManageThemselveException : Exception
{
    private const string DEFAULT_MESSAGE = "User cannot manage their's identity configuration";

    public UserCannotManageThemselveException() : this(DEFAULT_MESSAGE)
    {
    }

    public UserCannotManageThemselveException(string? message) : base(message)
    {
    }

    public UserCannotManageThemselveException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }

    [Obsolete("Obsolete")]
    protected UserCannotManageThemselveException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}