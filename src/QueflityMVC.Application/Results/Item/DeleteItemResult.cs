namespace QueflityMVC.Application.Results.Item;

public record DeleteItemResult
{
    public Exception? Exception { get; set; }

    public required DeleteItemStatus Status { get; set; }
}