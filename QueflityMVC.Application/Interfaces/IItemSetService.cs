﻿using QueflityMVC.Application.ViewModels.ItemSet;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemSetService
    {
        Task<int> CreateItemSet(ItemSetDTO ItemSetDTO, string contentRootPath);

        ItemSetDetailsVM GetDetailsVM(int id);

        ListItemSetsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex);

        ItemSetDTO GetItemSetVMForAdding();
    }
}
