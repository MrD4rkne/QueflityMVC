namespace QueflityMVC.Persistence.Setup;

public class DbSetupException : Exception
{
    public DbSetupException()
    {
    }

    public DbSetupException(string? message) : base(message)
    {
    }

    public DbSetupException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}