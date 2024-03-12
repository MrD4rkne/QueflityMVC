namespace QueflityMVC.Application.Results.Kit;

public record class DeleteKitResult
{
    public Exception? Exception { get; init; }

    public required DeleteKitStatus Status { get; init; }
}