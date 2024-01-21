using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.User
{
    public record UserForListVM : IMapFrom<ApplicationUser>
    {
        public required string Id { get; set; }

        public required string? UserName { get; set; }

        public required string? Email { get; set; }

        public required bool IsEnabled { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserForListVM>()
                .ReverseMap();
        }
    }
}