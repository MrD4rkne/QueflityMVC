namespace QueflityMVC.Application.ViewModels.Other;

public record ClaimForSelectionVm
{
    public string Id { get; set; }

    public bool IsSelected { get; set; }

    public ClaimForSelectionVm() { Id = string.Empty; }

    public ClaimForSelectionVm(string id) : this(id, false) { }

    public ClaimForSelectionVm(string id, bool isSelected)
    {
        Id = id;
        IsSelected = isSelected;
    }
}