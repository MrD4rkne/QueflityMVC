﻿using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Category;

public record CategoryVM : IMapFrom<Domain.Models.Category>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Category, CategoryVM>().ReverseMap();
    }
}