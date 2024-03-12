using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemComponentsSelectionVm
{
    public required ItemVm Item { get; set; }

    public required List<int> SelectedComponentsIds { get; set; }

    public required List<ComponentForSelection> AllComponents { get; set; }
}