namespace QueflityMVC.Application.Results.Item;

public record class DeleteItemResult
{
    public Exception? Exception { get; init; }

    public required DeleteItemStatus Status { get; init; }
}