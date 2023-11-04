using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Application.Mapping;
using System.Security.Claims;

namespace QueflityMVC.Application.ViewModels.Role
{
    public record ClaimForSelectionVM
    {
        public string Id { get; set; }

        public bool IsSelected { get; set; }

        public ClaimForSelectionVM(string id) : this(id, false) { }

        public ClaimForSelectionVM(string id, bool isSelected)
        {
            Id = id;
            IsSelected = isSelected;
        }
    }
}
