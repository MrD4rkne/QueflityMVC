using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemCategory;

namespace QueflityMVC.Application.ViewModels.ItemSet
{
    public class CrEdItemSetVM
    {
        public ItemSetDTO ItemSetVM { get; set; }

        public List<ItemForSelection> Items { get; set; }
    }
}
