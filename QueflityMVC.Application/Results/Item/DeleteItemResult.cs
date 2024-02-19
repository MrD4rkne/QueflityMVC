using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Results.Item;

public record class DeleteItemResult
{
    public Exception? Exception { get; init; }

    public required DeleteItemStatus Status { get; init; }
}

public static class DeleteItemResultsFactory
{
    public static DeleteItemResult Success() => new() { Status = DeleteItemStatus.Success };

    public static DeleteItemResult NotExist() => new() { Status = DeleteItemStatus.NotExist };

    public static DeleteItemResult ItemIsPartOfKit() => new() { Status = DeleteItemStatus.ItemIsPartOfKit };

    public static DeleteItemResult Exception(Exception ex) => new() { Status = DeleteItemStatus.Exception, Exception = ex };
}

public enum DeleteItemStatus
{
    Success,
    NotExist,
    ItemIsPartOfKit,
    Exception
}
