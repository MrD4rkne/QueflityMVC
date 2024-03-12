namespace QueflityMVC.Application.Results.Purchasable;

public class UpdateOrderResult
{
    public Exception? Exception { get; init; }

    public UpdateOrderStatus Status { get; init; }
}