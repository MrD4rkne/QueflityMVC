using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemComponentsSelectionVM
{
    public required ItemVM Item { get; set; }

    public required List<int> SelectedComponentsIds { get; set; }

    public required List<ComponentForSelection> AllComponents { get; set; }
}