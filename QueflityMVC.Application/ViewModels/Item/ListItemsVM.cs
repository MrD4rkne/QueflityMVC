using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class ListItemsVM
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int? ItemCategoryId { get; set; }

        public List<ItemForListVM> Items { get; set; }

        public string NameFilter { get; set; }
    }
}
