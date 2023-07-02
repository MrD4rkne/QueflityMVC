using QueflityMVC.Application.ViewModels.ItemCategory;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class CrEdItemVM
    {
        public ItemDTO ItemVM { get; set; }

        public List<ItemCategoryForSelectVM> ItemCategories { get; set; }
    }
}
