namespace QueflityMVC.Application.Results.Purchasable;

public record UpdateOrderResult
{
    public Exception? Exception { get; set; }

    public UpdateOrderStatus Status { get; set; }
}