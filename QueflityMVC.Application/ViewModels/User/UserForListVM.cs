using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
