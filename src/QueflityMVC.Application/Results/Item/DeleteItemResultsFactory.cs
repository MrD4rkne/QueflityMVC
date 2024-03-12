namespace QueflityMVC.Application.Results.Item;

public static class DeleteItemResultsFactory
{
    public static DeleteItemResult Success()
    {
        return new DeleteItemResult { Status = DeleteItemStatus.Success };
    }

    public static DeleteItemResult NotExist()
    {
        return new DeleteItemResult { Status = DeleteItemStatus.NotExist };
    }

    public static DeleteItemResult ItemIsPartOfKit()
    {
        return new DeleteItemResult { Status = DeleteItemStatus.ItemIsPartOfKit };
    }

    public static DeleteItemResult Exception(Exception ex)
    {
        return new DeleteItemResult { Status = DeleteItemStatus.Exception, Exception = ex };
    }
}