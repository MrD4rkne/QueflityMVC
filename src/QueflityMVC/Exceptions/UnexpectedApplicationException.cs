namespace QueflityMVC.Web.Exceptions;

public class UnexpectedApplicationException : Exception
{
    public UnexpectedApplicationException()
    {
    }

    public UnexpectedApplicationException(string? message) : base(message)
    {
    }

    public UnexpectedApplicationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}