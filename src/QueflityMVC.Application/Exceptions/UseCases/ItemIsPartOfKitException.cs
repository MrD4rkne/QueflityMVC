namespace QueflityMVC.Application.Exceptions.UseCases;

public class ItemIsPartOfKitException : Exception
{
    public ItemIsPartOfKitException()
    {
    }

    public ItemIsPartOfKitException(string? message) : base(message)
    {
    }

    public ItemIsPartOfKitException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}