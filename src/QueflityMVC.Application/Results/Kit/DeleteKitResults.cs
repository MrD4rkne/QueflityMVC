using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Results.Kit;
public record class DeleteKitResult
{
    public Exception? Exception { get; init; }

    public required DeleteKitStatus Status { get; init; }
}

public static class DeleteKitResultsFactory
{
    public static DeleteKitResult Success() => new() { Status = DeleteKitStatus.Success };

    public static DeleteKitResult NotExist() => new() { Status = DeleteKitStatus.NotExist };

    public static DeleteKitResult Exception(Exception ex) => new() { Status = DeleteKitStatus.Exception, Exception = ex };
}

public enum DeleteKitStatus
{
    Success,
    NotExist,
    Exception
}
