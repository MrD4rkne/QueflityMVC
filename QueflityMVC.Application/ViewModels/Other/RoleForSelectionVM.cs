using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Other
{
    public record RoleForSelectionVM : IMapFrom<IdentityRole>
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public bool IsSelected { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdentityRole, RoleForSelectionVM>()
                .ReverseMap();
        }
    }
}