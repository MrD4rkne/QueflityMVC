namespace QueflityMVC.Application.ViewModels.Other
{
    public record ClaimForSelectionVM
    {
        public string Id { get; set; }

        public bool IsSelected { get; set; }

        public ClaimForSelectionVM() { Id = string.Empty; }

        public ClaimForSelectionVM(string id) : this(id, false) { }

        public ClaimForSelectionVM(string id, bool isSelected)
        {
            Id = id;
            IsSelected = isSelected;
        }
    }
}