namespace QueflityMVC.Application.Results.Purchasable;

public class UpdateOrderResult
{
    public Exception? Exception { get; init; }

    public UpdateOrderStatus Status { get; init; }
}

public static class UpdateOrderResultsFactory
{
    public static UpdateOrderResult Success() => new() { Status = UpdateOrderStatus.Success };

    public static UpdateOrderResult NotValidOrder() => new() { Status = UpdateOrderStatus.NotValidOrder };

    public static UpdateOrderResult MissingPurchasable() => new() { Status = UpdateOrderStatus.MissingPurchasable };

    public static UpdateOrderResult Exception(Exception? ex) => new() { Status = UpdateOrderStatus.Exception, Exception = ex };
}

public enum UpdateOrderStatus
{
    Success,
    NotValidOrder,
    MissingPurchasable,
    Exception
}