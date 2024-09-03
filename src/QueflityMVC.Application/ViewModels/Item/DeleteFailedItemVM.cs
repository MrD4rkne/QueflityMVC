namespace QueflityMVC.Application.ViewModels.Item;

public class DeleteFailedItemVm
{
    public int ItemId { get; set; }

    public int? CategoryId { get; set; }

    public string? Message { get; set; }
}