using QueflityMVC.Application.ViewModels.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class CrEdItemVM
    {
        public ItemDTO ItemVM { get; set; }

        public List<ItemCategoryForSelectVM> ItemCategories { get; set; }
    }
}
