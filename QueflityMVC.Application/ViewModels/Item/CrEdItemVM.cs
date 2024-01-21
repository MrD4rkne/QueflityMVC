using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.ViewModels.Item
{
    public record CrEdItemVM
    {
        public required ItemDTO ItemVM { get; set; }

        public required List<CategoryForSelectVM> Categories { get; set; }
    }
}