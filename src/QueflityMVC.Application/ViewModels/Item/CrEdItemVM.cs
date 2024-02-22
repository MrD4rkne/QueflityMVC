using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.ViewModels.Item;

public record CrEdItemVM
{
    public ItemVM ItemVM { get; set; }

    public List<CategoryForSelectVM>? Categories { get; set; }
}