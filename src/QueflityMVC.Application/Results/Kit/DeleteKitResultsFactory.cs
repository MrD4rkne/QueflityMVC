namespace QueflityMVC.Application.Results.Kit;

public static class DeleteKitResultsFactory
{
    public static DeleteKitResult Success()
    {
        return new DeleteKitResult { Status = DeleteKitStatus.Success };
    }

    public static DeleteKitResult NotExist()
    {
        return new DeleteKitResult { Status = DeleteKitStatus.NotExist };
    }

    public static DeleteKitResult Exception(Exception ex)
    {
        return new DeleteKitResult { Status = DeleteKitStatus.Exception, Exception = ex };
    }
}