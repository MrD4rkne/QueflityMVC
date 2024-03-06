namespace QueflityMVC.Application.Exceptions.UseCases;

public class NoCategoriesException : Exception
{
    public NoCategoriesException()
    {
    }

    public NoCategoriesException(string? message) : base(message)
    {
    }

    public NoCategoriesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}