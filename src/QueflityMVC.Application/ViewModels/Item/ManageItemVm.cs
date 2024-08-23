using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.ViewModels.Item;

public record ManageItemVm
{
    public ItemVm? ItemVm { get; set; }

    public List<CategoryForSelectVm>? Categories { get; set; }
}