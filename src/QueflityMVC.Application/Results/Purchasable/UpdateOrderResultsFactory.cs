namespace QueflityMVC.Application.Results.Purchasable;

public static class UpdateOrderResultsFactory
{
    public static UpdateOrderResult Success()
    {
        return new UpdateOrderResult { Status = UpdateOrderStatus.Success };
    }

    public static UpdateOrderResult NotValidOrder()
    {
        return new UpdateOrderResult { Status = UpdateOrderStatus.NotValidOrder };
    }

    public static UpdateOrderResult MissingPurchasable()
    {
        return new UpdateOrderResult { Status = UpdateOrderStatus.MissingPurchasable };
    }

    public static UpdateOrderResult Exception(Exception? ex)
    {
        return new UpdateOrderResult { Status = UpdateOrderStatus.Exception, Exception = ex };
    }
}