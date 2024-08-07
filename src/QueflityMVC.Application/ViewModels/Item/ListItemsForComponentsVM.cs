﻿using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Item;

public record ListItemsForComponentsVm
{
    public required PaginationVm<ItemForListVm> Pagination { get; set; }

    public int? CategoryId { get; set; }

    public string? NameFilter { get; set; }

    public List<int>? KitComponentsIds { get; set; }

    public required int KitId { get; set; }

    public KitDetailsVm? KitDetailsVm { get; set; }
}