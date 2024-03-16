namespace QueflityMVC.Application.Results.Kit;

public record DeleteKitResult
{
    public Exception? Exception { get; set; }

    public required DeleteKitStatus Status { get; set; }
}